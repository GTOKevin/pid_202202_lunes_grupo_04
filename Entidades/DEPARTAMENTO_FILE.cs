using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Entidades
{

    public class Departamento_File_Res
    {
        public DTOHeader oHeader { get; set; }
        public List<Departamento_File> DepartamentoFileList { get; set; }
    }
    public class Departamento_File
    {
        public int id_departamento_file { get; set; }
        public string url_imagen { get; set; }
        public DateTime? fecha_creacion { get; set; }
        public int id_departamento { get; set; }

        public HttpPostedFileBase imagen {get; set;}
    }
    public class DepartamentoFile_Register
    {
        public DTOHeader oHeader { get; set; }
        public int id_register { get; set; }
    }
}
