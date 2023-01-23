using AutoMapper;
using DAL.AutoMapper;
using DAL.Data;
using E_LearningTask.Services;
using E_LearningTask.Services.Helper;
using E_LearningTask.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
////////////////////////////////////////////////////////////////////////////////////////
///////////AddCors
builder.Services.AddCors();

/////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////Authentication
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    var secretkey = Encoding.ASCII.GetBytes(builder.Configuration["SecretKey"]);
    o.SaveToken = true;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        //ValidIssuer = Configuration["JWT:Issuer"],
        //ValidAudience = Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(secretkey)
    };
});


//////////////////////////////////////////////////////////////////////////////
////////////////////Connect to DB
builder.Services.AddDbContext<ApplicationDBContext>(op =>
{
    op.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
//////////////////////////////////////////////////////////////////////////////
/////////////////IMapperRegister
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
///////////////////////////////////////////////////////////
//////////Extension
builder.Services.AddScoped<IExtension, Extension>();
//////////////////////////////////////////////////////////
//////////////Other Entities Services
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<ICourseServices, CourseServices>();
builder.Services.AddScoped<ICategoryServices, CategoryServices>();
builder.Services.AddScoped<IInstructorServices, InstructorServices>();
builder.Services.AddScoped<IStGroupServices, StGroupServices>();
builder.Services.AddScoped<IPlayListServices, PlayListServices>();
builder.Services.AddScoped<ILessonServices, LessonServices>();
builder.Services.AddScoped<IRoleServices, RoleServices>();


//////////////////////////Link
builder.Services.AddScoped<IUserCourseServices, UserCourseServices>();
builder.Services.AddScoped<IUserGroupServices, UserGroupServices>();
builder.Services.AddScoped<IPlayListGroupServices, PlayListGroupServices>();


///////////////////////////////////////////////////////////////////////////////
/////////Localization>>services
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
////////////////////////////////////////////////////////////////////////////////////
////////////////////Pipline
app.UseCors(o =>
{
    o.AllowAnyHeader(); //
    o.AllowAnyMethod();  //get,post,...Could select>>  o.WithMethods();
    o.AllowAnyOrigin();  //anyhost...  Could select>>  o.WithOrigin("url:port/","  ",...);
});

app.UseHttpsRedirection();


app.UseAuthentication();      //Login(User,,,,Password)
app.UseAuthorization();       //permission and role>>>>JWT from Token

app.MapControllers();

/////////////////////////////////////////////////////////////////////////////
//app.UseStaticFiles();  //to access files from url
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Files")),
    RequestPath = new PathString("/Files")
});

/////////////////////////////////////////////////////////////////////////////
///////////////localization
var supportedCultures = new[] { "en-US", "ar-EG" };
var localizationOptions =
    new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);
app.UseRequestLocalization(localizationOptions);  /////// default language 

app.Run();
