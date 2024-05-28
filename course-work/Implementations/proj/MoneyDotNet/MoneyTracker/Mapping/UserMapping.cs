using MoneyTracker.Dtos;
using MoneyTracker.Entities;

namespace MoneyTracker.Mapping;

public static class UserMapping
{
    public static User ToEntity(this CreateUserDto user){
            return new User()
            {
                Username = user.Username,
                Password = user.Password,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth,
                CreatedDate = DateOnly.FromDateTime(DateTime.UtcNow)
                
            };
    }
    public static User ToEntity(this UpdateUserDto user, int id){
             return new User()
            {
                UserId = id,
                Username = user.Username,
                Password = user.Password,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth
            };
    }
    public static UserDto ToDto (this User user){
                return new(
               user.UserId,
               user.Username,
               user.Password,
               user.Email!,
               user.CreatedDate,
               user.DateOfBirth
                );
    }
}
