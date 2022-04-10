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
    public class Departamento_File_DA
    {
        public Departamento_File_Res Listar()
        {
            Departamento_File_Res departamento_file_res = new Departamento_File_Res();
            List<Departamento_File> departamento_file_list = new List<Departamento_File>();
            DTOHeader oHeader = new DTOHeader();
            try
            {
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cm = new SqlCommand("USP_DEPARTAMENTO_FILE_LISTAR", cn);
                    cm.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = cm.ExecuteReader();
                    while (dr.Read())
                    {
                        Departamento_File departamento_file = new Departamento_File();
                        departamento_file.id_departamento_file = dr["id_departamento_file"].ToInt();
                        departamento_file.url_imagen = dr["url_imagen"].ToString();
                        departamento_file.fecha_creacion = dr["fecha_creacion"].ToDateTime();
                        departamento_file.id_departamento = dr["id_departamento"].ToInt();
                        departamento_file_list.Add(departamento_file);
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
            departamento_file_res.lista_Departamento_File = departamento_file_list;
            departamento_file_res.oHeader = oHeader;

            return departamento_file_res;
        }
    }
}
