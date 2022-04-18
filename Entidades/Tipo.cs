using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{

    public class Tipo_res
    {
        public DTOHeader oHeader { get; set; }
        public List<Tipo> TipoList { get; set; }
    }

    public class Tipo
    {
        public int id_tipo { get; set; }
        public string nombre { get; set; }
        public string unidad { get; set; }
    }
    public class Tipo_Register
    {
        public DTOHeader oHeader { get; set; }
        public int id_registrar { get; set; }
    }
}
