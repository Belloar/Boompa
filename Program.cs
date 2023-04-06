using Boompa.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Boompa.Auth;
using Boompa.Interfaces.IRepository;
using Boompa.Repositories;
using Boompa.Services;
using Boompa.Interfaces.IService;
using Boompa.Implementations.Services;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

//var connectionString = builder.Configuration.GetConnectionString("MySqlString");
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IIdentityRepository,IdentityRepository>();
builder.Services.AddScoped <IIdentityService, IdentityService>();
builder.Services.AddScoped<ILearnerRepository, LearnerRepository>();
builder.Services.AddScoped<ILearnerService, LearnerService>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<IAdminService,AdminService>();

builder.Services.AddDbContext<ApplicationContext>(options => options.UseMySql(builder.Configuration.GetConnectionString("MySqlString"), new MySqlServerVersion(
              new Version(8, 0, 29))));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
{
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    ValidIssuer = builder.Configuration["Jwt:Issuer"],
    ValidAudience = builder.Configuration["Jwt:Audience"],
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
});

builder.Services.AddCors(options => options.AddDefaultPolicy(builder =>
{
    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
}));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();
app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
