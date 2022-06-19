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
    public class ReciboController : Controller
    {
        Recibo_BS recibo_BS = new Recibo_BS();
        Servicio_BS servicio_BS = new Servicio_BS();
        Departamento_BS departamento_BS = new Departamento_BS();
        Torre_BS torre_BS = new Torre_BS();
        Sector_BS sector_BS = new Sector_BS();
        Sucursal_BS sucursal_BS = new Sucursal_BS();

        // GET: Recibo
        [PermisosRol(Roles.AgenteVisitas)]
        public ActionResult Index()
        {
            ReciboModel oModeView = new ReciboModel();
            var sucursal = sucursal_BS.lista(0);
            if (sucursal.oHeader.estado)
            {
                oModeView.Sucursales = sucursal.SucursalList;
            }
            return View(oModeView);
        }

        public JsonResult ListarRecibos()
        {
            Recibo_Res recibo_Res = new Recibo_Res();
            try
            {
                var rpta = recibo_BS.Listar_N();
                recibo_Res = rpta;
            }
            catch (Exception ex)
            {
                recibo_Res.oHeader.estado = false;
                recibo_Res.oHeader.mensaje = ex.Message;
            }
            return Json(recibo_Res, JsonRequestBehavior.AllowGet);
        }

        #region reciclaje
        //public JsonResult ListarSectores(int id_sector = 0)
        //{
        //    Sector_Suc_Res sector_res = new Sector_Suc_Res();
        //    try
        //    {
        //        var rpta = sector_BS.listar_suc(id_sector);
        //        sector_res = rpta;
        //    }
        //    catch (Exception ex)
        //    {
        //        sector_res.oHeader.estado = false;
        //        sector_res.oHeader.mensaje = ex.Message;
        //    }
        //    return Json(sector_res, JsonRequestBehavior.AllowGet);
        //}
        //public JsonResult ListarDepartamentos(int id_departamento = 0)
        //{
        //    var departamento = departamento_BS.lista(id_departamento);
        //    return Json(departamento, JsonRequestBehavior.AllowGet);
        //}
        //public JsonResult ListarTorre(int id_sector = 0)
        //{
        //    Torre_Res torre_Res = new Torre_Res();
        //    try
        //    {
        //        var rpta = torre_BS.Listar(0);
        //        if (rpta.oHeader.estado)
        //        {
        //            var lista = rpta.TorreList;
        //            var listaTorre = lista.Where(x => x.id_sector == id_sector).ToList();
        //            torre_Res.oHeader = rpta.oHeader;
        //            torre_Res.TorreList = listaTorre;
        //        }
        //        else
        //        {
        //            torre_Res = rpta;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        torre_Res.oHeader.estado = false;
        //        torre_Res.oHeader.mensaje = ex.Message;
        //    }

        //    return Json(torre_Res, JsonRequestBehavior.AllowGet);

        //}
        #endregion fin reciclaje

        [HttpPost]
        public JsonResult RegistrarRecibo(string servicio,int id_cliente=0, int anio=0,decimal monto=0)
        {


            Recibo_Res recibo_res = new Recibo_Res();
            List<Recibo> recibo_List = new List<Recibo>();
            DTOHeader oHeader = new DTOHeader();

            List<DateTime> fechas = new List<DateTime>();
            for (int i = 1;i <= 12; i++)
            {
                DateTime oPrimerDiaDelMes = new DateTime(anio,i,1);
                DateTime oUltimoDiaDelMes = oPrimerDiaDelMes.AddMonths(1).AddDays(-1);
                fechas.Add(oUltimoDiaDelMes);
            }

            var resDep = departamento_BS.listarDepProp(id_cliente);
            int contador = 0;
            if (resDep.lista_Departamento.Count > 0)
            {
                foreach(var fecha in fechas)
                {
                    foreach(var dep in resDep.lista_Departamento)
                    {
                        var recb_reg=recibo_BS.Registrar(servicio,dep.id_departamento,monto,fecha);
                        if (recb_reg.oHeader.estado)
                        {
                            contador++;
                        }
                    }
                }

                oHeader.estado = true;
                oHeader.mensaje = "Se han generado " + contador.ToString() + " recibos";

                
                var lista = recibo_BS.Listar_N();
                if(lista.oHeader.estado  && lista.ReciboList.Count > 0)
                {
                    recibo_List = lista.ReciboList;
                }

            }
            else
            {
                oHeader.estado = false;
                oHeader.mensaje = "Los recibos ya han sido generados";
            }

            recibo_res.oHeader = oHeader;
            recibo_res.ReciboList = recibo_List;


            return Json(recibo_res, JsonRequestBehavior.AllowGet);
        }


        public JsonResult ListarFiltro(string dni = "", string nombre = "", string servicio = "", int estado = 3)
        {
            Recibo_Res recibo_Res = new Recibo_Res();
            try
            {
                var rpta = recibo_BS.Listar_Filtro(dni,nombre,servicio,estado);
                recibo_Res = rpta;
            }
            catch (Exception ex)
            {
                recibo_Res.oHeader.estado = false;
                recibo_Res.oHeader.mensaje = ex.Message;
            }
            return Json(recibo_Res, JsonRequestBehavior.AllowGet);
        }


        public JsonResult PagarRecibo(int id_recibo, int estado)
        {
            Recibo_Res recibo_Res = new Recibo_Res();
            try
            {
                var rpta = recibo_BS.Pagar_Recibo(id_recibo, estado);
                recibo_Res = rpta;
            }
            catch (Exception ex)
            {
                recibo_Res.oHeader.estado = false;
                recibo_Res.oHeader.mensaje = ex.Message;
            }
            return Json(recibo_Res, JsonRequestBehavior.AllowGet);
        }
    }
}