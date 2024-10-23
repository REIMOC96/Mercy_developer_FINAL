using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MercDevs_ej2.Models;

public partial class Recepcionequipo
{
    public int Id { get; set; } 

    public int IdCliente { get; set; }

    public int IdServicio { get; set; }

    // validadores de fecha recepcion
    [Required(ErrorMessage ="Ingrese la fecha de recepcion")]
    [DataType(DataType.DateTime)]
    public DateTime? Fecha { get; set; } = null!;

    //validadores de Tipo pc
    //Admite numeros pero simplemente es un listado de valores
    [Range (0,3, ErrorMessage ="ingrese un valor dentro del rango establecido")]
    [Required(ErrorMessage ="Se requiere que ingrese el tipo de pc")]
    public int? TipoPc { get; set; } = null!;

    //validadores de Accesorios
    //este si puede ser nulo, puede que llegue solamente la torre incluso sin cable power
    [StringLength(70, ErrorMessage ="los accesorios no pueden tener mas de 70 caracteres")]
    public string? Accesorio { get; set; }


    //validadores de MarcaPc

    [StringLength(50, ErrorMessage = "La marca del pc no puede tener 50 caracteres.")]
    [Required(ErrorMessage = "La marca es Requerida")]
    [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Por favor ingrese un Apellido válido")]
    public string? MarcaPc { get; set; } = null!;

    // Validadores de Modelo pc
    // modelo pc depende de si es notebook o all in one princilalmente VV
    [StringLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres.")]
    [Required(ErrorMessage = "La marca es Requerida")]
    public string? ModeloPc { get; set; }


    //Validadores de N serie
    //este igual es ambiguo porque puede que el pc tenga como no tenga 
    //ya que si es pc custom no tendria, pero si tomamos el de la placa madre...
    //nose no me gusta la idea
    [StringLength (20,  ErrorMessage ="El N° de serie no puede tener mas de 20 caracteres ")]
    public string? Nserie { get; set; }


    // Validadores de Capacidad Ram
    // Igual que en el caso del estado, este se maneja con un listado de cosas definidas
    [Required(ErrorMessage ="Capacidad de Ram es obligatorio")]
    [Range(0,5, ErrorMessage ="Ingrese una capacidad dentro de los rangos definidos")]
    public int? CapacidadRam { get; set; }

    public int? TipoAlmacenamiento { get; set; }

    public string? CapacidadAlmacenamiento { get; set; }

    public int? TipoGpu { get; set; }

    public string? Grafico { get; set; }

    public int? Estado { get; set; }

    public virtual ICollection<Datosfichatecnica> Datosfichatecnicas { get; set; } = new List<Datosfichatecnica>();

    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    public virtual Servicio IdServicioNavigation { get; set; } = null!;
}