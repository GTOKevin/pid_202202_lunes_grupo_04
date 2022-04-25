using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;

namespace Negocio
{
    public class Usuario_BS
    {
        public Usuario_General_Res lista(int id)
        {
            return new Usuario_DA().Listar(id);
        }
        public DTOHeader Registrar(Usuario usu)
        {
            return new Usuario_DA().Registrar(usu);
        }
        public Usuario_Register Registrar_Usuario(Usuario usu)
        {
            return new Usuario_DA().Registrar_Usuario(usu);
        }
        public Usuario_Register CambiarEstado_Us(Usuario usu)
        {
            return new Usuario_DA().CambiarEstado_Us(usu);
        }
        public Usuario_Register CambiarContraseña_Us(Usuario usu)
        {
            return new Usuario_DA().CambiarContraseña_Us(usu);
        }
        public Usuario_Res ValidarUsuarioLogin(string userName, string clave)
        {
            return new Usuario_DA().ValidarUsuarioLogin(userName,clave);
        }
        public Usuario_Res ListarUsuarioPorIDPerfil(int id)
        {
            return new Usuario_DA().ListarUsuarioPorIDPerfil(id);
        }
    }
}
