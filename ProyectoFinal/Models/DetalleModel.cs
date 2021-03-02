using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal.Models
{
    public class DetalleModel
    {
        //se autogenera
        public int IdDetalle { get; set; }

        public string nombreProducto { get; set; }

        public string marca{ get; set; }

        public int cantidad { get; set; }

        //este es el precio unitario
        public int precio{ get; set; }
        
        //este precio es el que considera la cantidad de elementos y la multiplica por el precio unitario
        public int precioTotal{ get; set; }

        //FK
        public int idProducto { get; set; }
        //FK
        public int idFactura { get; set; }
        
    }
}
