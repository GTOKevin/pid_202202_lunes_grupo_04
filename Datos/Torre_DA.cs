using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Helpers;
using System.Data.SqlClient;
using System.Data;


namespace Datos
{
    public class Torre_DA
    {
        public Torre_Res Listar(int id)
        {
            Torre_Res torre_res = new Torre_Res();
            List<Torre> torre_list = new List<Torre>();
            DTOHeader oHeader = new DTOHeader();

            try
            {
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cm = new SqlCommand("USP_TORRE_LISTAR", cn);
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.AddWithValue("@id_torre", id);
                    SqlDataReader dr = cm.ExecuteReader();
                    while (dr.Read())
                    {
                        Torre torre = new Torre();
                        torre.id_torre = dr["id_torre"].ToInt();
                        torre.numero = dr["numero"].ToDecimal();
                        torre.fecha_creacion = dr["fecha_creacion"].ToDateTime();
                        torre.id_sector = dr["id_sector"].ToInt();
                        torre.nombre_sector = dr["nombre_sector"].ToString();
                        torre.nombre_sucursal = dr["nombre_sucursal"].ToString();
                        torre.id_sucursal = dr["id_sucursal"].ToInt();
                        torre_list.Add(torre);
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
            torre_res.TorreList = torre_list;
            torre_res.oHeader = oHeader;

            return torre_res;
        }

        public Torres_Register Registrar(Torre s)
        {

            Torres_Register torres_Register = new Torres_Register();
            DTOHeader oHeader = new DTOHeader();
            int id_register = 0;
            try
            {
                using (SqlConnection cn = Conexion.Conectar())
                {

                    cn.Open();
                    SqlCommand cm = new SqlCommand("USP_TORRE_REGISTRAR", cn);
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.AddWithValue("@id_torre", s.id_torre);
                    cm.Parameters.AddWithValue("@numero", s.numero);
                    cm.Parameters.AddWithValue("@id_sector", s.id_sector);
                    id_register = cm.ExecuteScalar().ToInt();
                    cn.Close();
                }
                oHeader.estado = true;
                if (s.id_sector == 0)
                {
                    oHeader.mensaje = "Se ha registrado una torre";
                }
                else
                {
                    oHeader.mensaje = "Se ha actualizado una torre";
                }
            }
            catch (Exception ex)
            {
                oHeader.estado = false;
                oHeader.mensaje = ex.Message;
            }
            torres_Register.oHeader = oHeader;
            torres_Register.id_register = id_register;
            return torres_Register;
        }
    }
}
