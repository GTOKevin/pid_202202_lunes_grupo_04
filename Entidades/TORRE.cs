using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Torre_Res
    {
        public DTOHeader oHeader { get; set; }
        public List<Torre> TorreList { get; set; }
        
    }
    public class Torres_Register 
    { 
        public DTOHeader oHeader { get; set; }
        public int id_register { get; set; }  
    }

    public class Torre
    {
        public int id_torre { get; set; }
        public decimal numero { get; set; }
        public DateTime? fecha_creacion { get; set; }
        public int id_sector { get; set; }
        //ADICIONALES
        public string nombre_sector { get; set; }

        public int id_sucursal { get; set; }
        public string nombre_sucursal { get; set; }
    }


}
