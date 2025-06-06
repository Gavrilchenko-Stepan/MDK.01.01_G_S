﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Test.TheQuestionsLoader;

namespace Test
{
    public class QuestionManager
    {
        public List<Question> Questions { get; set; } = new List<Question>();
        public string _filename;

        public QuestionManager(string filename)
        {
            _filename = filename;
        }


        public void AddQuestion(string text, string section)
        {
            Questions.Add(new Question(text, section));
            File.AppendAllLines(_filename, new List<string> { $"{section} | {text}" });
        }


        public Question GetRandomQuestion(string section, HashSet<Question> usedQuestions)
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
            var sections = new List<string>(Question.ALL_SECTIONS);
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
