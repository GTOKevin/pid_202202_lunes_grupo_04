﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Datos;
using Entidades;

namespace Negocio
{
    public class Sector_BS
    {
        public Sector_Res lista()
        {
            return new Sector_DA().Listar();
        }

    }
}