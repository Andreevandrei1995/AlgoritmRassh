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
        public override bool checkExisting(Moment moment, bool checkDependenciesInitialized)
        {
            if (checkDependenciesInitialized)
            {
                Program.allDependenciesInitializedOrException("velocityRestrictionSvetofor");
            }
            if (!(moment.svetofor.exist == true && moment.trainCoordinate.exist == true && moment.trainSvetofor.exist == true))
            {
                return false;
            }
            if (moment.svetofor.coordinate.GetMetricCoord() - moment.trainCoordinate.GetMetricCoord() > this.metricDistance)
            {
                return false;
            }
            if (moment.trainSvetofor.color != this.colorSvetofor)
            {
                return false;
            }
            return true;
        }
    }
}
