using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{

    public class Rol_Res
    {
        public DTOHeader oHeader { get; set; }
        public List<Rol> lista_rol { get; set; }

    }
    public class Rol
    {
        public int id_rol { get; set; }
        public string nombre { get; set; }
    }
}
