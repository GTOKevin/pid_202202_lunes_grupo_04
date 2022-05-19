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
   public class Visitante_DA
    {
        public Visitante_Res Listar(int id_visitante)
        {
            Visitante_Res visitante_res = new Visitante_Res();
            List<Visitante> visitante_list = new List<Visitante>();
            DTOHeader oHeader = new DTOHeader();
            try
            {
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("SP_VISITANTE_LISTAR", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_visitante",id_visitante);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Visitante visitante = new Visitante();
                        visitante.id_visitante= dr["id_visitante"].ToInt();
                        visitante.nombre = dr["nombre"].ToString();
                        visitante.apellidos = dr["apellidos"].ToString();
                        visitante.tipo_documento= dr["tipo_documento"].ToInt();
                        visitante.nombre_tipo = dr["nombre_tipo"].ToString();
                        visitante.nro_documento = dr["nro_documento"].ToString();
                        visitante.genero = dr["genero"].ToInt();
                        visitante.nombre_genero = dr["nombre_genero"].ToString();
                        visitante.fecha_creacion = dr["fecha_creacion"].ToDateTime();


                        visitante_list.Add(visitante);
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
            visitante_res.VisitanteList = visitante_list;
            visitante_res.oHeader = oHeader;

            return visitante_res;
        }


        public Visitante_Register Registrar(Visitante visi)
        {
                        Visitante_Register Visitante = new Visitante_Register();
            DTOHeader oHeader = new DTOHeader();
            int id_register = 0;
            try
            {
                int rpta = 0;
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("SP_VISITANTE_REGISTER", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_visitante",visi.id_visitante);
                    cmd.Parameters.AddWithValue("@nombre", visi.nombre);
                    cmd.Parameters.AddWithValue("@apellido",visi.apellidos);
                    cmd.Parameters.AddWithValue("@tipo_documento", visi.tipo_documento);
                    cmd.Parameters.AddWithValue("@nro_documento", visi.nro_documento);
                    cmd.Parameters.AddWithValue("@genero", visi.genero.ToString());

                    rpta = Convert.ToInt32(cmd.ExecuteScalar());
                    cn.Close();

                }
                id_register = rpta;
                oHeader.estado = true;
                if (visi.id_visitante > 0)
                {
                    oHeader.mensaje = "Se actualizo el visitante :" + visi.nombre;
                }
                else
                {
                    oHeader.mensaje = "Se registro el visitante :" + visi.nombre;
                }


            }
            catch (Exception ex)
            {
                oHeader.estado = false;
                oHeader.mensaje = ex.Message;
            }

            Visitante.oHeader = oHeader;
            Visitante.id_register = id_register;
            return Visitante;
        }

    }
}
