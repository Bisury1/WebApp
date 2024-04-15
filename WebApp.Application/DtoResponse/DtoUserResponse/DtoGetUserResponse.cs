namespace WebApp.Application.DtoResponse.DtoUserResponse;

public class DtoGetUserResponse
{
    public required string UserId { get; init; }
    public required string Email { get; init; }
    public string UserName { get; init; }
}