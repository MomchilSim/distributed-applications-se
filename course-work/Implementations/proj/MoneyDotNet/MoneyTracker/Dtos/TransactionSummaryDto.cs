namespace MoneyTracker.Dtos;

public record class TransactionSummaryDto(
    int TransactionID,
    string AccName,
    decimal Amount,
    string Category,
    string? Description,
    DateTime TransactionDate
);
