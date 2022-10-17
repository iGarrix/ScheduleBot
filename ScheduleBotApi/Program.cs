using Microsoft.OpenApi.Models;
using System.Reflection;
using Telegram.Bot.Types;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { 
        Title = "TelegramBot API",
        Description = "Rshu telegram schedule bot API",
        Version = "v1" });

    var fileDoc = Path.Combine(System.AppContext.BaseDirectory, $"{assemblyName}.xml");
    c.IncludeXmlComments(fileDoc);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//}
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
