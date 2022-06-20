using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;

namespace Negocio
{
    public class Departamento_BS
    {
        public Departamento_Register Registrar(Departamento enti)
        {
            return new Departamento_DA().Registrar(enti);
        }
        public Departamento_Res lista(int id)
        {
            return new Departamento_DA().Listar(id);
        }
        public DTOHeader Actualizar(Departamento dep)
        {
            return new Departamento_DA().Actualizar(dep);
        }
        public Departamento_Res FiltroDepartamento(FiltroDepa filtro)
        {
            return new Departamento_DA().FiltroDepartamento(filtro);
        }
    }
}
