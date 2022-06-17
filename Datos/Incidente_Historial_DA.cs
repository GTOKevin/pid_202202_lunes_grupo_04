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
   public class Incidente_Historial_DA
    {
        public Incidente_Historial_Res Listar(int id_incidente_historial)
        {
            Incidente_Historial_Res inci_historial_res = new Incidente_Historial_Res();
            List<Incidente_Historial> inci_historial_list = new List<Incidente_Historial>();
            DTOHeader oHeader = new DTOHeader();
            try
            {
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cm = new SqlCommand("USP_INCIDENTE_HISTORIAL_LISTAR", cn);
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.AddWithValue("@id_incidente_historial", id_incidente_historial);
                    SqlDataReader dr = cm.ExecuteReader();
                    while (dr.Read())
                    {
                        Incidente_Historial inci_historial = new Incidente_Historial();
                        inci_historial.id_incidente_historial = Convert.ToInt32(dr["id_incidente_historial"].ToString());
                        inci_historial.acciones = dr["acciones"].ToString();
                        inci_historial.fecha_historial = Convert.ToDateTime(dr["fecha_historial"]);
                        inci_historial.id_incidente = Convert.ToInt32(dr["id_incidente"].ToString());

                        inci_historial_list.Add(inci_historial);
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
            inci_historial_res.lista_Incidente_Historial = inci_historial_list;
            inci_historial_res.oHeader = oHeader;

            return inci_historial_res;
        }
        public DTOHeader Registrar(Incidente_Historial inci_historial)
        {
            DTOHeader oHeader = new DTOHeader();
            try
            {
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cm = new SqlCommand("USP_INCIDENTE_HISTORIAL_CREAR", cn);
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.AddWithValue("@acciones", inci_historial.acciones);
                    cm.Parameters.AddWithValue("@idincidente", inci_historial.id_incidente);
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
    }
}
