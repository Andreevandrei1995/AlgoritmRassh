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
            if (!AllActiveVelocityRestrictions.areAllRestrictionsInitialized())
            {
                throw new Exception("Не все ограничения могут быть проверены.");
            }
            this.list = new List<Object>();
            this.exist = false;
            List<Object> allVelocityRestrictions = new List<Object>(Program.allVelocityRestrictions);
            foreach (VelocityRestriction velocityRestriction in allVelocityRestrictions)
            {
                if (velocityRestriction.check())
                {
                    this.list.Add(velocityRestriction);
                    this.exist = true;
                }                
            }
        }
        public static bool areAllRestrictionsInitialized()
        {
            List<Object> allVelocityRestrictions = new List<Object>(Program.allVelocityRestrictions);      
            foreach (VelocityRestriction velocityRestriction in allVelocityRestrictions)
            {
                //Exception может выскочить при попытке проверить все необходимые элементы для проверки ограничения на факт,
                //что оно является активным в данный moment
                try
                {
                    velocityRestriction.check();
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
