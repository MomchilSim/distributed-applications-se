using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;

namespace MoneyTracker.Models
{
    public class TransactionModel
    {
        public int TransactionID { get; set; }
        [Required] public int AccountID { get; set; }
        [Required] public string AccountName { get; set; }
        [Required][ForeignKey("AccountID")] public UserModel? Account { get; set; }
        [Required] public decimal Amount { get; set; }
        [Required][StringLength(30)] public string Category { get; set; }
        [StringLength(1500)] public string? Description { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
