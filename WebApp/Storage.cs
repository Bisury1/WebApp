using WebApp.Domain.Entity;

namespace WebApp;

public static class Storage
{
    public static User User = new()
    {
        Email = "fnebaev@mail.ru",
        UserName = "a.nebaev"
    };

    public static string UserPassword = "123Az_";

    public static string Email = "fnebaev@mail.ru";
}