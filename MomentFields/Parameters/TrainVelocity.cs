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
            Program.allDependenciesInitializedOrException("trainVelocity");
            Moment currentMoment = Program.currentMoment;
            this.exist = true;
        }
    }
}
