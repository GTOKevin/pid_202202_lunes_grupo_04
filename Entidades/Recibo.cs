using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Recibo
    {
        private int id_recibo { get; set; }
        private int id_servicio { get; set; }
        private decimal monto { get; set; }
        private byte estado { get; set; }
        private DateTime fecha_pago { get; set; }
        private DateTime fecha_vencimiento { get; set; }
        private DateTime fecha_registro { get; set; }
    }
}
