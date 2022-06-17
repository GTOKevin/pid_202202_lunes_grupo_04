using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
   public class Incidente_Historial
    {
        public int id_incidente_historial { get; set; }
        public string acciones { get; set; }
        public DateTime? fecha_historial { get; set; }
        public int id_incidente { get; set; }
    }
    public class Incidente_Historial_Register
    {
        public DTOHeader oHeader { get; set; }
        public int id_register { get; set; }
    }
    public class Incidente_Historial_Res
    {
        public DTOHeader oHeader { get; set; }
        public List<Incidente_Historial> lista_Incidente_Historial { get; set; }
    }
}
