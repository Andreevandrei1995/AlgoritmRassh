using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmRassh
{
    class TrainCoordinate : Coordinate, InterfaceExist
    {
        public bool exist { get; set; }
        public TrainCoordinate(string km, int pk, int m) : base(km, pk, m)
        {
            Program.allNecessaryParamsFoundOrException("trainCoordinate");
            Moment thisMoment = Program.lastMoment;
            this.exist = true;
        }
    }
}
