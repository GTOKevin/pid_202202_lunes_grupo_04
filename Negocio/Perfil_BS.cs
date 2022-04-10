using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class Perfil_BS
    {
        public Perfil_res lista_Perfiles()
        {
            return new Perfil_DA().Listar_Perfil();
        }
    }
}
