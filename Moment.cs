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
        public CurrentCoordinate currentCoordinate;
        public LocoSvetofor locoSvetofor;
        public Svetofor svetofor;
        public CurrentVelocity currentVelocity;
        public AllActiveVelocityRestrictions allActiveVelocityRestrictions;
        public AllActiveVelocityExcesses allActiveVelocityExcesses;
        public Moment(int index)
        {
            this.index = index;
            this.currentCoordinate = null;
            this.locoSvetofor = null;
            this.svetofor = null;
            this.currentVelocity = null;
            this.allActiveVelocityRestrictions = null;
            this.allActiveVelocityExcesses = null;
        }
        public void init(InitialParamsForOneMoment initialParams)
        {            
            int maxCycles = 100;
            int quantityCycles = 0;
            while (!this.checkAllFieldsAreInitialized() && quantityCycles < maxCycles)
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
                quantityCycles++;
            }
        }
        private bool checkAllFieldsAreInitialized ()
        {
            Type type = this.GetType();
            FieldInfo[] allFields = type.GetFields();
            string beginOfOutString = "Проверка объекта класса Moment.";
            int quantityFoundNecessaryParams = 0;
            foreach (FieldInfo fieldInfo in type.GetFields())
            {
                var fieldValue = fieldInfo.GetValue(this);
                if (!(fieldValue is Object))
                {
                    Console.WriteLine(beginOfOutString + " Поле " + fieldInfo + " имеет тип поля не Object.");
                    continue;
                }
                if (fieldValue == null)
                {
                    Console.WriteLine(beginOfOutString + " Поле " + fieldInfo + " не инициализировано.");
                    continue;
                }
                //Работаем с полем exist
                if (!(fieldValue is InterfaceExist))
                {
                    Console.WriteLine(beginOfOutString + " Поле " + fieldInfo + " не наследует интерфейс InterfaceExist.");
                    continue;
                }
                quantityFoundNecessaryParams++;
            }
            if (quantityFoundNecessaryParams == allFields.Length)
            {
                return true;
            }
            return false;
        }
    }
}
