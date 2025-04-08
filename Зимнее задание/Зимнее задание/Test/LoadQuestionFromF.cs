using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using static Test.QuestionManager;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Net.NetworkInformation;

namespace Test
{
    public class LoadQuestionFromF
    {
        static public void TheQuestionLoader(QuestionManager questionmanager, out int loadedCount, 
            out int totalLines, out List<string> errors)
        {
            questionmanager.Questions.Clear(); // Очистка списка перед загрузкой
            loadedCount = 0;
            totalLines = 0;
            errors = new List<string>();

            if (!File.Exists(questionmanager._filename))
            {
                errors.Add($"Ошибка: файл {questionmanager._filename} не найден.");
                return;
            }

            string[] lines;
            try
            {
                lines = File.ReadAllLines(questionmanager._filename);
                totalLines = lines.Length;
            }
            catch (Exception ex)
            {
                errors.Add($"Ошибка при чтении файла: {ex.Message}");
                return;
            }

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    errors.Add($"Пустая строка");
                    continue;
                }

                var parts = line.Split('|');
                if (parts.Length == 2)
                {
                    var section = parts[0].Trim();
                    var text = parts[1].Trim();

                    if (string.IsNullOrEmpty(section)
                        || string.IsNullOrEmpty(text))
                    {
                        errors.Add($"Пустое значение в строке: {line}");
                        continue;
                    }

                    questionmanager.Questions.Add(new Question(text, section));
                    loadedCount++;
                }
                else
                {
                    errors.Add($"Ошибка формата (должно быть 2 части разделенные '|'): {line}");
                }
            }
        }

        static public bool CheckFileForErrors(string filePath, out List<string> errors)
        {
            errors = new List<string>();


            if (!File.Exists(filePath))
            {
                errors.Add($"Файл {filePath} не найден.");
                return false;
            }

            string[] lines;
            try
            {
                lines = File.ReadAllLines(filePath);
            }
            catch (Exception ex)
            {
                errors.Add($"Ошибка при чтении файла: {ex.Message}");
                return false;
            }
            
            bool hasErrors = false;
        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];
            if (string.IsNullOrWhiteSpace(line))
            {
                errors.Add($"Строка {i + 1}: Пустая строка");
                hasErrors = true;
                continue;
            }

            var parts = line.Split('|');
            if (parts.Length != 2)
            {
                errors.Add($"Строка {i + 1}: Ошибка формата (должно быть 2 части разделенные '|')");
                hasErrors = true;
            }
            else if (string.IsNullOrEmpty(parts[0].Trim()) 
                  || string.IsNullOrEmpty(parts[1].Trim()))
            {
                errors.Add($"Строка {i + 1}: Пустое значение секции или текста вопроса");
                hasErrors = true;
            }
        }

        return !hasErrors;
        }
    }
}
