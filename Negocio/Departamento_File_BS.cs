using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;

namespace Negocio
{
    public class Departamento_File_BS
    {
        public Departamento_File_Res lista()
        {
            return new Departamento_File_DA().Listar();
        }
    }
}
