using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmRassh
{
    class AllActiveVelocityRestrictions : InterfaceExist 
    {
        public bool exist { get; set; }
        public List<Object> list;

        public AllActiveVelocityRestrictions()
        {
            if (!AllActiveVelocityRestrictions.isAllRestrictionsCanBeChecked())
            {
                throw new Exception("Не все ограничения могут быть проверены.");
            }

            this.list = new List<Object>();
            this.exist = false;
            List<Object> allVelocityRestrictions = new List<Object>(Program.allVelocityRestrictions);
            foreach (object velocityRestriction in allVelocityRestrictions)
            {
                this.Add(velocityRestriction);
            }
        }

        public void Add (Object velocityRestriction)
        {
            if (!(velocityRestriction is VelocityRestriction && velocityRestriction != null)) 
            {
                return;                
            }
            VelocityRestriction vel = (VelocityRestriction)velocityRestriction;
            if (vel.check())
            {
                this.list.Add(velocityRestriction);
                this.exist = true;
            }
        }

        public static bool isAllRestrictionsCanBeChecked()
        {
            
            List<Object> allVelocityRestrictions = new List<Object>(Program.allVelocityRestrictions);      
            foreach (object velocityRestriction in allVelocityRestrictions)
            {
                if (!(velocityRestriction is VelocityRestriction && velocityRestriction != null))
                {
                    return false;                    
                }
                VelocityRestriction vel = (VelocityRestriction)velocityRestriction;
                //Exception может выскочить при попытке проверить все необходимые элементы для проверки ограничения на факт,
                //что оно является активным в данный moment
                try
                {
                    vel.check();
                }
                catch (Exception e)
                {
                    return false;
                }                
            }
            return true;
        }
    }
}
