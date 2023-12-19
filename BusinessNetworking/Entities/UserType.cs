using System.ComponentModel.DataAnnotations;

namespace BusinessNetworking.Entities
{
    public class UserType
    {
        public int UserTypeId { get; set; }

        [Required(ErrorMessage = "El tipo de usuario es obligatorio.")]
        [StringLength(50, ErrorMessage = "El tipo de usuario debe tener como máximo 50 caracteres.")]
        public string Type { get; set; }
    }
}
