using System;
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
        public TrainCoordinate trainCoordinate;
        public TrainSvetofor trainSvetofor;
        public Svetofor svetofor;
        public TrainVelocity trainVelocity;
        public AllActiveVelocityRestrictions allActiveVelocityRestrictions;
        public AllVelocityExcesses allVelocityExcesses;
        public Formulation formulation;
        public Moment(int index)
        {
            this.index = index;
            this.trainCoordinate = null;
            this.trainSvetofor = null;
            this.svetofor = null;
            this.trainVelocity = null;
            this.allActiveVelocityRestrictions = null;
            this.allVelocityExcesses = null;
            this.formulation = null;
        }
        /// <summary>
        /// Заполняем момент исходя из данных поездки (текущего состояния параметров)
        /// </summary>
        /// <param name="initialParams"></param>
        public void init(InitialParamsForOneMoment initialParams)
        {
            Type type = this.GetType();
            //получить массив полей класса Moment
            FieldInfo[] allFields = type.GetFields();
            int maxCycles = allFields.Length;
            int quantityCycles = 0;
            while (Program.checkDependinciesStatus("moment") == Program.dependenciesStatus.NotInitialized && quantityCycles < maxCycles)
            {
                if (this.trainCoordinate == null)
                {
                    try
                    {
                        this.trainCoordinate = new TrainCoordinate(initialParams.kmCoordinate, initialParams.pkCoordinate, initialParams.mCoordiante);
                    }
                    catch (Exception e)
                    {

                    }
                }
                if (this.trainSvetofor == null)
                {
                    try
                    {
                        this.trainSvetofor = new TrainSvetofor(initialParams.colorLocoSvetofor);
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
                if (this.trainVelocity == null)
                {
                    try
                    {
                        this.trainVelocity = new TrainVelocity(initialParams.valueVelocity);
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
                if (this.allVelocityExcesses == null)
                {
                    try
                    {
                        this.allVelocityExcesses = new AllVelocityExcesses();
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
