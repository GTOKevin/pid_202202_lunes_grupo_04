using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entidades;
using Negocio;
using Helpers;
using Web_Integrador.Security;

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

        public JsonResult ListarPerfil()
        {
            Perfil_res pr = new Perfil_res();

            int id = Session["PI_IDPERFIL"].ToInt();

            try
            {
                var rpta = pb.Listar_Perfil_Usuario(id);
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