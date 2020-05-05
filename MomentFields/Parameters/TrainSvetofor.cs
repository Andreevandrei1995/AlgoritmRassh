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
            Program.allDependenciesInitializedOrException("trainSvetofor");
            Moment currentMoment = Program.currentMoment;
            this.color = color;
            this.exist = true;
        }
    }
}
