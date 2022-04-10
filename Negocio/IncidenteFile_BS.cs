using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;

namespace Negocio
{
   public class IncidenteFile_BS
    {
        public IncidenteFile_Res lista()
        {
            return new IncidenteFile_DA().Listar();
        }
    }
}
