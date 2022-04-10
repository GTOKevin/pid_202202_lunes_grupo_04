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
    public class IncidenteFile_DA
    {
        public IncidenteFile_Res Listar()
        {
            IncidenteFile_Res incidentefile_res = new IncidenteFile_Res();
            List<Incidente_File> incidentefile_list = new List<Incidente_File>();
            DTOHeader oHeader = new DTOHeader();
            try
            {
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cm = new SqlCommand("USP_LIST_INCIDENTE_FILE", cn);
                    cm.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = cm.ExecuteReader();
                    while (dr.Read())
                    {
                        Incidente_File incifile = new Incidente_File();
                        incifile.id_incidente_file = Convert.ToInt32(dr["id_incidente_file"].ToString());
                        incifile.fecha_registro = Convert.ToDateTime(dr["fecha_registro"].ToString());
                        incifile.url_imagen =dr["url_imagen"].ToString();
                        incifile.id_incidente= Convert.ToInt32(dr["id_incidente"].ToString());
                        incidentefile_list.Add(incifile);
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
            incidentefile_res.IncidenteFileList = incidentefile_list;
            incidentefile_res.oHeader = oHeader;

            return incidentefile_res;
        }
        public DTOHeader Registrar(Incidente_File inci)
        {
            DTOHeader oHeader = new DTOHeader();
            try
            {
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cm = new SqlCommand("USP_INSERT_INCIDENTE_FILE", cn);
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.AddWithValue("@imagen",inci.url_imagen);
                    cm.Parameters.AddWithValue("@idincidente", inci.id_incidente);
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
        public DTOHeader Actualizar(Incidente_File inci)
        {
            DTOHeader oHeader = new DTOHeader();
            try
            {
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cm = new SqlCommand("USP_UPDATE_INCIDENTE_FILE", cn);
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.AddWithValue("@id", inci.id_incidente_file);
                    cm.Parameters.AddWithValue("@imagen", inci.url_imagen);
                    cm.Parameters.AddWithValue("@idincidente", inci.id_incidente);
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
