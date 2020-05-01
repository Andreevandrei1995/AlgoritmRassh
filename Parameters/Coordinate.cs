using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmRassh
{
    class Coordinate
    {
        public bool exist;

        public string km;

        public int pk;

        public int m;

        public Coordinate(string km, int pk, int m)
        {
            this.km = km;
            this.pk = pk;
            this.m = m;

            this.exist = true;
        }

        public int GetMetricCoord()
        {
            return Int32.Parse(this.km) * 1000 + pk * 100 + m;
        }
    }
}
