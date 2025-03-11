using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyLibrary.Models
{
   public class College
    {
        public string Name { get; set; } //имя узла

        public List<College> Children { get; } // список дочерних узлов

        public College(string name)
        {
            Name = name;
            Children = new List<College>();
        }

        public College AddChildNode(string text)
        {
            College node = new College(text);
            Children.Add(node);

            return node;
        }
    }
}
