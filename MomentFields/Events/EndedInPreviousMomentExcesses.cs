using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmRassh
{
    class EndedInPreviousMomentExcesses : InterfaceExist
    {
        public bool exist { get; set; }
        public List<VelocityExcess> list;
        public EndedInPreviousMomentExcesses()
        {
            Program.allDependenciesInitializedOrException("endedInPreviousMomentExcesses");
            Moment currentMoment = Program.currentMoment;
            Moment previousMoment = Program.previousMoment;

            this.list = new List<VelocityExcess>();
            List<VelocityExcess> currentMomentActiveList = currentMoment.activeVelocityExcesses.exist == true ? currentMoment.activeVelocityExcesses.list : new List<VelocityExcess>();
            List<VelocityExcess> previousMomentActiveList = new List<VelocityExcess>();
            if (previousMoment != null && previousMoment.activeVelocityExcesses.exist == true) {
                previousMomentActiveList = previousMoment.activeVelocityExcesses.list;
            }
            this.exist = false;           
            //Проставляем endMoment для превышений скорости, которые закончились в предыдущий момент (в этот момент для них уже нет нарушений)
            foreach (VelocityExcess previousMomentVelocityExcess in previousMomentActiveList)
            {
                if (currentMomentActiveList.IndexOf(previousMomentVelocityExcess) == -1)
                {
                    //Нарушение для ограничения скорости закончилось в предыдущий момент
                    previousMomentVelocityExcess.endMoment = previousMoment;
                    this.list.Add(previousMomentVelocityExcess);
                    this.exist = true;
                }
            }
        }
    }
}
