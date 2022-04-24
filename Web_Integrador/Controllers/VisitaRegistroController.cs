using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Negocio;
using Entidades;
using Helpers;
using Web_Integrador.Security;
using Web_Integrador.Model;

namespace Web_Integrador.Controllers
{
    [Authorize]
    public class VisitaRegistroController : Controller
    {
        VISITAREG_BS visitareg_BS = new VISITAREG_BS();
        Departamento_BS departamento_BS = new Departamento_BS();
        Visitante_BS visitante_BS = new Visitante_BS();
        Torre_BS torre_BS = new Torre_BS();
        Sector_BS sector_BS = new Sector_BS();
        Sucursal_BS sucursal_BS = new Sucursal_BS();




        // GET: Sucursal
        [PermisosRol(Roles.AgenteVisitas)]

        public ActionResult Index()
        {
            VisitaRegistroModel oModoView = new VisitaRegistroModel();
            var sucursal = sucursal_BS.lista(0);
            var visitante = visitante_BS.lista(0);
            if (sucursal.oHeader.estado)
            {
                oModoView.Sucursales = sucursal.SucursalList;
            }
            if (visitante.oHeader.estado)
            {
                oModoView.Visitantes = visitante.VisitanteList;
            }
            
            return View(oModoView);
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
        public JsonResult ListarVisitantes(int id_visitante = 0)
        {
            Visitante_Res visitante_Res = new Visitante_Res();
            try
            {
                var rpta = visitante_BS.lista(id_visitante);
                visitante_Res = rpta;
            }
            catch (Exception ex)
            {
                visitante_Res.oHeader.estado = false;
                visitante_Res.oHeader.mensaje = ex.Message;
            }
            return Json(visitante_Res, JsonRequestBehavior.AllowGet);
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


        public JsonResult ListarDepartamentos(int id_departamento = 0)
        {
            var departamento = departamento_BS.lista(id_departamento);
            return Json(departamento, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ListarTorre(int id_sector = 0)
        {
            Torre_Res torre_Res = new Torre_Res();
            try
            {
                var rpta = torre_BS.Listar(0);
                if (rpta.oHeader.estado)
                {
                    var lista = rpta.TorreList;
                    var listaTorre = lista.Where(x => x.id_sector == id_sector).ToList();
                    torre_Res.oHeader = rpta.oHeader;
                    torre_Res.TorreList = listaTorre;
                }
                else
                {
                    torre_Res = rpta;
                }
            }
            catch (Exception ex)
            {
                torre_Res.oHeader.estado = false;
                torre_Res.oHeader.mensaje = ex.Message;
            }

            return Json(torre_Res, JsonRequestBehavior.AllowGet);

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
    
