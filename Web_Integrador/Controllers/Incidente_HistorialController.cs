using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entidades;
using Negocio;
using Web_Integrador.Security;

namespace Web_Integrador.Controllers
{
    [Authorize]
    public class Incidente_HistorialController : Controller
    {
        Incidente_Historial_BS incidente_historial_BS = new Incidente_Historial_BS();
        [PermisosRol(Helpers.Roles.Administrador)]
        // GET: Incidente_Historial
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ListarIncidentesHistoriales(int incidente_historial=0)
        {
            Incidente_Historial_Res incidente_historial_Res = new Incidente_Historial_Res();
            try
            {
                var rpta = incidente_historial_BS.lista(incidente_historial);
                incidente_historial_Res = rpta;
            }
            catch (Exception ex)
            {
                incidente_historial_Res.oHeader.estado = false;
                incidente_historial_Res.oHeader.mensaje = ex.Message;
            }
            return Json(incidente_historial_Res, JsonRequestBehavior.AllowGet);
        }
        public JsonResult RegistrarIncidenteHistorial(Incidente_Historial incidente_historial)
        {
            Incidente_Historial_Res incidente_historial_Res = new Incidente_Historial_Res();
            List<Incidente_Historial> incidente_historial_List = new List<Incidente_Historial>();
            DTOHeader oHeader = new DTOHeader();
            try
            {
                var rpta = incidente_historial_BS.Registrar(incidente_historial);
                oHeader = new DTOHeader {  estado= rpta.estado , mensaje = rpta.mensaje};
          
            }
            catch (Exception ex)
            {
                incidente_historial_Res.oHeader.estado = false;
                incidente_historial_Res.oHeader.mensaje = ex.Message;
            }
            incidente_historial_Res.oHeader = oHeader;
            incidente_historial_Res.lista_Incidente_Historial = incidente_historial_List;


            return Json(incidente_historial_Res, JsonRequestBehavior.AllowGet);
        }
    }
}