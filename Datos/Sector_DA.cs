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
                    SqlCommand cm = new SqlCommand("select * from SECTOR", cn);
                    SqlDataReader dr = cm.ExecuteReader();
                    while (dr.Read())
                    {
                        Sector sector = new Sector();
                        sector.id_sector = dr["id_sector"].ToInt();
                        sector.nombre_sector = dr["nombre_sector"].ToString();
                        sector.fecha_creacion = dr["fecha_creacion"].ToDateTime();
                        sector.id_sucursal = dr["id_sucursal"].ToInt();
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
    }
}
