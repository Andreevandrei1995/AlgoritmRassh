using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmRassh
{
    class ActiveVelocityExcesses : InterfaceExist
    {
        public bool exist { get; set; }
        public List<VelocityExcess> list;
        public ActiveVelocityExcesses()
        {
            Program.allDependenciesInitializedOrException("activeVelocityExcesses");
            Moment currentMoment = Program.currentMoment;
            Moment previousMoment = Program.previousMoment;

            this.list = new List<VelocityExcess>();
            List<VelocityExcess> previousMomentActiveList = new List<VelocityExcess>();
            if (previousMoment != null && previousMoment.activeVelocityExcesses.exist == true) {
                previousMomentActiveList = previousMoment.activeVelocityExcesses.list;
            }
            this.exist = false;
            //Добавление превышений скорости
            //Новых или из предыдущего момента при условии, что превышение скорости уже существовало для данного типа ограничения скорости
            if (currentMoment.activeVelocityRestrictions.exist == true && currentMoment.trainVelocity.exist == true)
            {
                foreach (VelocityRestriction velocityRestriction in currentMoment.activeVelocityRestrictions.list)
                {
                    if (currentMoment.trainVelocity.value > velocityRestriction.velocity.value)
                    {
                        foreach (VelocityExcess previousMomentVelocityExcess in previousMomentActiveList)
                        {
                            if (velocityRestriction == previousMomentVelocityExcess.velocityRestriction)
                            {
                                //Добавление уже действующего превышения скорости
                                this.list.Add(previousMomentVelocityExcess);
                                goto endIf;
                            }
                        }
                        //Добавление нового превышения скорости
                        this.list.Add(new VelocityExcess(currentMoment, velocityRestriction));

                        endIf:
                        this.exist = true;
                    }
                }
            }
        }
    }
}
