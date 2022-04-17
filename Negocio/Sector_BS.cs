using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Datos;
using Entidades;

namespace Negocio
{
    public class Sector_BS
    {
        public Sector_Res lista(int id)
        {
            return new Sector_DA().Listar(id);
        }
        public Sector_Suc_Res listar_suc(int id)
        {
            return new Sector_DA().Listar_suc(id);
        }
        public Sector_Register Registrar(Sector s)
        {
            return new Sector_DA().Registrar(s);
        }
       
   
    }
}
