﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Negocio;
using Entidades;

namespace Web_Integrador.Controllers
{
    public class HomeController : BaseController
    {
        Usuario_BS user_bs = new Usuario_BS();
        Perfil_BS perfil_bs = new Perfil_BS();
        public ActionResult Index()
        {
            if (!ValidarAcceso())
            {
                return RedirectToAction("Login", "Auth");
            }


            return View();
        }



 
    }
}