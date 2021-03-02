namespace CapaDatos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Detalle")]
    public partial class Detalle
    {
        [Key]
        public int IdDetalle { get; set; }

        public int idProducto { get; set; }

        public int idFactura { get; set; }

        public int cantidad { get; set; }

        public virtual Factura Factura { get; set; }

        public virtual Producto Producto { get; set; }
    }
}
