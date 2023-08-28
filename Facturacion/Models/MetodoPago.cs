using System;
using System.Collections.Generic;

namespace Facturacion.Models;

public partial class MetodoPago
{
    public int IdMetodoPago { get; set; }

    public string Nombre { get; set; } = null!;
}
