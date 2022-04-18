using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class Perfil_BS
    {
        public Perfil_res lista_Perfiles()
        {
            return new Perfil_DA().Listar_Perfil();
        }
        public Perfil_res Listar_Perfil_Usuario(int id_perfil)
        {
            return new Perfil_DA().Listar_Perfil_Usuario(id_perfil);
        }

        public Perfil_Register Registrar_Perfil(Perfil perfil)
        {
            return new Perfil_DA().Registrar_Perfil(perfil);
        }

        public Perfil_Register Editar_MiPerfil(Perfil perfil)
        {
            return new Perfil_DA().Editar_MiPerfil(perfil);
        }

        public Perfil_res Eliminar_Perfil(int id)
        {
            return new Perfil_DA().Eliminar_Perfil(id);
        }
    }
}
