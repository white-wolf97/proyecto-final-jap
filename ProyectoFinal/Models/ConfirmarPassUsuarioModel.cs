using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoFinal.Models
{
    public class ConfirmarPassUsuarioModel
    {
        [Required(ErrorMessage = "Debe ingresar un nombre de usuario")]
        public string nombreUser { get; set; }
        [Required(ErrorMessage = "Debe ingresar una contraseña")]
        [StringLength(20,MinimumLength = 8,ErrorMessage ="Debe contener entre 8 y 20 caracteres")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,20}$", ErrorMessage = "Debe contener al menos una mayúscula, una minúscula, un número y un símbolo")]
        public string pass { get; set; }
        [Compare("pass",ErrorMessage ="Las contraseñas ingresadas no son iguales")]
        public string confirmationPass { get; set; }
    }
}