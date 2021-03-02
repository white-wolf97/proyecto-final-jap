using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesCompartidas
{
    public class FacturaDTO 
    {
        public int idFactura { get; set; }

        public DateTime fecha { get; set; }

        public int ci { get; set; }

        public int montoTotal { get; set; }

        public bool estaHabilitada { get; set; }
    }
}
