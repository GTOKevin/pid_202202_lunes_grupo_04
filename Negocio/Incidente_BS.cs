using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;

namespace Negocio
{
    public class Incidente_BS
    {
        public Incidente_Res lista(FiltroIncidente filtro)
        {
            return new Incidente_DA().Listar(filtro);
        }
        public DTOHeader Registrar(Incidente inc)
        {
            return new Incidente_DA().Registrar(inc);
        }
        public DTOHeader Actualizar(Incidente inc)
        {
            return new Incidente_DA().Actualizar(inc);
        }

        public object lista(object id_register)
        {
            throw new NotImplementedException();
        }
    }
}
