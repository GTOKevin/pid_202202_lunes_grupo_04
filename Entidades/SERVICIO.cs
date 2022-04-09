using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    class SERVICIO
    {
        public int id_servicio { get; set; }
        public int id_tipo { get; set; }
        public int id_departamento { get; set; }
        public string nombre { get; set; }
        public DateTime fecha_registro { get; set; }
    }
}
