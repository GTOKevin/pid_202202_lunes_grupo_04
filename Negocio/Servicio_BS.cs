using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Datos;
using Entidades;

namespace Negocio
{
    public class Servicio_BS
    {
        public Servicio_Res lista()
        {
            return new Servicio_DA().Listar();
        }
        public DTOHeader Registrar(Servicio s)
        {
            return new Servicio_DA().Registrar(s);
        }
        public DTOHeader Actualizar(Servicio s)
        {
            return new Servicio_DA().Actualizar(s);
        }
    }
}
