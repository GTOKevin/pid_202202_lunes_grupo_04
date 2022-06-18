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

        public Recibo_Register Registrar(string servicio, int id_departamento, decimal monto, DateTime? fecha_pago=null)
        {
            return new Recibo_DA().Registrar(servicio, id_departamento,monto,fecha_pago);
        }
    }
}
