using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entidades;
using Negocio;
using Web_Integrador.Security;


namespace Web_Integrador.Controllers
{
    [Authorize]

    public class IncidenteController : Controller
    {
        // GET: Incidente

        Incidente_BS incidente_BS = new Incidente_BS();
        Sucursal_BS sucursal_BS = new Sucursal_BS();
        Sector_BS sector_BS = new Sector_BS();
        Torre_BS torre_BS = new Torre_BS();
        Departamento_BS departamento_BS = new Departamento_BS();
        Tipo_BS tb = new Tipo_BS();


        [PermisosRol(Helpers.Roles.Administrador)]

        public ActionResult Index()
        {
            ViewBag.ListSucursal = sucursal_BS.lista(0).SucursalList.ToList();
            var dataDocumento = tb.Listar_TipoUtil("documento");
            ViewBag.lstdocumento = dataDocumento.TipoList.ToList();

            return View();
        }

        public JsonResult GetDepartamento(FiltroDepa filtro)
        {
            var departamento = departamento_BS.FiltroDepartamento(filtro);
            return Json(departamento, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListarIncidentes(FiltroIncidente filtro)
        {
            filtro.nro_documento_f = filtro.nro_documento_f == null ? "" : filtro.nro_documento_f;
            filtro.estado_f = filtro.estado_f == null ? "3" : filtro.estado_f;
            filtro.nombre_reportado_f = filtro.nombre_reportado_f == null ? "" : filtro.nombre_reportado_f;

            Incidente_Res incidente_Res = new Incidente_Res();
            try
            {
                var rpta = incidente_BS.lista(filtro);
                incidente_Res = rpta;
            }
            catch (Exception ex)
            {
                incidente_Res.oHeader.estado = false;
                incidente_Res.oHeader.mensaje = ex.Message;
            }
            return Json(incidente_Res, JsonRequestBehavior.AllowGet);
        }


        public JsonResult RegistrarInci(Incidente tipo)
        {
            Incidente_Res incidente_Res = new Incidente_Res();
            List<IncidenteDTO> incidente_List = new List<IncidenteDTO>();
            DTOHeader oHeader = new DTOHeader();
            tipo.id_usuario = (int)Session["PI_IDUSER"];
            try
            {
                var rpta = incidente_BS.Registrar(tipo);
                oHeader = new DTOHeader { estado = rpta.estado, mensaje = rpta.mensaje };
            }
            catch (Exception ex)
            {
                incidente_Res.oHeader.estado = false;
                incidente_Res.oHeader.mensaje = ex.Message;
            }
            incidente_Res.oHeader = oHeader;
            incidente_Res.lista_Incidente = incidente_List;


            return Json(incidente_Res, JsonRequestBehavior.AllowGet);
        }

    }
}