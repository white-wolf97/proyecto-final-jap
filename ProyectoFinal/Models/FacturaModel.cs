using EntidadesCompartidas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoFinal.Models
{
    public class FacturaModel
    {
        [Range(minimum: "1", maximum: "10000000", type: typeof(int), ErrorMessage = "Debe ser un entero entre 1 y 10000000")]
        public int idFactura { get; set; }

        public DateTime fecha { get; set; } //cuando la guardo en la DB le pongo la fecha actual.

        public int ci { get; set; }

        public int idProducto { get; set; }

        [Range(minimum: "1", maximum: "10000000", type: typeof(int), ErrorMessage = "Debe ser un entero mayor a cero")]
        public int cantidad { get; set; }

        public int montoTotal { get; set; }

        public List<SelectListItem> Productos { get; set;}

        public List<SelectListItem> Clientes{ get; set;}

        public List<DetalleModel> Detalles{ get; set; } = new List<DetalleModel>();

        public bool estaHabilitada { get; set; }
    }
}