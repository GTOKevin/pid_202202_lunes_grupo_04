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
    public class Movimiento_DA
    {
        public Movimiento_Res Listar()
        {
            Movimiento_Res movimiento_res = new Movimiento_Res();
            List<Movimiento> movimiento_list = new List<Movimiento>();
            DTOHeader oHeader = new DTOHeader();
            try
            {
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cm = new SqlCommand("USP_LIST_MOVIMIENTO", cn);
                    cm.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = cm.ExecuteReader();
                    while (dr.Read())
                    {
                        Movimiento movi = new Movimiento();
                        movi.id_movimiento = dr["id_movimiento"].ToInt();
                        movi.id_propietario =Convert.ToInt32(dr["id_propietario"].ToString());
                        movi.id_tipo=Convert.ToInt32( dr["id_tipo"].ToString());
                        movi.fecha_registro = Convert.ToDateTime( dr["fecha_registro"].ToDateTime());
                        movimiento_list.Add(movi);
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
            movimiento_res.MovimientoList = movimiento_list;
            movimiento_res.oHeader = oHeader;

            return movimiento_res;
        }
        public DTOHeader Registrar(Movimiento movi)
        {
            DTOHeader oHeader = new DTOHeader();
            try
            {
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cm = new SqlCommand("USP_INSERT_MOVIMIENTO", cn);
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.AddWithValue("@idpropietario", movi.id_propietario);
                    cm.Parameters.AddWithValue("@idtipo", movi.id_tipo);
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
