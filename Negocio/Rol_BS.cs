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
        public Rol_Res lista(int id_rol)
        {
            return new Rol_DA().Listar(id_rol);
        }
        public Rol_Register Registrar(Rol ro)
        {
            return new Rol_DA().Registrar(ro);
        }
    }

}
