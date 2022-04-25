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
    public class VisitanteController : Controller
    {
        Visitante_BS visitante_BS = new Visitante_BS();
        Tipo_BS tipo_BS = new Tipo_BS();
        // GET: Sucursal
        [PermisosRol(Roles.AgenteVisitas)]

        public ActionResult Index()
        {
            var tiposView = tipo_BS.lista_Tipos(0);

            var listaTipos = tiposView.TipoList;
            return View(listaTipos);
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

        public JsonResult RegistrarVisitante(Visitante visi)
        {
            Visitante_Res visitante_Res = new Visitante_Res();
            List<Visitante> vicitante_List = new List<Visitante>();
            DTOHeader oHeader = new DTOHeader();
            try
            {
                var rpta = visitante_BS.Registrar(visi);
                oHeader = rpta.oHeader;
                if (rpta.oHeader.estado)
                {
                    var getVisitante = visitante_BS.lista(rpta.id_register);
                    if (getVisitante.oHeader.estado)
                    {
                        vicitante_List = getVisitante.VisitanteList;
                    }
                }
            }
            catch (Exception ex)
            {
                visitante_Res.oHeader.estado = false;
                visitante_Res.oHeader.mensaje = ex.Message;
            }
            visitante_Res.oHeader = oHeader;
            visitante_Res.VisitanteList = vicitante_List;


            return Json(visitante_Res, JsonRequestBehavior.AllowGet);
        
    
}
    }
}