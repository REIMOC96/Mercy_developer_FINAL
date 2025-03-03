﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MercDevs_ej2.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    // Validadores para el Nombre
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Por favor ingrese un nombre válido.")]
    [Required(ErrorMessage = "El Nombre es obligatorio.")]
    [StringLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres.")]
    public string Nombre { get; set; } = null!;

    // Validadores para el Apellido
    [RegularExpression(@"^[a-zA-ZñÑáéíóúÁÉÍÓÚ\s]+$", ErrorMessage = "Por favor ingrese un apellido válido.")]
    [Required(ErrorMessage = "El apellido es obligatorio.")]
    [StringLength(50, ErrorMessage = "El apellido no puede tener más de 50 caracteres.")]
    public string Apellido { get; set; } = null!;

    // Validadores para el Correo Electrónico
    [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
    [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
    [StringLength(80, ErrorMessage = "El correo electrónico no puede tener más de 80 caracteres.")]
    public string Correo { get; set; } = null!;

    // Validadores de contraseña
    [Required(ErrorMessage = "Por favor ingrese una contraseña.")]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$",
    ErrorMessage = "La contraseña debe tener al menos 6 caracteres, incluyendo una mayúscula, un número y un carácter especial.")]
    [DataType(DataType.Password)]
    public string? Password { get; set; } = null!;

    // Confirmar Password
    [Required(ErrorMessage = "Se debe confirmar la contraseña.")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
    [NotMapped]
    public string ConfirmPassword { get; set; } = null!;

    // Contraseña actual
    [Required(ErrorMessage = "Se debe confirmar con la contraseña.")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Contrseña incorrecta")]
    [NotMapped]
    public string CurrentPassword { get; set; } = null!;

    public virtual ICollection<Servicio> Servicios { get; set; } = new List<Servicio>();
}
