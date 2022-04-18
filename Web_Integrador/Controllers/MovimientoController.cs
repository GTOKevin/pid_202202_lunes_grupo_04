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
    public class MovimientoController : Controller
    {
        Movimiento_BS movimiento_BS = new Movimiento_BS();

        [PermisosRol(Roles.AgenteVisitas)]

        public ActionResult Index()
        {
            return View();
        }
        public JsonResult ListarMovimientos(int id_movimiento = 0)
        {
            Movimiento_Res movimiento_Res = new Movimiento_Res();
            try
            {
                var rpta = movimiento_BS.lista(id_movimiento);
                movimiento_Res = rpta;
            }
            catch (Exception ex)
            {
                movimiento_Res.oHeader.estado = false;
                movimiento_Res.oHeader.mensaje = ex.Message;
            }
            return Json(movimiento_Res, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RegistrarMovimiento(Movimiento movi)
        {
            Movimiento_Res movimiento_Res = new Movimiento_Res();
            List<Movimiento> movimiento_List = new List<Movimiento>();
            DTOHeader oHeader = new DTOHeader();
            try
            {
                var rpta = movimiento_BS.Registrar(movi);
                oHeader = rpta.oHeader;
                if (rpta.oHeader.estado)
                {
                    var getMovimiento = movimiento_BS.lista(rpta.id_register); 
                    if (getMovimiento.oHeader.estado)
                    {
                        movimiento_List = getMovimiento.MovimientoList;
                    }
                }
            }
            catch (Exception ex)
            {
                movimiento_Res.oHeader.estado = false;
                movimiento_Res.oHeader.mensaje = ex.Message;
            }
            movimiento_Res.oHeader = oHeader;
            movimiento_Res.MovimientoList = movimiento_List;


            return Json(movimiento_Res, JsonRequestBehavior.AllowGet);
        
    }
}
}