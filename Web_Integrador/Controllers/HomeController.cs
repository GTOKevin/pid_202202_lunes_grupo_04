using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Negocio;
using Entidades;

namespace Web_Integrador.Controllers
{
    public class HomeController : Controller
    {
        Usuario_BS user_bs = new Usuario_BS();
        Perfil_BS perfil_bs = new Perfil_BS();
        public ActionResult Index()
        {

            return View();
        }


        public ActionResult Login()
        {
            return View();
        }

        public JsonResult LoginUsuario(string userName,string clave)
        {
           Usuario_Login_Res respLogin = new Usuario_Login_Res();
           Usuario_Login userLogin= new Usuario_Login();
           DTOHeader oHeader = new DTOHeader();
            try
            {
                var respUser = user_bs.ValidarUsuarioLogin(userName, clave);
                if (respUser.oHeader.estado == true)
                {
                    userLogin.usuario = respUser.UsuarioList.FirstOrDefault();
                    var respPerfil = perfil_bs.Listar_Perfil_Usuario(userLogin.usuario.id_usuario);
                    if (respPerfil.oHeader.estado == true)
                    {
                        userLogin.perfil = respPerfil.Lista_Perfiles.FirstOrDefault();
                    }
                    Session["PI_USUARIO"] = userLogin;
                    oHeader.estado = true;
                    oHeader.mensaje = "Conectarse";
                }
                else
                {
                    oHeader.estado = false;
                    oHeader.mensaje = "Usuario y/o contraseña incorrecta";
                }
            }
            catch (Exception ex)
            {
                oHeader.estado = false;
                oHeader.mensaje= ex.Message;
            }
            respLogin.Usuario_Perfil = userLogin;
            respLogin.oHeader = oHeader;

            return Json(respLogin,JsonRequestBehavior.AllowGet);
            
        }

        public void logout()
        {
            Session.Abandon();
            Session.RemoveAll();
            Session.Clear();
            RedirectToAction("Index", "Home");
        }
    }
}