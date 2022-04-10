using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class IncidenteFile_Res
    {
        public DTOHeader oHeader { get; set; }
        public List<Incidente_File> IncidenteFileList { get; set; }
    }
    public  class Incidente_File
    {
        public int id_incidente_file { get; set; }
        public DateTime fecha_registro { get; set; }
        public string url_imagen { get; set; }
        public int id_incidente { get; set; }

    }
}
