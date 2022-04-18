using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;

namespace Negocio
{
    public class Propietario_BS
    {
        public Propietario_Res lista(int id_dep)
        {
            return new Propietario_DA().Listar(id_dep);
        }

        public DTOHeader Registrar(Propietario enti)
        {
            return new Propietario_DA().Registrar(enti);
        }
    }
}
