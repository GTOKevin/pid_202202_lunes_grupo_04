using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class Tipo_BS
    {
        public Tipo_res lista_Tipos(int id_tipo)
        {
            return new Tipo_DA().Listar_Tipos(id_tipo);
        }

        public Tipo_Register Registrar_Tipo(Tipo tip)
        {
            return new Tipo_DA().Registrar_Tipo(tip);
        }

       /* public Tipo_res Actualizar_Tipo(int id, Tipo tipo)
        {
            return new Tipo_DA().Actualizar_Tipo(id,tipo);
        }*/
        public Tipo_res Eliminar_Tipo(int id)
        {
            return new Tipo_DA().Eliminar_Tipo(id);
        }
    }
}
