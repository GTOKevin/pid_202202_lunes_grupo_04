using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Incidente_Res
    {
        public DTOHeader oHeader { get; set; }
        public List<IncidenteDTO> lista_Incidente { get; set; }
    }
    public class Incidente
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


    }
    public class Incidente_Register
    {
        public DTOHeader oHeader { get; set; }
        public int id_register { get; set; }
    }

    public class IncidenteDTO
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
        public int departamento { get; set; }
        public int id_sucursal { get; set; }
        public string sucursal { get; set; }
        public int id_sector { get; set; }
        public string sector { get; set; }
        public int id_torre { get; set; }
        public decimal torre { get; set; }

    }

    public class FiltroIncidente
    {
        public int id_incidente_f { get; set; }
        public string nombre_reportado_f { get; set; }
        public string nro_documento_f { get; set; }
        public string estado_f { get; set; }
        public int departamento_f { get; set; }
    }
}
