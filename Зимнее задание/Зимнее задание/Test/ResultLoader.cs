using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public struct ResultLoader
    {
        public int LoadedCount { get; set; }
        public int TotalLines { get; set; }
        public List<string> Errors { get; set; }
        public bool HasErrors => Errors.Count > 0;
    }
}
