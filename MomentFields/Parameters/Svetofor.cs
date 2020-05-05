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
        public Svetofor(string km, int pk, int m, string name)
        {
            Program.allDependenciesInitializedOrException("svetofor");
            Moment currentMoment = Program.currentMoment;
            this.coordinate = new Coordinate(km, pk, m);
            this.name = name;
            this.exist = true;
        }
    }
}
