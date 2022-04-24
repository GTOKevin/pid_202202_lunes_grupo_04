using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Integrador.Model
{
    public class DepartamentoModelRes
    {
        public DTOHeader oHeader { get; set; }
        public List<Departamento> lista_Departamento { get; set; }
        public List<Propietario> lista_Propietario { get; set; }
    }

    public class DepartamentoModel
    {
        public List<Tipo> Tipos { get; set; }
        public List<Sucursal> Sucursales { get; set; }
    }
}