using System.ComponentModel.DataAnnotations;

namespace CarsManager.Server.Models
{
    public class UpdatePasswordModel
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string OldPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
    }
}
