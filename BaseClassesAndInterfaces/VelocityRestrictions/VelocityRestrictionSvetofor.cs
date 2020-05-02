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
        public override bool check()
        {
            Program.allNecessaryParamsFoundOrException("velocityRestrictionSvetofor");
            Moment moment = Program.lastMoment;
            if (moment.svetofor.coordinate.GetMetricCoord() - moment.currentCoordinate.GetMetricCoord() > this.metricDistance)
            {
                return false;
            }
            if (moment.locoSvetofor.color != this.colorSvetofor)
            {
                return false;
            }
            return true;
        }
    }
}
