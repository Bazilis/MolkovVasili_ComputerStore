using System.ComponentModel.DataAnnotations;

namespace ComputerStore.BLL.Models.UserModels
{
    public class RegisterDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "PassError")]
        public string Password { get; set; }
    }
}
