using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmRassh
{
    abstract class VelocityRestriction
    {
        public double velocity;

        public string type;

        abstract public bool check();

        public VelocityRestriction(double velocity, string type)
        {            
            this.velocity = velocity;
            this.type = type;
        }
    }
}
