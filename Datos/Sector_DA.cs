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

        public Sector_Res Listar(int id)
        {
            Sector_Res sector_res = new Sector_Res();
            List<Sector> sector_list = new List<Sector>();
            DTOHeader oHeader = new DTOHeader();

            try
            {
                using(SqlConnection cn= Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cm = new SqlCommand("USP_SECTOR_LISTAR", cn);
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.AddWithValue("@id_sector", id);
                    SqlDataReader dr = cm.ExecuteReader();
                    while (dr.Read())
                    {
                        Sector sector = new Sector();
                        sector.id_sector = dr["id_sector"].ToInt();
                        sector.nombre_sector = dr["nombre_sector"].ToString();
                        sector.fecha_creacion = dr["fecha_creacion"].ToDateTime() ;
                        sector.id_sucursal = dr["id_sucursal"].ToInt();
                        sector_list.Add(sector);
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

        
        public Sector_Register Registrar(Sector s)
        {

            Sector_Register sector_Register = new Sector_Register();
            DTOHeader oHeader = new DTOHeader();
            int id_register = 0;
            try
            {
                using (SqlConnection cn = Conexion.Conectar())
                {   
                    
                    cn.Open();
                    SqlCommand cm = new SqlCommand("USP_SECTOR_REGISTRAR", cn);
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.AddWithValue("@id_sector", s.id_sector);
                    cm.Parameters.AddWithValue("@nombre_sector", s.nombre_sector);
                    cm.Parameters.AddWithValue("@id_sucursal", s.id_sucursal);
                    id_register = cm.ExecuteScalar().ToInt();
                    cn.Close();
                }
                oHeader.estado = true;
                if (s.id_sector == 0)
                {
                    oHeader.mensaje = "Se ha registrado un sector";
                }
                else
                {
                    oHeader.mensaje = "Se ha actualizado un sector";
                }
            }
            catch (Exception ex)
            {
                oHeader.estado = false;
                oHeader.mensaje = ex.Message;
            }
            sector_Register.oHeader= oHeader;
            sector_Register.id_register = id_register;
            return sector_Register;
        }


        public Sector_Suc_Res Listar_suc(int id)
        {
            Sector_Suc_Res sector_res = new Sector_Suc_Res();
            List<Sector_Suc> sector_list = new List<Sector_Suc>();
            DTOHeader oHeader = new DTOHeader();

            try
            {
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cm = new SqlCommand("USP_SECTOR_LISTAR_SUC", cn);
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.AddWithValue("@id_sector", id);
                    SqlDataReader dr = cm.ExecuteReader();
                    while (dr.Read())
                    {
                        Sector_Suc sector = new Sector_Suc();
                        sector.id_sector = dr["id_sector"].ToInt();
                        sector.nombre_sector = dr["nombre_sector"].ToString();
                        sector.fecha_creacion = dr["fecha_creacion"].ToDateTime();
                        sector.id_sucursal = dr["id_sucursal"].ToInt();
                        sector.nombre_sucursal = dr["nombre_sucursal"].ToString();
                        sector_list.Add(sector);
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
