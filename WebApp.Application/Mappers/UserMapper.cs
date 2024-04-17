using WebApp.Application.DtoResponse.DtoUserResponse;
using WebApp.Application.Mappers.Interfaces;
using WebApp.Domain.Entity;

namespace WebApp.Application.Mappers;

public class UserMapper: IUserMapper
{
    public DtoGetUserResponse MapToUserResponse(User user)
        => new()
        {
            UserId = user.Id,
            UserName = user.UserName!
        };
}