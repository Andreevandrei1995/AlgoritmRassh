using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmRassh
{
    class Formulation : InterfaceExist
    {
        public bool exist { get; set; }
        public List<string> formulations = null;
        public Formulation()
        {
            Program.allNecessaryParamsFoundOrException("formulation");
            Moment thisMoment = Program.lastMoment;
            Moment previousMoment = Program.previousMoment;

            this.formulations = new List<String>();
            this.exist = false;
            if (!(previousMoment != null && previousMoment.allActiveVelocityExcesses.exist == true))
            {
                return;
            }
            List<VelocityExcess> listFromPreviousMoment = previousMoment.allActiveVelocityExcesses.list;
            foreach (VelocityExcess previousMomentVelocityExcess in listFromPreviousMoment)
            {
                if (
                    previousMomentVelocityExcess.velocityRestriction is VelocityRestrictionSvetofor &&
                    previousMomentVelocityExcess.velocityRestriction.type == "velocityRestrictionSvetofor" &&
                    previousMomentVelocityExcess.endMoment == previousMoment
                )
                {
                    VelocityRestrictionSvetofor velocityRestrictionSvetofor = (VelocityRestrictionSvetofor)previousMomentVelocityExcess.velocityRestriction;
                    formulations.Add("Формулировка ограничения для velocityRestrictionSvetofor");
                    this.exist = true;
                } 
            }
        }
    }
}
