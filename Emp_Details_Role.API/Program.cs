using Emp_Details_Role.API.Data;
using Emp_Details_Role.API.Repositories;
using Microsoft.EntityFrameworkCore;
using Emp_Details_Role.API.Data;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
var SecurityScheme = new OpenApiSecurityScheme
{
    Name = "JWT Authentication",
    Description = "Enter Valid JWT bearer token",
    In = ParameterLocation.Header,
    Type = SecuritySchemeType.Http,
    Scheme = "bearer",
    BearerFormat = "JWT",
    Reference = new OpenApiReference
    {
        Id = JwtBearerDefaults.AuthenticationScheme,
        Type = ReferenceType.SecurityScheme
    }
};
    options.AddSecurityDefinition(SecurityScheme.Reference.Id, SecurityScheme);
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {SecurityScheme,new string[]{ } }
    });
});

builder.Services.AddDbContext<EmpDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Emp_Details_Roles"));
});

builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<IEmpRoleRepository, EmpRoleRepository>();
builder.Services.AddScoped<IEmpDetRepository,EmpDetRepository>();

builder.Services.AddSingleton<IUserRepository, StaticUserRepository>();
builder.Services.AddScoped<ITokenHandler, Emp_Details_Role.API.Repositories.TokenHandler>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddFluentValidation(options => options.RegisterValidatorsFromAssemblyContaining<Program>());

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
