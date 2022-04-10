using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;

namespace Negocio
{
    public class Departamento_BS
    {
        public Departamento_Res lista()
        {
            return new Departamento_DA().Listar();
        }
    }
}
