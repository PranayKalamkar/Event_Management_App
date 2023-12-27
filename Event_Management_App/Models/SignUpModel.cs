using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Event_Management_App.Models
{
    [Table("SignUp")]
    public class SignUpModel
    {
        [Required]
        [StringLength(50)]
        public string? Username { get; set; }

        [Required]
        [StringLength(50)]
        public string? Email { get; set; }

        [Required]
        [StringLength(50)]
        public string? SignUpPassword { get; set; }

        [Required]
        [StringLength(50)]
        public string? ConfirmSignUpPassword { get; set; }
    }
}
