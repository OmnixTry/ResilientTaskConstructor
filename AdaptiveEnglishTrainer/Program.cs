using AdaptiveEnglishTrainer.Authorization;
using AdaptiveEnglishTrainer.Authorization.Db;
using AdaptiveEnglishTrainer.Authorization.Entity;
using AdaptiveEnglishTrainer.Authorization.Jwt;
using Component.Groups.BLL;
using Component.Groups.DAL;
using Component.TestCompetion.DAL;
using Component.TestCompletion.BLL;
using Component.TestManagement.BLL;
using Component.TestManagement.DAL;
using Component.TestManagement.DAL.EF;
using Component.TestManagement.DAL.Entity;
using Infrastructure.DAL.Extension;
using Infrastructure.DAL.Repo;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSwaggerGen();

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddDbContext<AuthenticationContext>(opts =>
                opts.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnection"), b => b.MigrationsAssembly("AdaptiveEnglishTrainer")));

builder.Services.AddHttpContextAccessor();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddMvc().AddApplicationPart(Assembly.Load(new AssemblyName("Component.TestManagement.PL")));
builder.Services.AddMvc().AddApplicationPart(Assembly.Load(new AssemblyName("Component.Groups.PL")));
builder.Services.AddMvc().AddApplicationPart(Assembly.Load(new AssemblyName("Component.TestCompletion.PL")));

 // Config authenticvation
 builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<AuthenticationContext>();
    
var jwtConfig = builder.Configuration.GetSection("JwtSettings");
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtConfig.GetSection("validIssuer").Value,
        ValidAudience = jwtConfig.GetSection("validAudience").Value,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.GetSection("securityKey").Value))
    };
});

builder.Services.AddScoped<JwtService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin",
    builder =>
    {
        builder.WithOrigins("http://localhost:4200")
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
    });
});

/// <summary>
/// Register component services
/// </summary>

builder.Services.RegisterAuthServices();
string connectionString = builder.Configuration.GetConnectionString("sqlConnection");
builder.Services.RegisterEfRepositories();
builder.Services.RegisterDAL(connectionString);
builder.Services.RegisterGroupsDAL(connectionString);
builder.Services.RegisterCompletionDAL(connectionString);
builder.Services.RegisterTestManagementBll();
builder.Services.RegisterGroupsBLL();
builder.Services.RegisterTestCompletionnBll();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();

