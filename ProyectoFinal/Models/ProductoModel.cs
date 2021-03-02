using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoFinal.Models
{
    public class ProductoModel
    {
        [Required(ErrorMessage ="Debe ingresar un nombre")]
        public string nombre { get; set; }
        [Required(ErrorMessage = "Debe ingresar una marca")]
        public string marca { get; set; }
        [Required(ErrorMessage = "Debe ingresar un precio")]
        [Range(minimum:"1" , maximum:"1000000", type:typeof(int),ErrorMessage = "Debe ser un entero mayor a cero") ]
        public int precio { get; set; }
        [Required(ErrorMessage = "Debe ingresar un ID")]
        [Range(minimum: "0", maximum: "10000000", type: typeof(int), ErrorMessage = "Debe ser un entero entre 0 y 10000000")]
        public int idProducto { get; set; }
    }
}