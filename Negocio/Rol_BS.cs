using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;
namespace Negocio
{
    public class Rol_BS
    {
        public Rol_Res lista()
        {
            return new Rol_DA().Listar();
        }
    }
}
