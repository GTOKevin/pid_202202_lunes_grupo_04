using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Integrador.Model
{
    
        public class RecibModelRes
        {
            public DTOHeader oHeader { get; set; }
            public List<Servicio> lista_Servicio { get; set; }
        }

        public class ReciboModel
        {
            public List<Recibo> lista_Recibo { get; set; }
            public List<Sucursal> Sucursales { get; set; }
            
        }
}