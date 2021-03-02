using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesCompartidas
{
    public class UsuarioDTO
    {
        public string nombreUser { get; set; }
        public string password { get; set; }
        public bool esAdmin { get; set; }
        public bool estaHabilitado { get; set; }
    }
}
