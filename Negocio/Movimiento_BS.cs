using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;


namespace Negocio
{
    public class Movimiento_BS
    {
        public Movimiento_Res lista(int id_movimiento)
        {
            return new Movimiento_DA().Listar( id_movimiento);
        }
        public Movimiento_Register Registrar(Movimiento mov)
        {
            return new Movimiento_DA().Registrar(mov);
        }
        
    }
}
