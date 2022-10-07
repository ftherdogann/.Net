using ExcelCreate.Hubs;
using ExcelCreate.Models;
using ExcelCreate.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});
builder.Services.AddIdentity<IdentityUser,IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddSingleton(sp => new ConnectionFactory() { HostName = "localhost", DispatchConsumersAsync = true });
builder.Services.AddSingleton<RabbitMqClientService>();
builder.Services.AddSingleton<RabbitMqPublisher>();
builder.Services.AddSignalR();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<MyHub>("/MyHub");
});
using (var scope = app.Services.CreateScope())
{

    var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    //appDbContext.Database.Migrate();

    if (!appDbContext.Users.Any())
    {
        userManager.CreateAsync(new IdentityUser()
        {
            UserName = "Sinem",
            Email = "Sinem@outlook.com"
        }, "Sinem12*").Wait();
        userManager.CreateAsync(new IdentityUser()
        {
            UserName = "Fatih",
            Email = "Fatih@outlook.com"
        }, "Fatih12*").Wait();
    }
}
    app.Run();
