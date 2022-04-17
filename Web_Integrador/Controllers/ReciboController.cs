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
    public class ReciboController : Controller
    {
        Recibo_BS recibo_BS = new Recibo_BS();

        // GET: Recibo
        [PermisosRol(Roles.AgenteVisitas)]
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ListarRecibos(int id_recibo = 0)
        {
            Recibo_Res recibo_Res = new Recibo_Res();
            try
            {
                var rpta = recibo_BS.lista(id_recibo);
                recibo_Res = rpta;
            }
            catch (Exception ex)
            {
                recibo_Res.oHeader.estado = false;
                recibo_Res.oHeader.mensaje = ex.Message;
            }
            return Json(recibo_Res, JsonRequestBehavior.AllowGet);
        }
        public JsonResult RegistrarRecibo(Recibo enti)
        {
            Recibo_Res recibo_Res = new Recibo_Res();
            List<Recibo> recibo_List = new List<Recibo>();
            DTOHeader oHeader = new DTOHeader();
            try
            {
                var rpta = recibo_BS.Registrar(enti);
                oHeader = rpta.oHeader;
                if (rpta.oHeader.estado)
                {
                    var getRecibo = recibo_BS.lista(rpta.id_register);
                    if (getRecibo.oHeader.estado)
                    {
                        recibo_List = getRecibo.ReciboList;
                    }
                }
            }
            catch (Exception ex)
            {
                recibo_Res.oHeader.estado = false;
                recibo_Res.oHeader.mensaje = ex.Message;
            }
            recibo_Res.oHeader = oHeader;
            recibo_Res.ReciboList = recibo_List;


            return Json(recibo_Res, JsonRequestBehavior.AllowGet);
        }
    }
}