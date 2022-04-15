using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Helpers;
using Entidades;

namespace Web_Integrador.Security
{
    public class PermisosRolAttribute:ActionFilterAttribute
    {
        private Roles idRol;



        public PermisosRolAttribute(Roles _idRol)
        {
            idRol = _idRol; 
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["PI_USUARIO"] != null)
            {
                Usuario_Login userlog = HttpContext.Current.Session["PI_USUARIO"] as Usuario_Login;
                if(userlog.usuario.id_rol != Roles.Administrador)
                {
                    if (userlog.usuario.id_rol != this.idRol)
                    {
                        filterContext.Result = new RedirectResult("~/Auth/Blocked");
                    }
                }
               
            }
            base.OnActionExecuting(filterContext);
        }
    }
}