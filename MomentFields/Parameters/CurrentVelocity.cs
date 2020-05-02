using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmRassh
{
    class CurrentVelocity : Velocity, InterfaceExist
    {
        public bool exist { get; set; }
        public CurrentVelocity(double value) : base(value)
        {
            Program.allNecessaryParamsFoundOrException("currentVelocity");
            Moment moment = Program.lastMoment;
            this.exist = true;
        }
    }
}
