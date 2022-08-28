using EventAPI.Business.Interfaces;
using EventAPI.Business.Services;
using EventAPI.DAL.Context;
using EventAPI.DAL.Interfaces;
using EventAPI.DAL.Repositories;
using EventAPI.UI.DI;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var configurationBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
var options = new DbContextOptionsBuilder().Options;
builder.Services.AddDbContext<ApplicationDbContext>(_ => new ApplicationDbContext(options: options, configurationBuilder));

builder.Services.AddAuthentificationDI();

builder.Services.ConfigureApplicationCookie(config =>
{
    config.Cookie.Name = "Events.Identity.Cookie";
    config.LoginPath = "/Auth/Login";
    config.LogoutPath = "/Auth/Logout";
});

builder.Services.AddTransient<IEventRepository, EventRepository>();
builder.Services.AddTransient<IEventService, EventService>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

builder.Services.AddMapperDI();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    config.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(config =>
    {
        config.RoutePrefix = String.Empty;
        config.SwaggerEndpoint("swagger/v1/swagger.json", "Events API");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseIdentityServer();

app.Run();
