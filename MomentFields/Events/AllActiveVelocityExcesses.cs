﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmRassh
{
    class AllActiveVelocityExcesses : InterfaceExist
    {
        public bool exist { get; set; }
        public List<VelocityExcess> list;
        public AllActiveVelocityExcesses()
        {
            Program.allNecessaryParamsFoundOrException("allActiveVelocityExcesses");
            Moment thisMoment = Program.lastMoment;
            Moment previousMoment = Program.previousMoment;

            this.list = new List<VelocityExcess>();
            List<VelocityExcess> listFromPreviousMoment = new List<VelocityExcess>();
            if (previousMoment != null && previousMoment.allActiveVelocityExcesses.exist == true) {
                listFromPreviousMoment = previousMoment.allActiveVelocityExcesses.list;
            }
            this.exist = false;
            //Добавление превышений скорости
            //Новых или из предыдущего момента при условии, что превышение скорости уже существовало для данного типа ограничения скорости
            if (thisMoment.allActiveVelocityRestrictions.exist == true && thisMoment.trainVelocity.exist == true)
            {
                foreach (VelocityRestriction velocityRestriction in thisMoment.allActiveVelocityRestrictions.list)
                {
                    if (thisMoment.trainVelocity.value > velocityRestriction.velocity.value)
                    {
                        foreach (VelocityExcess previousMomentVelocityExcess in listFromPreviousMoment)
                        {
                            if (velocityRestriction == previousMomentVelocityExcess.velocityRestriction)
                            {
                                //Добавление уже действующего превышения скорости
                                this.list.Add(previousMomentVelocityExcess);
                                goto endIf;
                            }
                        }
                        //Добавление нового превышения скорости
                        this.list.Add(new VelocityExcess(thisMoment, velocityRestriction));

                        endIf:
                        this.exist = true;
                    }
                }
            }            
            //Проставляем endMoment для превышений скорости, которые закончились в предыдущий момент (в этот момент для них уже нет нарушений)
            foreach (VelocityExcess previousMomentVelocityExcess in listFromPreviousMoment)
            {
                if (this.list.IndexOf(previousMomentVelocityExcess) == -1)
                {
                    //Нарушение для ограничения скорости закончилось в предыдущий момент
                    previousMomentVelocityExcess.endMoment = previousMoment;
                }
            }
        }
    }
}
