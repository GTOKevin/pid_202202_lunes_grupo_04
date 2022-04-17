using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;

namespace Negocio
{
    public class Sucursal_BS
    {

        public Sucursal_Res lista(int id_sucursal)
        {
            return new Sucursal_DA().Listar(id_sucursal);
        }

        public Sucursal_Register Registrar(Sucursal ent)
        {
            return new Sucursal_DA().Registrar(ent);
        }

    }
}
