using System;
using System.Collections.Generic;

namespace MercDevs_ej2.Models;

public partial class Datosfichatecnica
{
    public int IdDatosFichaTecnica { get; set; }

    public DateTime? FechaInicio { get; set; }

    public DateTime? FechaFinalizacion { get; set; }

    /// <summary>
    /// Por el Tecnico
    /// </summary>
    public string? PobservacionesRecomendaciones { get; set; }

    /// <summary>
    /// 0:Windows 10 ; 1: Windows 11; 2: Linux
    /// </summary>
    public int? Soinstalado { get; set; }

    /// <summary>
    /// 0: Microsoft Office 2013 ; 1: Microsoft Office 2019 ; 2: Microsoft Office 365 ; 3: OpenOffice
    /// </summary>
    public int? SuiteOfficeInstalada { get; set; }

    /// <summary>
    /// 0:No Instalado ; 1: Instalado 2: No aplica
    /// </summary>
    public int? LectorPdfinstalado { get; set; }

    /// <summary>
    /// 0:No instalado ; 1: Chrome ; 2: Firefox; 3: Chrome y Firefox
    /// </summary>
    public int? NavegadorWebInstalado { get; set; }

    public string? AntivirusInstalado { get; set; }

    public int RecepcionEquipoId { get; set; }

    public int Estado { get; set; }

    public virtual ICollection<Diagnosticosolucion> Diagnosticosolucions { get; set; } = new List<Diagnosticosolucion>();

    public virtual Recepcionequipo RecepcionEquipo { get; set; } = null!;
}
