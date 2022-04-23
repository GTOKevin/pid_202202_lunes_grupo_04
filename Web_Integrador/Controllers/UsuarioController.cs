﻿using Entidades;
using Helpers;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Integrador.Model;
using Web_Integrador.Security;

namespace Web_Integrador.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        Usuario_BS ub = new Usuario_BS();
        Perfil_BS pb = new Perfil_BS();
        // GET: Usuario
        [PermisosRol(Roles.Administrador)]
        public ActionResult Index()
        {

            ViewBag.lstgenero = new List<SelectListItem>() { 
                new SelectListItem() { Value = "1", Text = "Masculino" },
                new SelectListItem() { Value = "2", Text = "Femenino" },
                new SelectListItem() { Value = "3", Text = "Sin Especificar" }
            };
            ViewBag.lstdocumento = new List<SelectListItem>() {
                new SelectListItem() { Value = "1", Text = "DNI" },
                new SelectListItem() { Value = "2", Text = "Pasaporte" }
            };
            ViewBag.lstnacionalidad = new List<SelectListItem>() {
                new SelectListItem() { Value = "1", Text = "Peruano" },
                new SelectListItem() { Value = "2", Text = "Extranjero" }
            };

            return View();
        }

        public JsonResult ListarUsuarios(int id = 0)
        {
            Usuario_General_Res ur = new Usuario_General_Res();
            try
            {
                var rpta = ub.lista(id);
                ur = rpta;
            }
            catch (Exception ex)
            {
                ur.oHeader.estado = false;
                ur.oHeader.mensaje = ex.Message;
            }
            
            return Json(ur, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarPerfil(int id = 0)
        {
            Perfil_res pr = new Perfil_res();
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

        public JsonResult CrearUsuario(Usuario user)
        {
            Usuario_General_Res ur = new Usuario_General_Res();
            List<UsuarioGeneral> ulist = new List<UsuarioGeneral>();
            DTOHeader oHeader = new DTOHeader();
            try
            {
                var rpta = ub.Registrar_Usuario(user);
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

        public JsonResult RegistrarPerfil(Perfil p)
        {
            Perfil_res_UsG ur = new Perfil_res_UsG();
            List<UsuarioGeneral> plist = new List<UsuarioGeneral>();
            DTOHeader oHeader = new DTOHeader();
            try
            {
                var rpta = pb.Registrar_Perfil(p);
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


        public JsonResult CambiarEstado(Usuario u)
        {
            Usuario_General_Res ur = new Usuario_General_Res();
            List<UsuarioGeneral> ulist = new List<UsuarioGeneral>();
            DTOHeader oHeader = new DTOHeader();
            try
            {
                var rpta = ub.CambiarEstado_Us(u);
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



    }
}