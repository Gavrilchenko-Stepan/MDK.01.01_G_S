using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using static Test.QuestionManager;
using System.Runtime.CompilerServices;

namespace Test
{
    public class LoadQuestionFromF
    {
        static public void LoadQuestionsFromFile(QuestionManager questionmanager)
        {
            questionmanager.Questions.Clear(); // Очистка списока перед загрузкой


            string[] lines = File.ReadAllLines(questionmanager._filename);

            foreach (var line in lines)
            {
                var parts = line.Split('|');
                if (parts.Length == 2)
                {
                    var section = parts[0].Trim();
                    var text = parts[1].Trim();
                    questionmanager.Questions.Add(new Question(text, section));

                }
                else
                {
                    Console.WriteLine($"Ошибка формата строки в файле: {line}");
                }
            }

            Console.WriteLine($"Загружено {questionmanager.Questions.Count} из {lines.Length} вопросa(-ов).");
        }
    }
}
