using Entidades;
using Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class Perfil_DA
    {
        public Perfil_res Listar_Perfil()
        {
            Perfil_res pr = new Perfil_res();
            List<Perfil> list = new List<Perfil>();
            DTOHeader oHeader = new DTOHeader();
            try
            {
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cm = new SqlCommand("USP_LIST_PERFIL", cn);
                    cm.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = cm.ExecuteReader();
                    while (dr.Read())
                    {
                        Perfil p = new Perfil();
                        p.id_perfil = dr["id_perfil"].ToInt();
                        p.nombres = dr["nombres"].ToString();
                        p.primer_apellido = dr["primer_apellido"].ToString();
                        p.segundo_apellido = dr["segundo_apellido"].ToString();
                        p.fecha_nacimiento = dr["fecha_nacimiento"].ToDateTime();
                        p.tipo_documento = dr["tipo_documento"].ToString();
                        p.nro_documento = dr["nro_documento"].ToString();
                        p.genero = dr["genero"].ToString();
                        p.nacionalidad = dr["nacionalidad"].ToString();
                        p.direccion = dr["direccion"].ToString();
                        list.Add(p);
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
            pr.Lista_Perfiles = list;
            pr.oHeader = oHeader;

            return pr;
        }

        public Perfil_res Listar_Perfil_Usuario(int id_perfil)
        {
            Perfil_res pr = new Perfil_res();
            List<Perfil> list = new List<Perfil>();
            DTOHeader oHeader = new DTOHeader();
            try
            {
                if(id_perfil > 0)
                {
                    using (SqlConnection cn = Conexion.Conectar())
                    {
                        cn.Open();
                        SqlCommand cm = new SqlCommand("USP_PERFIL_LIST_USUARIO", cn);
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.Parameters.AddWithValue("@id_perfil", id_perfil);
                        SqlDataReader dr = cm.ExecuteReader();
                        while (dr.Read())
                        {
                            Perfil p = new Perfil();
                            p.id_perfil = dr["id_perfil"].ToInt();
                            p.nombres = dr["nombres"].ToString();
                            p.primer_apellido = dr["primer_apellido"].ToString();
                            p.segundo_apellido = dr["segundo_apellido"].ToString();
                            p.fecha_nacimiento = dr["fecha_nacimiento"].ToDateTime();
                            p.tipo_documento = dr["tipo_documento"].ToString();
                            p.nro_documento = dr["nro_documento"].ToString();
                            p.genero = dr["genero"].ToString();
                            p.nacionalidad = dr["nacionalidad"].ToString();
                            p.direccion = dr["direccion"].ToString();
                            list.Add(p);
                        }
                        cn.Close();
                    }
                    oHeader.estado = true;
                }
                else
                {
                    oHeader.estado = false;
                    oHeader.mensaje = "no se encontro perfil";
                }
              
            }
            catch (Exception ex)
            {
                oHeader.estado = false;
                oHeader.mensaje = ex.Message;
            }
            pr.Lista_Perfiles = list;
            pr.oHeader = oHeader;

            return pr;
        }

        public Perfil_Register Registrar_Perfil(Perfil perfil)
        {
            Perfil_Register pr = new Perfil_Register();
            DTOHeader oHeader = new DTOHeader();
            int id_register = 0;
            try
            {
                int rpta = 0;
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("USP_PERFIL_REGISTER", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_perfil", perfil.id_perfil);
                    cmd.Parameters.AddWithValue("@nombres", perfil.nombres);
                    cmd.Parameters.AddWithValue("@primer_apellido", perfil.primer_apellido);
                    cmd.Parameters.AddWithValue("@segundo_apellido", perfil.segundo_apellido);
                    cmd.Parameters.AddWithValue("@fecha_nacimiento", perfil.fecha_nacimiento);
                    cmd.Parameters.AddWithValue("@tipo_documento", perfil.tipo_documento);
                    cmd.Parameters.AddWithValue("@nro_documento", perfil.nro_documento);
                    cmd.Parameters.AddWithValue("@genero", perfil.genero);
                    cmd.Parameters.AddWithValue("@nacionalidad", perfil.nacionalidad);
                    cmd.Parameters.AddWithValue("@direccion", perfil.direccion);
                    rpta = Convert.ToInt32(cmd.ExecuteScalar());
                    cn.Close();

                }
                id_register = rpta;
                oHeader.estado = true;
                if (perfil.id_perfil > 0)
                {
                    oHeader.mensaje = "Se actualizo el perfil :" + perfil.nombres;
                }
                else
                {
                    oHeader.mensaje = "Se registro el perfil :" + perfil.nombres;
                }


            }
            catch (Exception ex)
            {
                oHeader.estado = false;
                oHeader.mensaje = ex.Message;
            }

            pr.oHeader = oHeader;
            pr.id_register = id_register;
            return pr;
        }

        public Perfil_Register Editar_MiPerfil(Perfil perfil)
        {
            Perfil_Register pr = new Perfil_Register();
            DTOHeader oHeader = new DTOHeader();
            int id_register = 0;
            try
            {
                int rpta = 0;
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("USP_PERFIL_EDIT_US", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_perfil", perfil.id_perfil);
                    cmd.Parameters.AddWithValue("@nombres", perfil.nombres);
                    cmd.Parameters.AddWithValue("@primer_apellido", perfil.primer_apellido);
                    cmd.Parameters.AddWithValue("@segundo_apellido", perfil.segundo_apellido);
                    cmd.Parameters.AddWithValue("@fecha_nacimiento", perfil.fecha_nacimiento);
                    cmd.Parameters.AddWithValue("@tipo_documento", perfil.tipo_documento);
                    cmd.Parameters.AddWithValue("@nro_documento", perfil.nro_documento);
                    cmd.Parameters.AddWithValue("@genero", perfil.genero);
                    cmd.Parameters.AddWithValue("@nacionalidad", perfil.nacionalidad);
                    cmd.Parameters.AddWithValue("@direccion", perfil.direccion);
                    rpta = Convert.ToInt32(cmd.ExecuteScalar());
                    cn.Close();

                }
                id_register = rpta;
                oHeader.estado = true;
                if (perfil.id_perfil > 0)
                {
                    oHeader.mensaje = "Se actualizo el perfil :" + perfil.nombres;
                }
                else
                {
                    oHeader.mensaje = "Se registro el perfil :" + perfil.nombres;
                }


            }
            catch (Exception ex)
            {
                oHeader.estado = false;
                oHeader.mensaje = ex.Message;
            }

            pr.oHeader = oHeader;
            pr.id_register = id_register;
            return pr;
        }



        public Perfil_res Eliminar_Perfil(int id)
        {
            Perfil_res pr = new Perfil_res();
            DTOHeader oHeader = new DTOHeader();

            try
            {
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cm = new SqlCommand("USP_DELETE_PERFIL", cn);
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.AddWithValue("@id_perfil", id);
                    cm.ExecuteNonQuery();
                    cn.Close();
                }

                oHeader.mensaje = "SE ELIMINO CORRECTAMENTE";
                oHeader.estado = true;

            }
            catch (Exception ex)
            {
                oHeader.estado = false;
                oHeader.mensaje = ex.Message;
            }

            pr.oHeader = oHeader;

            return pr;
        }
    }
}
