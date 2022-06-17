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

    public Departamento_Res Listar(int id)
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
                cm.Parameters.AddWithValue("@id_departamento", id);
                SqlDataReader dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    Departamento departamento = new Departamento();
                    departamento.id_departamento = dr["id_departamento"].ToInt();
                    departamento.piso = dr["piso"].ToInt();
                    departamento.numero = dr["numero"].ToInt();
                    departamento.metros_cuadrado = dr["metros_cuadrado"].ToInt();
                    departamento.dormitorio = dr["dormitorio"].ToInt();
                    departamento.banio = dr["banio"].ToInt();
                    departamento.id_torre = dr["id_torre"].ToInt();
                    departamento.id_usuario = dr["id_usuario"].ToInt();
                        //adicionales
                        departamento.numero_torre = dr["numero_torre"].ToInt();
                        departamento.id_sector = dr["id_sector"].ToInt();
                        departamento.nombre_sector = dr["nombre_sector"].ToString();
                        departamento.id_sucursal = dr["id_sucursal"].ToInt();
                        departamento.nombre_sucursal = dr["nombre_sucursal"].ToString();
                    departamento_list.Add(departamento);
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
        departamento_res.lista_Departamento = departamento_list;
        departamento_res.oHeader = oHeader;

        return departamento_res;
    }

    public Departamento_Register Registrar(Departamento enti)
    {

        Departamento_Register departamento_Register = new Departamento_Register();
        DTOHeader oHeader = new DTOHeader();
        int id_register = 0;
        try
        {
            using (SqlConnection cn = Conexion.Conectar())
            {

                cn.Open();
                SqlCommand cm = new SqlCommand("USP_DEPARTAMETO_REGISTRAR", cn);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@id_departamento", enti.id_departamento);
                cm.Parameters.AddWithValue("@piso", enti.piso);
                cm.Parameters.AddWithValue("@numero", enti.numero);
                cm.Parameters.AddWithValue("@metros_cuadrado", enti.metros_cuadrado);
                cm.Parameters.AddWithValue("@dormitorio", enti.dormitorio);
                cm.Parameters.AddWithValue("@banio", enti.banio);
                cm.Parameters.AddWithValue("@id_torre", enti.id_torre);
                cm.Parameters.AddWithValue("@id_usuario", enti.id_usuario);
                id_register = cm.ExecuteScalar().ToInt();
                cn.Close();
            }
          
                if (id_register == -1)
                {
                    oHeader.estado = false;
                    oHeader.mensaje = "Departamento ya existente";
                }
                else
                {
                    oHeader.estado = true;
                    if (enti.id_departamento == 0)
                    {
                        oHeader.mensaje = "Se ha registrado una torre";
                    }
                    else
                    {
                        oHeader.mensaje = "Se ha actualizado una torre";
                    }
                }
       
        }
        catch (Exception ex)
        {
            oHeader.estado = false;
            oHeader.mensaje = ex.Message;
        }

        departamento_Register.oHeader = oHeader;
        departamento_Register.id_register = id_register;
        return departamento_Register;
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
