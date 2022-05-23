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
        public Departamento_File_Res lista(int id_departamento_file)
        {
            return new Departamento_File_DA().Listar(id_departamento_file);
        }
        public DepartamentoFile_Register Registrar(Departamento_File depf)
        {
            return new Departamento_File_DA().Registrar(depf);
        }
        /* public DTOHeader Actualizar(Departamento_File depf)
         {
             return new Departamento_File_DA().Actualizar(depf);
         }*/
        public Departamento_File_Res listaIDDepartamentofile(int id_dep)
        {
            return new Departamento_File_DA().ListarFileIDDep(id_dep);
        }
    }
}
