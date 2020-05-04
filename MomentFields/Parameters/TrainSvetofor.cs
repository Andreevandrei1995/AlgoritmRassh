using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmRassh
{
    class TrainSvetofor : InterfaceExist
    {
        public bool exist { get; set; }        
        public string color;
        public TrainSvetofor(string color)
        {
            Program.allNecessaryParamsFoundOrException("trainSvetofor");
            Moment thisMoment = Program.lastMoment;
            this.color = color;
            this.exist = true;
        }
    }
}
