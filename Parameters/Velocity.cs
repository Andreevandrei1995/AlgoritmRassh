using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmRassh
{
    class Velocity : InterfaceExist
    {
        public bool exist { get; set; }

        public double value;

        public Velocity(double velocityValue)
        {
            Program.allNecessaryParamsFoundOrException("velocity");
            this.value = velocityValue;
            this.exist = true;
        }
    }
}
