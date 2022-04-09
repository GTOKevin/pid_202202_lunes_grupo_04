using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace Datos
{
    public class Conexion
    {
        public static SqlConnection Conectar()
        {
            string coneStr= ConfigurationManager.ConnectionStrings["dev"].ConnectionString;
            SqlConnection cn = new SqlConnection(coneStr);
            return cn;
        }
    }
}
