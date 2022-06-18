using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;

namespace Negocio
{
    public class Incidente_Historial_BS
    {
        public Incidente_Historial_Res lista(int id_incidente_historial)
        {
            return new Incidente_Historial_DA().Listar(id_incidente_historial);
        }
        public DTOHeader Registrar(Incidente_Historial inci_historial)
        {
            return new Incidente_Historial_DA().Registrar(inci_historial);

        }
    }
}
