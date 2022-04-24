using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Datos;
using Entidades;

namespace Negocio
{
    public class Servicio_BS
    {
        public Servicio_Res lista(int id_servicio)
        {
            return new Servicio_DA().Listar(id_servicio);
        }
        public Servicio_Register Registrar(Servicio ent)
        {
            return new Servicio_DA().Registrar(ent);
        }
    }
}
