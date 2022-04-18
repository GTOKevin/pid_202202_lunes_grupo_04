using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Propietario_Res
    {
        public DTOHeader oHeader { get; set; }
        public List<Propietario> lista_Propietario { get; set; }
    }
    public class Propietario
    {
        public int id_propietario { get; set; }
        public string nombres { get; set; }
        public string primer_apellido { get; set; }
        public string segundo_apellido { get; set; }
        public string tipo_documento { get; set; }
        public string nro_documento { get; set; }
        public string nacionalidad { get; set; }
        public DateTime? fecha_registro { get; set; }
        public byte estado { get; set; }
        public int id_departamento { get; set; }
        public int id_tipo { get; set; }
    }
}
