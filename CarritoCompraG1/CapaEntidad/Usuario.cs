using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Usuario
    {
        public int usua_id { get; set; }
        public string usua_nombre { get; set; }
        public string usua_apellido { get; set; }
        public string usua_correo { get; set; }
        public string usua_clave { get; set; }
        public bool usua_restablecer { get; set; }
        public bool usua_activo { get; set; }
    }
}
