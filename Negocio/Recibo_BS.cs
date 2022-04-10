using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;


namespace Negocio
{
    public class Recibo_BS
    {
        public Recibo_Res lista()
        {
            return new Recibo_DA().Listar();
        }
    }
}
