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
        public Tipo_res Listar_Tipos()
        {
            Tipo_res tr = new Tipo_res();
            List<Tipo> list = new List<Tipo>();
            DTOHeader oHeader = new DTOHeader();
            try
            {
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cm = new SqlCommand("USP_LISTAR_TIPO", cn);
                    cm.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = cm.ExecuteReader();
                    while (dr.Read())
                    {
                        Tipo t = new Tipo();
                        t.id_tipo = dr["id_tipo"].ToInt();
                        t.nombre = dr["nombre"].ToString();
                        t.unidad = dr["unidad"].ToString();
                        list.Add(t);
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
            tr.Lista_Tipos = list;
            tr.oHeader = oHeader;

            return tr;
        }


        public Tipo_res Registrar_Tipo(Tipo tipo)
        {
            Tipo_res tr = new Tipo_res();
            DTOHeader oHeader = new DTOHeader();

            try
            {
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cm = new SqlCommand("USP_CREAR_TIPO", cn);
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.AddWithValue("@nombre", tipo.nombre);
                    cm.Parameters.AddWithValue("@unidad", tipo.unidad);
                    cm.ExecuteNonQuery();
                    cn.Close();
                }

                oHeader.estado = true;
                oHeader.mensaje = "SE REGISTRO CORRECTAMENTE";

            }
            catch (Exception ex)
            {
                oHeader.estado = false;
                oHeader.mensaje = ex.Message;
            }

            

            tr.oHeader = oHeader;
            

            return tr;
        }

        public Tipo_res Actualizar_Tipo(int id, Tipo tipo)
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
        }

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

    }
}
