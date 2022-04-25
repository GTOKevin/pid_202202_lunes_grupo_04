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
    public class Perfil_File_DA
    {
        public DTOHeader Registrar(Perfil_File pf)
        {
            DTOHeader oHeader = new DTOHeader();
            try
            {
                int rpta;
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cm = new SqlCommand("USP_FILE_PERFIL", cn);
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.AddWithValue("@id_perfil", pf.id_perfil);
                    cm.Parameters.AddWithValue("@nombrefile", pf.nombrefile);
                    rpta = cm.ExecuteNonQuery();
                    cn.Close();
                }
                if (rpta > 0)
                {
                    oHeader.estado = true;
                }
                else
                {
                    oHeader.estado = false;
                    oHeader.mensaje = "No pudo registrarse, contacte con un administrador";
                }

            }
            catch (Exception ex)
            {
                oHeader.estado = false;
                oHeader.mensaje = ex.Message;
            }
            return oHeader;
        }

        public Perfil_File_res ListarFile(int id)
        {
            Perfil_File_res pfr = new Perfil_File_res();
            List<Perfil_File> flist = new List<Perfil_File>();
            DTOHeader oHeader = new DTOHeader();

            try
            {
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cm = new SqlCommand("USP_LISTAR_PERFILFILE", cn);
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.AddWithValue("@id_file", id);
                    SqlDataReader dr = cm.ExecuteReader();
                    while (dr.Read())
                    {
                        Perfil_File pf = new Perfil_File();
                        pf.id_file = dr["id_file"].ToInt();
                        pf.nombrefile = dr["nombrefile"].ToString();
                        pf.id_perfil = dr["id_perfil"].ToInt();
                        flist.Add(pf);
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
            pfr.FileList = flist;
            pfr.oHeader = oHeader;

            return pfr;
        }

    }
}
