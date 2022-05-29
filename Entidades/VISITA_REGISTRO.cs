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
    public class VISITAREGISTROEntrada_Res
    {
        public DTOHeader oHeader { get; set; }
        public List<Visitante> VisitaRegistroList { get; set; }
    }
    public class VISITA_REGISTRO
    {
        public int id_visita_registro { get; set; }
        public DateTime? fecha_ingreso { get; set; }
        public DateTime? fecha_salida { get; set; }
        public int id_sucursal { get; set; }
        public int id_departamento { get; set; }
        public int id_visitante { get; set; }
        public int id_sector { get; set; }
        public int id_torre { get; set; }
        public int numero_departamento { get; set; }
        public int numero_torre { get; set; }
        public string nombre_sucursal { get; set; }
        public string nombre_sector { get; set; }
        public string nro_documento { get; set; }
        public string nombre_visitante { get; set; }
    }
    public class VISITA_REGISTRO_Register
    {
        public DTOHeader oHeader { get; set; }
        public int id_register { get; set; }
    }

    public class VisitaVisitante
    {
        public int id_departamento { get; set;}
        public List<Visitante> visitantes { get; set; }
    }

}
