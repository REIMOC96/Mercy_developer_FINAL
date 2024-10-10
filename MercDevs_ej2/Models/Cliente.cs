using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace MercDevs_ej2.Models;

public partial class Cliente
{
    public int IdCliente { get; set; }

    // Validadores para el Nombre
    [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Por favor ingrese un nombre válido")]
    [Required(ErrorMessage = "El Nombre es Obligatorio")]
    [StringLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres.")]
    public string Nombre { get; set; } = null!;

    // Validadores para el Apellido
    [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Por favor ingrese un Apellido válido")]
    [Required(ErrorMessage = "El apellido es Obligatorio")]
    [StringLength(50, ErrorMessage = "El apellido no puede tener más de 50 caracteres.")]
    public string Apellido { get; set; } = null!;

    // Validadores para el Correo Electrónico
    [Required(ErrorMessage = "El correo electrónico es Obligatorio")]
    [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido")]
    [StringLength(80, ErrorMessage = "El correo electrónico no puede tener más de 80 caracteres.")]
    public string Correo { get; set; } = null!;

    // Validadores para el Teléfono
    [Phone(ErrorMessage = "Por favor ingrese un número válido")]
    [Required(ErrorMessage = "El número de teléfono es Obligatorio")]
    [RegularExpression(@"^\d{10}$", ErrorMessage = "El número de teléfono debe tener 10 dígitos.")]
    public string? Telefono { get; set; }

    // Validadores para la Dirección
    [Required(ErrorMessage = "Por favor ingrese una dirección")]
    [StringLength(80, ErrorMessage = "La dirección no puede tener más de 80 caracteres.")]
    public string? Direccion { get; set; }

    // Validadores para el Estado
    [Range(0, 2, ErrorMessage = "Por favor seleccione un Estado válido")]
    [Required(ErrorMessage = "Por favor seleccione un Estado")]
    public int Estado { get; set; }

    public virtual ICollection<Recepcionequipo> Recepcionequipos { get; set; } = new List<Recepcionequipo>();
}
