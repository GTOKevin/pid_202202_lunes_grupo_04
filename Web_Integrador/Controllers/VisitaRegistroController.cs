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
    public class VisitaRegistroController : Controller
    {
        VISITAREG_BS visitareg_BS = new VISITAREG_BS();
        // GET: Sucursal
        [PermisosRol(Roles.AgenteVisitas)]

        public ActionResult Index()
        {
            return View();
        }
        public JsonResult ListarVisitasReg(int id_visita_registro = 0)
        {
            VISITAREGISTRO_Res visitareg_Res = new VISITAREGISTRO_Res();
            try
            {
                var rpta = visitareg_BS.lista(id_visita_registro);
                visitareg_Res = rpta;
                
            }
            catch (Exception ex)
            {
                visitareg_Res.oHeader.estado = false;
                visitareg_Res.oHeader.mensaje = ex.Message;
            }
            return Json(visitareg_Res, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RegistrarVisitasReg(VISITA_REGISTRO visreg)
        {
            VISITAREGISTRO_Res visreg_Res = new VISITAREGISTRO_Res();
            List<VISITA_REGISTRO> visreg_List = new List<VISITA_REGISTRO>();
            DTOHeader oHeader = new DTOHeader();
            try
            {
                var rpta = visitareg_BS.Registrar(visreg); 
                oHeader = rpta.oHeader;
                if (rpta.oHeader.estado)
                {
                    var getVisitaReg = visitareg_BS.lista(rpta.id_register);
                    if (getVisitaReg.oHeader.estado)
                    {
                        visreg_List = getVisitaReg.VisitaRegistroList;
                    }
                }
            }
            catch (Exception ex)
            {
                visreg_Res.oHeader.estado = false;
                visreg_Res.oHeader.mensaje = ex.Message;
            }
            visreg_Res.oHeader = oHeader;
            visreg_Res.VisitaRegistroList = visreg_List;


            return Json(visreg_Res, JsonRequestBehavior.AllowGet);
        }
    }
}
    
