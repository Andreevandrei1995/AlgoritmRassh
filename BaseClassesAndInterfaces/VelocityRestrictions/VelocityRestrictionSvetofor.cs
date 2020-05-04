using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmRassh
{
    class VelocityRestrictionSvetofor : VelocityRestriction
    {
        public string colorSvetofor;
        public int metricDistance;
        public VelocityRestrictionSvetofor(double valueVelocity, string type, string colorSvetofor, int metricDistance) : base(valueVelocity, type)
        {
            this.colorSvetofor = colorSvetofor;
            this.metricDistance = metricDistance;
        }
        public override bool checkNecessaryParams()
        {
            Program.allNecessaryParamsFoundOrException("velocityRestrictionSvetofor");
            Moment thisMoment = Program.lastMoment;
            if (thisMoment.svetofor.coordinate.GetMetricCoord() - thisMoment.trainCoordinate.GetMetricCoord() > this.metricDistance)
            {
                return false;
            }
            if (thisMoment.trainSvetofor.color != this.colorSvetofor)
            {
                return false;
            }
            return true;
        }
    }
}
