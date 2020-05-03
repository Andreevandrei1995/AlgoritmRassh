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
        public Formulation()
        {
            Program.allNecessaryParamsFoundOrException("formulation");
            //Moment thisMoment = Program.lastMoment;
            //this.list = new List<VelocityRestriction>();
            //this.exist = false;
            //foreach (VelocityRestriction velocityRestriction in thisMoment.allActiveVelocityRestrictions.list)
            //{
            //    if (thisMoment.currentVelocity.value > velocityRestriction.velocity.value)
            //    {
            //        this.list.Add(velocityRestriction);
            //        this.exist = true;
            //    }
            //}
        }
    }
}
