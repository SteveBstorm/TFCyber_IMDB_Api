using Asp_Demo_Archi_BLL.Interfaces;
using ASP_Demo_Archi_DAL.Repositories;
using DAL = ASP_Demo_Archi_DAL.Services;
using BLL = Asp_Demo_Archi_BLL.Services;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "IMDB Api",
            Description = "Api fournissant des infos ciné",
            Contact = new OpenApiContact
            {
                Name = "Steve Lorent",
                Email = "steve.lorent@bstorm.be"
            },
            Version = "v1"
        });
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFile));
    });

builder.Services.AddScoped<IMovieRepo, DAL.MovieService>();
builder.Services.AddScoped<IPersonRepo, DAL.PersonService>();
builder.Services.AddScoped<IUserRepo, DAL.UserService>();
builder.Services.AddScoped<IMovie_PersonRepo, DAL.Movie_PersonService>();
builder.Services.AddScoped<IMovieService, BLL.MovieService>();

//builder.Configuration.GetConnectionString("default");

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
