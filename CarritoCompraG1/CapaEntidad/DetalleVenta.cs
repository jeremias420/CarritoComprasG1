using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class DetalleVenta
    {
        public int deta_id { get; set; }
        public int deta_vent_id { get; set; }
        public Producto oProducto { get; set; }
        public int deta_cantidad { get; set; }
        public decimal deta_total { get; set; }
    }
}
