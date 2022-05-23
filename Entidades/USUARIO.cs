using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helpers;

namespace Entidades
{
    public class Usuario_Login_Res
    {
        public DTOHeader oHeader { get; set; }
        public Usuario_Login Usuario_Perfil { get; set; }
    }
    public class Usuario_Login
    {
        public Usuario usuario { get; set; }
        public Perfil perfil { get; set; }
        public Perfil_File file { get; set; }
    }
    public class Usuario_Res
    {
        public DTOHeader oHeader { get; set; }
        public List<Usuario> UsuarioList { get; set; }
    }
    public class Usuario_General_Res
    {
        public DTOHeader oHeader { get; set; }
        public List<UsuarioGeneral> UsuarioList { get; set; }
    }

    public class UsuarioGeneral
    {
        public int id_usuario { get; set; }
        public string username { get; set; }
        public DateTime? fecha_registro { get; set; }
        public string nombre_rol { get; set; }
        public int id_perfil { get; set; }
        public Roles id_rol { get; set; }
        public int id_estado { get; set; }
        public string nombre_perfil { get; set; }
        public string nombre_estado { get; set; }
    }

    public class Usuario
    {
        public int id_usuario { get; set; }
        public string username { get; set; }
        public string clave { get; set; }
        public DateTime? fecha_registro { get; set; }
        public Roles id_rol { get; set; }
        public int id_perfil { get; set; }
        public int id_estado{ get; set; }
       
    }

    public class SetRolUser
    {
        public int id_usuario { get; set; }
        public int id_rol { get; set; }
    }

    public class Usuario_Register
    {
        public DTOHeader oHeader { get; set; }
        public int id_register { get; set; }
    }
}
