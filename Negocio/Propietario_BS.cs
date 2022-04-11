using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;

namespace Negocio
{
    public class Propietario_BS
    {
        public Propietario_Res lista()
        {
            return new Propietario_DA().Listar();
        }
    }
}
