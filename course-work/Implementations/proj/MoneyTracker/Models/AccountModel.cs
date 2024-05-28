using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyTracker.Models
{
    public class AccountModel
    {
        public int AccountId { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")] public UserModel? User { get; set; }
        [Required][StringLength(30)] public string AccountName { get; set; }
        [Required] public decimal Balance { get; set; }
        [Required][StringLength(15)] public string Currency { get; set; }
        public DateTime CreatedDate{get; set;}
    }
}
