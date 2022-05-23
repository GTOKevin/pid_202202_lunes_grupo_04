using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Negocio;
using Entidades;
using Helpers;
using Web_Integrador.Security;
using System.IO;

namespace Web_Integrador.Controllers
{
    
    [Authorize]
    public class DepartamentoFileController : Controller
    {
        Departamento_File_BS Departamento_File_BS = new Departamento_File_BS();
        Departamento_BS departamento_BS = new Departamento_BS();
        // GET: DepartamentoFile
        [PermisosRol(Roles.Administrador)]
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult ListarDepartamentosFile(int id_departamento_file = 0)
        {
            Departamento_File_Res departamentofile_Res = new Departamento_File_Res();
            try
            {
                var rpta = Departamento_File_BS.lista(id_departamento_file);
                departamentofile_Res = rpta;
            }
            catch (Exception ex)
            {
                departamentofile_Res.oHeader.estado = false;
                departamentofile_Res.oHeader.mensaje = ex.Message;
            }
            return Json(departamentofile_Res, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RegistrarDepartamentosFile(Departamento_File depf)
        {
            Departamento_File_Res departamentofile_Res = new Departamento_File_Res();
            List<Departamento_File> departamentofileList = new List<Departamento_File>();
            DTOHeader oHeader = new DTOHeader();
            try
            {

                var rpta = Departamento_File_BS.Registrar(depf);
                oHeader = rpta.oHeader;
                if (rpta.oHeader.estado)
                {
                    var getDepartamentoFile = Departamento_File_BS.lista(rpta.id_register);
                    if (getDepartamentoFile.oHeader.estado)
                    {
                       departamentofileList = getDepartamentoFile.DepartamentoFileList;
                    }
                    
                }
            }
            catch (Exception ex)
            {
                departamentofile_Res.oHeader.estado = false;
                departamentofile_Res.oHeader.mensaje = ex.Message;
               
            }
            departamentofile_Res.oHeader = oHeader;
            departamentofile_Res.DepartamentoFileList = departamentofileList;


            return Json(departamentofile_Res, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Index(Departamento_File departamento_File)
        {
            Departamento_BS departamento_BS = new Departamento_BS();
            
            var data = departamento_BS.lista(1);
            Departamento departamentos = new Departamento();
            departamentos = data.lista_Departamento.FirstOrDefault();

            String ruta = "";

                ruta = Server.MapPath(@"/Assets/Imagenes/"+departamentos.id_departamento.ToString()+@"/"+departamentos.numero.ToString());
            
            try
            {
                bool carpetaexiste;
                carpetaexiste = Directory.Exists(ruta);
                if(carpetaexiste == false)
                {

                    Directory.CreateDirectory(ruta);
                    string filename = Path.GetFileNameWithoutExtension(departamento_File.imagen.FileName);
                    string extension = Path.GetExtension(departamento_File.imagen.FileName);
                    filename = filename + extension;
                    departamento_File.url_imagen = @"/Assets/Imagenes/" + departamentos.id_departamento.ToString() + @"/" + departamentos.numero.ToString() + @"/" + filename;
                    filename = Path.Combine(Server.MapPath("~/Assets/Imagenes/" + departamentos.id_departamento.ToString() + "/" + departamentos.numero.ToString() + "/"), filename);
                    
                    departamento_File.imagen.SaveAs(filename);

                    List<Departamento_File> lstFile = new List<Departamento_File>();
                    var datafile = Departamento_File_BS.listaIDDepartamentofile(departamento_File.id_departamento);
                    lstFile = datafile.DepartamentoFileList.ToList();
                    Departamento_File_BS.Registrar(departamento_File);

                    ViewBag.listadata = lstFile;

                }

                else
                {
                    string filename = Path.GetFileNameWithoutExtension(departamento_File.imagen.FileName);
                    string extension = Path.GetExtension(departamento_File.imagen.FileName);
                    filename = filename + extension;
                    departamento_File.url_imagen = @"/Assets/Imagenes/" + departamentos.id_departamento.ToString() + @"/" + departamentos.numero.ToString() + @"/" + filename;
                    filename = Path.Combine(Server.MapPath("~/Assets/Imagenes/" + departamentos.id_departamento.ToString() + "/" + departamentos.numero.ToString() + "/"), filename);
                  
                    departamento_File.imagen.SaveAs(filename);
                    List<Departamento_File> lstFile = new List<Departamento_File>();
                    var datafile = Departamento_File_BS.listaIDDepartamentofile(departamento_File.id_departamento);
                    lstFile = datafile.DepartamentoFileList.ToList();
                    Departamento_File_BS.Registrar(departamento_File);

                    ViewBag.listadata = lstFile;

                }

            }

            catch(Exception ex)
            {
                ViewBag.ErrorMsg = "Error:" + ex;
            }

            return Redirect("Index");
        }

         public JsonResult ListarDepartamentos(int id_departamento = 0)
        {
            var departamento = departamento_BS.lista(id_departamento);
            return Json(departamento,JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarImagenes(int id_departamento = 0)
        {
            var departamento = Departamento_File_BS.listaIDDepartamentofile(id_departamento);
            return Json(departamento, JsonRequestBehavior.AllowGet);
        }












        /* protected void CargarArchivo_Click(object sender, EventArgs e)
         {
             Byte[] Archivo = null;
             string nombreArchivo = string.Empty;
             string extensionArchivo = string.Empty;
             if (fuArchivo.HasFile == true)
             {
                 using (BinaryReader reader = new
                 BinaryReader(fuArchivo.PostedFile.InputStream))
                 {
                     Archivo = reader.ReadBytes(fuArchivo.PostedFile.ContentLength);
                 }
                 nombreArchivo = Path.GetFileNameWithoutExtension(fuArchivo.FileName);
                 extensionArchivo = Path.GetExtension(fuArchivo.FileName);
         }    
         }*/


    }
}