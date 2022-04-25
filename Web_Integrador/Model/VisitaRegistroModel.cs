using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Integrador.Model
{
    public class VisitaRegistroModelRes
    {
        public DTOHeader oHeader { get; set; }
        public List<VISITA_REGISTRO> lista_VisitaRegistro { get; set; }
    }

    public class VisitaRegistroModel
    {
        public List<VISITA_REGISTRO> lista_VisitaRegistro { get; set; }
        public List<Sucursal> Sucursales { get; set; }
        public List<Visitante> Visitantes { get; set; }
    }
}