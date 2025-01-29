using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    internal class QuestionManager
    {
        public List<Question> Questions { get; set; } = new List<Question>();
        private string _filename;

        public QuestionManager(string filename)
        {
            _filename = filename;
            LoadQuestionsFromFile();
        }

        public void LoadQuestionsFromFile()
        {
            Questions.Clear(); // Очистка списока перед загрузкой

            if (!File.Exists(_filename))
            {
                File.Create(_filename).Close();
                Console.WriteLine($"Файл '{_filename}' создан.");
                return;
            }
            var lines = File.ReadAllLines(_filename);

            foreach (var line in lines)
            {
                var parts = line.Split('|');
                if (parts.Length == 2)
                {
                    var section = parts[0].Trim();
                    var text = parts[1].Trim();
                    Questions.Add(new Question(text, section));

                }
                else
                {
                    Console.WriteLine($"Ошибка формата строки в файле: {line}");
                }
            }

            Console.WriteLine($"Загружено {Questions.Count} вопросов.");
        }


        public void AddQuestion(string text, string section)
        {
            Questions.Add(new Question(text, section));
            File.AppendAllLines(_filename, new List<string> { $"{section} | {text}" });
            Console.WriteLine($"Вопрос '{text}' добавлен в раздел '{section}'.");
        }


        public Question GetRandomQuestion(string section, List<Question> usedQuestions)
        {
            var AvailableQuestions = new List<Question>();
            foreach (var question in Questions)
            {
                if (question.Section == section && !usedQuestions.Contains(question))
                {
                    AvailableQuestions.Add(question);
                }
            }

            if (AvailableQuestions.Count == 0)
            {
                return null;
            }
            Random rnd = new Random();
            int index = rnd.Next(AvailableQuestions.Count);

            return AvailableQuestions[index];
        }


        public bool HasEnoughQuestions(int numTickets)
        {
            var sections = new List<string> { "знать", "уметь", "владеть" };
            foreach (var section in sections)
            {
                int count = 0;
                foreach (var question in Questions)
                {
                    if (question.Section == section)
                    {
                        count++;
                    }
                }
                if (count < numTickets)
                {
                    return false;
                }

            }
            return true;
        }
    }
}
