using System.ComponentModel.DataAnnotations;
using MoneyTracker.Entities;
namespace MoneyTracker.Dtos;

public record class AccountFullDto(
    int AccountId,
    int UserId,
    string AccountName,
    decimal Balance,
    string Currency,
    DateTime CreatedDate);