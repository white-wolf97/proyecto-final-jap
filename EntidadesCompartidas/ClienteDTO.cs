using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesCompartidas
{
    public class ClienteDTO
    {
        public string nombre { get; set; }
        public int ci { get; set; }
        public string domicilio { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public bool estaHabilitado { get; set; }
    }
}
