using BuildingReport.Business.Abstract;
using BuildingReport.Business.Concrete;
using BuildingReport.DataAcess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Text.Json;
using BuildingReport.Business.Extensions;
using Microsoft.Extensions.Logging;
using BuildingReport.Business.Logging.Abstract;
using BuildingReport.Business.Logging.Concrete;
using BuildingReport.Business.CustomExceptionMiddleware;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IRoleService, RoleManager>()
                .AddScoped<IAuthorityService, AuthorityManager>()
                .AddScoped<IRoleAuthorityService, RoleAuthorityManager>()
                .AddScoped<IUserService, UserManager>()
                .AddScoped<IBuildingService, BuildingManager>()
                .AddScoped<IDocumentService, DocumentManager>()
                .AddScoped<IHashService, HashManager>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

var key = "ThisIsSigninKey12345";
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddSingleton<IJWTAuthenticationService>(new JWTAuthenticationManager(key));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat ="JWT",
        Scheme="bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }

    });



});

//Registering DbContext
builder.Services.AddDbContext<ArcelikBuildingReportDbContext>();
var myAllowSpesificOrigins = "_myAllowSpesificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpesificOrigins,
    policy =>
    {
        policy.WithOrigins("http://localhost:3000")
    .AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});
builder.Services.AddControllers(
options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);


builder.Services.AddSingleton<ILoggerManager, LoggerManager>();

builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.ClearProviders();
    loggingBuilder.AddConsole();
});

var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddConsole();
});

var logger = new LoggerManager(loggerFactory.CreateLogger<LoggerManager>());

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddAutoMapper(typeof(AuthorityMappingProfile), typeof(UserMappingProfile), typeof(RoleMappingProfile), typeof(RoleAuthorityMappingProfile),
    typeof(BuildingMappingProfile), typeof(DocumentMappingProfile));


builder.Services.AddControllers()
    .AddNewtonsoftJson(options => 
    { 
        options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver 
        { 
            NamingStrategy = new Newtonsoft.Json.Serialization.CamelCaseNamingStrategy() 
        }; 
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore; 
        options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore; 
        options.SerializerSettings.Converters.Add(new JsonPatchConverter()); 
    });




var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//app.ConfigureExceptionHandler(logger);
//app.ConfigureCustomExceptionMiddleware();



app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseCors(myAllowSpesificOrigins);

app.Run();
