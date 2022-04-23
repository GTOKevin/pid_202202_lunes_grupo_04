using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;


namespace Negocio
{
    public class Recibo_BS
    {
        public Recibo_Res lista(int id_recibo)
        {
            return new Recibo_DA().Listar(id_recibo);
        }

        public Recibo_Servicio_Res listar_servicio(int id)
        {
            return new Recibo_DA().Listar_servicio(id);
        }
        public Recibo_Register Registrar(Recibo ent)
        {
            return new Recibo_DA().Registrar(ent);
        }
    }
}
