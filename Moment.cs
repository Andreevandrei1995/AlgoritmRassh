﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmRassh
{
    class Moment
    {
        public int index { get; set; }
        public CurrentCoordinate currentCoordinate;
        public LocoSvetofor locoSvetofor;
        public Svetofor svetofor;
        public CurrentVelocity currentVelocity;
        public AllActiveVelocityRestrictions allActiveVelocityRestrictions;
        public AllActiveVelocityExcesses allActiveVelocityExcesses;
        public Formulation formulation;
        public Moment(int index)
        {
            this.index = index;
            this.currentCoordinate = null;
            this.locoSvetofor = null;
            this.svetofor = null;
            this.currentVelocity = null;
            this.allActiveVelocityRestrictions = null;
            this.allActiveVelocityExcesses = null;
            this.formulation = null;
        }
        public void init(InitialParamsForOneMoment initialParams)
        {
            Type type = this.GetType();
            FieldInfo[] allFields = type.GetFields();
            int maxCycles = allFields.Length;
            int quantityCycles = 0;
            while (Program.checkNecessaryParams("moment") != Program.NecessaryParamsStatus.NotInitialized && quantityCycles < maxCycles)
            {
                if (this.currentCoordinate == null)
                {
                    try
                    {
                        this.currentCoordinate = new CurrentCoordinate(initialParams.kmCoordinate, initialParams.pkCoordinate, initialParams.mCoordiante);
                    }
                    catch (Exception e)
                    {

                    }
                }
                if (this.locoSvetofor == null)
                {
                    try
                    {
                        this.locoSvetofor = new LocoSvetofor(initialParams.colorLocoSvetofor);
                    }
                    catch (Exception e)
                    {

                    }
                }
                if (this.svetofor == null)
                {
                    try
                    {
                        this.svetofor = new Svetofor(initialParams.kmSvetofor, initialParams.pkSvetofor, initialParams.mSvetofor, initialParams.nameSvetofor);
                    }
                    catch (Exception e)
                    {

                    }
                }
                if (this.currentVelocity == null)
                {
                    try
                    {
                        this.currentVelocity = new CurrentVelocity(initialParams.valueVelocity);
                    }
                    catch (Exception e)
                    {

                    }
                }
                if (this.allActiveVelocityRestrictions == null)
                {
                    try
                    {
                        this.allActiveVelocityRestrictions = new AllActiveVelocityRestrictions();
                    }
                    catch (Exception e)
                    {

                    }
                }
                if (this.allActiveVelocityExcesses == null)
                {
                    try
                    {
                        this.allActiveVelocityExcesses = new AllActiveVelocityExcesses();
                    }
                    catch (Exception e)
                    {

                    }
                }
                if (this.formulation == null)
                {
                    try
                    {
                        this.formulation = new Formulation();
                    }
                    catch (Exception e)
                    {

                    }
                }
                quantityCycles++;
            }
        }
    }
}
