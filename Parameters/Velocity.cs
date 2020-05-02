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

        public double velocity;

        public Velocity(double velocity)
        {
            this.velocity = velocity;
            this.exist = true;
        }
    }
}
