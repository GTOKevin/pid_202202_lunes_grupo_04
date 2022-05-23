using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Negocio;
using Entidades;
using Web_Integrador.Model;

namespace Web_Integrador.Controllers
{
    [Authorize]
    public class DepartamentoController : Controller
    {
        Sucursal_BS sucursal_BS = new Sucursal_BS();
        Torre_BS torre_BS = new Torre_BS(); 
        Departamento_BS departamento_BS = new Departamento_BS();
        Propietario_BS propietario_BS=new Propietario_BS();
        Tipo_BS tipo_BS = new Tipo_BS();
        // GET: Departamento
        public ActionResult Index()
        {
            DepartamentoModel oModeView = new DepartamentoModel();
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

        public ActionResult Pureba()
        {
            return View();
        }


        public JsonResult ListarDepartamentos(int id_departamento = 0)
        {
            var departamento = departamento_BS.lista(id_departamento);
            return Json(departamento,JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarTorre(int id_sector=0)
        {
            Torre_Res torre_Res=new Torre_Res();
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
            }catch(Exception ex)
            {
                torre_Res.oHeader.estado = false;
                torre_Res.oHeader.mensaje = ex.Message;
            }

            return Json(torre_Res,JsonRequestBehavior.AllowGet);

        }

        public JsonResult RegistrarDepartamento(DepartamentoPropietario enti)
        {
            Departamento_Res departamento_Res = new Departamento_Res();
            List<Departamento> departamentoList = new List<Departamento>();
            DTOHeader oHeader = new DTOHeader();
            var userId = ((Usuario_Login)Session["PI_USUARIO"]).usuario.id_usuario;
            enti.departamento.id_usuario = userId;
            try
            {
                var rpta = departamento_BS.Registrar(enti.departamento);
                oHeader = rpta.oHeader;
                if (rpta.oHeader.estado)
                    
                {
                    if (enti.propietarios != null)
                    {
                        for (int i = 0; i < enti.propietarios.Count; i++)
                        {
                            Propietario prop = new Propietario();
                            prop.id_propietario = enti.propietarios[i].id_propietario;
                            prop.nombres = enti.propietarios[i].nombres;
                            prop.primer_apellido = enti.propietarios[i].primer_apellido;
                            prop.segundo_apellido = enti.propietarios[i].segundo_apellido;
                            prop.tipo_documento = enti.propietarios[i].tipo_documento;
                            prop.nro_documento = enti.propietarios[i].nro_documento;
                            prop.nro_documento = enti.propietarios[i].nro_documento;
                            prop.nacionalidad = enti.propietarios[i].nacionalidad;
                            prop.id_tipo = enti.propietarios[i].id_tipo;
                            prop.id_departamento = rpta.id_register;
                            propietario_BS.Registrar(prop);
                        }
                    }
                    

                    var getDepartamento = departamento_BS.lista(rpta.id_register);
                    if (getDepartamento.oHeader.estado)
                    {
                        departamentoList = getDepartamento.lista_Departamento;
                    }
                }
            }
            catch (Exception ex)
            {
                departamento_Res.oHeader.estado = false;
                departamento_Res.oHeader.mensaje = ex.Message;
            }
            departamento_Res.oHeader = oHeader;
            departamento_Res.lista_Departamento = departamentoList;


            return Json(departamento_Res, JsonRequestBehavior.AllowGet);
        }


        public JsonResult ListarTorrePropietario(int id_departamento = 0)
        {
            DepartamentoPropietarioRes departamentoModelRes = new DepartamentoPropietarioRes();
            DTOHeader oHeader = new DTOHeader();

            try
            {
                var depRes = departamento_BS.lista(id_departamento);
                if (depRes.oHeader.estado)
                {
                    oHeader=depRes.oHeader;
                    departamentoModelRes.lista_Departamento = depRes.lista_Departamento;
                    var propRes = propietario_BS.lista(id_departamento);
                    if (propRes.oHeader.estado)
                    {
                        departamentoModelRes.propietarios = propRes.lista_Propietario;
                    }
                }
                else
                {
                    oHeader = depRes.oHeader;
                }
                
            }
            catch (Exception ex)
            {
                oHeader.estado = false;
                oHeader.mensaje=ex.Message;
            }
            departamentoModelRes.oHeader=oHeader;  

            return Json(departamentoModelRes, JsonRequestBehavior.AllowGet);
        }
      
    }

}