using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MoneyTracker.Models
{
    public class AccountDisplayModel
    {
        public int AccountId { get; set; }
        [Required][StringLength(50)]public string? Name { get; set; }
        [Required][StringLength(30)] public string AccountName { get; set; }
        [Required] public decimal Balance { get; set; }
        [Required][StringLength(15)] public string Currency { get; set; }
        public DateTime CreatedDate{get; set;}
    }
}
