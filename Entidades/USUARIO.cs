using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Usuario_Res
    {
        public DTOHeader oHeader { get; set; }
        public List<Usuario> UsuarioList { get; set; }
    }

    public class Usuario
    {
        public int id_usuario { get; set; }
        public string username { get; set; }
        public string clave { get; set; }
        public DateTime fecha_registro { get; set; }
        public int id_rol { get; set; }
        public int id_perfil { get; set; }
        public bool estado { get; set; }

    }
}
