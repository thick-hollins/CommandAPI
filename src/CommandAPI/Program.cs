using System.Windows.Input;
using CommandApi.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddScoped<IRepo, MockRepo>();
var app = builder.Build();

app.MapGet("/", () => "Hell006 World!");

app.MapControllers();

app.Run();
