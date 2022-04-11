using Entidades;
using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Datos
{
    public class Recibo_DA
    {
        public Recibo_Res Listar()
        {
            Recibo_Res recibo_res = new Recibo_Res();
            List<Recibo> recibo_list = new List<Recibo>();
            DTOHeader oHeader = new DTOHeader();
            try
            {
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cm = new SqlCommand("USP_RECIBO_LISTAR", cn);
                    cm.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = cm.ExecuteReader();
                    while (dr.Read())
                    {
                        Recibo recibo = new Recibo();
                        recibo.id_recibo = dr["id_recibo"].ToInt();
                        recibo.id_servicio = dr["id_servicio"].ToInt();
                        recibo.monto = dr["monto"].ToDecimal();
                        recibo.estado = (byte)dr["estado"].ToInt();
                        recibo.fecha_pago = dr["fecha_pago"].ToDateTime();
                        recibo.fecha_vencimiento = dr["fecha_vencimiento"].ToDateTime();
                        recibo.fecha_registro = dr["fecha_registro"].ToDateTime();
                        recibo_list.Add(recibo);
                    }
                    cn.Close();
                }
                oHeader.estado = true;
            }catch (Exception ex)
            {
                oHeader.estado = false;
                oHeader.mensaje = ex.Message;
            }
            recibo_res.lista_Recibo = recibo_list;
            recibo_res.oHeader = oHeader;
           
            return recibo_res;
        }

    }
}
