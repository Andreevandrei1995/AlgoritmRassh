using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmRassh
{
    abstract class VelocityRestriction
    {
        public Velocity velocity;
        public string type;
        abstract public bool checkExisting(Moment moment, bool checkNecessaryParams);
        public bool checkExisting(bool checkNecessaryParams)
        {
            return this.checkExisting(Program.lastMoment, checkNecessaryParams);
        }
        public VelocityRestriction(double valueVelocity, string type)
        {            
            this.velocity = new Velocity(valueVelocity);
            this.type = type;
        }
    }
}
