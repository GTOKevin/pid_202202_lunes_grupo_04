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
        public Movimiento_Res Listar(int id_movimiento)
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
                    cm.Parameters.AddWithValue("@idmovi", id_movimiento);
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
        public Movimiento_Register Registrar(Movimiento movi)
        {
            Movimiento_Register movimiento = new Movimiento_Register();
            DTOHeader oHeader = new DTOHeader();
            int id_register = 0;
            try
            {
                int rpta = 0;
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cm = new SqlCommand("USP_INSERT_MOVIMIENTO", cn);
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.AddWithValue("@idmovi", movi.id_movimiento);
                    cm.Parameters.AddWithValue("@idpropietario", movi.id_propietario);
                    cm.Parameters.AddWithValue("@idtipo", movi.id_tipo);
                    rpta = Convert.ToInt32(cm.ExecuteScalar());
                    cn.Close();
                }
                id_register = rpta;
                oHeader.estado = true;

                if(movi.id_movimiento > 0)
                {
                    oHeader.mensaje = "Se actualizo el movimiento : " + movi.id_propietario;
                }
                else
                {
                    oHeader.mensaje = "Se registro el movimiento : " + movi.id_propietario;
                }
            }
            catch (Exception ex)
            {
                oHeader.estado = false;
                oHeader.mensaje = ex.Message;
            }
            movimiento.oHeader = oHeader;
            movimiento.id_register = id_register;
            return movimiento;
        }
         
    }
}
