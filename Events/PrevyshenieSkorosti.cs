using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmRassh
{
    class PrevyshenieSkorosti : InterfaceExist
    {
        public bool exist { get; set; }

        public List<Object> list;

        public PrevyshenieSkorosti()
        {
            Program.allNecessaryParamsFoundOrException("prevyshenieSkorosti");
            Moment moment = Program.lastMoment;            
            foreach (VelocityRestriction velocityRestriction in moment.allActiveVelocityRestrictions.list)
            {
                if (moment.velocity.value > velocityRestriction.velocity)
                {
                    this.list.Add(velocityRestriction);
                }
            }
        }
    }
}
