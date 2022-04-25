using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Entidades
{
    public class Perfil_File
    {
        public int id_file { get; set; }
        public string nombrefile { get; set; }
        public int id_perfil { get; set; }
        public HttpPostedFileBase ImagenFile { get; set; }

    }

    public class Perfil_File_res
    {
        public DTOHeader oHeader { get; set; }
        public List<Perfil_File> FileList { get; set; }
    }

}
