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


    //validadores tipo CPU
    //CPU agregado a la bbdd

    [Required (ErrorMessage = "Porfavor ingrese el modelo del CPU")]
    [MaxLength (20, ErrorMessage ="El nombre del cpu no puede tener mas de 20 caracteres")]
    public string? CPU { get; set; } = null!;

    //validadores de Accesorios
    //este si puede ser nulo, puede que llegue solamente la torre incluso sin cable power
    [StringLength(70, ErrorMessage ="los accesorios no pueden tener mas de 70 caracteres")]
    public string? Accesorio { get; set; } = null!;


    //validadores de MarcaPc

    [MaxLength(25, ErrorMessage = "La marca del pc no puede tener 25 caracteres.")]
    [Required(ErrorMessage = "La marca es Requerida")]
    [RegularExpression(@"^[a-zA-Z0-9\s\-]+$", ErrorMessage = "Por favor ingrese una Marca válida")]
    public string? MarcaPc { get; set; } = null!;

    // Validadores de Modelo pc
    // modelo pc depende de si es notebook o all in one princilalmente o si es comprado || armado
    [StringLength(25, ErrorMessage = "El modelo del pc no puede tener más de 25 caracteres.")]
    [Required(ErrorMessage = "La marca es Requerida")]
    public string? ModeloPc { get; set; } = null!;


    //Validadores de N serie
    //este igual es ambiguo porque puede que el pc tenga como no tenga 
    //ya que si es pc custom no tendria, pero si tomamos el de la placa madre...
    //no se, no termina de convencer...
    // decidi dejarlo como required, simplemente ingresas que no figura y ya
    [Required(ErrorMessage = "Se requiere que ingrese el N de serie, si no hay escribir : 'No Figura' ")]
    [StringLength (20,  ErrorMessage ="El N° de serie no puede tener mas de 20 caracteres, si no hay escribir : 'No Figura' ")]
    public string? Nserie { get; set; } = null!;


    // Validadores de Capacidad Ram
    // Igual que en el caso del estado, este se maneja con un listado de cosas definidas
    [Required(ErrorMessage ="Capacidad de Ram es obligatorio")]
    [Range(0,6, ErrorMessage ="Ingrese una capacidad dentro de los rangos definidos")]
    public int? CapacidadRam { get; set; } = null!;


    //validadores tipo almacenamiento
    //otro listado de cosas definidas
    [Required(ErrorMessage ="El tipo de Almacenamiento eso bligatorio")]
    [Range(0,3, ErrorMessage = "porfavor seleccione un tipo de almacenamiento")]
    public int? TipoAlmacenamiento { get; set; } = null!;



    //validadores de capacidad almacenamiento
    //es string
    [Required(ErrorMessage = "Igrese la capacidad total de almacenamiento")]
    [StringLength(10,ErrorMessage ="La capacidad de almacenamiento no puede superar los 10 caracteres")]
    public string? CapacidadAlmacenamiento { get; set; } = null!;

    //validadores tipo cpu
    //listado 0 = chip, 1 = tarjeta, 2 = otro..
    [Required(ErrorMessage ="Porfavor seleccione el tipo de Gpu")]
    [Range (0, 2, ErrorMessage = "Porfavor ingrese uno de los valores definidos")]
    public int? TipoGpu { get; set; } = null!;


    //validadores graficos
    //este es string
    [Required(ErrorMessage ="Porfavor ingrese el modelo de la gpu")]
    [MaxLength(25, ErrorMessage = "el modelo de la gpu no puede superar los 25 caracteres")]
    public string? Grafico { get; set; } = null!;

    [Required (ErrorMessage ="Porfavor seleccione el estado de la recepcion")]
    [Range(0,2 ,ErrorMessage ="Porfavor seleccione un estado valido")]
    public int? Estado { get; set; } = null!;

    public virtual ICollection<Datosfichatecnica> Datosfichatecnicas { get; set; } = new List<Datosfichatecnica>();

    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    public virtual Servicio IdServicioNavigation { get; set; } = null!;
}