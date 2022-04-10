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
    }
}
