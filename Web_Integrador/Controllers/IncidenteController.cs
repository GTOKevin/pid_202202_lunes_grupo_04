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

    public class IncidenteController : Controller
    {
        // GET: Incidente

        Incidente_BS incidente_BS = new Incidente_BS();


        [PermisosRol(Helpers.Roles.Administrador)]

        public ActionResult Index()
        {

            return View();
        }
        public ActionResult ListarIncidentes()
        {
            Incidente_Res incidente_Res = new Incidente_Res();
            try
            {
                var rpta = incidente_BS.lista();
                incidente_Res = rpta;
            }
            catch (Exception ex)
            {
                incidente_Res.oHeader.estado = false;
                incidente_Res.oHeader.mensaje = ex.Message;
            }
            return Json(incidente_Res, JsonRequestBehavior.AllowGet);
        }
        public JsonResult RegistrarInci(Incidente tipo)
        {
            Incidente_Res incidente_Res = new Incidente_Res();
            List<Incidente> incidente_List = new List<Incidente>();
            DTOHeader oHeader = new DTOHeader();
            try
            {
                var rpta = incidente_BS.Registrar(tipo);
                oHeader = rpta.oHeader;
                if (rpta.oHeader.estado)
                {
                    var getIncidente = incidente_BS.lista();
                    if (getIncidente.oHeader.estado)
                    {
                        incidente_List = getIncidente.lista_Incidente;
                    }
                }
            }
            catch (Exception ex)
            {
                incidente_Res.oHeader.estado = false;
                incidente_Res.oHeader.mensaje = ex.Message;
            }
            incidente_Res.oHeader = oHeader;
            incidente_Res.lista_Incidente = incidente_List;


            return Json(incidente_Res, JsonRequestBehavior.AllowGet);
        }

    }
}