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
        public Servicio_Res Listar(int id_servicio)
        {
            Servicio_Res servicio_res = new Servicio_Res();
            List<Servicio> servicio_list = new List<Servicio>();
            DTOHeader oHeader = new DTOHeader();

            try
            {
                using(SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("SP_SERVICIO_LISTAR", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_servicio", id_servicio);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Servicio servicio = new Servicio();
                        servicio.id_servicio = dr["id_servicio"].ToInt();
                        servicio.id_tipo = dr["id_tipo"].ToInt();
                        servicio.id_departamento = dr["id_departamento"].ToInt();
                        servicio.nombre = dr["nombre"].ToString();
                        servicio.fecha_registro = dr["fecha_registro"].ToDateTime();
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


        public Servicio_Register Registrar(Servicio Enti)
        {
            Servicio_Register Servicio = new Servicio_Register();
            DTOHeader oHeader = new DTOHeader();
            int id_register = 0;
            try
            {
                int rpta = 0;
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("SP_SERVICIO_REGISTER", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_servicio", Enti.id_servicio);
                    cmd.Parameters.AddWithValue("@id_tipo", Enti.id_tipo);
                    cmd.Parameters.AddWithValue("@id_departamento", Enti.id_departamento);
                    cmd.Parameters.AddWithValue("@nombre", Enti.nombre);
                    rpta = Convert.ToInt32(cmd.ExecuteScalar());
                    cn.Close();
                }
                id_register = rpta;
                oHeader.estado = true;
                if (Enti.id_servicio > 0)
                {
                    oHeader.mensaje = "Se actualizo el servicio :" + Enti.nombre;
                }
                else
                {
                    oHeader.mensaje = "Se registro el servicio :" + Enti.nombre;
                }
            }
            catch (Exception ex)
            {
                oHeader.estado = false;
                oHeader.mensaje = ex.Message;
            }
            Servicio.oHeader = oHeader;
            Servicio.id_register = id_register;
            return Servicio;
        }      
    }
}
