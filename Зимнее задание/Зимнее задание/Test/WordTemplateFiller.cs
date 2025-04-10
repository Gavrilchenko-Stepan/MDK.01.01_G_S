﻿using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Test
{
    public static class WordTemplateFiller
    {
        public static void GenerateFromTemplate(string templatePath, string outputPath, List<Ticket> tickets)
        {
            // 1. Читаем весь шаблон в память (без блокировки)
            byte[] templateData;
            using (var fileStream = new FileStream(templatePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                templateData = new byte[fileStream.Length];
                fileStream.Read(templateData, 0, (int)fileStream.Length);
            }

            // 2. Работаем полностью в памяти
            using (var memoryStream = new MemoryStream())
            {
                // Копируем данные шаблона в MemoryStream
                memoryStream.Write(templateData, 0, templateData.Length);
                memoryStream.Position = 0;

                using (var doc = WordprocessingDocument.Open(memoryStream, true))
                {
                    ProcessDocument(doc, tickets);
                    doc.Save();
                }

                // 3. Сохраняем результат
                File.WriteAllBytes(outputPath, memoryStream.ToArray());
            }
        }

        private static void ProcessDocument(WordprocessingDocument doc, List<Ticket> tickets)
        {
            var body = doc.MainDocumentPart.Document.Body;
            var templateElements = body.Elements().ToList();

            // Определяем разделитель (последний элемент)
            var ticketTemplate = templateElements.Take(templateElements.Count - 1).ToList();
            body.RemoveAllChildren();

            for (int i = 0; i < tickets.Count; i++)
            {
                // Копируем шаблон для каждого билета
                foreach (var element in ticketTemplate)
                {
                    body.Append((OpenXmlElement)element.CloneNode(true));
                }

                // Заменяем плейсхолдеры
                ReplacePlaceholders(body, new Dictionary<string, string>
                {
                    ["[НОМЕР БИЛЕТА]"] = (i + 1).ToString(),
                    ["[ВОПРОС_ЗНАТЬ]"] = tickets[i].Questions[0].Text,
                    ["[ВОПРОС_УМЕТЬ]"] = tickets[i].Questions[1].Text,
                    ["[ВОПРОС_ВЛАДЕТЬ]"] = tickets[i].Questions[2].Text,
                    ["[ГОД]"] = DateTime.Now.Year.ToString()
                });

                // Добавляем разрыв страницы (кроме последнего билета)
                if (i < tickets.Count - 1)
                {
                    body.Append(new Paragraph(new Run(new Break() { Type = BreakValues.Page })));
                }
            }
        }

        private static void ReplacePlaceholders(Body body, Dictionary<string, string> replacements)
        {
            foreach (var text in body.Descendants<Text>())
            {
                foreach (var kvp in replacements)
                {
                    if (text.Text.Contains(kvp.Key))
                    {
                        text.Text = text.Text.Replace(kvp.Key, kvp.Value);
                    }
                }
            }
        }
    }
}
