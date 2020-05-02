using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmRassh
{
    class CurrentCoordinate : Coordinate, InterfaceExist
    {
        public bool exist { get; set; }

        public CurrentCoordinate(string km, int pk, int m) : base(km, pk, m)
        {
            Program.allNecessaryParamsFoundOrException("currentCoordinate");
            this.exist = true;
        }
    }
}
