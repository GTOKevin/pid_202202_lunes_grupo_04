using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;


namespace Negocio
{
    public class Visitante_BS
    {
        public Visitante_Res lista(int id_visitante)
        {
            return new Visitante_DA().Listar(id_visitante);
        }

        public Visitante_Register Registrar(Visitante vis)
        {
            return new Visitante_DA().Registrar(vis);
        }

    }
}
