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
            this.list = new List<Object>();
            this.exist = false;            
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
