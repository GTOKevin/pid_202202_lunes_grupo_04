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
        public Recibo_Res Listar(int id_recibo)
        {
            Recibo_Res recibo_res = new Recibo_Res();
            List<Recibo> recibo_list = new List<Recibo>();
            DTOHeader oHeader = new DTOHeader();
            try
            {
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("SP_RECIBO_LISTAR", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_recibo", id_recibo);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Recibo recibo = new Recibo();
                        recibo.id_recibo = dr["id_recibo"].ToInt();
                        recibo.id_servicio = dr["id_servicio"].ToInt();
                        recibo.monto = dr["monto"].ToDecimal();
                        recibo.estado = dr["estado"].ToBool();
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
            recibo_res.ReciboList = recibo_list;
            recibo_res.oHeader = oHeader;
           
            return recibo_res;
        }

        public Recibo_Register Registrar(Recibo Enti)
        {
            Recibo_Register recibo_Register = new Recibo_Register();
            DTOHeader oHeader = new DTOHeader();
            int id_register = 0;
            try
            {
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("SP_RECIBO_REGISTER", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_recibo", Enti.id_recibo);
                    cmd.Parameters.AddWithValue("@id_servicio", Enti.id_servicio);
                    cmd.Parameters.AddWithValue("@monto", Enti.monto);
                    cmd.Parameters.AddWithValue("@estado", Enti.estado);
                    cmd.Parameters.AddWithValue("@fecha_pago", Enti.fecha_pago);
                    cmd.Parameters.AddWithValue("@fecha_vencimiento", Enti.fecha_vencimiento);
                    id_register = cmd.ExecuteScalar().ToInt();
                    cn.Close();

                }

                oHeader.estado = true;
                if (Enti.id_recibo == 0)
                {
                    oHeader.mensaje = "Se ha registrado el recibo";
                }
                else
                {
                    oHeader.mensaje = "Se ha actualizado el recibo";
                }


            }
            catch (Exception ex)
            {
                oHeader.estado = false;
                oHeader.mensaje = ex.Message;
            }
            recibo_Register.oHeader = oHeader;
            recibo_Register.id_register = id_register;
            return recibo_Register;
        }
        public Recibo_Servicio_Res Listar_servicio(int id)
        {
            Recibo_Servicio_Res recibo_res = new Recibo_Servicio_Res();
            List<Recibo_Servicio> recibo_list = new List<Recibo_Servicio>();
            DTOHeader oHeader = new DTOHeader();

            try
            {
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cm = new SqlCommand("USP_RECIBO_LISTAR_SERVICIO", cn);
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.AddWithValue("@id_recibo", id);
                    SqlDataReader dr = cm.ExecuteReader();
                    while (dr.Read())
                    {
                        Recibo_Servicio recibo = new Recibo_Servicio();
                        recibo.id_recibo = dr["id_recibo"].ToInt();
                        recibo.id_servicio = dr["id_servicio"].ToInt();
                        recibo.nombre_servicio = dr["nombre_servicio"].ToString();
                        recibo.monto = dr["monto"].ToDecimal();
                        recibo.estado = dr["estado"].ToBool();
                        recibo.fecha_pago = dr["fecha_pago"].ToDateTime();
                        recibo.fecha_vencimiento = dr["fecha_vencimiento"].ToDateTime();
                        recibo.fecha_registro = dr["fecha_registro"].ToDateTime();
                        recibo_list.Add(recibo);
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
            recibo_res.ReciboList = recibo_list;
            recibo_res.oHeader = oHeader;

            return recibo_res;
        }
    }
}

