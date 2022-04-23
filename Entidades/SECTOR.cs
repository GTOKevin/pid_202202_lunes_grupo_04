using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Sector_Res
    {
        public DTOHeader oHeader { get; set; }
        public List<Sector> SectorList { get; set; }
    }
    public class Sector_Suc_Res
    {
        public DTOHeader oHeader { get; set; }
        public List<Sector_Suc> SectorList { get; set; }
    }
    public class Sector
    {
        public int id_sector { get; set; }
        public string nombre_sector { get; set; }
        public DateTime? fecha_creacion { get; set; }
        public int id_sucursal { get; set; }
    }
    public class Sector_Suc
    {
        public int id_sector { get; set; }
        public string nombre_sector { get; set; }
        public DateTime? fecha_creacion { get; set; }
        public int id_sucursal { get; set; }
        public string nombre_sucursal { get; set; }
    }
    public class Sector_Register
    {
        public DTOHeader oHeader { get; set; }
        public int id_register { get; set; }
    }
}
