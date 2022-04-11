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
    public class Servicio_DA
    {
        public Servicio_Res Listar()
        {
            Servicio_Res servicio_res = new Servicio_Res();
            List<Servicio> servicio_list = new List<Servicio>();
            DTOHeader oHeader = new DTOHeader();

            try
            {
                using(SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cm = new SqlCommand("USP_LIST_SERVICIO", cn);
                    SqlDataReader dr = cm.ExecuteReader();
                    while (dr.Read())
                    {
                        Servicio servicio = new Servicio();
                        servicio.id_servicio = dr["id_servicio"].ToInt();
                        servicio.id_tipo = Convert.ToInt32(dr["id_tipo"].ToString());
                        servicio.id_departamento = Convert.ToInt32(dr["id_departamento"].ToString());
                        servicio.nombre = dr["nombre"].ToString();
                        servicio.fecha_registro = Convert.ToDateTime(dr["fecha_registro"].ToString());
                        servicio_list.Add(servicio);
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
            servicio_res.ServicioList = servicio_list;
            servicio_res.oHeader = oHeader;

            return servicio_res;
        }

       
        public DTOHeader Registrar(Servicio s)
        {
            DTOHeader oHeader = new DTOHeader();
            try
            {
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cm = new SqlCommand("USP_INSERT_SERVICIO", cn);
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.AddWithValue("@id_tipo", s.id_tipo);
                    cm.Parameters.AddWithValue("@id_departamento", s.id_departamento);
                    cm.Parameters.AddWithValue("@nombre", s.nombre);
                    cm.Parameters.AddWithValue("@fecha_registro", s.fecha_registro);
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

        public DTOHeader Actualizar(Servicio s)
        {
            DTOHeader oHeader = new DTOHeader();
            try
            {
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cm = new SqlCommand("USP_UPDATE_SERVICIO", cn);
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.AddWithValue("@id_servicio", s.id_servicio);
                    cm.Parameters.AddWithValue("@id_tipo", s.id_tipo);
                    cm.Parameters.AddWithValue("@id_departamento", s.id_departamento);
                    cm.Parameters.AddWithValue("@nombre", s.nombre);
                    cm.Parameters.AddWithValue("@fecha_registro", s.fecha_registro);
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
