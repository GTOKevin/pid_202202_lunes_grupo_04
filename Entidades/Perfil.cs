using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{

    public class Perfil_res
    {
        public DTOHeader oHeader { set; get; }
        public List<Perfil> Lista_Perfiles { set; get; }
    }

    public class Perfil_res_Us
    {
        public DTOHeader oHeader { set; get; }
        public List<Usuario> ListaUsuarioP { set; get; }
    }

    public class Perfil_res_UsG
    {
        public DTOHeader oHeader { set; get; }
        public List<UsuarioGeneral> ListaUsuarioP { set; get; }
    }

    public class Perfil
    {
        public int id_perfil { get; set; }
        public string nombres { get; set; }
        public string primer_apellido { get; set; }
        public string segundo_apellido { get; set; }
        public DateTime? fecha_nacimiento { get; set; }
        public string tipo_documento { get; set; }
        public string nro_documento { get; set; }
        public string genero { get; set; }
        public string nacionalidad { get; set; }
        public string direccion { get; set; }
    }

    public class Perfil_Register
    {
        public DTOHeader oHeader { get; set; }
        public int id_register { get; set; }
    }
}
