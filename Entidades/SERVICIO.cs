using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Servicio_Res
    {
        public DTOHeader oHeader { get; set; }
        public List<Servicio> ServicioList { get; set; }
    }
    public class Servicio
    {
        public int id_servicio { get; set; }
        public int id_tipo { get; set; }
        public int id_departamento { get; set; }
        public string nombre { get; set; }
        public DateTime? fecha_registro { get; set; }
    }
}
