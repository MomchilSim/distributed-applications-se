using System.ComponentModel.DataAnnotations;
namespace MoneyTracker.Dtos;

public record class CreateTransactionDto
(
    int AccountID,
    [Required]decimal Amount,
    [StringLength(30)]string Category,
    [StringLength(1500)]string? Description,
    [Required] DateTime TransactionDate
);
