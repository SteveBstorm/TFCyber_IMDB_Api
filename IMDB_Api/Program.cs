using Asp_Demo_Archi_BLL.Interfaces;
using ASP_Demo_Archi_DAL.Repositories;
using DAL = ASP_Demo_Archi_DAL.Services;
using BLL = Asp_Demo_Archi_BLL.Services;
using Microsoft.OpenApi.Models;
using System.Reflection;
using IMDB_Api.Tools;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//Configuration Swagger Doc
#region Swagger
builder.Services.AddSwaggerGen(
c =>
{
c.SwaggerDoc("v1", new OpenApiInfo
{
    Title = "IMDB Api",
    Description = "Api fournissant des infos cin�",
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
#endregion

//Bloc d'enregistrements des services dans la DI
#region Injection de d�pendances
builder.Services.AddTransient<SqlConnection>(sp 
    => new SqlConnection(builder.Configuration.GetConnectionString("default")));

builder.Services.AddScoped<IMovieRepo, DAL.MovieService>();
builder.Services.AddScoped<IPersonRepo, DAL.PersonService>();
builder.Services.AddScoped<IUserRepo, DAL.UserService>();
builder.Services.AddScoped<IMovie_PersonRepo, DAL.Movie_PersonService>();
builder.Services.AddScoped<IMovieService, BLL.MovieService>();
builder.Services.AddScoped<IPersonService, BLL.PersonService>();
builder.Services.AddScoped<IUserService, BLL.UserService>();

builder.Services.AddScoped<TokenGenerator>();
#endregion

//Config de la s�curit� via Token JWT
#region Authentification JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            builder.Configuration.GetSection("TokenInfo").GetSection("secretKey").Value)),
        ValidateLifetime = true,
        ValidAudience = "https://monclient.com",
        ValidIssuer = "https://monapi.com",
        ValidateAudience = false
    };
}
);

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("adminPolicy", policy => policy.RequireRole("admin"));
    //options.AddPolicy("modoPolicy", policy => policy.RequireRole("admin", "moderator"));
    //options.AddPolicy("adminPolicy", policy => policy.RequireClaim("UserId", "1");
    options.AddPolicy("isConnectedPolicy", policy => policy.RequireAuthenticatedUser());
});
#endregion




//builder.Services.AddCors(options => options.AddPolicy("maSecurite",
//    o => o.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowCredentials().AllowAnyHeader()));
//builder.Configuration.GetConnectionString("default");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//app.UseCors("maSecurite");
app.UseCors(o=> o.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
//Dans cet ordre pr�cis sinon c'est tout nu dans les orties
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(app.Environment.ContentRootPath, "upload")),
    RequestPath = "/upload"
});

app.Run();
