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
        public override bool checkExisting(Moment moment, bool checkNecessaryParams)
        {
            if (checkNecessaryParams)
            {
                Program.allNecessaryParamsFoundOrException("velocityRestrictionSvetofor");
            }            
            Moment thisMoment = moment;
            if (!(thisMoment.svetofor.exist == true && thisMoment.trainCoordinate.exist == true && thisMoment.trainSvetofor.exist == true))
            {
                return false;
            }
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
