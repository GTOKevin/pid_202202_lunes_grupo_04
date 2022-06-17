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
    public class Incidente_DA
    {
        public Incidente_Res Listar()
        {
            Incidente_Res incidente_res = new Incidente_Res();
            List<Incidente> incidente_list = new List<Incidente>();
            DTOHeader oHeader = new DTOHeader();
            try
            {
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cm = new SqlCommand("USP_INCIDENTE_LISTAR", cn);
                    cm.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = cm.ExecuteReader();
                    while (dr.Read())
                    {
                        Incidente incidente = new Incidente();
                        incidente.id_incidente = dr["id_incidente"].ToInt();
                        incidente.fecha_incidente = dr["fecha_incidente"].ToDateTime();
                        incidente.descripcion = dr["descripcion"].ToString();
                        incidente.nombre_reportado = dr["nombre_reportado"].ToString();
                        incidente.tipo_documento = dr["tipo_documento"].ToString();
                        incidente.nro_documento = dr["nro_documento"].ToString();
                        incidente.fecha_registro = dr["fecha_registro"].ToDateTime();
                        incidente.id_departamento = dr["id_departamento"].ToInt();
                        incidente.id_usuario = dr["id_usuario"].ToInt();
                        incidente_list.Add(incidente);
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
            incidente_res.lista_Incidente = incidente_list;
            incidente_res.oHeader = oHeader;

            return incidente_res;
        }
        public DTOHeader Registrar(Incidente inc)
        {

            DTOHeader oHeader = new DTOHeader();
            try
            {
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cm = new SqlCommand("USP_INCIDENTE_CREAR", cn);
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.AddWithValue("@descripcion", inc.descripcion);
                    cm.Parameters.AddWithValue("@nombre_reportado", inc.nombre_reportado);
                    cm.Parameters.AddWithValue("@tipodocumento", inc.tipo_documento);
                    cm.Parameters.AddWithValue("@nro_documento", inc.nro_documento);
                    cm.Parameters.AddWithValue("@id_departamento", inc.id_departamento);
                    cm.Parameters.AddWithValue("@idusuario", inc.id_usuario);

                    SqlDataReader dr = cm.ExecuteReader();

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
        public DTOHeader Actualizar(Incidente inc)
        {
            DTOHeader oHeader = new DTOHeader();
            try
            {
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cm = new SqlCommand("USP_INCIDENTE_ACTUALIZAR", cn);
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.AddWithValue("@id_incidente", inc.id_incidente);
                    cm.Parameters.AddWithValue("@fecha_incidente", inc.fecha_incidente);
                    cm.Parameters.AddWithValue("@descripcion", inc.descripcion);
                    cm.Parameters.AddWithValue("@nombre_reportado", inc.nombre_reportado);
                    cm.Parameters.AddWithValue("@tipo_documento", inc.tipo_documento);
                    cm.Parameters.AddWithValue("@nro_documento", inc.nro_documento);
                    cm.Parameters.AddWithValue("@id_departamento", inc.id_departamento);
                    cm.Parameters.AddWithValue("@id_usuario", inc.id_usuario);
                    
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
    }
}
