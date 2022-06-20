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

    public class IncidenteHistorialDTO
    {
        public int id_incidente { get; set; }
        public DateTime? fecha_incidente { get; set; }
        public string descripcion { get; set; }
        public string nombre_reportado { get; set; }
        public string tipo_documento { get; set; }
        public string nro_documento { get; set; }
        public DateTime? fecha_registro { get; set; }
        public int id_departamento { get; set; }
        public int id_usuario { get; set; }
        public bool estado { get; set; }
        public int id_incidente_historial { get; set; }
        public string acciones { get; set; }
        public DateTime? fecha_historial { get; set; }

        public string username { get; set; }
        public string nombre_usuarioreg { get; set; }
        public string documento_usuarioreg { get; set; }

        public int departamento { get; set; }
        public int id_sucursal { get; set; }
        public string sucursal { get; set; }
        public int id_sector { get; set; }
        public string sector { get; set; }
        public int id_torre { get; set; }
        public decimal torre { get; set; }

    }
}
