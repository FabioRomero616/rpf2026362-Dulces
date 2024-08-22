using System.ComponentModel.DataAnnotations;

namespace Chocoworld.Models
{
    public class CambiarContrasenaViewModel
    {
        // En tu carpeta Models
        
            [Required(ErrorMessage = "La contraseña actual es requerida")]
            [DataType(DataType.Password)]
            public string ContrasenaActual { get; set; }

            [Required(ErrorMessage = "La nueva contraseña es requerida")]
            [DataType(DataType.Password)]
            public string NuevaContrasena { get; set; }

            [Compare("NuevaContrasena", ErrorMessage = "La confirmación de la contraseña no coincide")]
            [DataType(DataType.Password)]
            public string ConfirmarNuevaContrasena { get; set; }
        

    }
}
