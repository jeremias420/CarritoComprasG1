using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CapaEntidad;
using CapaNegocio;

using System.IO;

namespace CapaPresentacionTienda.Controllers
{
    public class TiendaController : Controller
    {
        // GET: Tienda
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListaCategorias()
        {
            List<Categoria> lista = new List<Categoria>();
            lista = new CN_Categoria().Listar();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarMarcaPorCategoria(int idcategoria)
        {
            List<Marca> lista = new List<Marca>();
            lista = new CN_Marca().ListarMarcaPorCategoria(idcategoria);
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarProducto(int idcategoria, int idmarca)
        {
            List<Producto> lista = new List<Producto>();

            bool conversion;

            lista = new CN_Producto().Listar().Select(p => new Producto()
            {
                prod_id = p.prod_id,
                prod_nombre = p.prod_nombre,
                prod_descripcion = p.prod_descripcion,
                oMarca = p.oMarca,
                oCategoria = p.oCategoria,
                prod_precio = p.prod_precio,
                prod_stock = p.prod_stock,
                prod_rutaImagen = p.prod_rutaImagen,
                prod_Base64 = CN_Recursos.ConvertirBase64(Path.Combine(p.prod_rutaImagen, p.prod_nombreImagen), out conversion),
                prod_Extension = Path.GetExtension(p.prod_nombreImagen),
                prod_activo = p.prod_activo
            }).Where(p =>
                p.oCategoria.cate_id == (idcategoria == 0 ? p.oCategoria.cate_id : idcategoria) &&
                p.oMarca.marc_id == (idmarca == 0 ? p.oMarca.marc_id : idmarca) &&
                p.prod_stock > 0 && p.prod_activo == true
            ).ToList();

            var jsonresult = Json(new { data = lista }, JsonRequestBehavior.AllowGet);
            jsonresult.MaxJsonLength = int.MaxValue;

            return jsonresult;
        }
    }
}