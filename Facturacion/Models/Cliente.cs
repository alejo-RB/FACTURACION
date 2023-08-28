using System;
using System.Collections.Generic;

namespace Facturacion.Models;

public partial class Cliente
{
    public int IdCliente { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Telefono { get; set; }

    public string? Direccion { get; set; }

    public virtual ICollection<FacturaCabecera> FacturaCabeceras { get; set; } = new List<FacturaCabecera>();
}
