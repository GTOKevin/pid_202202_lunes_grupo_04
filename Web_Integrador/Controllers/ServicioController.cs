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
    public class ServicioController : Controller
    {
        Servicio_BS servicio_BS = new Servicio_BS();
        // GET: Servicio
        [PermisosRol(Roles.AgenteVisitas)]
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult ListarServicios(int id_servicio = 0)
        {
            Servicio_Res servicio_Res = new Servicio_Res();
            try
            {
                var rpta = servicio_BS.lista(id_servicio);
                servicio_Res = rpta;
            }
            catch (Exception ex)
            {
                servicio_Res.oHeader.estado = false;
                servicio_Res.oHeader.mensaje = ex.Message;
            }
            return Json(servicio_Res, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RegistrarServicio(Servicio enti)
        {
            Servicio_Res servicio_Res = new Servicio_Res();
            List<Servicio> servicio_List = new List<Servicio>();
            DTOHeader oHeader = new DTOHeader();
            try
            {
                var rpta = servicio_BS.Registrar(enti);
                oHeader = rpta.oHeader;
                if (rpta.oHeader.estado)
                {
                    var getServicio = servicio_BS.lista(rpta.id_register);
                    if (getServicio.oHeader.estado)
                    {
                        servicio_List = getServicio.ServicioList;
                    }
                }
            }
            catch (Exception ex)
            {
                servicio_Res.oHeader.estado = false;
                servicio_Res.oHeader.mensaje = ex.Message;
            }
            servicio_Res.oHeader = oHeader;
            servicio_Res.ServicioList = servicio_List;


            return Json(servicio_Res, JsonRequestBehavior.AllowGet);
        }
    }
}