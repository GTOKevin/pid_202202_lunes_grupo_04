﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;


namespace Negocio
{
    public class Movimiento_BS
    {
        public Movimiento_Res lista()
        {
            return new Movimiento_DA().Listar();
        }
    }
}
