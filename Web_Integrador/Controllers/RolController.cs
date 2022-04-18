using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Negocio;
using Entidades;
using Helpers;
using Web_Integrador.Security;

namespace Web_Integrador.Controllers
{
    [Authorize]
    public class RolController : Controller
    {
        Rol_BS Rol_BS = new Rol_BS();
        // GET: Rol
        [PermisosRol(Roles.Administrador)]
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ListarRol(int id_rol = 0)
        {
            Rol_Res rol_Res = new Rol_Res();
            try
            {
                var rpta = Rol_BS.lista(id_rol);
                rol_Res = rpta;
            }
            catch (Exception ex)
            {
                rol_Res.oHeader.estado = false;
                rol_Res.oHeader.mensaje = ex.Message;
            }
            return Json(rol_Res, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RegistrarRol(Rol ro)
        {
            Rol_Res rol_Res = new Rol_Res();
            List<Rol> rol_List = new List<Rol>();
            DTOHeader oHeader = new DTOHeader();
            try
            {
                var rpta = Rol_BS.Registrar(ro);
                oHeader = rpta.oHeader;
                if (rpta.oHeader.estado)
                {
                    var getRol = Rol_BS.lista(rpta.id_registrar);
                    if (getRol.oHeader.estado)
                    {
                        rol_List = getRol.RolList;
                    }
                }
            }
            catch (Exception ex)
            {
                rol_Res.oHeader.estado = false;
                rol_Res.oHeader.mensaje = ex.Message;
            }
            rol_Res.oHeader = oHeader;
            rol_Res.RolList = rol_List;


            return Json(rol_Res, JsonRequestBehavior.AllowGet);
        }
    }
}