using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesCompartidas
{
    public class ProductoDTO
    {
        public string nombre { get; set; }
        public int idProducto { get; set; }
        public string marca { get; set; }
        public int precio { get; set; }
        public bool estaHabilitado { get; set; }
    }
}
