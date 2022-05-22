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
    public class ServicioController : Controller
    {
        Servicio_BS servicio_BS = new Servicio_BS();
        Departamento_BS departamento_BS = new Departamento_BS();
        Torre_BS torre_BS = new Torre_BS();
        Sector_BS sector_BS = new Sector_BS();
        Sucursal_BS sucursal_BS = new Sucursal_BS();
        Tipo_BS tipo_BS = new Tipo_BS();
        // GET: Servicio
        [PermisosRol(Roles.AgenteVisitas)]
        public ActionResult Index()
        {
            ServicioModel oModeView = new ServicioModel();
            var sucursal = sucursal_BS.lista(0);
            var tipo = tipo_BS.lista_Tipos(0);
            if (sucursal.oHeader.estado)
            {
                oModeView.Sucursales = sucursal.SucursalList;
            }
            if (tipo.oHeader.estado)
            {
                oModeView.Tipos = tipo.TipoList;
            }
            return View(oModeView);
        }
        public JsonResult ListarServicios(int id_servicio = 0)
        {
            Servicio_Res servicio_Res = new Servicio_Res();
            try
            {
                var rpta = servicio_BS.lista(id_servicio);
                servicio_Res = rpta;
            }
            catch (Exception ex)
            {
                servicio_Res.oHeader.estado = false;
                servicio_Res.oHeader.mensaje = ex.Message;
            }
            return Json(servicio_Res, JsonRequestBehavior.AllowGet);
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
        public JsonResult RegistrarServicio(Servicio enti)
        {
            Servicio_Res servicio_Res = new Servicio_Res();
            List<Servicio> servicio_List = new List<Servicio>();
            DTOHeader oHeader = new DTOHeader();
            try
            {
                var rpta = servicio_BS.Registrar(enti);
                oHeader = rpta.oHeader;
                if (rpta.oHeader.estado)
                {
                    var getServicio = servicio_BS.lista(rpta.id_register);
                    if (getServicio.oHeader.estado)
                    {
                        servicio_List = getServicio.ServicioList;
                    }
                }
            }
            catch (Exception ex)
            {
                servicio_Res.oHeader.estado = false;
                servicio_Res.oHeader.mensaje = ex.Message;
            }
            servicio_Res.oHeader = oHeader;
            servicio_Res.ServicioList = servicio_List;


            return Json(servicio_Res, JsonRequestBehavior.AllowGet);
        }
    }
}