using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Sucursal_Res
    {
        public DTOHeader oHeader { get; set; }
        public List<Sucursal> SucursalList { get; set; } 
    }

    public class Sucursal
    {   public int id_sucursal { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public DateTime? fecha_creacion { get; set; }
    }

    public class  Sucursal_Register
    {
        public DTOHeader oHeader { get; set; }
        public int id_register { get; set; }
    }
     
}
