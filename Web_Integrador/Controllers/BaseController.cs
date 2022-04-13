using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web_Integrador.Controllers
{
    public abstract class BaseController : Controller
    {

        public bool ValidarAcceso()
        {
            bool validarAcceso = false;
            try
            {
                if (Session["PI_USUARIO"]!=null)
                {
                    validarAcceso=true;
                }
            }catch (Exception ex)
            {
                validarAcceso = false;
            }

            return validarAcceso;


        }
        public ActionResult VerificarSesion(HttpSessionStateBase sesion)
        {
            try
            {
                ActionResult result = null;

                bool sesionActiva = sesion.Count > 0;

                if (!sesionActiva)
                    result = RedirectToAction("logout", "Home");

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}