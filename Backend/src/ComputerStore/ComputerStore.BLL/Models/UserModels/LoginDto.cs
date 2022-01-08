using System.ComponentModel.DataAnnotations;

namespace ComputerStore.BLL.Models.UserModels
{
    public class LoginDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
