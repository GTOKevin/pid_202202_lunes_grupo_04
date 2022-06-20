using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Recibo_Res
    {
        public DTOHeader oHeader { get; set; }
        public List<Recibo> ReciboList { get; set; }
    }
   
    public class Recibo
    {
        public int id_recibo { get; set; }
        public string servicio { get; set; }
        public decimal monto { get; set; }
        public bool estado { get; set; }
        public DateTime? fecha_pago { get; set; }
        public int id_departamento { get; set; }
        public Propietario oPropietario { get; set; }

        //extras

        public int numero_departamento { get; set; }
        public int id_torre { get; set; }
        public int numero_torre { get; set; }
        public int id_sector { get; set; }
        public string nombre_sector { get; set; }
        public int id_sucursal { get; set; }
        public string nombre_sucursal { get; set; }
        public string nombre_servicio { get; set; }

    }

    public class Recibo_Register
    {
        public DTOHeader oHeader { get; set; }
        public int id_register { get; set; }
    }
}
