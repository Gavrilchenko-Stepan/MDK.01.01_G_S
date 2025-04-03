using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    internal class Program
    {
        LoadQuestionFromF loadquestinofromfile = new LoadQuestionFromF();
        static void Main(string[] args)
        {
            Console.WriteLine("Введите путь к файлу с вопросами (например, questions.txt):");
            string fileName = Console.ReadLine();

            var questionManager = new QuestionManager(fileName);
            LoadQuestionFromF.TheQuestionLoader(questionManager);

            while (true)
            {
                Console.WriteLine("Введите количество билетов, которое нужно сгенерировать:");
                if (!int.TryParse(Console.ReadLine(), out int numTickets) || numTickets <= 0)
                {
                    Console.WriteLine("Некорректный ввод. Введите целое число больше 0.");
                    continue;
                }
                var ticketGenerator = new TicketGenerator();
                var tickets = ticketGenerator.GenerateTickets(questionManager, numTickets);

                if (tickets == null)
                {
                    Console.WriteLine("Недостаточно вопросов для создания такого числа билетов.");
                    AddMoreQuestions(questionManager, numTickets);
                    continue;
                }

                Console.WriteLine("Сгенерированные билеты:");
                foreach (var ticket in tickets)
                {
                    Console.Write(ticket.ToString());
                }

                Console.WriteLine("Хотите сохранить билеты в файл? (y/n)");
                var saveAnswer = Console.ReadLine();
                if (saveAnswer.ToLower() == "y")
                {
                    SaveTicketsToFile(tickets);
                }
                Console.WriteLine("Сгенерировать еще билеты? (y/n)");
                var continueAnswer = Console.ReadLine();
                if (continueAnswer.ToLower() != "y")
                {
                    break;
                }
            }

            Console.WriteLine("Программа завершена.");
        }
        private static void AddMoreQuestions(QuestionManager questionManager, int numTickets)
        {
            var sections = new List<string> { "знать", "уметь", "владеть" };
            foreach (var section in sections)
            {
                while (true)
                {
                    int count = 0;
                    foreach (var question in questionManager.Questions)
                    {
                        if (question.Section == section)
                        {
                            count++;
                        }
                    }
                    if (count >= numTickets)
                        break;
                    Console.WriteLine($"Недостаточно вопросов в разделе '{section}'. Введите новый вопрос:");
                    var questionText = Console.ReadLine();
                    questionManager.AddQuestion(questionText, section);
                }
            } //проверить успешность выполнения AddQuestions
        }

        private static void SaveTicketsToFile(List<Ticket> tickets)
        {
            Console.WriteLine("Введите путь к файлу для сохранения билетов (например, tickets.txt):");
            string saveFileName = Console.ReadLine();

            using (StreamWriter writer = new StreamWriter(saveFileName))
            {
                foreach (var ticket in tickets)
                {
                    writer.Write(ticket.ToString());
                }
            }
            Console.WriteLine($"Билеты сохранены в файл '{saveFileName}'.");
        }
    }
}
