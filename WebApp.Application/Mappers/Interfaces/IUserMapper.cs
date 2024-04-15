using WebApp.Application.DtoResponse.DtoUserResponse;
using WebApp.Domain.Entity;

namespace WebApp.Application.Mappers.Interfaces;

public interface IUserMapper
{
    DtoGetUserResponse MapToUserResponse(User user);
}