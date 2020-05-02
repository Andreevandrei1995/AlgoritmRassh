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
            this.list = new List<Object>();
            this.exist = false;
            Moment moment = Program.lastMoment;            
            foreach (VelocityRestriction velocityRestriction in moment.allActiveVelocityRestrictions.list)
            {
                if (moment.currentVelocity.value > velocityRestriction.velocity.value)
                {
                    this.list.Add(velocityRestriction);
                    this.exist = true;
                }
            }            
        }
    }
}
