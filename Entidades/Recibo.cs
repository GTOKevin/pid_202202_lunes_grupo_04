using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Recibo_Res
    {
        public DTOHeader oHeader { get; set; }
        public List<Recibo> ReciboList { get; set; }
    }
    public class Recibo
    {
        public int id_recibo { get; set; }
        public int id_servicio { get; set; }
        public decimal monto { get; set; }
        public bool estado { get; set; }
        public DateTime? fecha_pago { get; set; }
        public DateTime? fecha_vencimiento { get; set; }
        public DateTime? fecha_registro { get; set; }
    }

    public class Recibo_Register
    {
        public DTOHeader oHeader { get; set; }
        public int id_register { get; set; }
    }
}
