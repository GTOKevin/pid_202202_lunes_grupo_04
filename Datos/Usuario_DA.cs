using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Entidades;

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
                    SqlDataReader dr = cm.ExecuteReader();
                    while (dr.Read())
                    {
                        Usuario usuario = new Usuario();
                        usuario.id_usuario  = Convert.ToInt32( dr["id_usuario"].ToString());
                        usuario.username = dr["username"].ToString();
                        usuario.clave = dr["clave"].ToString();
                        usuario.fecha_registro =Convert.ToDateTime(dr["fecha_registro"].ToString());
                        usuario.id_rol = Convert.ToInt32( dr["id_rol"].ToString());
                        usuario.id_perfil =Convert.ToInt32( dr["id_perfil"].ToString());
                        usuario.estado = Convert.ToBoolean(dr["estado"].ToString());

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
    }
}
