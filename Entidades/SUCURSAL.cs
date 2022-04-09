using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class SUCURSAL
    {
        public int id_sucursal { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public DateTime fecha_creacion { get; set; }
    }
}
