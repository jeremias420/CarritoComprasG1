﻿using System;
using System.Linq;
using System.Web.Mvc;
using CapaEntidad;
using CapaNegocio;
using System.Web.Security;


namespace CarritoCompraG1.Controllers
{
    public class AccesoController : Controller
    {
        // GET: Acceso
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Registrar()
        {
            return View();
        }

        public ActionResult Reestablecer()
        {
            return View();
        }

        public ActionResult CambiarClave()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registrar(Usuario objeto)
        {
            int resultado;
            string mensaje = string.Empty;

            ViewData["Nombres"] = string.IsNullOrEmpty(objeto.usua_nombre) ? "" : objeto.usua_nombre;
            ViewData["Apellidos"] = string.IsNullOrEmpty(objeto.usua_apellido) ? "" : objeto.usua_apellido;
            ViewData["Correo"] = string.IsNullOrEmpty(objeto.usua_correo) ? "" : objeto.usua_correo;

            if (objeto.usua_clave != objeto.usua_confirmarclave)
            {
                ViewBag.Error = "Las contraseñas no coinciden";
                return View();
            }

            resultado = new CN_Usuarios().Registrar(objeto, out mensaje);

            if (resultado > 0)
            {
                ViewBag.Error = null;
                return RedirectToAction("Index", "Acceso");
            }
            else
            {
                ViewBag.Error = mensaje;
                return View();
            }
        }

        [HttpPost]
        public ActionResult Index(string correo, string clave)
        {
            Usuario oUsuario = null;
            oUsuario = new CN_Usuarios().Listar().Where(item => item.usua_correo == correo && item.usua_clave == CN_Recursos.ConvertirSha256(clave)).FirstOrDefault();

            if (oUsuario == null)
            {
                ViewBag.Error = "Correo o contraseña no son correctas";
                return View();
            }
            else
            {
                if (oUsuario.usua_restablecer)
                {
                    TempData["IdCliente"] = oUsuario.usua_id;
                    return RedirectToAction("CambiarClave", "Acceso");
                }
            }
                FormsAuthentication.SetAuthCookie(oUsuario.usua_correo, false);
                Session["Cliente"] = oUsuario;
                ViewBag.Error = null;
                return RedirectToAction("Index", "Tienda");
        }

        [HttpPost]
        public ActionResult Reestablecer(string correo)
        {
            Usuario oUsuario = new CN_Usuarios().Listar().FirstOrDefault(item => item.usua_correo == correo);

            if (oUsuario == null)
            {
                ViewBag.Error = "No se encontró un cliente relacionado a ese correo";
                return View();
            }

            string mensaje = string.Empty;
            bool respuesta = new CN_Cliente().ReestablecerClave(oUsuario.usua_id, correo, out mensaje);

            if (respuesta)
            {
                ViewBag.Error = null;
                return RedirectToAction("Index", "Acceso");
            }
            else
            {
                ViewBag.Error = mensaje;
                return View();
            }
        }

        [HttpPost]
        public ActionResult CambiarClave(string idUsuario, string claveactual, string nuevaclave, string confirmaclave)
        {
            Usuario oUsuario = new CN_Usuarios().Listar().Where(u => u.usua_id == int.Parse(idUsuario)).FirstOrDefault();

            if (oUsuario == null)
            {
                ViewBag.Error = "Cliente no encontrado";
                return View();
            }

            if (oUsuario.usua_clave != CN_Recursos.ConvertirSha256(claveactual))
            {
                TempData["IdUsuario"] = idUsuario;
                ViewData["vclave"] = "" ;
                ViewBag.Error = "La contraseña actual no es correcta";
                return View();
            }
            else if (nuevaclave != confirmaclave)
            {
                TempData["IdUsuario"] = idUsuario;
                ViewData["vclave"] = claveactual;
                ViewBag.Error = "Las contraseñas no coinciden";
                return View();
            }

            ViewData["vclave"] = "";
            nuevaclave = CN_Recursos.ConvertirSha256(nuevaclave);

            string mensaje = string.Empty;
            bool respuesta = new CN_Usuarios().CambiarClave(int.Parse(idUsuario), nuevaclave, out mensaje);

            if (respuesta)
            {
                return RedirectToAction("Index");
            }
            else
            {
                TempData["IdUsuario"] = idUsuario;
                ViewBag.Error = mensaje;
                return View();
            }
        }

        public ActionResult CerrarSesion()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Acceso");
        }
    }
}
