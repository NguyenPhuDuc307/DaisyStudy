﻿using DaisyStudy.Data.EF;
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
using DaisyStudy.Application.System.Roles;
using DaisyStudy.Application.Catalog.Homeworks;
using DaisyStudy.Application.Catalog.Submissions;
using DaisyStudy.Application.Catalog.Notifications;
using DaisyStudy.Application.Catalog.Comments;
using System.Text.Json.Serialization;
using DaisyStudy.Application.Catalog.Messages;
using DaisyStudy.BackendApi.Hubs;
using DaisyStudy.Application.Catalog.Rooms;
using DaisyStudy.Application.Catalog.Uploads;
using DaisyStudy.Application.Catalog.ExamSchedules;
using DaisyStudy.Application.Catalog.Questions;
using DaisyStudy.Application.Catalog.Answers;
using DaisyStudy.Application.Catalog.StudentExams;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DaisyStudyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString(SystemConstants.MainConnectionString)
    ?? throw new InvalidOperationException("Connection string 'DaisyStudyDbContext' not found.")));

builder.Services.AddIdentity<AppUser, AppRole>()
    .AddEntityFrameworkStores<DaisyStudyDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("https://localhost:5003")
                .AllowAnyHeader()
                .WithMethods("GET", "POST", "PUT", "DELETE")
                .AllowCredentials();
        });
});
// Declare DI
builder.Services.AddTransient<IStorageService, FileStorageService>();

builder.Services.AddTransient<IClassService, ClassService>();
builder.Services.AddTransient<IHomeworkService, HomeworkService>();
builder.Services.AddTransient<ISubmissionService, SubmissionService>();
builder.Services.AddTransient<INotificationService, NotificationService>();
builder.Services.AddTransient<ICommentService, CommentService>();
builder.Services.AddTransient<IRoomService, RoomService>();
builder.Services.AddTransient<IMessageService, MessageService>();
builder.Services.AddTransient<IUploadImageService, UploadImageService>();
builder.Services.AddTransient<IExamSchedulesService, ExamSchedulesService>();
builder.Services.AddTransient<IQuestionService, QuestionService>();
builder.Services.AddTransient<IAnswerService, AnswerService>();
builder.Services.AddTransient<IStudentExamService, StudentExamService>();


builder.Services.AddTransient<UserManager<AppUser>, UserManager<AppUser>>();
builder.Services.AddTransient<SignInManager<AppUser>, SignInManager<AppUser>>();
builder.Services.AddTransient<RoleManager<AppRole>, RoleManager<AppRole>>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IRoleService, RoleService>();

builder.Services.AddTransient<IValidator<LoginRequest>, LoginRequestValidator>();
builder.Services.AddTransient<IValidator<RegisterRequest>, RegisterRequestValidator>();



// Add services to the container.
builder.Services.AddControllers()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<LoginRequestValidator>());

builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


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

builder.Services.AddSignalR();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

}
app.UseCors();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();
app.UseRouting();

app.UseAuthorization();

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger DaisyStudy V1");
});

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHub<ChatHub>("/chatHub");
 

app.Run();

