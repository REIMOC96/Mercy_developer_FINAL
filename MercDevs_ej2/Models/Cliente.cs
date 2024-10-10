using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace MercDevs_ej2.Models;

public partial class Cliente
{
    public int IdCliente { get; set; }



    [Required(ErrorMessage = "El Nombre es Obligatorio")]
    public string Nombre { get; set; } = null!;




    [Required(ErrorMessage = "El apellido es Obligatorio")]
    public string Apellido { get; set; } = null!;



    [Required(ErrorMessage = "El correo electronico es  Obligatorio")]
    [EmailAddress(ErrorMessage = "EL formato del correo electronico no es valido")]
    [StringLength(100, ErrorMessage = "El correo electrónico no puede tener más de 100 caracteres.")]
    public string Correo { get; set; } = null!;


    [Required(ErrorMessage ="El numero de telefono es Obligatorio")]
    [RegularExpression(@"^\d{10}$", ErrorMessage = "El número de teléfono debe tener 10 dígitos.")]
    public string? Telefono { get; set; }


    [Required(ErrorMessage ="Porfavor ingrese una direccion")]

    public string? Direccion { get; set; }

    [Required(ErrorMessage ="Porfavor seleccione un Estado")]
    public int Estado { get; set; }

    public virtual ICollection<Recepcionequipo> Recepcionequipos { get; set; } = new List<Recepcionequipo>();
}
