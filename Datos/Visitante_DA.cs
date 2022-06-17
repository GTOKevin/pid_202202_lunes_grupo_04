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
   public class Visitante_DA
    {
        public Visitante_Res Listar(int id_visitante)
        {
            Visitante_Res visitante_res = new Visitante_Res();
            List<Visitante> visitante_list = new List<Visitante>();
            DTOHeader oHeader = new DTOHeader();
            try
            {
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("SP_VISITANTE_LISTAR", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_visitante",id_visitante);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Visitante visitante = new Visitante();
                        visitante.id_visitante= dr["id_visitante"].ToInt();
                        visitante.nombre = dr["nombre"].ToString();
                        visitante.apellidos = dr["apellidos"].ToString();
                        visitante.tipo_documento= dr["tipo_documento"].ToInt();
                        visitante.nombre_tipo = dr["nombre_tipo"].ToString();
                        visitante.nro_documento = dr["nro_documento"].ToString();
                        visitante.genero = dr["genero"].ToInt();
                        visitante.nombre_genero = dr["nombre_genero"].ToString();
                        visitante.fecha_creacion = dr["fecha_creacion"].ToDateTime();


                        visitante_list.Add(visitante);
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
            visitante_res.VisitanteList = visitante_list;
            visitante_res.oHeader = oHeader;

            return visitante_res;
        }

        //DNI
        public Visitante_Res ListarXDni(string nro_documento)
        {
            Visitante_Res visitante_res = new Visitante_Res();
            List<Visitante> visitante_list = new List<Visitante>();
            DTOHeader oHeader = new DTOHeader();
            try
            {
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("USP_FILTRO_DNI_VIS", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nro_documento", nro_documento);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Visitante visitante = new Visitante();
                        visitante.id_visitante = dr["id_visitante"].ToInt();
                        visitante.nombre = dr["nombre"].ToString();
                        visitante.apellidos = dr["apellidos"].ToString();
                        visitante.tipo_documento = dr["tipo_documento"].ToInt();
                        visitante.nombre_tipo = dr["nombre_tipo"].ToString();
                        visitante.nro_documento = dr["nro_documento"].ToString();
                        visitante.genero = dr["genero"].ToInt();
                        visitante.nombre_genero = dr["nombre_genero"].ToString();
                        visitante.fecha_creacion = dr["fecha_creacion"].ToDateTime();


                        visitante_list.Add(visitante);
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
            visitante_res.VisitanteList = visitante_list;
            visitante_res.oHeader = oHeader;

            return visitante_res;
        }


        public Visitante_Register Registrar(Visitante visi)
        {
            Visitante_Register Visitante = new Visitante_Register();
            DTOHeader oHeader = new DTOHeader();
            int id_register = 0;
            try
            {
                int rpta = 0;
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("SP_VISITANTE_REGISTER", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_visitante",visi.id_visitante);
                    cmd.Parameters.AddWithValue("@nombre", visi.nombre);
                    cmd.Parameters.AddWithValue("@apellido",visi.apellidos);
                    cmd.Parameters.AddWithValue("@tipo_documento", visi.tipo_documento);
                    cmd.Parameters.AddWithValue("@nro_documento", visi.nro_documento);
                    cmd.Parameters.AddWithValue("@genero", visi.genero.ToString());

                    rpta = Convert.ToInt32(cmd.ExecuteScalar());
                    cn.Close();

                }
                id_register = rpta;
                oHeader.estado = true;
                if(rpta == -1)
                {
                    oHeader.estado = false;
                    oHeader.mensaje = "El DNI: '"+ visi.nro_documento +"' ya existe";
                }
                else if(rpta > 0)
                {
                    oHeader.mensaje = "Se actualizo el visitante :" + visi.nombre;
                }
                else
                {
                    oHeader.mensaje = "Se registro el visitante :" + visi.nombre;
                }


            }
            catch (Exception ex)
            {
                oHeader.estado = false;
                oHeader.mensaje = ex.Message;
            }

            Visitante.oHeader = oHeader;
            Visitante.id_register = id_register;
            return Visitante;
        }




        //REGISTRAR ENTRADA
        public VISITA_REGISTRO_Register RegistrarEntradaUsuario(VISITA_REGISTRO visreg)
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
                    cmd.Parameters.AddWithValue("@id_departamento", visreg.id_departamento);
                    cmd.Parameters.AddWithValue("@id_visitante", visreg.id_visitante);
                    rpta = Convert.ToInt32(cmd.ExecuteScalar());
                    cn.Close();

                }
                id_register = rpta;
               
                if(rpta == -1)
                {
                    oHeader.estado = false;
                    oHeader.mensaje = "Este visitante, tiene registro de visita sin salida";
                }
                else if (rpta > 0)
                {
                    oHeader.estado = true;
                    oHeader.mensaje = "Se actualizo visita de registro :" + visreg.id_departamento;
                }
                else
                {
                    oHeader.estado = true;
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

        public VISITA_REGISTRO_Register BuscarXDni(string nro_documento)
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
                    SqlCommand cmd = new SqlCommand("USP_FILTRO_DNI_VIS_REG", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nro_documento", nro_documento);
                    rpta = Convert.ToInt32(cmd.ExecuteScalar());
                    cn.Close();
                }
                id_register = rpta;

                if(rpta == -1)
                {
                    oHeader.estado = false;
                    oHeader.mensaje = "Visitante con el Dni: '"+nro_documento+"' no se le marco la salida";
                }
                else if (rpta == -2)
                {
                    oHeader.estado = false;
                    oHeader.mensaje = "Visitante con el Dni: '" + nro_documento + "' no se encuentra registrado";
                }
                else
                {
                    oHeader.estado = true;
                    oHeader.mensaje = "Se agrego a la lista de visitantes";
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

        public VISITAREGISTRO_Res ListarHistorial(int id_visitante)
        {
            VISITAREGISTRO_Res visitareg_res = new VISITAREGISTRO_Res();
            List<VISITA_REGISTRO> visitareg_list = new List<VISITA_REGISTRO>();
            DTOHeader oHeader = new DTOHeader();
            try
            {
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("USP_LISTAR_HISTORIAL_VISITA", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_visitante", id_visitante);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        VISITA_REGISTRO visita = new VISITA_REGISTRO();
                        visita.id_visita_registro = dr["id_visita_registro"].ToInt();
                        visita.fecha_ingreso = Convert.ToDateTime(dr["fecha_ingreso"].ToDateTime());
                        visita.fecha_salida = Convert.ToDateTime(dr["fecha_salida"].ToDateTime());
                        visita.id_sucursal = dr["id_sucursal"].ToInt();
                        visita.id_sector = dr["id_sector"].ToInt();
                        visita.id_torre = dr["id_torre"].ToInt();
                        visita.nombre_sucursal = dr["nombre_sucursal"].ToString();
                        visita.nombre_sector = dr["nombre_sector"].ToString();
                        visita.numero_torre = dr["numero_torre"].ToInt();
                        visita.id_departamento = dr["id_departamento"].ToInt();
                        visita.numero_departamento = dr["numero_departamento"].ToInt();
                        visita.id_visitante = dr["id_visitante"].ToInt();
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





    }
}
