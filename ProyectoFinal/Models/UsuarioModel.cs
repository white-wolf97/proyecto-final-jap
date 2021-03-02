using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoFinal.Models
{
    public class UsuarioModel
    {
        [Required(ErrorMessage = "Debe ingresar un nombre de usuario")]
        public string nombreUser { get; set; }
        public bool esAdmin { get; set; }
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Debe contener entre 8 y 20 caracteres")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,20}$", ErrorMessage = "Debe contener al menos una mayúscula, una minúscula, un número y un símbolo")]
        [Required(ErrorMessage = "Debe ingresar una contraseña")]
        public string pass { get; set; }
    }
}