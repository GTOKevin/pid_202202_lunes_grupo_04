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
        Servicio_BS servicio_BS = new Servicio_BS();

        // GET: Recibo
        [PermisosRol(Roles.AgenteVisitas)]
        public ActionResult Index()
        {
            List<Servicio> listaServicio = new List<Servicio>();
            var resServicio = servicio_BS.lista(0);
            if (resServicio.oHeader.estado)
            {
                listaServicio = resServicio.ServicioList;
            }
            return View(listaServicio);
        }

        public JsonResult ListarRecibos(int id_recibo = 0)
        {
            Recibo_Servicio_Res recibo_Res = new Recibo_Servicio_Res();
            try
            {
                var rpta = recibo_BS.listar_servicio(id_recibo);
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
            Recibo_Servicio_Res recibo_res = new Recibo_Servicio_Res();
            List<Recibo_Servicio> recibo_List = new List<Recibo_Servicio>();
            DTOHeader oHeader = new DTOHeader();
            try
            {
                var rpta = recibo_BS.Registrar(enti);
                oHeader = rpta.oHeader;
                if (rpta.oHeader.estado)
                {
                    var getRecibo = recibo_BS.listar_servicio(rpta.id_register);
                    if (getRecibo.oHeader.estado)
                    {
                        recibo_List = getRecibo.ReciboList;
                    }
                }
            }
            catch (Exception ex)
            {
                recibo_res.oHeader.estado = false;
                recibo_res.oHeader.mensaje = ex.Message;
            }
            recibo_res.oHeader = oHeader;
            recibo_res.ReciboList = recibo_List;


            return Json(recibo_res, JsonRequestBehavior.AllowGet);
        }
    }
}