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
        public Usuario_Res Listar()
        {
            Usuario_Res usuario_res = new Usuario_Res();
            List<Usuario> usuario_list = new List<Usuario>();
            DTOHeader oHeader = new DTOHeader();
            try
            {
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cm = new SqlCommand("USP_LIST_USUARIO", cn);
                    cm.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = cm.ExecuteReader();
                    while (dr.Read())
                    {
                        Usuario usuario = new Usuario();
                        usuario.id_usuario  = Convert.ToInt32( dr["id_usuario"].ToString());
                        usuario.username = dr["username"].ToString();
                        usuario.clave = dr["clave"].ToString();
                        usuario.fecha_registro =Convert.ToDateTime(dr["fecha_registro"].ToString());
                        usuario.id_rol = (Roles)dr["id_rol"];
                        usuario.id_perfil =Convert.ToInt32( dr["id_perfil"].ToString());
                        usuario.id_estado = dr["id_estado"].ToInt();

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
        public DTOHeader Actualizar(Usuario usu)
        {
            DTOHeader oHeader = new DTOHeader();
            try
            {
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cm = new SqlCommand("USP_UPDATE_USUARIO", cn);
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.AddWithValue("@id", usu.id_usuario);
                    cm.Parameters.AddWithValue("@username", usu.username);
                    cm.Parameters.AddWithValue("@clave", usu.clave);
                    cm.Parameters.AddWithValue("@idrol", usu.id_rol);
                    cm.Parameters.AddWithValue("@estado", usu.id_estado);

                    cm.ExecuteNonQuery();
                    cn.Close();
                }
                oHeader.estado = true;
            }
            catch (Exception ex)
            {
                oHeader.estado = false;
                oHeader.mensaje = ex.Message;
            }
            return oHeader;
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
    }
}
