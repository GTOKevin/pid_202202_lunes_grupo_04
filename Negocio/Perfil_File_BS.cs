using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class Perfil_File_BS
    {
        public DTOHeader Registrar(Perfil_File pf)
        {
            return new Perfil_File_DA().Registrar(pf);
        }

        public Perfil_File_res ListarFile(int id)
        {
            return new Perfil_File_DA().ListarFile(id);
        }
    }
}
