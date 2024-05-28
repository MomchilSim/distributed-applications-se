using System.ComponentModel.DataAnnotations;
using MoneyTracker.Entities;
namespace MoneyTracker.Dtos;

public record class AccountSummaryDto(
    int AccountId,
    string Name,
    string AccountName,
    decimal Balance,
    string Currency,
    DateTime CreatedDate);