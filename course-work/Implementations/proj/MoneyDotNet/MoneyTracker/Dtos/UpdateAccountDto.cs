using System.ComponentModel.DataAnnotations;

namespace MoneyTracker.Dtos;

public record class UpdateAccountDto
(
    int UserId,
    [StringLength(30)]string AccountName,
    [Required]decimal Balance,
    [Required][StringLength(15)]string Currency,
    [Required] DateTime CreatedDate
    );