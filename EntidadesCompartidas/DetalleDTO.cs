using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesCompartidas
{
    public class DetalleDTO
    {
        public int IdDetalle { get; set; }

        public int idProducto { get; set; }

        public int idFactura { get; set; }

        public int cantidad { get; set; }
    }
}
