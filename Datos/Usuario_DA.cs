using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Entidades;
using Helpers;

namespace Datos
{
    public class Usuario_DA
    {
        public Usuario_General_Res Listar(int id)
        {
            Usuario_General_Res usuario_res = new Usuario_General_Res();
            List<UsuarioGeneral> usuario_list = new List<UsuarioGeneral>();
            DTOHeader oHeader = new DTOHeader();
            try
            {
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cm = new SqlCommand("USP_LIST_USUARIO", cn);
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.AddWithValue("@id_usuario", id);
                    SqlDataReader dr = cm.ExecuteReader();
                    while (dr.Read())
                    {
                        UsuarioGeneral usuario = new UsuarioGeneral();
                        usuario.id_usuario  = Convert.ToInt32( dr["id_usuario"].ToString());
                        usuario.username = dr["username"].ToString();
                        usuario.fecha_registro =Convert.ToDateTime(dr["fecha_registro"].ToString());
                        usuario.nombre_rol = dr["nombre_rol"].ToString();
                        usuario.id_perfil = Convert.ToInt32(dr["id_perfil"].ToString());
                        usuario.id_rol = (Roles)dr["id_rol"];
                        usuario.id_estado = Convert.ToInt32(dr["id_estado"].ToString());
                        usuario.nombre_perfil = dr["nombre_perfil"].ToString();
                        usuario.nombre_estado = dr["nombre_estado"].ToString();

                        usuario_list.Add(usuario);
                    }
                    cn.Close();
                }
                oHeader.estado = true;

            }
            catch (Exception ex)
            {
                oHeader.estado = false;
                oHeader.mensaje = ex.Message;
            }
            usuario_res.UsuarioList = usuario_list;
            usuario_res.oHeader = oHeader;

            return usuario_res;
        }

        public DTOHeader Registrar(Usuario usu)
        {
            DTOHeader oHeader = new DTOHeader();
            usu.clave = EncryptMD5.Encrypt(usu.clave);
            try
            {
                int rpta;
                    using (SqlConnection cn = Conexion.Conectar())
                    {
                        cn.Open();
                        SqlCommand cm = new SqlCommand("USP_INSERT_USUARIO", cn);
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.Parameters.AddWithValue("@username", usu.username);
                        cm.Parameters.AddWithValue("@clave", usu.clave);
                        rpta= cm.ExecuteNonQuery();
                        cn.Close();
                    }
                    if (rpta>0)
                    {
                        oHeader.estado = true;
                    }
                    else
                    {
                        oHeader.estado = false;
                        oHeader.mensaje = "No pudo registrarse, contacte con un administrador";
                    }
                
            }
            catch (Exception ex)
            {
                oHeader.estado = false;
                oHeader.mensaje = ex.Message;
            }
            return oHeader;
        }

        public Usuario_Register Registrar_Usuario(Usuario usu)
        {
            Usuario_Register ur = new Usuario_Register();
            DTOHeader oHeader = new DTOHeader();
            int id_register = 0;
            usu.clave = EncryptMD5.Encrypt(usu.clave);
            try
            {
                int rpta = 0;
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("SP_USER_REGISTER", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_usuario", usu.id_usuario);
                    cmd.Parameters.AddWithValue("@username", usu.username);
                    cmd.Parameters.AddWithValue("@clave", usu.clave);
                    cmd.Parameters.AddWithValue("@id_rol", usu.id_rol);
                    cmd.Parameters.AddWithValue("@id_estado", usu.id_estado);
                    rpta = Convert.ToInt32(cmd.ExecuteScalar());
                    cn.Close();

                }
                id_register = rpta;
                oHeader.estado = true;
                if (usu.id_usuario > 0)
                {
                    oHeader.mensaje = "Se actualizo el Usuario :" + usu.username;
                }
                else
                {
                    oHeader.mensaje = "Se registro el Usuario :" + usu.username;
                }


            }
            catch (Exception ex)
            {
                oHeader.estado = false;
                oHeader.mensaje = ex.Message;
            }

            ur.oHeader = oHeader;
            ur.id_register = id_register;
            return ur;
        }

        public Usuario_Register CambiarEstado_Us(Usuario usu)
        {
            Usuario_Register ur = new Usuario_Register();
            DTOHeader oHeader = new DTOHeader();
            int id_register = 0;
            try
            {
                int rpta = 0;
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("USP_EDIT_ESTADOUS", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_usuario", usu.id_usuario);
                    cmd.Parameters.AddWithValue("@id_estado", usu.id_estado);
                    rpta = Convert.ToInt32(cmd.ExecuteScalar());
                    cn.Close();

                }
                id_register = rpta; 
                if (usu.id_usuario > 0)
                {
                    oHeader.mensaje = "Se actualizo el Usuario :" + usu.username;
                    oHeader.estado = true;
                }
                else
                {
                    oHeader.mensaje = "Error al Cambiar Estado";
                    oHeader.estado = false;
                }


            }
            catch (Exception ex)
            {
                oHeader.estado = false;
                oHeader.mensaje = ex.Message;
            }

            ur.oHeader = oHeader;
            ur.id_register = id_register;
            return ur;
        }

