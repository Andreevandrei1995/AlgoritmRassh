using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmRassh
{
    class TrainVelocity : Velocity, InterfaceExist
    {
        public bool exist { get; set; }
        public TrainVelocity(double value) : base(value)
        {
            Program.allNecessaryParamsFoundOrException("trainVelocity");
            Moment thisMoment = Program.lastMoment;
            this.exist = true;
        }
    }
}
