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
    public class Departamento_DA
    {
         public Departamento_Res Listar()
        {
            Departamento_Res departamento_res = new Departamento_Res();
            List<Departamento> departamento_list = new List<Departamento>();
            DTOHeader oHeader = new DTOHeader();
            try
            {
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cm = new SqlCommand("USP_DEPARTAMENTO_LISTAR", cn);
                    cm.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = cm.ExecuteReader();
                    while (dr.Read())
                    {
                        Departamento departamento = new Departamento();
                        departamento.id_departamento=dr["id_departamento"].ToInt();
                        departamento.piso= dr["piso"].ToInt();
                        departamento.numero= dr["numero"].ToInt();
                        departamento.metros_cuadrado = dr["metros_cuadrado"].ToInt();
                        departamento.dormitorio = dr["dormitorio"].ToInt();
                        departamento.banio=dr["banio"].ToInt();
                        departamento.fecha_creacion = dr["fecha_creacion"].ToDateTime();
                        departamento.fecha_actualizacion = dr["fecha_actualizacion"].ToDateTime();
                        departamento.id_torre = dr["id_torre"].ToInt();
                        departamento.id_usuario= dr["id_usuario"].ToInt();
                        departamento_list.Add(departamento);
                    }
                    cn.Close();
                }
                oHeader.estado = true;

            }catch(Exception ex)
            {
                oHeader.estado = false;
                oHeader.mensaje = ex.Message;
            }
            departamento_res.lista_Departamento = departamento_list;
            departamento_res.oHeader = oHeader;

            return departamento_res;
        }
        public DTOHeader Registrar(Departamento dep) 
        {
          
            DTOHeader oHeader = new DTOHeader();
            try
            {
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cm = new SqlCommand("USP_DEPARTAMENTO_CREAR", cn);
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.AddWithValue("@piso", dep.piso);
                    cm.Parameters.AddWithValue("@numero", dep.numero);
                    cm.Parameters.AddWithValue("@metros_cuadrado", dep.metros_cuadrado);
                    cm.Parameters.AddWithValue("@dormitorio", dep.dormitorio);
                    cm.Parameters.AddWithValue("@banio", dep.banio);
                    cm.Parameters.AddWithValue("@id_torre", dep.piso);
                    SqlDataReader dr = cm.ExecuteReader();
                   
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
        }
        public DTOHeader Actualizar(Departamento dep)
        {
            DTOHeader oHeader = new DTOHeader();
            try
            {
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cm = new SqlCommand("USP_DEPARTAMENTO_ACTUALIZAR", cn);
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.AddWithValue("@id_departamento", dep.id_departamento);
                    cm.Parameters.AddWithValue("@piso", dep.piso);
                    cm.Parameters.AddWithValue("@numero", dep.numero);
                    cm.Parameters.AddWithValue("@metros_cuadrado", dep.metros_cuadrado);
                    cm.Parameters.AddWithValue("@dormitorio", dep.dormitorio);
                    cm.Parameters.AddWithValue("@banio", dep.banio);
                    cm.Parameters.AddWithValue("@fecha_creacion", dep.fecha_creacion);
                    cm.Parameters.AddWithValue("@id_torre", dep.id_torre);
                    cm.Parameters.AddWithValue("@id_usuario", dep.id_usuario);
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
        }
    }
}
