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
    public class VISITA_REGISTRO_DA
    {
        public VISITAREGISTRO_Res Listar(int id_visita_registro)
        {
            VISITAREGISTRO_Res visitareg_res = new VISITAREGISTRO_Res();
            List<VISITA_REGISTRO> visitareg_list = new List<VISITA_REGISTRO>();
            DTOHeader oHeader = new DTOHeader();
            try
            {
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("USP_LISTAR_VISITAREG", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_visita_registro", id_visita_registro);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                       VISITA_REGISTRO visita = new VISITA_REGISTRO();
                        visita.id_visita_registro = dr["id_visita_registro"].ToInt();
                        visita.fecha_ingreso = Convert.ToDateTime(dr["fecha_ingreso"].ToDateTime());
                        visita.fecha_salida = Convert.ToDateTime(dr["fecha_salida"].ToDateTime());
                        visita.nombre_sucursal = dr["nombre_sucursal"].ToString();
                        visita.nombre_sector = dr["nombre_sector"].ToString();
                        visita.numero_torre = dr["numero_torre"].ToInt();
                        visita.id_departamento = dr["id_departamento"].ToInt();
                        visita.numero_departamento = dr["numero_departamento"].ToInt();
                        visita.id_visitante = dr["id_visitante"].ToInt();
                        //
                       visita.nombre_visitante = dr["nombre_visitante"].ToString();

                        visitareg_list.Add(visita);
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
            visitareg_res.VisitaRegistroList = visitareg_list;
            visitareg_res.oHeader = oHeader;

            return visitareg_res;
        }


        public VISITA_REGISTRO_Register Registrar(VISITA_REGISTRO visreg)
        {
            VISITA_REGISTRO_Register Visireg = new VISITA_REGISTRO_Register();
            DTOHeader oHeader = new DTOHeader();
            int id_register = 0;
            try
            {
                int rpta = 0;
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("SP_VISITAREGISTER_REGISTER", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_visita_registro", visreg.id_visita_registro);
                    cmd.Parameters.AddWithValue("@fecha_ingreso", visreg.fecha_ingreso.ToDateTime());
                    cmd.Parameters.AddWithValue("@fecha_salida", visreg.fecha_salida.ToDateTime());
                    cmd.Parameters.AddWithValue("@id_departamento", visreg.id_departamento);
                    cmd.Parameters.AddWithValue("@id_visitante", visreg.id_visitante);
                    rpta = Convert.ToInt32(cmd.ExecuteScalar());
                    cn.Close();

                }
                id_register = rpta;
                oHeader.estado = true;
                if (visreg.id_visita_registro > 0)
                {
                    oHeader.mensaje = "Se actualizo visita de registro :" + visreg.id_departamento;
                }
                else
                {
                    oHeader.mensaje = "Se registro visita de registro :" + visreg.id_departamento;
                }


            }
            catch (Exception ex)
            {
                oHeader.estado = false;
                oHeader.mensaje = ex.Message;
            }

            Visireg.oHeader = oHeader;
            Visireg.id_register = id_register;
            return Visireg;
        }




    }
}

    
