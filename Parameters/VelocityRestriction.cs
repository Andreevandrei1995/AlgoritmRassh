using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmRassh
{
    abstract class VelocityRestriction
    {
        public bool exist;

        public Velocity velocity;

        public string type;

        abstract public bool check();

        public VelocityRestriction(Velocity velocity, string type)
        {
            this.velocity = velocity;
            this.type = type;

            if (this.check())
            {
                this.exist = true;
            }
            else
            {
                this.exist = false;
            }
        }
    }
}
