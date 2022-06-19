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

        public Recibo_Res Listar_N()
        {
            return new Recibo_DA().Listar_N();
        }

        public Recibo_Res Listar_Filtro(string dni = "", string nombre = "", string servicio = "", int estado=0)
        {
            return new Recibo_DA().Listar_Filtro(dni,nombre,servicio,estado);
        }

        public Recibo_Res Pagar_Recibo(int id_Recibo, int estado)
        {
            return new Recibo_DA().Pagar_Recibo(id_Recibo, estado);
        }
    }
}
