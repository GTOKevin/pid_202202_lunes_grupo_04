using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Propietario
    {
        private int id_propietario { get; set; }
        private string nombres { get; set; }
        private string primer_apellido { get; set; }
        private string segundo_apellido { get; set; }
        private string tipo_documento { get; set; }
        private string nro_documento { get; set; }
        private string nacionalidad { get; set; }
        private DateTime fecha_registro { get; set; }
        private byte estado { get; set; }
        private int id_departamento { get; set; }
        private int id_tipo { get; set; }
    }
}
