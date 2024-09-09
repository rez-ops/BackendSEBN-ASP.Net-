using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using WebApplication1.Data;
using WebApplication1.Repositories;
using WebApplication1.Repositories.IRepositories;
using WebApplication1.Services;
using WebApplication1.Services.IServices;

var builder = WebApplication.CreateBuilder(args);

// Configure CORS pour permettre aux requêtes provenant de l'application Angular (http://localhost:4200) d'accéder à l'API
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200") // URL de votre application Angular
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// Configure le contexte de la base de données PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Enregistrer les services et les dépôts
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<ICongeRepositorie, CongeRepositorie>();
builder.Services.AddScoped<ICongeService, CongeService>();

builder.Services.AddScoped<ICompensationRepository, CompensationRepository>();
builder.Services.AddScoped<ICompensationService, CompensationService>();

builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
builder.Services.AddScoped<IpermisionService, PermisionService>();

builder.Services.AddScoped<IChangHoraRepository, ChangHoraRepository>();
builder.Services.AddScoped<IChangHoraService, ChangHoraService>();

builder.Services.AddScoped<IBadgeManqRepository, BadgeManqRepository>();
builder.Services.AddScoped<IBadgeManqService, BadgeManqService>();

builder.Services.AddScoped<IComandeRepository,ComandeRepository>();
builder.Services.AddScoped<IComandeService,ComandeService>();

// Ajouter les contrôleurs et configurer Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "WebApplication1 API",
        Version = "v1"
    });

    // Configurer le schéma de sécurité pour JWT dans Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Veuillez entrer le token JWT avec le mot 'Bearer' suivi d'un espace. Exemple: 'Bearer abc123'",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
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
            Array.Empty<string>()
        }
    });
});

// Configurer l'authentification JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});









var app = builder.Build();

// Configurer le middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApplication1 API v1");
        c.RoutePrefix = string.Empty; 
    });
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Utiliser la politique CORS
app.UseCors("AllowAngularApp");

// Middleware commun
app.UseHttpsRedirection();
//app.UseStaticFiles();
//app.UseRouting();
app.UseAuthentication();  // Activer l'authentification
app.UseAuthorization();   // Activer l'autorisation

// Mapper les contrôleurs
app.MapControllers();

app.Run();
