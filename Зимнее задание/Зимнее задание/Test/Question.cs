using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class Question
    {
        public const string KNOW_SECTION = "знать";
        public const string ABILITY_SECTION = "уметь";
        public const string MASTERY_SECTION = "владеть";

        public static readonly string[] ALL_SECTIONS = { KNOW_SECTION, ABILITY_SECTION, MASTERY_SECTION };

        public string Text { get; set; }
        public string Section { get; set; } // "знать", "уметь", "владеть"

        public Question(string text, string section)
        {
            Text = text;
            Section = section;
        }
    }
}
