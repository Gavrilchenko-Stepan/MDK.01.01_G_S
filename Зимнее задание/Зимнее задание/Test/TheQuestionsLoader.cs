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
    public class TheQuestionsLoader
    {
        public struct LoadResult
        {
            public int LoadedCount { get; set; }
            public int TotalLines { get; set; }
            public List<string> Errors { get; set; }
            public bool HasErrors => Errors.Count > 0;
        }
        static public LoadResult LoadQuestions(QuestionManager questionmanager)
        {
            var result = new LoadResult
            {
                LoadedCount = 0,
                TotalLines = 0,
                Errors = new List<string>()
            };
            questionmanager.Questions.Clear(); // Очистка списка перед загрузкой
            

            if (!File.Exists(questionmanager._filename))
            {
                result.Errors.Add($"Ошибка: файл {questionmanager._filename} не найден.");
                return result;
            }
            string[] lines;
            try
            {
                lines = File.ReadAllLines(questionmanager._filename);
                result.TotalLines = lines.Length;
            }
            catch (Exception ex)
            {
                result.Errors.Add($"Ошибка при чтении файла: {ex.Message}");
                return result;
            }

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                if (string.IsNullOrWhiteSpace(line))
                {
                    result.Errors.Add($"Строка {i + 1}: Пустая строка");
                    continue;
                }

                var parts = line.Split('|');
                if (parts.Length != 2)
                {
                    result.Errors.Add($"Строка {i + 1}: Ошибка формата (должно быть 2 части разделенные '|')");
                    continue;
                }

                var section = parts[0].Trim();
                var text = parts[1].Trim();

                if (string.IsNullOrEmpty(section))
                {
                    result.Errors.Add($"Строка {i + 1}: Пустое значение секции");
                    continue;
                }

                if (string.IsNullOrEmpty(text))
                {
                    result.Errors.Add($"Строка {i + 1}: Пустое значение текста вопроса");
                    continue;
                }

                if (!Question.ALL_SECTIONS.Contains(section))
                {
                    result.Errors.Add($"Строка {i + 1}: Неизвестная секция '{section}'. Допустимые значения: {string.Join(", ", Question.ALL_SECTIONS)}");
                    continue;
                }

                questionmanager.Questions.Add(new Question(text, section));
                result.LoadedCount++;
            }
            return result;
        }
    }
}
