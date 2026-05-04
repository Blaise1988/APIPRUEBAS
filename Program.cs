using Microsoft.EntityFrameworkCore;
using APIPRUEBAS.models;
using System.Text.Json.Serialization;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DbapiContext>(opt=>opt.UseSqlServer(builder.Configuration.GetConnectionString("cadenaSQL")));
builder.Services.AddControllers().AddJsonOptions(c => { c.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; });

var misReglasCors = "ReglasCors";
builder.Services.AddCors(c => {
    c.AddPolicy(name: misReglasCors, builder => { builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

// esto es una prueba
app.MapGet("/", () => Results.Redirect("/swagger/index.html"));
app.UseSwagger();
app.UseSwaggerUI();





app.UseCors(misReglasCors);


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
