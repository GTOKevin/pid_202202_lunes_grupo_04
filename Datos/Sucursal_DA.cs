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
    public class Sucursal_DA
    {

        public Sucursal_Res Listar(int id_sucursal)
        {
            Sucursal_Res sucursal_res = new Sucursal_Res();
            List<Sucursal> sucursal_list = new List<Sucursal>();
            DTOHeader oHeader = new DTOHeader();
            try
            {
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("SP_SUCURSAL_LISTAR", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_sucursal", id_sucursal);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Sucursal sucursal = new Sucursal();
                        sucursal.id_sucursal = dr["id_sucursal"].ToInt();
                        sucursal.nombre = dr["nombre"].ToString();
                        sucursal.descripcion = dr["descripcion"].ToString();
                        sucursal.fecha_creacion = dr["fecha_creacion"].ToDateTime();
                        sucursal_list.Add(sucursal);
                    }
                    cn.Close();
                }
                oHeader.estado = true;

            }catch(Exception ex)
            {
                oHeader.estado = false;
                oHeader.mensaje = ex.Message;
            }
            sucursal_res.SucursalList = sucursal_list;
            sucursal_res.oHeader = oHeader;

            return sucursal_res;
        }

        public Sucursal_Register Registrar(Sucursal Enti)
        {
            Sucursal_Register Sucursal = new Sucursal_Register();
            DTOHeader oHeader = new DTOHeader();
            int id_register = 0;
            try
            {
                int rpta = 0;
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("SP_SUCURSAL_REGISTER", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_sucursal", Enti.id_sucursal);
                    cmd.Parameters.AddWithValue("@nombre", Enti.nombre);
                    cmd.Parameters.AddWithValue("@descripcion", Enti.descripcion);
                    rpta = cmd.ExecuteScalar().ToInt();
                    cn.Close();
                    
                }
                id_register = rpta;
                oHeader.estado = true;
                if (Enti.id_sucursal > 0)
                {
                    oHeader.mensaje = "Se actualizo la sucursal :" + Enti.nombre;
                }
                else
                {
                    oHeader.mensaje = "Se registro la sucursal :" + Enti.nombre;
                }   
         
                
            }catch(Exception ex)
            {
                oHeader.estado=false; 
                oHeader.mensaje=ex.Message;
            }

            Sucursal.oHeader = oHeader;
            Sucursal.id_register = id_register;
            return Sucursal;
        }



    }
}
