﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;

namespace Negocio
{
    public class Usuario_BS
    {
        public Usuario_Res lista()
        {
            return new Usuario_DA().Listar();
        }
    }
}
