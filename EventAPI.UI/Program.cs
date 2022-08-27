using AutoMapper;
using EventAPI.Business.Interfaces;
using EventAPI.Business.Services;
using EventAPI.DAL.Context;
using EventAPI.DAL.Interfaces;
using EventAPI.DAL.Repositories;
using EventAPI.UI.Mappers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
var configurationBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
var options = new DbContextOptionsBuilder().Options;
builder.Services.AddDbContext<ApplicationDbContext>(_ => new ApplicationDbContext(options: options, configurationBuilder));
builder.Services.AddTransient<IEventRepository, EventRepository>();
builder.Services.AddTransient<IEventService, EventService>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

var config = new MapperConfiguration(c =>
{
    c.AddProfile(new UIModelsMapper());
});
IMapper mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
