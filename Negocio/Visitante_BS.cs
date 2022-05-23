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
        //FILTROXDNI
        public Visitante_Res listaXDni(string nro_documento)
        {
            return new Visitante_DA().ListarXDni(nro_documento);
        }


        public Visitante_Register Registrar(Visitante vis)
        {
            return new Visitante_DA().Registrar(vis);
        }

        public VISITA_REGISTRO_Register RegistrarEntradaUsuario(VISITA_REGISTRO visreg)
        {
            return new Visitante_DA().RegistrarEntradaUsuario(visreg);
        }


    }
}
