﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmRassh
{
    class LocoSvetofor : InterfaceExist
    {
        public bool exist { get; set; }

        /// <summary>
        /// Цвет светофора
        /// </summary>
        public string color;

        public LocoSvetofor(string color)
        {
            this.color = color;

            this.exist = true;
        }
    }
}
