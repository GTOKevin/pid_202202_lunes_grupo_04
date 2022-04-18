using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class VISITAREGISTRO_Res
    {
        public DTOHeader oHeader { get; set; }
        public List<VISITA_REGISTRO> VisitaRegistroList { get; set; }
    }

    public class VISITA_REGISTRO
    {
        public int id_visita_registro { get; set; }
        public DateTime fecha_ingreso { get; set; }
        public DateTime fecha_salida { get; set; }
        public int id_departamento { get; set; }
        public int id_visitante { get; set; }


    }
    public class VISITA_REGISTRO_Register
    {
        public DTOHeader oHeader { get; set; }
        public int id_register { get; set; }
    }

}
