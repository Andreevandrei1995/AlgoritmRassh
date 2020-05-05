using System;
using System.Collections.Generic;

namespace AlgoritmRassh
{
    class Formulation : InterfaceExist
    {
        public bool exist { get; set; }
        public List<string> formulations = null;
        public Formulation()
        {
            Program.allDependenciesInitializedOrException("formulation");
            Moment currentMoment = Program.currentMoment;

            this.formulations = new List<String>();
            this.exist = false;
            List<VelocityExcess> endedInPreviousMomentList = currentMoment.allVelocityExcesses.endedInPreviousMomentList;
            foreach (VelocityExcess endedVelocityExcess in endedInPreviousMomentList)
            {
                if (
                    endedVelocityExcess.velocityRestriction is VelocityRestrictionSvetofor &&
                    endedVelocityExcess.velocityRestriction.type == "velocityRestrictionSvetofor"
                )
                {
                    VelocityRestrictionSvetofor velocityRestrictionSvetofor = (VelocityRestrictionSvetofor)endedVelocityExcess.velocityRestriction;
                    Svetofor svetofor = endedVelocityExcess.endMoment.svetofor;
                    Velocity maxVelocity = endedVelocityExcess.startMoment.trainVelocity;
                    for (int i = endedVelocityExcess.startMoment.index + 1; i <= endedVelocityExcess.endMoment.index; i++)
                    {
                        Velocity currentVelocity = Program.allMoments[i].trainVelocity;
                        if (currentVelocity.value > maxVelocity.value)
                        {
                            maxVelocity = currentVelocity;
                        }
                    }
                    string formulation = String.Format("Превышение скорости {0} км/ч за {1} метров до светофора {2} максимально на {3} км/ч.\nС {4} по {5} секунду.",
                        velocityRestrictionSvetofor.velocity.value.ToString(),
                        velocityRestrictionSvetofor.metricDistance.ToString(),
                        svetofor.name,
                        (maxVelocity.value - velocityRestrictionSvetofor.velocity.value).ToString(),
                        endedVelocityExcess.startMoment.index,
                        endedVelocityExcess.endMoment.index
                    );
                    formulations.Add(formulation);
                    //Console.WriteLine(formulation);
                    this.exist = true;
                }
            }
        }
    }
}
