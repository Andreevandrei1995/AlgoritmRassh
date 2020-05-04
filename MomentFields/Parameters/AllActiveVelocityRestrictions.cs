﻿using System;
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
        public List<VelocityRestriction> list;
        public AllActiveVelocityRestrictions()
        {
            foreach (string necessaryParamName in Program.getListNecessaryParamNames("AllActiveVelocityRestrictions"))
            {
                Program.allNecessaryParamsFoundOrException(necessaryParamName);
            }
            this.list = new List<VelocityRestriction>();
            this.exist = false;
            List<VelocityRestriction> allVelocityRestrictions = new List<VelocityRestriction>(Program.allVelocityRestrictions);
            foreach (VelocityRestriction velocityRestriction in allVelocityRestrictions)
            {
                if (velocityRestriction.checkExisting(false))
                {
                    this.list.Add(velocityRestriction);
                    this.exist = true;
                }
            }
        }
    }
}
