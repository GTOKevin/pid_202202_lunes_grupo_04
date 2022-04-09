using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
   public  class INCIDENTE_FILE
    {
        public int id_incidente_file { get; set; }
        public DateTime fecha_registro { get; set; }
        public string url_imagen { get; set; }
        public int id_incidente { get; set; }

    }
}
