namespace MoneyTracker.Dtos;

public record class TransactionFullDto(
    int TransactionID,
    int AccountID,
    decimal Amount,
    string Category,
    string? Description,
    DateTime TransactionDate
);
