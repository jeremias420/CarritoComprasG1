using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Producto
    {
        public int prod_id { get; set; }
        public string prod_nombre { get; set; }
        public string prod_descripcion { get; set; }
        public Marca oMarca { get; set; }
        public Categoria oCategoria { get; set; }
        public decimal prod_precio { get; set; }
        public string prod_PrecioTexto { get; set; }
        public int prod_stock { get; set; }
        public string prod_rutaImagen { get; set; }
        public string prod_nombreImagen { get; set; }
        public bool prod_activo { get; set; }
        public string prod_Base64 { get; set; }
        public string prod_Extension { get; set; }
    }
}
