using System.ComponentModel.DataAnnotations;

namespace MoneyTracker.Models
{
    public class UserModel
    {
        public int UserId { get; set; }
        [Required][StringLength(30)] public string UserName { get; set; }
        [Required][StringLength(30)] public string Password { get; set; }
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Your Email is not valid.")][StringLength(65)] public string Email { get; set; }
        public DateOnly CreatedDate { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
