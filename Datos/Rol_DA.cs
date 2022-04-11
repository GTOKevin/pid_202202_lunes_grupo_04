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
        public Rol_Res Listar()
        {
            Rol_Res rol_Res = new Rol_Res();
            List<Rol> rol_list = new List<Rol>();
            DTOHeader oHeader = new DTOHeader();
            try
            {
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cm = new SqlCommand("USP_ROL_LISTAR", cn);
                    cm.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = cm.ExecuteReader();
                    while (dr.Read())
                    {
                        Rol rol = new Rol();
                        rol.id_rol=dr["id_rol"].ToInt();
                        rol.nombre = dr["nombre"].ToString();
                        rol_list.Add(rol);     
                    }
                    cn.Close();
                }
                oHeader.estado = true;
            }catch (Exception ex)
            {
                oHeader.estado = false;
                oHeader.mensaje = ex.Message;
            }
            return rol_Res;
        } 
     }
}
