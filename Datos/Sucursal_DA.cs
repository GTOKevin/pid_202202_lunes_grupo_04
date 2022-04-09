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

        public Sucursal_Res Listar()
        {
            Sucursal_Res sucursal_res = new Sucursal_Res();
            List<Sucursal> sucursal_list = new List<Sucursal>();
            DTOHeader oHeader = new DTOHeader();
            try
            {
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cm = new SqlCommand("select * from SUCURSAL", cn);
                    SqlDataReader dr = cm.ExecuteReader();
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


        public Sucursal_Res Registrar(Sucursal Enti)
        {
            Sucursal_Res Sucursal = new Sucursal_Res();

            return Sucursal;
        }




    }
}
