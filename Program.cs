using Microsoft.EntityFrameworkCore;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers()
                .AddJsonOptions(options =>
                    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();

var sqlBuilder = new NpgsqlConnectionStringBuilder
{
    Host = config["HOST"],
    Database = config["DB"],
    Username = config["USERNAME"],
    Password = config["PASSWORD"],
    IncludeErrorDetail = true
};

builder.Services.AddDbContext<SeasonalDbContext>(options =>
    options.UseNpgsql(sqlBuilder.ConnectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting(); 

app.MapControllers();

app.Run();

