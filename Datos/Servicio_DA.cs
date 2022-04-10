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
                    SqlCommand cm = new SqlCommand("select * from SERVICIO", cn);
                    SqlDataReader dr = cm.ExecuteReader();
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
    }
}
