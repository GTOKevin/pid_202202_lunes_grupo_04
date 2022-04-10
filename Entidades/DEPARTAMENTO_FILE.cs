using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Departamento_File
    {
        public int id_departamento_file { get; set; }
        public string url_imagen { get; set; }
        public DateTime fecha_creacion { get; set; }
        public int id_departamento { get; set; }
    }
}
