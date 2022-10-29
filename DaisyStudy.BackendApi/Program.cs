using DaisyStudy.Data.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using DaisyStudy.Application.Common;
using DaisyStudy.Utilities.Constants;
using Microsoft.AspNetCore.Identity;
using DaisyStudy.Data.Entities;
using DaisyStudy.Application.System.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using FluentValidation.AspNetCore;
using FluentValidation;
using DaisyStudy.ViewModels.System.Users;
using DaisyStudy.Application.Catalog.Classes;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DaisyStudyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString(SystemConstants.MainConnectionString) ?? throw new InvalidOperationException("Connection string 'DaisyStudyDbContext' not found.")));
builder.Services.AddIdentity<AppUser, AppRole>()
    .AddEntityFrameworkStores<DaisyStudyDbContext>()
    .AddDefaultTokenProviders();

// Declare DI
builder.Services.AddTransient<IStorageService, FileStorageService>();

builder.Services.AddTransient<IPublicClassService, PublicClassService>();
builder.Services.AddTransient<IManageClassService, ManageClassService>();
builder.Services.AddTransient<UserManager<AppUser>, UserManager<AppUser>>();
builder.Services.AddTransient<SignInManager<AppUser>, SignInManager<AppUser>>();
builder.Services.AddTransient<RoleManager<AppRole>, RoleManager<AppRole>>();
builder.Services.AddTransient<IUserService, UserService>();

builder.Services.AddTransient<IValidator<LoginRequest>, LoginRequestValidator>();
builder.Services.AddTransient<IValidator<RegisterRequest>, RegisterRequestValidator>();

builder.Services.AddControllers()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<LoginRequestValidator>());

// Add services to the container.
builder.Services.AddControllers().AddFluentValidation();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Swagger DaisyStudy", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token in the text input below. Example: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
      {
        {
          new OpenApiSecurityScheme
          {
            Reference = new OpenApiReference
              {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
              },
              Scheme = "oauth2",
              Name = "Bearer",
              In = ParameterLocation.Header,
          },
          new List<string>()
        }
      }
    );
});

var issuer = builder.Configuration["Tokens:Issuer"];
var signingKey = builder.Configuration["Tokens:Key"];

byte[] signingKeyBytes = System.Text.Encoding.UTF8.GetBytes(signingKey);

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidIssuer = issuer,
        ValidateAudience = true,
        ValidAudience = issuer,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = System.TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(signingKeyBytes)
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

}

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger DaisyStudy v1"));

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

