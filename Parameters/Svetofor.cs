using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmRassh
{
    class Svetofor : InterfaceExist
    {
        public bool exist { get; set; }

        public string name;

        public Coordinate coordinate;

        public Svetofor(Coordinate coordinate, string name)
        {
            Program.allNecessaryParamsFoundOrException("svetofor");
            this.coordinate = coordinate;
            this.name = name;

            this.exist = true;
        }
    }
}
