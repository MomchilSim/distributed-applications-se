namespace MoneyTracker.Dtos;

public record class UserDto(
    int UserId,
    string Username,
    string Password,   
    string Email,
    DateOnly CreatedDate,
    DateTime DateOfBirth
);
