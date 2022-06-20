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
                        recibo.id_sucursal = dr["id_sucursal"].ToInt();
                        recibo.nombre_sucursal = dr["nombre_sucursal"].ToString();
                        recibo.id_sector = dr["id_sector"].ToInt();
                        recibo.nombre_sector = dr["nombre_sector"].ToString();
                        recibo.id_torre = dr["id_torre"].ToInt();
                        recibo.numero_torre = dr["numero_torre"].ToInt();
                        recibo.id_departamento = dr["id_departamento"].ToInt();
                        recibo.numero_departamento = dr["numero_departamento"].ToInt();
                        //recibo.id_servicio = dr["id_servicio"].ToInt();
                        recibo.nombre_servicio = dr["nombre_servicio"].ToString();
                        recibo.monto = dr["monto"].ToDecimal();
                        recibo.fecha_pago = dr["fecha_pago"].ToDateTime();
                        //recibo.fecha_vencimiento = dr["fecha_vencimiento"].ToDateTime();
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

        public Recibo_Register Registrar(string servicio, int id_departamento,decimal monto=0,DateTime? fecha_pago = null)
        {
            Recibo_Register recibo_Register = new Recibo_Register();
            DTOHeader oHeader = new DTOHeader();
            int id_register = 0;
            try
            {
                int rpta = 0;
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("SP_RECIBO_REGISTER", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@monto", monto);
                    cmd.Parameters.AddWithValue("@id_departamento", id_departamento);
                    cmd.Parameters.AddWithValue("@servicio", servicio);
                    cmd.Parameters.AddWithValue("@fecha_pago", fecha_pago);
                    rpta = Convert.ToInt32(cmd.ExecuteScalar());
                    cn.Close();

                }
                id_register = rpta;
                if (id_register > 0)
                {

                    oHeader.estado = true;
                    oHeader.mensaje = "Se ha generado los recibos";
                }
                else
                {
                    oHeader.estado = false;
                    id_register = rpta;
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




        public Recibo_Res Listar_N()
        {
            Propietario_DA da_prop = new Propietario_DA();

            var respProp = da_prop.ListarDuenios();

            var duenios = respProp.lista_Propietario;

            Recibo_Res recibo_res = new Recibo_Res();
            List<Recibo> recibo_list = new List<Recibo>();
            DTOHeader oHeader = new DTOHeader();
            try
            {
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("SP_RECIBO_LISTAR_N", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Recibo recibo = new Recibo();
                        recibo.id_recibo = dr["id_recibo"].ToInt();
                        recibo.monto = dr["monto"].ToDecimal();
                        recibo.estado = dr["estado"].ToBool();
                        recibo.id_departamento = dr["id_departamento"].ToInt();
                        recibo.fecha_pago = dr["fecha_pago"].ToDateTime();
                        recibo.servicio =dr["servicio"].ToString();
                        recibo.oPropietario= duenios.Where(x=>x.id_departamento==recibo.id_departamento).FirstOrDefault();

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




        public Recibo_Res Listar_Filtro(string dni="", string nombre="", string servicio= "", int estado=0)
        {
            Propietario_DA da_prop = new Propietario_DA();

            var respProp = da_prop.ListarDuenios();

            var duenios = respProp.lista_Propietario;

            Recibo_Res recibo_res = new Recibo_Res();
            List<Recibo> recibo_list = new List<Recibo>();
            DTOHeader oHeader = new DTOHeader();
            try
            {
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("sp_recibo_listar_filtro", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@dni", dni);
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@servicio", servicio);
                    cmd.Parameters.AddWithValue("@estado", estado);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Recibo recibo = new Recibo();
                        recibo.id_recibo = dr["id_recibo"].ToInt();
                        recibo.monto = dr["monto"].ToDecimal();
                        recibo.estado = dr["estado"].ToBool();
                        recibo.id_departamento = dr["id_departamento"].ToInt();
                        recibo.fecha_pago = dr["fecha_pago"].ToDateTime();
                        recibo.servicio = dr["servicio"].ToString();
                        recibo.oPropietario = duenios.Where(x => x.id_departamento == recibo.id_departamento).FirstOrDefault();

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

        public Recibo_Res Pagar_Recibo(int id_recibo=0, int estado = 0)
        {
            Recibo_Res recibo_Res = new Recibo_Res();
            DTOHeader oHeader = new DTOHeader();
            int id_register = 0;
            try
            {
                int rpta = 0;
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("sp_recibo_pagar", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_recibo", id_recibo);
                    cmd.Parameters.AddWithValue("@estado", estado);
                    rpta = Convert.ToInt32(cmd.ExecuteScalar());
                    cn.Close();

                }
                id_register = rpta;
                oHeader.estado = true;
                if (id_register > 0)
                {
                    oHeader.mensaje = "Se ha pagado el recibo "+ id_recibo.ToString();
                }



            }
            catch (Exception ex)
            {
                oHeader.estado = false;
                oHeader.mensaje = ex.Message;
            }
            recibo_Res.oHeader = oHeader;
            recibo_Res.ReciboList =new List<Recibo>();
            return recibo_Res;
        }

    }
}

