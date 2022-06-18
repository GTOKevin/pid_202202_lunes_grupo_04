using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Negocio;
using Entidades;
using System.Web.Security;

namespace Web_Integrador.Controllers
{
    public class AuthController : Controller
    {
        Usuario_BS user_bs = new Usuario_BS();
        Perfil_BS perfil_bs = new Perfil_BS();
        Perfil_File_BS perfil_file_bs = new Perfil_File_BS();
        // GET: Auth
        public ActionResult Login()
        {
            if(Session["PI_USUARIO"] != null)
            {
                return RedirectToAction("Index","Home");
            }
            return View();
        }


        public ActionResult NotFound()
        {
            return View();
        }

        public ActionResult Blocked()
        {
            return View();
        }

        [HttpPost]
        public JsonResult LoginUsuario(Usuario user)
        {
            Usuario_Login_Res respLogin = new Usuario_Login_Res();
            Usuario_Login userLogin = new Usuario_Login();
            DTOHeader oHeader = new DTOHeader();
            try
            {
                var respUser = user_bs.ValidarUsuarioLogin(user.username,user.clave);
                var respFile = perfil_file_bs.ListarFile(user.id_perfil);
                if (respUser.oHeader.estado == true)
                {
                    userLogin.usuario = respUser.UsuarioList.FirstOrDefault();
                    userLogin.file = respFile.FileList.FirstOrDefault();
                    userLogin.usuario.clave = null;
                    var respPerfil = perfil_bs.Listar_Perfil_Usuario(userLogin.usuario.id_perfil);
                    if (respPerfil.oHeader.estado == true)
                    {
                        userLogin.perfil = respPerfil.Lista_Perfiles.FirstOrDefault();
                    }
                    if (userLogin.usuario.id_estado == 1)
                    {

                        Session["PI_USUARIO"] = userLogin;
                        Session["PI_USERNAME"] = userLogin.usuario.username;
                        Session["PI_IDUSER"] = userLogin.usuario.id_usuario;
                        Session["PI_IDPERFIL"] = userLogin.usuario.id_perfil;
                        Session["PI_ROL"] = userLogin.usuario.id_rol;


                        if(userLogin.file != null)
                        {
                            if (userLogin.file.nombrefile != "" && userLogin.file.nombrefile != null)
                            {
                                Session["PI_FILE"] = userLogin.file.nombrefile;
                            }
                            else
                            {

                                string ruta = "/Assets/img/avatars/default.jpg";
                                Session["PI_FILE"] = ruta;
                            }
                        }
                        else
                        {

                            string ruta = "/Assets/img/avatars/default.jpg";
                            Session["PI_FILE"] = ruta;
                        }

                        oHeader.estado = true;
                        oHeader.mensaje = "Conectarse";

                        FormsAuthentication.SetAuthCookie(userLogin.usuario.username, false);
                    }
                    else
                    {
                        oHeader.estado = false;
                        oHeader.mensaje = "Acceso no permitido: Su cuenta no se encuentra disponible";
                    }
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
                oHeader.mensaje = ex.Message;
            }
            respLogin.Usuario_Perfil = userLogin;
            respLogin.oHeader = oHeader;
            return Json(respLogin, JsonRequestBehavior.AllowGet);

        }

        public JsonResult CrearUsuario(Usuario user)
        {
            DTOHeader oHeader = new DTOHeader();
            try
            {
                var respuesta = user_bs.Registrar(user);
                oHeader = respuesta;
        
            }catch(Exception ex)
            {
                oHeader.estado = false;
                oHeader.mensaje=ex.Message;
            }
            return Json(oHeader, JsonRequestBehavior.AllowGet);
        }

        public ActionResult logout()
        {
            Session.Abandon();
            Session.RemoveAll();
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Auth");
        }
    }
}