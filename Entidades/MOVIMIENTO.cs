using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Movimiento_Res
    {
        public DTOHeader oHeader { get; set; }
        public List<Movimiento> MovimientoList { get; set; }
    }
    public  class Movimiento
    {
        public int id_movimiento { get; set; }
        public int id_propietario { get; set; }
        public int id_tipo { get; set; }
        public DateTime fecha_registro { get; set; }

    }
    public class Movimiento_Register
    {
        public DTOHeader oHeader { get; set; }
        public int id_register { get; set; }
    }

}