        public Usuario_Register CambiarContraseña_Us(Usuario usu)
        {
            Usuario_Register ur = new Usuario_Register();
            DTOHeader oHeader = new DTOHeader();
            usu.clave = EncryptMD5.Encrypt(usu.clave);
            int id_register = 0;
            try
            {
                int rpta = 0;
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("USP_EDIT_CONTRASEÑA", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_usuario", usu.id_usuario);
                    cmd.Parameters.AddWithValue("@clave", usu.clave);
                    rpta = Convert.ToInt32(cmd.ExecuteScalar());
                    cn.Close();

                }
                id_register = rpta;
                if (usu.id_usuario > 0)
                {
                    oHeader.mensaje = "Se actualizo el Usuario :" + usu.username;
                    oHeader.estado = true;
                }
                else
                {
                    oHeader.mensaje = "Error al Cambiar Contraseña";
                    oHeader.estado = false;
                }


            }
            catch (Exception ex)
            {
                oHeader.estado = false;
                oHeader.mensaje = ex.Message;
            }

            ur.oHeader = oHeader;
            ur.id_register = id_register;
            return ur;
        }




        public Usuario_Res ValidarUsuarioLogin(string userName,string clave)
        {
            Usuario_Res usuarioRes = new Usuario_Res();
            DTOHeader oHeader = new DTOHeader();
            List<Usuario> usuarioList = new List<Usuario>();
            try
            {
                if (!String.IsNullOrEmpty(userName) && !String.IsNullOrEmpty(clave))
                {
                    using (SqlConnection cn = Conexion.Conectar())
                    {
                        cn.Open();
                        SqlCommand cmd = new SqlCommand("SP_USUARIO_OBTENER_LOGIN", cn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@userName", userName);
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            Usuario usuario = new Usuario();
                            usuario.id_usuario=dr["id_usuario"].ToInt();
                            usuario.username=dr["username"].ToString();
                            usuario.clave=dr["clave"].ToString();
                            usuario.fecha_registro = dr["fecha_registro"].ToDateTime();
                            usuario.id_rol = (Roles)dr["id_rol"];
                            usuario.id_perfil = dr["id_perfil"].ToInt();
                            usuario.id_estado = dr["id_estado"].ToInt();
                            usuarioList.Add(usuario);
                        }
                        cn.Close();
                    }

                    if (usuarioList.Count > 0)
                    {
                        var getClave = EncryptMD5.Decrypt(usuarioList.FirstOrDefault().clave);
                        var getUsuer = usuarioList.FirstOrDefault().username;
                        if(getUsuer==userName && getClave == clave)
                        {
                            oHeader.estado = true;
                            oHeader.mensaje = "Correcto";
                        }
                        else
                        {
                            oHeader.estado = false;
                            oHeader.mensaje = "Usuario o/u contraseña incorrecta";
                            usuarioList=new List<Usuario>();
                        }

                    }
                    else
                    {
                        oHeader.estado = false;
                        oHeader.mensaje = "Usuario o/u contraseña incorrecta";
                        usuarioList = new List<Usuario>();
                    }
                  

                }
                else
                {
                    oHeader.estado = false;
                    oHeader.mensaje = "Usuario o/u contraseña incorrecta";
                }
            }
            catch (Exception ex)
            {
                oHeader.estado= false;
                oHeader.mensaje= ex.Message;
            }

            usuarioRes.UsuarioList = usuarioList;
            usuarioRes.oHeader = oHeader;

            return usuarioRes;
         
        }

        public Usuario_Res ListarUsuarioPorIDPerfil(int id)
        {
            Usuario_Res pfr = new Usuario_Res();
            List<Usuario> flist = new List<Usuario>();
            DTOHeader oHeader = new DTOHeader();

            try
            {
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cm = new SqlCommand("USP_LISTAR_USUARIO_PORPERFIL", cn);
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.AddWithValue("@id_perfil", id);
                    SqlDataReader dr = cm.ExecuteReader();
                    while (dr.Read())
                    {
                        Usuario usuario = new Usuario();
                        usuario.id_usuario = dr["id_usuario"].ToInt();
                        usuario.username = dr["username"].ToString();
                        usuario.fecha_registro = dr["fecha_registro"].ToDateTime();
                        usuario.id_rol = (Roles)dr["id_rol"];
                        usuario.id_perfil = dr["id_perfil"].ToInt();
                        usuario.id_estado = dr["id_estado"].ToInt();
                        flist.Add(usuario);
                    }
                    cn.Close();
                }
                oHeader.estado = true;
            }
            catch (Exception ex)
            {
                oHeader.estado = false;
                oHeader.mensaje = ex.Message;
            }
            pfr.UsuarioList = flist;
            pfr.oHeader = oHeader;

            return pfr;
        }
    }
}
