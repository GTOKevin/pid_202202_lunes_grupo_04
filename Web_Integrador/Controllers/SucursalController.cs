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
    public class SucursalController : Controller
    {
        Sucursal_BS sucursal_BS = new Sucursal_BS();
        // GET: Sucursal
        [PermisosRol(Roles.AgenteVisitas)]
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ListarSucursales(int id_sucursal=0)
        {
            Sucursal_Res sucursal_Res = new Sucursal_Res();
            try
            {
                var rpta = sucursal_BS.lista(id_sucursal);
                sucursal_Res = rpta;                
            }
            catch (Exception ex)
            {
                sucursal_Res.oHeader.estado = false;
                sucursal_Res.oHeader.mensaje = ex.Message;
            }
            return Json(sucursal_Res,JsonRequestBehavior.AllowGet);
        }

        public JsonResult RegistrarSucursal(Sucursal enti)
        {
            Sucursal_Res sucursal_Res = new Sucursal_Res();
            List<Sucursal> sucursal_List = new List<Sucursal>();
            DTOHeader oHeader = new DTOHeader();
            try
            {
                var rpta = sucursal_BS.Registrar(enti);
                oHeader = rpta.oHeader;
                if (rpta.oHeader.estado)
                {
                    var getSucursal = sucursal_BS.lista(rpta.id_register);
                    if (getSucursal.oHeader.estado)
                    {
                        sucursal_List = getSucursal.SucursalList;
                    }
                }
            }
            catch (Exception ex)
            {
                sucursal_Res.oHeader.estado = false;
                sucursal_Res.oHeader.mensaje = ex.Message;
            }
            sucursal_Res.oHeader = oHeader;
            sucursal_Res.SucursalList = sucursal_List;
           

            return Json(sucursal_Res, JsonRequestBehavior.AllowGet);
        }
    }
}