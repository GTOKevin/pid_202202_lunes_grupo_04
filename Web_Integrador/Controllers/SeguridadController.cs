using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entidades;
using Negocio;

namespace Web_Integrador.Controllers
{
    public class SeguridadController : BaseController
    {
        Perfil_BS perfil_BS = new Perfil_BS();
        // GET: Seguridad
        public ActionResult Index()
        {
            if (!ValidarAcceso())
            {
                return RedirectToAction("Login", "Auth");
            }

            var perfil = ((Usuario_Login)Session["PI_USUARIO"]).perfil;



            return View(perfil);
        }
    }
}