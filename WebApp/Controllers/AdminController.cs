using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.Domain.Entity;
using WebApp.Domain.Enum;

namespace WebApp.Controllers;

[Route("/v1/admin")]
[Authorize(Roles = nameof(UserRoles.Admin))]
public class AdminV1Controller(UserManager<User> userManager) : ControllerBase
{
    /// <summary>
    /// Добавление пользователя в админы
    /// </summary>
    /// <param name="email">Почта пользователя</param>
    /// <returns></returns>
    [HttpPatch]
    [Route("/users/roles/edit")]
    public async Task<IActionResult> EditRoleToAdmin([FromForm] string email)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user is null)
            return BadRequest();
        
        var result = await userManager.AddToRoleAsync(user, nameof(UserRoles.Admin));
        return result.Succeeded
            ? Ok()
            : BadRequest();
    }

    /// <summary>
    /// Удаление пользователя
    /// </summary>
    /// <param name="email">Почта пользователя</param>
    /// <returns></returns>
    [HttpDelete]
    [Route("/users/delete")]
    public async Task<IActionResult> DeleteUser([FromForm] string email)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user is null)
            return BadRequest();
        
        var result = await userManager.DeleteAsync(user);
        return result.Succeeded
            ? Ok()
            : BadRequest();
    }
}