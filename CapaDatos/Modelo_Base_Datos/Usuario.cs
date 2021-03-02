namespace CapaDatos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Usuario")]
    public partial class Usuario
    {
        [Key]
        [StringLength(50)]
        public string nombreUser { get; set; }

        [Required]
        [StringLength(50)]
        public string pass { get; set; }

        public bool esAdmin { get; set; }

        public bool estaHabilitado { get; set; }
    }
}
