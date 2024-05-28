using System.ComponentModel.DataAnnotations;

namespace MoneyTracker.Dtos;

public record class UpdateUserDto(
    [Required][StringLength(30)]string Username,
    [Required][StringLength(30)]string Password,   
    [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Your Email is not valid.")][StringLength(65)]string Email,
    [Required]DateTime DateOfBirth
);
