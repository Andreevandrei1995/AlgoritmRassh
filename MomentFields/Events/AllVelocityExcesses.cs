using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmRassh
{
    class AllVelocityExcesses : InterfaceExist
    {
        public bool exist { get; set; }
        public List<VelocityExcess> activeList;
        public List<VelocityExcess> endedInPreviousMomentList;
        public AllVelocityExcesses()
        {
            Program.allDependenciesInitializedOrException("allVelocityExcesses");
            Moment currentMoment = Program.currentMoment;
            Moment previousMoment = Program.previousMoment;

            this.activeList = new List<VelocityExcess>();
            this.endedInPreviousMomentList = new List<VelocityExcess>();
            List<VelocityExcess> previousMomentActiveList = new List<VelocityExcess>();
            if (previousMoment != null && previousMoment.allVelocityExcesses.exist == true) {
                previousMomentActiveList = previousMoment.allVelocityExcesses.activeList;
            }
            this.exist = false;
            //Добавление превышений скорости
            //Новых или из предыдущего момента при условии, что превышение скорости уже существовало для данного типа ограничения скорости
            if (currentMoment.allActiveVelocityRestrictions.exist == true && currentMoment.trainVelocity.exist == true)
            {
                foreach (VelocityRestriction velocityRestriction in currentMoment.allActiveVelocityRestrictions.list)
                {
                    if (currentMoment.trainVelocity.value > velocityRestriction.velocity.value)
                    {
                        foreach (VelocityExcess previousMomentVelocityExcess in previousMomentActiveList)
                        {
                            if (velocityRestriction == previousMomentVelocityExcess.velocityRestriction)
                            {
                                //Добавление уже действующего превышения скорости
                                this.activeList.Add(previousMomentVelocityExcess);
                                goto endIf;
                            }
                        }
                        //Добавление нового превышения скорости
                        this.activeList.Add(new VelocityExcess(currentMoment, velocityRestriction));

                        endIf:
                        this.exist = true;
                    }
                }
            }            
            //Проставляем endMoment для превышений скорости, которые закончились в предыдущий момент (в этот момент для них уже нет нарушений)
            foreach (VelocityExcess previousMomentVelocityExcess in previousMomentActiveList)
            {
                if (this.activeList.IndexOf(previousMomentVelocityExcess) == -1)
                {
                    //Нарушение для ограничения скорости закончилось в предыдущий момент
                    previousMomentVelocityExcess.endMoment = previousMoment;
                    endedInPreviousMomentList.Add(previousMomentVelocityExcess);
                }
            }
        }
    }
}
