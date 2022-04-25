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
    public class TipoController : Controller
    {
        Tipo_BS tipo_BS = new Tipo_BS();
        // GET: Tipo
        [PermisosRol(Roles.AgenteVisitas)]
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult ListarTipos(int id_tipo = 0)
        {
            Tipo_res tipo_Res = new Tipo_res();
            try
            {
                var rpta = tipo_BS.lista_Tipos(id_tipo);
                tipo_Res = rpta;
            }
            catch (Exception ex)
            {
                tipo_Res.oHeader.estado = false;
                tipo_Res.oHeader.mensaje = ex.Message;
            }
            return Json(tipo_Res, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RegistrarTipo(Tipo tipo)
        {
            Tipo_res tipo_Res = new Tipo_res();
            List<Tipo> tipo_List = new List<Tipo>();
            DTOHeader oHeader = new DTOHeader();
            try
            {
                var rpta = tipo_BS.Registrar_Tipo(tipo);
                oHeader = rpta.oHeader;
                if (rpta.oHeader.estado)
                {
                    var getTipo = tipo_BS.lista_Tipos(rpta.id_registrar);
                    if (getTipo.oHeader.estado)
                    {
                        tipo_List = getTipo.TipoList;
                    }
                }
            }
            catch (Exception ex)
            {
                tipo_Res.oHeader.estado = false;
                tipo_Res.oHeader.mensaje = ex.Message;
            }
            tipo_Res.oHeader = oHeader;
            tipo_Res.TipoList = tipo_List;


            return Json(tipo_Res, JsonRequestBehavior.AllowGet);
        }
    }
}