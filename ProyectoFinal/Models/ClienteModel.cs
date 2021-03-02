using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoFinal.Models
{
    public class ClienteModel
    {
        [Required(ErrorMessage = "Debe ingresar un nombre")]
        public string nombre { get; set; }
        [Required(ErrorMessage = "Debe ingresar una cédula")]
        [Range(minimum: 1000000, maximum: 99999999, ErrorMessage = "Debe ingresar una cédula válida")]
        public int ci { get; set; }
        [Required(ErrorMessage = "Debe ingresar un domicilio")]
        public string domicilio { get; set; }
        [DataType(DataType.Date, ErrorMessage = "fecha con formato mm/dd/yyyy")]
        [Required(ErrorMessage = "Debe ingresar una fecha de nacimiento")]
        public DateTime fechaNacimiento { get; set; }
    }
}