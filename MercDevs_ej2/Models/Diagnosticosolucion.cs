using System;
using System.Collections.Generic;

namespace MercDevs_ej2.Models;

public partial class Diagnosticosolucion
{
    public int IdDiagnosticoSolucion { get; set; }

    public string DescripcionDiagnostico { get; set; } = null!;

    public string? DescripcionSolucion { get; set; }

    public int DatosFichaTecnicaId { get; set; }

    public virtual Datosfichatecnica DatosFichaTecnica { get; set; } = null!;
}
