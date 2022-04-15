using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Negocio;
using Entidades;

namespace Web_Integrador.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        Usuario_BS user_bs = new Usuario_BS();
        Perfil_BS perfil_bs = new Perfil_BS();
        public ActionResult Index()
        {
            return View();
        }



 
    }
}