using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Integrador.Model
{
    public class ServicioModel
    {
        public class VisitaServicioModelRes
        {
            public DTOHeader oHeader { get; set; }
            public List<Servicio> lista_Servicio { get; set; }
        }
        public List<Servicio> lista_Servicio { get; set; }
        public List<Sucursal> Sucursales { get; set; }
        public List<Tipo> Tipos { get; set; }
    }
}