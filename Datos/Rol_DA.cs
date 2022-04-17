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
    public class Rol_DA
    {
        public Rol_Res Listar(int id_rol)
        {
            Rol_Res rol_Res = new Rol_Res();
            List<Rol> rol_list = new List<Rol>();
            DTOHeader oHeader = new DTOHeader();
            try
            {
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cm = new SqlCommand("SP_ROL_LISTAR", cn);
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.AddWithValue("@id_rol", id_rol);
                    SqlDataReader dr = cm.ExecuteReader();
                    while (dr.Read())
                    {
                        Rol rol = new Rol();
                        rol.id_rol=dr["id_rol"].ToInt();
                        rol.nombre = dr["nombre"].ToString();
                        rol.descripcion = dr["descripcion"].ToString();
                        rol_list.Add(rol);     
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
            rol_Res.RolList = rol_list;
            rol_Res.oHeader = oHeader;
            return rol_Res;
        }

        public Rol_Register Registrar(Rol rol)
        {
            Rol_Register Rol = new Rol_Register();
            DTOHeader oHeader = new DTOHeader();
            int id_registrar = 0;
            try
            {
                int rpta = 0;
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("SP_CREAR_ROL", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_rol", rol.id_rol);
                    cmd.Parameters.AddWithValue("@nombre", rol.nombre);
                    cmd.Parameters.AddWithValue("@descripcion", rol.descripcion);
                    rpta = Convert.ToInt32(cmd.ExecuteScalar());
                    cn.Close();

                }
                id_registrar = rpta;
                oHeader.estado = true;
                oHeader.mensaje = "Se registro el Rol :" + rol.nombre;


            }
            catch (Exception ex)
            {
                oHeader.estado = false;
                oHeader.mensaje = ex.Message;
            }

           Rol.oHeader = oHeader;
            Rol.id_registrar = id_registrar;
            return Rol;
        }
    }
}
