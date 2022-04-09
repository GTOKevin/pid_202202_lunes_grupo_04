using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class SECTOR
    {
        public int id_sector { get; set; }
        public string nombre_sector { get; set; }
        public DateTime fecha_creacion { get; set; }
        public int id_sucursal { get; set; }
    }
}
