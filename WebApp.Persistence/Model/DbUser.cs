using Microsoft.AspNetCore.Identity;

namespace WebApp.Persistence.Model;

public class DbUser: IdentityUser
{
    public IEnumerable<DbReview>? Reviews { get; set; }
}