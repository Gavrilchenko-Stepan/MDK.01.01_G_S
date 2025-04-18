﻿using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Test
{
    internal class Program
    {
        private static QuestionManager questionManager;
        private static string currentTemplatePath;
        static void Main(string[] args)
        {
            Console.WriteLine("=== Генератор экзаменационных билетов ===");

            InitializeQuestionManager();
            LoadTemplate();

            while (true)
            {
                Console.WriteLine("\nГлавное меню:");
                Console.WriteLine("1. Сгенерировать билеты");
                Console.WriteLine("2. Добавить вопросы");
                Console.WriteLine("3. Изменить шаблон");
                Console.WriteLine("4. Выход");
                Console.Write("Выберите действие: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        GenerateTickets();
                        break;
                    case "2":
                        AddQuestionsMenu();
                        break;
                    case "3":
                        LoadTemplate();
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Неверный ввод!");
                        break;
                }
            }
        }

        private static void InitializeQuestionManager()
        {
            while (true)
            {
                Console.Write("\nВведите путь к файлу с вопросами: ");
                string fileName = Console.ReadLine();

                if (!File.Exists(fileName))
                {
                    Console.WriteLine("Файл не найден! Проверьте путь и попробуйте снова.");
                    continue;
                }

                questionManager = new QuestionManager(fileName);
                var loadResult = TheQuestionsLoader.LoadQuestions(questionManager);

                Console.WriteLine($"\nУспешно загружено {loadResult.LoadedCount} вопросов из {loadResult.TotalLines} строк.");

                if (loadResult.HasErrors)
                {
                    Console.WriteLine($"\nОбнаружены ошибки ({loadResult.Errors.Count}):");
                    foreach (var error in loadResult.Errors)
                    {
                        Console.WriteLine($"- {error}");
                    }
                }

                Console.WriteLine("\nПроверьте загруженные вопросы:");
                bool anyQuestionsLoaded = false;
                foreach (var section in Question.ALL_SECTIONS)
                {
                    int count = questionManager.Questions.Count(q => q.Section == section);
                    Console.WriteLine($"- {section}: {count} вопросов");
                    if (count > 0) anyQuestionsLoaded = true;
                }

                if (!anyQuestionsLoaded)
                {
                    Console.WriteLine("\nВнимание: не загружено ни одного вопроса!");
                    Console.WriteLine("Возможные причины:");
                    Console.WriteLine("1. Файл пустой");
                    Console.WriteLine("2. Все строки содержат ошибки");
                    Console.WriteLine("3. Неверный формат файла (должен быть: 'раздел | текст вопроса')");
                    Console.Write("Попробовать другой файл? (y/n): ");
                    if (Console.ReadLine().ToLower() == "y") continue;
                    else Environment.Exit(0);
                }

                Console.Write("\nВсе верно? (y/n): ");
                if (Console.ReadLine().ToLower() == "y")
                    break;
            }
        }

        private static void LoadTemplate()
        {
            Console.Write("\nВведите путь к шаблону Word: ");
            currentTemplatePath = Console.ReadLine();

            if (!File.Exists(currentTemplatePath))
            {
                Console.WriteLine("Файл шаблона не найден!");
                return;
            }

            Console.WriteLine("Шаблон успешно загружен.");
        }

        private static void GenerateTickets()
        {
            if (string.IsNullOrEmpty(currentTemplatePath))
            {
                Console.WriteLine("Сначала загрузите шаблон!");
                return;
            }

            Console.Write("\nВведите количество билетов: ");
            if (!int.TryParse(Console.ReadLine(), out int numTickets) || numTickets <= 0)
            {
                Console.WriteLine("Некорректное количество!");
                return;
            }

            var tickets = new TicketGenerator().GenerateTickets(questionManager, numTickets);
            if (tickets == null)
            {
                Console.WriteLine("Недостаточно вопросов!");
                return;
            }

            PreviewTickets(tickets);

            Console.Write("Сохранить билеты? (y/n): ");
            if (Console.ReadLine().ToLower() == "y")
            {
                SaveTicketsToDocx(tickets);
            }
        }

        private static void PreviewTickets(List<Ticket> tickets)
        {
            Console.WriteLine("\nПредпросмотр билетов:");
            foreach (var ticket in tickets)
            {
                Console.WriteLine($"Билет №{tickets.IndexOf(ticket) + 1}");
                Console.WriteLine(ticket.ToString());
            }
        }

        private static void SaveTicketsToDocx(List<Ticket> tickets)
        {
            Console.Write("\nВведите путь для сохранения (.docx): ");
            string outputPath = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(outputPath))
            {
                Console.WriteLine("Путь не может быть пустым!");
                return;
            }

            // Добавляем расширение, если его нет
            if (!outputPath.EndsWith(".docx", StringComparison.OrdinalIgnoreCase))
            {
                outputPath += ".docx";
            }

            try
            {
                WordTemplateFiller.GenerateFromTemplate(currentTemplatePath, outputPath, tickets);
                Console.WriteLine($"\nФайл успешно сохранен: {Path.GetFullPath(outputPath)}");
                Console.WriteLine("Откройте файл и заполните:");
                Console.WriteLine("- Название организации");
                Console.WriteLine("- Название дисциплины");
                Console.WriteLine("- Название специальности");

                try
                {
                    var processStartInfo = new System.Diagnostics.ProcessStartInfo(outputPath)
                    {
                        UseShellExecute = true
                    };
                    System.Diagnostics.Process.Start(processStartInfo);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Документ сохранён, но не удалось открыть его: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nОшибка при создании документа: {ex.Message}");

                // Попытка удалить повреждённый файл
                try
                {
                    if (File.Exists(outputPath))
                        File.Delete(outputPath);
                }
                catch { }
            }
        }

        private static void AddQuestionsMenu()
        {
            Console.WriteLine("\nДобавление вопросов:");
            Console.WriteLine("1. Вручную");
            Console.WriteLine("2. Из файла");
            Console.Write("Выберите: ");

            switch (Console.ReadLine())
            {
                case "1":
                    AddQuestionManually();
                    break;
                case "2":
                    AddQuestionsFromFile();
                    break;
                default:
                    Console.WriteLine("Неверный выбор!");
                    break;
            }
        }

        private static void AddQuestionManually()
        {
            Console.Write("\nВведите раздел (знать/уметь/владеть): ");
            string section = Console.ReadLine().ToLower();

            if (!new[] { "знать", "уметь", "владеть" }.Contains(section))
            {
                Console.WriteLine("Неверный раздел!");
                return;
            }

            Console.Write("Введите текст вопроса: ");
            string text = Console.ReadLine();

            questionManager.AddQuestion(text, section);
            Console.WriteLine("Вопрос добавлен!");
        }

        private static void AddQuestionsFromFile()
        {
            Console.Write("\nВведите путь к файлу: ");
            string path = Console.ReadLine();

            if (!File.Exists(path))
            {
                Console.WriteLine("Файл не найден!");
                return;
            }

            var tempManager = new QuestionManager(path);
            var loadResult = TheQuestionsLoader.LoadQuestions(tempManager);

            if (loadResult.HasErrors)
            {
                Console.WriteLine("Ошибки в файле:");
                foreach (var error in loadResult.Errors)
                {
                    Console.WriteLine($"- {error}");
                }
                return;
            }
        }
    }
}
