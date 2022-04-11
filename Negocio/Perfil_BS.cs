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
        public Perfil_res Listar_Perfil_Usuario(int id_user)
        {
            return new Perfil_DA().Listar_Perfil_Usuario(id_user);
        }

        public Perfil_res Registrar_Perfil(Perfil perfil)
        {
            return new Perfil_DA().Registrar_Perfil(perfil);
        }

        public Perfil_res Actualizar_Perfil(int id,Perfil perfil)
        {
            return new Perfil_DA().Actualizar_Perfil(id,perfil);
        }

        public Perfil_res Eliminar_Perfil(int id)
        {
            return new Perfil_DA().Eliminar_Perfil(id);
        }
    }
}
