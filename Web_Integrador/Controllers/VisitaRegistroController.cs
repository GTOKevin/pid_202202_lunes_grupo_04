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
        Tipo_BS tb = new Tipo_BS();




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

            var dataGenero = tb.Listar_TipoUtil("genero");
            var dataDocumento = tb.Listar_TipoUtil("documento");
            ViewBag.lstgenero = dataGenero.TipoList.ToList();
            ViewBag.lstdocumento = dataDocumento.TipoList.ToList();

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


        public JsonResult RegistrarVisitasRegSalida(VISITA_REGISTRO visreg)
        {
            VISITAREGISTRO_Res visreg_Res = new VISITAREGISTRO_Res();
            List<VISITA_REGISTRO> visreg_List = new List<VISITA_REGISTRO>();
            DTOHeader oHeader = new DTOHeader();
            try
            {
                var rpta = visitareg_BS.Registrar_Salida(visreg);
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

        public JsonResult RegistrarEntrada(VisitaVisitante enti)
        {
            VISITAREGISTROEntrada_Res visreg_Res = new VISITAREGISTROEntrada_Res();
            List<Visitante> visitanteList = new List<Visitante>();
            DTOHeader oHeader = new DTOHeader();
            var rpta = new VISITA_REGISTRO_Register();
            try
            {
                    foreach(var vv in enti.visitantes)
                    {
                        VISITA_REGISTRO prop = new VISITA_REGISTRO();
                        prop.id_visita_registro = 0;
                        prop.id_departamento = enti.id_departamento;
                        prop.id_visitante = vv.id_visitante;
                        rpta = visitante_BS.RegistrarEntradaUsuario(prop);
                    }
                    oHeader = rpta.oHeader;
                    if (rpta.oHeader.estado)
                    {
                        var getVisitaReg = visitante_BS.lista(rpta.id_register);
                        if (getVisitaReg.oHeader.estado)
                        {
                            visitanteList = getVisitaReg.VisitanteList;
                        }
                    }

            }
            catch (Exception ex)
            {
                visreg_Res.oHeader.estado = false;
                visreg_Res.oHeader.mensaje = ex.Message;
            }
            visreg_Res.oHeader = oHeader;
            visreg_Res.VisitaRegistroList = visitanteList;


            return Json(visreg_Res, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ListarVisitantesActPorDni( string nro_documento = "", int estado =1)
        {
            VISITAREGISTRO_Res visitante_Res = new VISITAREGISTRO_Res();
            try
            {
                var rpta = visitareg_BS.ListarVisitanteActXDNI(nro_documento, estado);
                visitante_Res = rpta;
            }
            catch (Exception ex)
            {
                visitante_Res.oHeader.estado = false;
                visitante_Res.oHeader.mensaje = ex.Message;
            }
            return Json(visitante_Res, JsonRequestBehavior.AllowGet);
        }
    }
}
    
