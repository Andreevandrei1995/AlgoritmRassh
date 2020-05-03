using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmRassh
{
    class VelocityExcess
    {
        public Moment startMoment;
        public Moment endMoment;
        public VelocityRestriction velocityRestriction;
        public VelocityExcess(Moment startMoment, VelocityRestriction velocityRestriction)
        {
            this.startMoment = startMoment;
            this.velocityRestriction = velocityRestriction;
            this.endMoment = null;
        }
    }
}
