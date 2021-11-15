using CommandApi.Data;
using Microsoft.EntityFrameworkCore;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers();
builder.Services.AddScoped<IRepo, SQLRepo>();

var cstring = new NpgsqlConnectionStringBuilder();
cstring.ConnectionString = builder.Configuration.GetConnectionString("PostgreSqlConnection");
cstring.Username = builder.Configuration["UserID"];
cstring.Password = builder.Configuration["Password"];

builder.Services.AddDbContext<CommandContext>(
    opt => opt.UseNpgsql(cstring.ConnectionString));

var app = builder.Build();

app.MapGet("/", () => "Hell006 World!");

app.MapControllers();

app.Run();
