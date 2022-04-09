using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
   public  class MOVIMIENTO
    {
        public int id_movimiento { get; set; }
        public int id_propietario { get; set; }
        public int id_tipo { get; set; }
        public DateTime fecha_registro { get; set; }

    }
}
