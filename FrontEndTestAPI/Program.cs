using FrontEndTestAPI.Data.AppDbContext;
using FrontEndTestAPI.DataServices;
using FrontEndTestAPI.DbAccessLayer.DataServices;
using FrontEndTestAPI.DbAccessLayer.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Cors;

var builder = WebApplication.CreateBuilder(args);

/*----------------------------------------------------------------------------*/
/*---------------------------------DI CONTAINER-------------------------------*/
/*----------------------------------------------------------------------------*/

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.WriteIndented = true;
        //options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddEndpointsApiExplorer();         // Swagger Stuff
builder.Services.AddSwaggerGen();                   // Swagger Stuff

// API to Use an SQL Server and providing the Connection String
// The Connection String will lead to the location of the DB
builder.Services.AddDbContext<ApplicationDbContext>(options => 
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
        )
    );

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AngularPolicy",
        cfg =>
        {
            cfg.AllowAnyHeader();
            cfg.AllowAnyMethod();
            cfg.WithOrigins(builder.Configuration["AllowedCORS"]);
        });
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 9;
}).AddEntityFrameworkStores<ApplicationDbContext>();


builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        RequireExpirationTime = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecurityKey"]))
    };
});


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());    

builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<JwtCreatorService>();        // Injecting the Concrete Implementation

var app = builder.Build();

/*----------------------------------------------------------------------------*/
/*----------------------------MIDDLEWARE PIPELINE-----------------------------*/
/*----------------------------------------------------------------------------*/

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("AngularPolicy");

app.MapControllers();

// Minimal API
app.MapMethods("/api/heartbeat", new[] { "HEAD" }, () => Results.Ok());


app.Run();
