using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entidades;
using Negocio;

namespace Web_Integrador.Controllers
{
    [Authorize]
    public class TorreController : Controller
    {

        Sector_BS sector_BS = new Sector_BS();
        Sucursal_BS sucursal_BS = new Sucursal_BS();
        Torre_BS torre_BS = new Torre_BS();
        // GET: Torre
        public ActionResult Index()
        {
            List<Sucursal> listaSucursal = new List<Sucursal>();
            var resSucursal = sucursal_BS.lista(0);
            if (resSucursal.oHeader.estado)
            {
                listaSucursal = resSucursal.SucursalList;
            }
            return View(listaSucursal);
        }


        public JsonResult ListarTorres(int id_torre = 0)
        {
            Torre_Res torre_res = new Torre_Res();
            try
            {
                var rpta = torre_BS.Listar(id_torre);
                torre_res = rpta;
            }
            catch (Exception ex)
            {
                torre_res.oHeader.estado = false;
                torre_res.oHeader.mensaje = ex.Message;
            }
            return Json(torre_res, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RegistrarTorre(Torre enti)
        {
            Torre_Res torre_res = new Torre_Res();
            List<Torre> torre_List = new List<Torre>();
            DTOHeader oHeader = new DTOHeader();
            try
            {
                var rpta = torre_BS.Register(enti);
                oHeader = rpta.oHeader;
                if (rpta.oHeader.estado)
                {
                    var getTorre = torre_BS.Listar(rpta.id_register);
                    if (getTorre.oHeader.estado)
                    {
                        torre_List = getTorre.TorreList;
                    }
                }
            }
            catch (Exception ex)
            {
                torre_res.oHeader.estado = false;
                torre_res.oHeader.mensaje = ex.Message;
            }
            torre_res.oHeader = oHeader;
            torre_res.TorreList = torre_List;


            return Json(torre_res, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarSector(int id_sucursal = 0)
        {
            Sector_Res sector_res = new Sector_Res();
            try
            {
                var rpta = sector_BS.lista(0);
                if (rpta.oHeader.estado)
                {
                    var lista = rpta.SectorList;
                    var listaSector = lista.Where(x => x.id_sucursal == id_sucursal).ToList();
                    sector_res.oHeader = rpta.oHeader;
                    sector_res.SectorList = listaSector;
                }
            }
            catch (Exception ex)
            {
                sector_res.oHeader.estado = false;
                sector_res.oHeader.mensaje = ex.Message;
            }
            return Json(sector_res, JsonRequestBehavior.AllowGet);
        }

    }
}