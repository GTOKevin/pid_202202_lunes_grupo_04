using Entidades;
using Helpers;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Integrador.Security;

namespace Web_Integrador.Controllers
{
    [Authorize]
    public class PerfilController : Controller
    {
        Usuario_BS ub = new Usuario_BS();
        Perfil_BS pb = new Perfil_BS();
        Tipo_BS tb = new Tipo_BS();
        // GET: Usuario
        public ActionResult Index()
        {

            var dataDocumento = tb.Listar_TipoUtil("documento");
            var dataGenero = tb.Listar_TipoUtil("genero");
            var datanacionalidad = tb.Listar_TipoUtil("nacionalidad");

            ViewBag.lstgenero = dataGenero.TipoList.ToList();
            ViewBag.lstdocumento = dataDocumento.TipoList.ToList();
            ViewBag.lstnacionalidad = datanacionalidad.TipoList.ToList();

            

            return View();
        }

        public JsonResult CambiarContraseña(Usuario u)
        {
            Usuario_General_Res ur = new Usuario_General_Res();
            List<UsuarioGeneral> ulist = new List<UsuarioGeneral>();
            DTOHeader oHeader = new DTOHeader();
            try
            {
                var rpta = ub.CambiarContraseña_Us(u);
                oHeader = rpta.oHeader;
                if (rpta.oHeader.estado)
                {
                    var getUsuario = ub.lista(rpta.id_register);
                    if (getUsuario.oHeader.estado)
                    {
                        ulist = getUsuario.UsuarioList;
                    }
                }
            }
            catch (Exception ex)
            {
                ur.oHeader.estado = false;
                ur.oHeader.mensaje = ex.Message;
            }
            ur.oHeader = oHeader;
            ur.UsuarioList = ulist;


            return Json(ur, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EditarMiPerfil(Perfil p)
        {
            Perfil_res_UsG ur = new Perfil_res_UsG();
            List<UsuarioGeneral> plist = new List<UsuarioGeneral>();
            DTOHeader oHeader = new DTOHeader();
            try
            {
                var rpta = pb.Editar_MiPerfil(p);
                oHeader = rpta.oHeader;
                if (rpta.oHeader.estado)
                {
                    var getPerfil = ub.lista(rpta.id_register);
                    if (getPerfil.oHeader.estado)
                    {
                        plist = getPerfil.UsuarioList;
                    }
                }
            }
            catch (Exception ex)
            {
                ur.oHeader.estado = false;
                ur.oHeader.mensaje = ex.Message;
            }
            ur.oHeader = oHeader;
            ur.ListaUsuarioP = plist;


            return Json(ur, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarPerfil()
        {
            Perfil_res pr = new Perfil_res();

            int id = Session["PI_IDPERFIL"].ToInt();

            try
            {
                var rpta = pb.Listar_Perfil_Usuario(id);
                pr = rpta;
            }
            catch (Exception ex)
            {
                pr.oHeader.estado = false;
                pr.oHeader.mensaje = ex.Message;
            }
            return Json(pr, JsonRequestBehavior.AllowGet);
        }
    }


   
}