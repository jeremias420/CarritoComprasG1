using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Carrito
    {
        public int carr_id { get; set; }
        public Cliente oCliente { get; set; }
        public Producto oProducto { get; set; }
        public int carr_cantidad { get; set; }
    }
}
