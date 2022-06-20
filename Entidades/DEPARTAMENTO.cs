using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Departamento_Register
    {
        public DTOHeader oHeader { get; set; }
        public int id_register { get; set; }
    }
    public class Departamento_Res
    {
        public DTOHeader oHeader { get; set; }
        public List<Departamento> lista_Departamento { get; set; }
    }
    public class Departamento
    {
        public int id_departamento { get; set; }
        public int piso { get; set; }
        public int numero{ get; set; }
        public int metros_cuadrado { get; set; }
        public int dormitorio { get; set; }
        public int banio { get; set; }
        public DateTime? fecha_creacion { get; set; }
        public DateTime? fecha_actualizacion { get; set; }
        public int id_torre { get; set; }
        public int id_usuario { get; set; }
        //adicionales
        public int numero_torre { get; set; }
        public int id_sector { get; set; }  
        public string nombre_sector { get; set; }
        public int id_sucursal { get; set; }
        public string nombre_sucursal { get; set; }
    }


    public class DepartamentoPropietario
    {
        public Departamento departamento { get; set; }
        public List<Propietario> propietarios { get; set; }
    }

    public class DepartamentoPropietarioRes
    {
        public DTOHeader oHeader { get; set; }
        public List<Departamento> lista_Departamento { get; set; }
        public List<Propietario> propietarios { get; set; }
    }

    public class FiltroDepa
    {
        public int id_sucursal_f { get; set; }
        public int id_sector_f { get; set; }
        public int id_torre_f { get; set; }
        public int numero_f { get; set; }
    }
}
