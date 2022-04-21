using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Visitante_Res
    {
        public DTOHeader oHeader { get; set; }
        public List<Visitante> VisitanteList { get; set; }
    }

    public class Visitante
    {
        public int id_visitante { get; set; }
        public string nombre { get; set; }
        public string apellidos { get; set; }   
        public int tipo_documento { get; set; }
        public string nombre_tipo { get; set; } //adicional
        public string nro_documento { get; set; }
        public int genero { get; set; }
        public string nombre_genero { get; set; } //adicional
        public DateTime? fecha_creacion { get; set; }
   

    }

    public class Visitante_Register
    {
        public DTOHeader oHeader { get; set; }
        public int id_register { get; set; }
    }

}
