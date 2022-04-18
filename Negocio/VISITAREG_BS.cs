using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;



namespace Negocio
{
    public class VISITAREG_BS
    {
        public VISITAREGISTRO_Res lista(int id_visita_registro)
        {
            return new VISITA_REGISTRO_DA().Listar(id_visita_registro);
        }

        public VISITA_REGISTRO_Register Registrar(VISITA_REGISTRO vis)
        {
            return new VISITA_REGISTRO_DA().Registrar(vis); 
        }

    }
}
