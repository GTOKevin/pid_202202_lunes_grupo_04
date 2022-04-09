using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Negocio;

namespace Web_Integrador.Controllers
{
    public class HomeController : Controller
    {

        Sucursal_BS sucursal_BS = new Sucursal_BS();
        public ActionResult Index()
        {
            var datos = sucursal_BS.lista();

            return View();
        }


        public ActionResult Login()
        {
            return View();
        }

  
    }
}