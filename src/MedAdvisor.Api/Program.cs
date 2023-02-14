
using System.Text;
using System.Text.Json.Serialization;
using AutoMapper;
using MedAdvisor.Commons.Email;
using MedAdvisor.DataAccess.MySql;
using MedAdvisor.DataAccess.MySql.DataContext;
using MedAdvisor.DataAccess.MySql.Repositories;
using MedAdvisor.DataAccess.MySql.Repositories.Allergies;
using MedAdvisor.DataAccess.MySql.Repositories.Users;
using MedAdvisor.Infrastructrure.Interfaces;
using MedAdvisor.Infrastructrure.Repositories;
using MedAdvisor.Services.Okta.Interfaces;
using MedAdvisor.Services.Okta.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddScoped<IAllergyService, AllergyService>();
builder.Services.AddScoped<IAllergyRepository, AllergyRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserServices, UserService>();
builder.Services.AddScoped<IMedicineService, MedicineService>();
builder.Services.AddScoped<ImedicineRepository, MedicineRepository>();
builder.Services.AddScoped<IVaccineService, VaccineService>();
builder.Services.AddScoped<IVaccineRepository, VaccineRepository>();
builder.Services.AddScoped<IDiagnosesService, DiagnosesService>();
builder.Services.AddScoped<IDiagnosesRepository,DiagnosesRepository>();
builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
builder.Services.AddScoped<IDocumentService, DocumentService>();
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<IProfileService, ProfileService>();

System.Net.ServicePointManager.Expect100Continue = false;
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
// adding data seeder initializer
builder.Services.AddTransient<DataSeeder>();


// Auto Mapper Configurations  
var mappingConfig = new MapperConfiguration(mc => {
    mc.AddProfile(new Mapping());
    mc.AllowNullCollections = true;
    mc.AllowNullDestinationValues = true;
});

IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);


builder.Services.AddCors(options =>
{

    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

// add db contect 
builder.Services.AddDbContext<AppDbContext>(
    options => options.UseMySql(builder.Configuration.GetConnectionString("dbConnection"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("dbConnection"))));

//
builder.Services.AddControllers().AddJsonOptions(x =>
x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.Configure<GoogleAuthSettings>(builder.Configuration.GetSection("google"));
// For Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();


// Adding Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})


// Adding Jwt Bearer
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidAudience = configuration["JWT:ValidAudience"],
        ValidIssuer = configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
    };
});




builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "JWTToken_Auth_API",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
        }
    });
});



var app = builder.Build();


if (args.Length == 1 && args[0].ToLower() == "seeddata")
    SeedData(app);

//Seed Data
void SeedData(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
    using (var scope = scopedFactory?.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<DataSeeder>();
        service?.Seed();
    }
}

app.UseCors();

app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path
    .Combine(builder.Environment.ContentRootPath, @"Resources")),
    RequestPath = new PathString("/Resources")
});



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();



public partial class Program { };











