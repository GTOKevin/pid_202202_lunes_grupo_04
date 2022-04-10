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
    public class Sector_DA
    {
        //Listar
        public Sector_Res Listar()
        {
            Sector_Res sector_res = new Sector_Res();
            List<Sector> sector_list = new List<Sector>();
            DTOHeader oHeader = new DTOHeader();

            try
            {
                using(SqlConnection cn= Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cm = new SqlCommand("USP_LIST_SECTOR", cn);
                    SqlDataReader dr = cm.ExecuteReader();
                    while (dr.Read())
                    {
                        Sector sector = new Sector();
                        sector.id_sector = Convert.ToInt32(dr["id_sector"].ToString());
                        sector.nombre_sector = dr["nombre_sector"].ToString();
                        sector.fecha_creacion = Convert.ToDateTime(dr["fecha_creacion"].ToString());
                        sector.id_sucursal = Convert.ToInt32(dr["id_sucursal"].ToString());
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
            sector_res.SectorList = sector_list;
            sector_res.oHeader = oHeader;

            return sector_res;
        }

        
        public DTOHeader Registrar(Sector s)
        {
            DTOHeader oHeader = new DTOHeader();
            try
            {
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cm = new SqlCommand("USP_INSERT_SECTOR", cn);
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.AddWithValue("@nombre_sector", s.nombre_sector);
                    cm.Parameters.AddWithValue("@fecha_creacion", s.fecha_creacion);
                    cm.Parameters.AddWithValue("@id_sucursal", s.id_sucursal);
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

        public DTOHeader Actualizar(Sector s)
        {
            DTOHeader oHeader = new DTOHeader();
            try
            {
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cm = new SqlCommand("USP_UPDATE_SECTOR", cn);
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.AddWithValue("@id_sector", s.id_sector);
                    cm.Parameters.AddWithValue("@nombre_sector", s.nombre_sector);
                    cm.Parameters.AddWithValue("@fecha_creacion", s.fecha_creacion);
                    cm.Parameters.AddWithValue("@id_sucursal", s.id_sucursal);

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
