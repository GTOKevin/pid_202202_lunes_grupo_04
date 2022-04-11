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
        public Propietario_Res Listar()
        {
            Propietario_Res propietario_res = new Propietario_Res();
            List<Propietario> propietario_list = new List<Propietario>();
            DTOHeader oHeader = new DTOHeader();
            try
            {
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cm = new SqlCommand("USP_PROPIETARIO_LISTAR", cn);
                    cm.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = cm.ExecuteReader();
                    while (dr.Read())
                    {
                        Propietario propietario = new Propietario();
                        propietario.id_propietario = dr["id_propietario"].ToInt();
                        propietario.nombres = dr["nombres"].ToString();
                        propietario.primer_apellido = dr["primer_apellido"].ToString();
                        propietario.segundo_apellido = dr["segundo_apellido"].ToString();
                        propietario.tipo_documento = dr["tipo_documento"].ToString();
                        propietario.nro_documento = dr["nro_documento"].ToString();
                        propietario.nacionalidad = dr["nacionalidad"].ToString();
                        propietario.fecha_registro = dr["fecha_registro"].ToDateTime();
                        propietario.estado = (byte)dr["estado"].ToInt();
                        propietario.id_departamento = dr["id_departamento"].ToInt();
                        propietario.id_tipo = dr["id_tipo"].ToInt();
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
