using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Departamento
    {
        public int depa_id { get; set; }
        public string depa_descripcion { get; set; }
        public Provincia oProvincia { get; set; }
    }
}
