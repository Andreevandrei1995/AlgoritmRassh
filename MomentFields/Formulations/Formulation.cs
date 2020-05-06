using System;
using System.Collections.Generic;

namespace AlgoritmRassh
{
    class Formulation : InterfaceExist
    {
        public bool exist { get; set; }
        public List<string> list = null;
        public Formulation()
        {
            Program.allDependenciesInitializedOrException("formulation");
            Moment currentMoment = Program.currentMoment;

            this.list = new List<String>();
            this.exist = false;
            List<VelocityExcess> endedInPreviousMomentList = currentMoment.endedInPreviousMomentVelocityExcesses.exist == true ? currentMoment.endedInPreviousMomentVelocityExcesses.list : new List<VelocityExcess>();
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
                    list.Add(formulation);
                    Console.WriteLine(formulation);
                    this.exist = true;
                }
                if (
                    endedVelocityExcess.velocityRestriction is VelocityRestrictionPermanent &&
                    endedVelocityExcess.velocityRestriction.type == "velocityRestrictionPermanent"
                )
                {
                    VelocityRestrictionPermanent velocityRestrictionPermanent = (VelocityRestrictionPermanent)endedVelocityExcess.velocityRestriction;
                    Velocity maxVelocity = endedVelocityExcess.startMoment.trainVelocity;
                    for (int i = endedVelocityExcess.startMoment.index + 1; i <= endedVelocityExcess.endMoment.index; i++)
                    {
                        Velocity currentVelocity = Program.allMoments[i].trainVelocity;
                        if (currentVelocity.value > maxVelocity.value)
                        {
                            maxVelocity = currentVelocity;
                        }
                    }
                    string formulation = String.Format("Превышение постоянного скорости {0} км/ч на {1} км {2} пк с {3} км {4} пк по {5} км {6} пк максимально на {7} км/ч.\nС {8} по {9} секунду.",
                        velocityRestrictionPermanent.velocity.value.ToString(),
                        velocityRestrictionPermanent.coordBegin.km,
                        velocityRestrictionPermanent.coordBegin.pk.ToString(),
                        endedVelocityExcess.startMoment.trainCoordinate.km,
                        endedVelocityExcess.startMoment.trainCoordinate.pk,
                        endedVelocityExcess.endMoment.trainCoordinate.km,
                        endedVelocityExcess.endMoment.trainCoordinate.pk,
                        (maxVelocity.value - velocityRestrictionPermanent.velocity.value).ToString(),
                        endedVelocityExcess.startMoment.index,
                        endedVelocityExcess.endMoment.index
                    );
                    list.Add(formulation);
                    Console.WriteLine(formulation);
                    this.exist = true;
                }
            }
        }
    }
}
