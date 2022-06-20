using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entidades;
using Negocio;
using Helpers;
using Web_Integrador.Security;
using System.IO;

namespace Web_Integrador.Controllers
{
    [Authorize]
    public class SeguridadController : Controller
    {
        Perfil_BS pb = new Perfil_BS();
        // GET: Seguridad
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Perfil_File pf)
        {
            Usuario_BS usuario_BS = new Usuario_BS();
            Perfil_File_BS perfil_File_BS = new Perfil_File_BS();
            Usuario general = new Usuario();
            String ruta = "";
            Perfil_File_res pfr = new Perfil_File_res();
            List<Perfil_File> flist = new List<Perfil_File>();
            var data = usuario_BS.ListarUsuarioPorIDPerfil(pf.id_perfil);
            general = data.UsuarioList.FirstOrDefault();

            ruta = Server.MapPath(@"/Assets/img/avatars/"+ general.id_usuario.ToString() + @"/" + general.username.ToString() + @"/" + pf.id_perfil.ToString());

            try
            {
                bool Existe;
                Existe = Directory.Exists(ruta);
                if (!Existe)
                {
                    Directory.CreateDirectory(ruta);
                    string fileName = Path.GetFileNameWithoutExtension(pf.ImagenFile.FileName);
                    string extension = Path.GetExtension(pf.ImagenFile.FileName);
                    fileName = fileName + extension;
                    pf.nombrefile = @"/Assets/img/avatars/" + general.id_usuario.ToString() + @"/" + general.username.ToString() + @"/" + pf.id_perfil.ToString() + @"/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/Assets/img/avatars/" + general.id_usuario.ToString() + "/" + general.username.ToString() + "/" + pf.id_perfil.ToString() + "/"), fileName);
                    pf.ImagenFile.SaveAs(fileName);

                    perfil_File_BS.Registrar(pf);

                 
                    Session["PI_FILE"] = pf.nombrefile;
                }
                else
                {
                    Directory.Delete(ruta,true);
                    Directory.CreateDirectory(ruta);
                    string fileName = Path.GetFileNameWithoutExtension(pf.ImagenFile.FileName);
                    string extension = Path.GetExtension(pf.ImagenFile.FileName);
                    fileName = fileName + extension;
                    pf.nombrefile = @"/Assets/img/avatars/" + general.id_usuario.ToString() + @"/" + general.username.ToString() + @"/" + pf.id_perfil.ToString() + @"/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/Assets/img/avatars/" + general.id_usuario.ToString() + "/" + general.username.ToString() + "/" + pf.id_perfil.ToString() + "/"), fileName);
                    pf.ImagenFile.SaveAs(fileName);
                    perfil_File_BS.Registrar(pf);

                    Session["PI_FILE"] = pf.nombrefile;
                }

            }
            catch (Exception ex )
            {
                ViewBag.ErrorMessage = "Hubo un Error al cambiar su foto:" + ex.Message;

                
            }
            return Redirect("Index");
        }

        public JsonResult ListarPerfil()
        {
            Perfil_res pr = new Perfil_res();

            int id = Session["PI_IDPERFIL"].ToInt();

            try
            {
                var rpta = pb.Listar_PerfilData(id);
                pr = rpta;
            }
            catch (Exception ex)
            {
                pr.oHeader.estado = false;
                pr.oHeader.mensaje = ex.Message;
            }
            return Json(pr, JsonRequestBehavior.AllowGet);
        }


    }
}