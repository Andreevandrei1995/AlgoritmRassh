using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmRassh
{
    class LocoSvetofor : InterfaceExist
    {
        public bool exist { get; set; }        
        public string color;
        public LocoSvetofor(string color)
        {
            Program.allNecessaryParamsFoundOrException("locoSvetofor");
            this.color = color;
            this.exist = true;
        }
    }
}
