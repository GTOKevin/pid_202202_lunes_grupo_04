using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;

namespace Negocio
{
    public class Torre_BS
    {
        public Torres_Register Register(Torre enti)
        {
            return new Torre_DA().Registrar(enti);
        }

        public Torre_Res Listar(int id)
        {
            return new Torre_DA().Listar(id);
        }
    }
}
