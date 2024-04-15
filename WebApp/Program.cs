using System.Reflection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WebApp;
using WebApp.Application.DI;
using WebApp.Domain.Entity;
using WebApp.Domain.Enum;
using WebApp.Infrastructure.AppDbContext;
using WebApp.Infrastructure.DI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => options.LoginPath = "/v1/users/auth");
builder.Services.AddAuthorization();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Books Reviews API",
        Description = "An ASP.NET Core Web API for managing items",
    });
    
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddControllers();
builder.Services.AddApplication();
builder.Services.AddLogging(option => option.AddSimpleConsole());

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();

    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles = await roleManager.Roles.ToListAsync();
    if (roles.FirstOrDefault(r => r.Name == nameof(UserRoles.Admin)) is null)
        await roleManager.CreateAsync(new IdentityRole { Name = nameof(UserRoles.Admin), NormalizedName = "ADMIN" });
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    var user = Storage.User;
    await userManager.CreateAsync(user, Storage.UserPassword);
    user = await userManager.FindByNameAsync("a.nebaev");
    await userManager.AddToRoleAsync(user, nameof(UserRoles.Admin));
}

app.UseSwagger(options =>
{
    options.SerializeAsV2 = true;
});
app.UseSwaggerUI();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseAuthentication();
app.UseAuthorization();

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapControllers();

app.UseAuthorization();

app.Run();