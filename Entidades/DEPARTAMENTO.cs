﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Departamento
    {
        public int id_departamento { get; set; }
        public int piso { get; set; }
        public int numero{ get; set; }
        public int metros_cuadrado { get; set; }
        public int dormitorio { get; set; }
        public int banio { get; set; }
        public DateTime fecha_creacion { get; set; }
        public DateTime fecha_actualizacion { get; set; }
        public int id_torre { get; set; }
        public int id_usuario { get; set; }


    }
}