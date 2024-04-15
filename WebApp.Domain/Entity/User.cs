using Microsoft.AspNetCore.Identity;

namespace WebApp.Domain.Entity;

public class User: IdentityUser
{
    public IEnumerable<Review>? Reviews { get; set; }
}