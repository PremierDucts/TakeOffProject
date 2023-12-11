using TakeOffAPI.Service.Interface;
using TakeOffAPI.Service;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Cryptography;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using TakeOffAPI.Configuration;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).Enrich.FromLogContext().CreateLogger();
// Add services to the container.
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
var connectionString = builder.Configuration.GetConnectionString("MySQLConnectionString");
builder.Services.AddDbContext<QldDataContext>(options => options.UseMySql(connectionString,ServerVersion.AutoDetect(connectionString)));
builder.Services.AddSwaggerGen(c=>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Add-in api",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter JWT here"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id  ="Bearer"
                }
            },new string[] {}
        }
    });
});
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IFileService, FileService>();
//builder.Services.Configure<ServerSettings>(builder.Configuration.GetSection("Server"));
//builder.Configuration.AddJsonFile("appsettings.json",optional:false,reloadOnChange: true);

builder.WebHost.UseUrls(builder.Configuration.GetValue<string>("URL:API_Port"));
builder.Services.AddAuthentication(c =>
{
    c.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    c.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    c.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]));

    var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]));
    //options.SaveToken = true;
    //options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateAudience = false,
        ValidateIssuer = false,
        ValidateIssuerSigningKey=true,
        IssuerSigningKey = new SymmetricSecurityKey( hmac.Key)

    };
});
builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();

