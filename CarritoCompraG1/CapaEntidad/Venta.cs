using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Venta
    {
        public int vent_id { get; set; }
        public Cliente oCliente { get; set; }
        public int vent_totalProducto { get; set; }
        public decimal vent_montoTotal { get; set; }
        public string vent_contacto { get; set; }
        public Ciudad oCiudad { get; set; }
        public string vent_telefono { get; set; }
        public string vent_direccion { get; set; }
        public int vent_tran_id { get; set; }
        public string vent_fechaVenta { get; set; }


    }
}
