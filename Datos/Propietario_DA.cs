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
    public class Propietario_DA
    { 
        public DTOHeader Registrar(Propietario enti)
        {
            DTOHeader oHeader = new DTOHeader();

            try
            {
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("USP_PROPIETARIO_REGISTRAR", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_propietario", enti.id_propietario);
                    cmd.Parameters.AddWithValue("@nombres", enti.nombres);
                    cmd.Parameters.AddWithValue("@primer_apellido", enti.primer_apellido);
                    cmd.Parameters.AddWithValue("@segundo_apellido", enti.segundo_apellido);
                    cmd.Parameters.AddWithValue("@tipo_documento", enti.tipo_documento.ToString());
                    cmd.Parameters.AddWithValue("@nro_documento", enti.nro_documento);
                    cmd.Parameters.AddWithValue("@nacionalidad", enti.nacionalidad.ToString());
                    cmd.Parameters.AddWithValue("@id_departamento", enti.id_departamento);
                    cmd.Parameters.AddWithValue("@id_tipo", enti.id_tipo);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
                oHeader.estado = true;
            }catch(Exception ex)
            {
                oHeader.estado = false;
                oHeader.mensaje = ex.Message;
            }
            return oHeader;
        }

        public Propietario_Res Listar(int id_dep)
        {
            Propietario_Res propietario_res = new Propietario_Res();
            List<Propietario> propietario_list = new List<Propietario>();
            DTOHeader oHeader = new DTOHeader();
            try
            {
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cm = new SqlCommand("USP_PROPIETARIO_DEP_LISTAR", cn);
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.AddWithValue("@id_departamento", id_dep);
                    SqlDataReader dr = cm.ExecuteReader();
                    while (dr.Read())
                    {
                        Propietario propietario = new Propietario();
                        propietario.id_propietario = dr["id_propietario"].ToInt();
                        propietario.nombres = dr["nombres"].ToString();
                        propietario.primer_apellido = dr["primer_apellido"].ToString();
                        propietario.segundo_apellido = dr["segundo_apellido"].ToString();
                        propietario.tipo_documento = dr["tipo_documento"].ToInt();
                        propietario.nro_documento = dr["nro_documento"].ToString();
                        propietario.nacionalidad = dr["nacionalidad"].ToInt();
                        propietario.fecha_registro = dr["fecha_registro"].ToDateTime();
                        propietario.estado = (byte)dr["estado"].ToInt();
                        propietario.id_departamento = dr["id_departamento"].ToInt();
                        propietario.id_tipo = dr["id_tipo"].ToInt();
                        //adicionales
                        propietario.nombre_documento = dr["nombre_documento"].ToString();
                        propietario.nombre_nacionalidad = dr["nombre_nacionalidad"].ToString();
                        propietario.nombre_tipo = dr["nombre_tipo"].ToString();
                        propietario_list.Add(propietario);
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
            propietario_res.lista_Propietario = propietario_list;
            propietario_res.oHeader = oHeader;

            return propietario_res;
        }
    }
}
