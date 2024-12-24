using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Lotus_Dashboard1.Data;
using Lotus_Dashboard1.Areas.Identity.Data;
using Lotus_Dashboard.Apis.JWT;
using Lotus_Dashboard1.Apis;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Caching.Memory;
using Lotus_Dashboard1.Apis.GoldEtemadContext;
using Lotus_Dashboard1.Apis.GoldEtemadServices;
//using Microsoft.AspNetCore.Authentication.Certificate;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Lotus_Dashboard1ContextConnection"); 
builder.Services.AddDbContext<Lotus_Dashboard1Context>(options =>
    options.UseOracle(connectionString));


var connectionStringSqlServer = builder.Configuration.GetConnectionString("LotusContextConnectionSqlServer");


builder.Services.AddDbContext<LotusibBIContext>(options =>
    options.UseSqlServer(connectionStringSqlServer));



// Add services to the container.
builder.Services.AddTransient<IDW_Services, DW_Services>();
builder.Services.AddIdentity<Lotus_Dashboard1User, IdentityRole>(

    opts =>
    {

        opts.SignIn.RequireConfirmedAccount = true;
        opts.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyz1234567890._-";
        opts.Password.RequiredLength = 6;
        opts.Password.RequireNonAlphanumeric = false;
        opts.Password.RequireLowercase = false;
        opts.Password.RequireUppercase = false;
        opts.Password.RequireDigit = false;
        opts.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        opts.Lockout.MaxFailedAccessAttempts = 5;
        opts.Lockout.AllowedForNewUsers = false;
        opts.SignIn.RequireConfirmedEmail = false;
        opts.SignIn.RequireConfirmedAccount = false;
        opts.SignIn.RequireConfirmedPhoneNumber = false;
        opts.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
        opts.Lockout.MaxFailedAccessAttempts = 5;


    })
.AddEntityFrameworkStores<Lotus_Dashboard1Context>();

builder.Services.AddAuthentication(options =>
{
    //options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    //options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    //options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;



}).AddJwtBearer(options =>
{
    var secretkey = Encoding.UTF8.GetBytes("1234567890asdfgh");
    var encriptionkey = Encoding.UTF8.GetBytes("qwsadfrewtyh4532");

    var validationParameters = new TokenValidationParameters
    {
        RequireSignedTokens = true,

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(secretkey),

        RequireExpirationTime = true,
        ValidateLifetime = true,

        ValidateAudience = false,

        ValidateIssuer = false,

        TokenDecryptionKey = new SymmetricSecurityKey(encriptionkey)
    };

    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = validationParameters;



}).AddCookie(options =>
{
    options.LoginPath = "./LoginUser";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.ForwardAuthenticate = "/Identity/Account/AccessDenied";
});



builder.Services.AddControllersWithViews();
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          builder.WithOrigins("https://localhost:44409").AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                          builder.WithOrigins("http://localhost:7037").AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                          
                      });
});
builder.Services.AddTransient<IJwtservice, jwtservice>();
//builder.Services.AddAuthentication(
//        CertificateAuthenticationDefaults.AuthenticationScheme)
//        .AddCertificate();

builder.Services.AddAuthentication()
    .AddIdentityServerJwt();

builder.Services.AddMemoryCache();

//builder.Services.AddSingleton<IMemoryCache>();

builder.Services.Configure<JwtBearerOptions>(IdentityServerJwtConstants.IdentityServerJwtBearerScheme, options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidIssuers = new string[] { "https://localhost:44409", "https://localhost:7037" }
    };
});
var app = builder.Build();



//builder.Services.AddHsts(options =>
//{
//    options.Preload = true;
//    options.IncludeSubDomains = true;
//    options.MaxAge = TimeSpan.FromDays(60);
//    options.ExcludedHosts.Add("https://localhost:44400");
//    options.ExcludedHosts.Add("www.example.com");
//});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//builder.Services.AddHttpsRedirection(options =>
//{
//    options.RedirectStatusCode = (int)HttpStatusCode.TemporaryRedirect;
//    options.HttpsPort = 44400;
//});

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();