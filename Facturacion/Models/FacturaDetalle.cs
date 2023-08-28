using System;
using System.Collections.Generic;

namespace Facturacion.Models;

public partial class FacturaDetalle
{
    public int IdFacturaDetalle { get; set; }

    public int? IdFacturaCabecera { get; set; }

    public int? IdProducto { get; set; }

    public int? Cantidad { get; set; }

    public decimal? PrecioUnitario { get; set; }

    public decimal? Total { get; set; }

    public virtual FacturaCabecera? IdFacturaCabeceraNavigation { get; set; }

    public virtual Producto? IdProductoNavigation { get; set; }
}
