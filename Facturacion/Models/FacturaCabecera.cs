using System;
using System.Collections.Generic;

namespace Facturacion.Models;

public partial class FacturaCabecera
{
    public int IdFacturaCabecera { get; set; }

    public DateTime? FechaFactura { get; set; }

    public int? IdCliente { get; set; }

    public decimal? Total { get; set; }

    public virtual ICollection<FacturaDetalle> FacturaDetalles { get; set; } = new List<FacturaDetalle>();

    public virtual Cliente? IdClienteNavigation { get; set; }
}
