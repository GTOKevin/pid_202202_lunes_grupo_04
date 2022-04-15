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
        Perfil_BS perfil_BS = new Perfil_BS();
        // GET: Seguridad
        [PermisosRol(Roles.Administrador)]
        public ActionResult Index()
        {
            var perfil = ((Usuario_Login)Session["PI_USUARIO"]).perfil;

            return View(perfil);
        }
    }
}