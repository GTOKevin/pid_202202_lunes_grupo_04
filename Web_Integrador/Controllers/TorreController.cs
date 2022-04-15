using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web_Integrador.Controllers
{
    [Authorize]
    public class TorreController : Controller
    {
        // GET: Torre
        public ActionResult Index()
        {
            return View();
        }
    }
}