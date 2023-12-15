using System.ComponentModel.DataAnnotations;

namespace BusinessNetworking.Entities
{
    public class UserCliente
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "El nombre de usuario es obligatorio")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Name { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio")]
        [EmailAddress(ErrorMessage = "El correo electrónico no tiene un formato válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres")]
        public string Password { get; set; }
    }
}
