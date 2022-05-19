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
        public Departamento_File_Res Listar(int id_departamento_file)
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
                    cm.Parameters.AddWithValue("@id_departamento_file", id_departamento_file);
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
            departamento_file_res.DepartamentoFileList = departamento_file_list;
            departamento_file_res.oHeader = oHeader;

            return departamento_file_res;
        }
        public Departamento_File_Res ListarFileIDDep(int id_departamento)
        {
            Departamento_File_Res departamento_file_res = new Departamento_File_Res();
            List<Departamento_File> departamento_file_list = new List<Departamento_File>();
            DTOHeader oHeader = new DTOHeader();
            try
            {
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cm = new SqlCommand("USP_LISTARDEPARTAMENTOFILEPORIDDEPARTAMENTO", cn);
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.AddWithValue("@id_departamento", id_departamento);
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
            departamento_file_res.DepartamentoFileList = departamento_file_list;
            departamento_file_res.oHeader = oHeader;

            return departamento_file_res;
        }


        public DepartamentoFile_Register Registrar(Departamento_File depf)
        {

            DepartamentoFile_Register departamento_file = new DepartamentoFile_Register();
            DTOHeader oHeader = new DTOHeader();
            int id_register = 0;
            try
            {
                int rpta = 0;
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cm = new SqlCommand("USP_INSERTARDEPARTMENTOFILE", cn);
                    cm.CommandType = CommandType.StoredProcedure;

                    cm.Parameters.AddWithValue("@id_departamento", depf.id_departamento);
                    cm.Parameters.AddWithValue("@url_imagen", depf.url_imagen);
                
                    rpta = cm.ExecuteScalar().ToInt();
                    cn.Close();
                    //SqlDataReader dr = cm.ExecuteReader();

                }
                id_register = rpta;

                oHeader.estado = true;
                
            }
            catch (Exception ex)
            {
                oHeader.estado = false;
                oHeader.mensaje = ex.Message;
            }

            departamento_file.oHeader = oHeader;
            departamento_file.id_register = id_register;
            return departamento_file;
        } 


        /*public DTOHeader Actualizar(Departamento_File depf)
        {
            DTOHeader oHeader = new DTOHeader();
            try
            {
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cm = new SqlCommand("USP_DEPARTAMENTO_FILE_ACTUALIZAR", cn);
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.AddWithValue("@id_departamento_file", depf.id_departamento_file);
                    cm.Parameters.AddWithValue("@url_imagen", depf.url_imagen);
                    cm.Parameters.AddWithValue("@id_departamento", depf.id_departamento);
                    cm.ExecuteNonQuery();
                    cn.Close();
                }
                oHeader.estado = true;
            }
            catch (Exception ex)
            {
                oHeader.estado = false;
                oHeader.mensaje = ex.Message;
            }
            return oHeader;
        }*/
    }
}
