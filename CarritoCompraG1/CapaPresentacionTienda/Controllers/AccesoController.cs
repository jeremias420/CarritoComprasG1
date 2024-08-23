using System;
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
        public ActionResult Registrar(Cliente objeto)
        {
            int resultado;
            string mensaje = string.Empty;

            ViewData["Nombres"] = string.IsNullOrEmpty(objeto.clie_nombre) ? "" : objeto.clie_nombre;
            ViewData["Apellidos"] = string.IsNullOrEmpty(objeto.clie_apellido) ? "" : objeto.clie_apellido;
            ViewData["Correo"] = string.IsNullOrEmpty(objeto.clie_correo) ? "" : objeto.clie_correo;

            if (objeto.clie_clave != objeto.clie_ConfirmarClave)
            {
                ViewBag.Error = "Las contraseñas no coinciden";
                return View();
            }

            resultado = new CN_Cliente().Registrar(objeto, out mensaje);

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
            Cliente oCliente = null;
            oCliente = new CN_Cliente().Listar().Where(item => item.clie_correo == correo && item.clie_clave == CN_Recursos.ConvertirSha256(clave)).FirstOrDefault();

            if (oCliente == null)
            {
                ViewBag.Error = "Correo o contraseña no son correctas";
                return View();
            }

            if (oCliente.clie_restablecer)
            {
                TempData["IdCliente"] = oCliente.clie_id;
                return RedirectToAction("CambiarClave", "Acceso");
            }
            else
            {
                FormsAuthentication.SetAuthCookie(oCliente.clie_correo, false);
                Session["Cliente"] = oCliente;
                ViewBag.Error = null;
                return RedirectToAction("Index", "Tienda");
            }
        }

        [HttpPost]
        public ActionResult Reestablecer(string correo)
        {
            Cliente cliente = new CN_Cliente().Listar().FirstOrDefault(item => item.clie_correo == correo);

            if (cliente == null)
            {
                ViewBag.Error = "No se encontró un cliente relacionado a ese correo";
                return View();
            }

            string mensaje = string.Empty;
            bool respuesta = new CN_Cliente().ReestablecerClave(cliente.clie_id, correo, out mensaje);

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
        public ActionResult CambiarClave(string idCliente, string claveactual, string nuevaclave, string confirmaclave)
        {
            Cliente oCliente = new CN_Cliente().Listar().Where(u => u.clie_id == int.Parse(idCliente)).FirstOrDefault();

            if (oCliente == null)
            {
                ViewBag.Error = "Cliente no encontrado";
                return View();
            }

            if (oCliente.clie_clave != CN_Recursos.ConvertirSha256(claveactual))
            {
                TempData["IdCliente"] = idCliente;
                ViewData["vclave"] = "";
                ViewBag.Error = "La contraseña actual no es correcta";
                return View();
            }
            else if (nuevaclave != confirmaclave)
            {
                TempData["IdCliente"] = idCliente;
                ViewData["vclave"] = claveactual;
                ViewBag.Error = "Las contraseñas no coinciden";
                return View();
            }

            ViewData["vclave"] = "";
            nuevaclave = CN_Recursos.ConvertirSha256(nuevaclave);

            string mensaje = string.Empty;
            bool respuesta = new CN_Cliente().CambiarClave(int.Parse(idCliente), nuevaclave, out mensaje);

            if (respuesta)
            {
                return RedirectToAction("Index");
            }
            else
            {
                TempData["IdCliente"] = idCliente;
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
