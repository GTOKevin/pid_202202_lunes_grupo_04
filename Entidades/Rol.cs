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
        public List<Rol> RolList { get; set; }

    }
    public class Rol
    {
        public int id_rol { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }

    }

    public class Rol_Register
    {
        public DTOHeader oHeader { get; set; }
        public int id_registrar { get; set; }
    }
}
