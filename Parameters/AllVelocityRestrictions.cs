using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmRassh
{
    class AllVelocityRestrictions : InterfaceExist 
    {
        public bool exist { get; set; }
        List<Object> list;

        public AllVelocityRestrictions () {
            this.list = new List<Object>();
        }

        public void Add (Object velocityRestriction)
        {
            if (velocityRestriction is VelocityRestriction && velocityRestriction != null) {
                VelocityRestriction vel = (VelocityRestriction)velocityRestriction;
                if (vel.exist)
                {
                    this.list.Add(velocityRestriction);
                    this.exist = true;
                }
            }
        }
    }
}
