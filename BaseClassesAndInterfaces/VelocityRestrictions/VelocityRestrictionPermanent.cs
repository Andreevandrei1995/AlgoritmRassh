using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmRassh
{
    class VelocityRestrictionPermanent : VelocityRestriction
    {
        public Coordinate coordBegin;
        public Coordinate coordEnd;
        //public string trafficKind;

        public VelocityRestrictionPermanent(double valueVelocity, string type, Coordinate coordBegin, Coordinate coordEnd) :base(valueVelocity, type)
        {
            this.coordBegin = coordBegin;
            this.coordEnd = coordEnd;
        }

        public VelocityRestrictionPermanent(double valueVelocity, string type, string kmBegin, int pkBegin, int mBegin, string kmEnd, int pkEnd, int mEnd) : base(valueVelocity, type)
        {
            this.coordBegin = new Coordinate(kmBegin,pkBegin,mBegin);
            this.coordEnd = new Coordinate(kmEnd, pkEnd, mEnd);
        }

        public override bool checkExisting(Moment moment, bool checkDependenciesInitialized)
        {
            if (checkDependenciesInitialized)
            {
                Program.allDependenciesInitializedOrException("velocityRestrictionPermanent");
            }
            if (!(moment.trainCoordinate.exist == true))
            {
                return false;
            }
            if (moment.trainCoordinate.GetMetricCoord() < coordBegin.GetMetricCoord()
                || moment.trainCoordinate.GetMetricCoord() > coordEnd.GetMetricCoord())
            {
                return false;
            }
            return true;
        }
    }
}
