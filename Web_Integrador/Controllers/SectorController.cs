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
    public class SectorController : Controller
    {


        Sector_BS sector_BS = new Sector_BS();
        Sucursal_BS sucursal_BS = new Sucursal_BS();    
        // GET: Sector
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


        public JsonResult ListarSectores(int id_sector = 0)
        {
            Sector_Suc_Res sector_res = new Sector_Suc_Res();
            try
            {
                var rpta = sector_BS.listar_suc(id_sector);
                sector_res = rpta;
            }
            catch (Exception ex)
            {
                sector_res.oHeader.estado = false;
                sector_res.oHeader.mensaje = ex.Message;
            }
            return Json(sector_res, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RegistrarSector(Sector enti)
        {
            Sector_Suc_Res sector_res = new Sector_Suc_Res();
            List<Sector_Suc> sector_List = new List<Sector_Suc>();
            DTOHeader oHeader = new DTOHeader();
            try
            {
                var rpta = sector_BS.Registrar(enti);
                oHeader = rpta.oHeader;
                if (rpta.oHeader.estado)
                {
                    var getSector = sector_BS.listar_suc(rpta.id_register);
                    if (getSector.oHeader.estado)
                    {
                        sector_List = getSector.SectorList;
                    }
                }
            }
            catch (Exception ex)
            {
                sector_res.oHeader.estado = false;
                sector_res.oHeader.mensaje = ex.Message;
            }
            sector_res.oHeader = oHeader;
            sector_res.SectorList = sector_List;


            return Json(sector_res, JsonRequestBehavior.AllowGet);
        }

    }
}