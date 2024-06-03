using backend.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config =>
{
    config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.ApiKey,
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "Bearer",
    });
    config.AddSecurityRequirement(
        new OpenApiSecurityRequirement
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
                    In = ParameterLocation.Header
                },
                new List<string>()
            }
        });
});
// builder.Services.AddAuthorization();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IContentService, ContentService>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddHttpContextAccessor();
// Add JWT authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Secret"]))
        };
    });

var app = builder.Build();

// Create the database if it doesn't exist
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// app.UseRouting();
//
app.UseAuthentication();
// app.UseAuthorization();


// User routes
app.MapPost("/api/auth/register", (UserDto userDto, IUserService userService) => userService.Register(userDto));
app.MapPost("/api/auth/login", (LoginDto loginDto, IUserService userService) => userService.Login(loginDto));
app.MapGet("/api/users", (IUserService userService) => userService.GetAllUsers());
app.MapGet("/api/users/username/{username}", (string username, IUserService userService) => userService.GetUserByUsername(username));
app.MapGet("/api/users/{id}", (int id, IUserService userService) => userService.GetUserById(id));
app.MapGet("/api/users/current", (HttpContext httpContext, IUserService userService) => userService.GetCurrentUser(httpContext));


// Content routes
app.MapGet("/api/content", (IContentService contentService) => contentService.GetAllContent());
app.MapGet("/api/content/{id}", (int id, IContentService contentService) => contentService.GetContentById(id));
app.MapGet("/api/content/user/{userId}", (HttpContext httpContext, IContentService contentService) => contentService.GetContentByUserId(httpContext));
app.MapGet("/api/content/user/current", (HttpContext httpContext, IContentService contentService) => contentService.GetCurrentUserContent(httpContext));
app.MapPost("/api/content", (ContentDto contentDto, IContentService contentService, HttpContext httpContext) => contentService.CreateContent(contentDto, httpContext)); 
app.MapPut("/api/content/{id}", (int id, ContentDto contentDto, IContentService contentService, HttpContext httpContext) => contentService.UpdateContent(id, contentDto, httpContext));
app.MapDelete("/api/content/{id}", (int id, IContentService contentService, HttpContext httpContext) => contentService.DeleteContent(id, httpContext));

// Friendship routes
app.MapPost("/api/friends/request", (int friendId, IUserService userService, HttpContext httpContext) => userService.SendFriendRequest(friendId, httpContext));
app.MapPut("/api/friends/accept", (int friendId, IUserService userService, HttpContext httpContext) => userService.AcceptFriendRequest(friendId, httpContext));
app.MapDelete("/api/friends/delete/{friendId}", (int friendId, IUserService userService, HttpContext httpContext) => userService.RemoveFriend(friendId, httpContext));
app.MapGet("/api/friends/requests/{userId}", (HttpContext httpContext, IUserService userService) => userService.GetPendingFriendRequests(httpContext));
app.MapGet("/api/friends/{userId}", (HttpContext httpContext, IUserService userService) => userService.GetFriends(httpContext));

app.UseCors();


app.Run();