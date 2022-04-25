using Entidades;
using Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class Tipo_DA
    {
        public Tipo_res Listar_Tipos(int id_tipo)
        {
            Tipo_res tipo_res = new Tipo_res();
            List<Tipo> tipo_list = new List<Tipo>();
            DTOHeader oHeader = new DTOHeader();
            try
            {
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cm = new SqlCommand("SP_TIPO_LISTAR", cn);
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.AddWithValue("@id_tipo", id_tipo);
                    SqlDataReader dr = cm.ExecuteReader();
                    while (dr.Read())
                    {
                        Tipo t = new Tipo();
                        t.id_tipo = dr["id_tipo"].ToInt();
                        t.nombre = dr["nombre"].ToString();
                        t.unidad = dr["unidad"].ToString();
                        tipo_list.Add(t);
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
            tipo_res.TipoList = tipo_list;
            tipo_res.oHeader = oHeader;

            return tipo_res;
        }


        public Tipo_Register Registrar_Tipo(Tipo tipo)
        {
            Tipo_Register Tipo = new Tipo_Register();
            DTOHeader oHeader = new DTOHeader();
            int id_registrar = 0;

            try
            {
                int rpta = 0;
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cm = new SqlCommand("SP_CREAR_TIPO", cn);
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.AddWithValue("@id_tipo", tipo.id_tipo);
                    cm.Parameters.AddWithValue("@nombre", tipo.nombre);
                    cm.Parameters.AddWithValue("@unidad", tipo.unidad);
                    rpta = Convert.ToInt32(cm.ExecuteScalar());
                    cn.Close();
                }

                id_registrar = rpta;
                oHeader.estado = true;
                if (tipo.id_tipo > 0)
                {
                    oHeader.mensaje = "Se actualizo el tipo :" + tipo.nombre;
                }
                else
                {
                    oHeader.mensaje = "Se registro el tipo :" + tipo.nombre;
                }

            }
            catch (Exception ex)
            {
                oHeader.estado = false;
                oHeader.mensaje = ex.Message;
            }

            Tipo.oHeader = oHeader;
            Tipo.id_registrar = id_registrar;
            return Tipo;
        }

       /* public Tipo_res Actualizar_Tipo(int id, Tipo tipo)
        {
            Tipo_res tr = new Tipo_res();
            DTOHeader oHeader = new DTOHeader();

            try
            {
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cm = new SqlCommand("USP_EDITAR_TIPO", cn);
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.AddWithValue("@id_tipo", id);
                    cm.Parameters.AddWithValue("@nombre", tipo.nombre);
                    cm.Parameters.AddWithValue("@unidad", tipo.unidad);
                    cm.ExecuteNonQuery();
                    cn.Close();
                }

                oHeader.estado = true;
                oHeader.mensaje = "SE EDITO CORRECTAMENTE";

            }
            catch (Exception ex)
            {
                oHeader.estado = false;
                oHeader.mensaje = ex.Message;
            }

            
            tr.oHeader = oHeader;
            
            return tr;
        }*/

        public Tipo_res Eliminar_Tipo(int id)
        {
            Tipo_res tr = new Tipo_res();
            DTOHeader oHeader = new DTOHeader();

            try
            {
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cm = new SqlCommand("USP_ELIMINAR_TIPO", cn);
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.AddWithValue("@id_tipo", id);
                    cm.ExecuteNonQuery();
                    cn.Close();
                }

                oHeader.estado = true;
                oHeader.mensaje = "SE ELIMINO CORRECTAMENTE";

            }
            catch (Exception ex)
            {
                oHeader.estado = false;
                oHeader.mensaje = ex.Message;
            }


            tr.oHeader = oHeader;

            return tr;
        }

        public Tipo_res Listar_TipoUtil(string unidad)
        {
            Tipo_res tipo_res = new Tipo_res();
            List<Tipo> tipo_list = new List<Tipo>();
            DTOHeader oHeader = new DTOHeader();
            try
            {
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cm = new SqlCommand("USP_LISTAR_TIPO_PORUNIDAD", cn);
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.AddWithValue("@unidad", unidad);
                    SqlDataReader dr = cm.ExecuteReader();
                    while (dr.Read())
                    {
                        Tipo t = new Tipo();
                        t.id_tipo = dr["id_tipo"].ToInt();
                        t.nombre = dr["nombre"].ToString();
                        t.unidad = dr["unidad"].ToString();
                        tipo_list.Add(t);
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
            tipo_res.TipoList = tipo_list;
            tipo_res.oHeader = oHeader;

            return tipo_res;
        }

    }
}
