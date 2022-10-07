using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using RabbitMqApp.Imagewatermarkprocess.BackgroundServices;
using RabbitMqApp.Imagewatermarkprocess.Models;
using RabbitMqApp.Imagewatermarkprocess.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton(sp => new ConnectionFactory() { HostName = "localhost",DispatchConsumersAsync=true });
builder.Services.AddSingleton<RabbitMqClientService>();
builder.Services.AddSingleton<RabbitMqPublisher>();
builder.Services.AddHostedService<ImageWatermarkProcessBackgroundService>();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseInMemoryDatabase(databaseName: "productDb");
});
// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
