using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using OnlineStoreExample;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);
if (!builder.Environment.IsDevelopment())
{
   string connectionString = await System.IO.File.ReadAllTextAsync("/var/www/store.devportfolio.site/connectionstring.txt");
   Console.WriteLine(connectionString);

   // Add services to the container.
   builder.Services.AddDbContext<AppDbContext>
       (options =>
       options.UseSqlServer(connectionString));
}
else
{
   // Add services to the container.
   builder.Services.AddDbContext<AppDbContext>
       (options =>
       options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
}
builder.Services.AddTransient<ICategory, CategoryData>();
builder.Services.AddTransient<IProduct, ProductData>();
builder.Services.AddTransient<ICart, CartData>();
builder.Services.AddTransient<OnlineStoreLibrary.DataAccess.IEmailSender, EmailSender>();
builder.Services.AddResponseCaching();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor()
    .AddHubOptions(options =>
    {
       options.ClientTimeoutInterval = TimeSpan.FromSeconds(30);
       options.EnableDetailedErrors = false;
       options.HandshakeTimeout = TimeSpan.FromSeconds(15);
       options.KeepAliveInterval = TimeSpan.FromSeconds(15);
       options.MaximumParallelInvocationsPerClient = 1;
       options.MaximumReceiveMessageSize = 32 * 1024;
       options.StreamBufferCapacity = 10;
    });
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddIdentity<UserModel, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders()
    .AddDefaultUI();
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<UserModel>>();

builder.Services.AddAuthorization(options =>
{
   options.AddPolicy("AdminPolicy", policy => policy.RequireClaim("Admin"));
   options.AddPolicy("OwnerPolicy", policy => policy.RequireClaim("Owner"));
});




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   app.UseMigrationsEndPoint();
   app.UseResponseCaching();

   app.UseStaticFiles(new StaticFileOptions
   {
      OnPrepareResponse = ctx =>
      {
         // Проверяем, является ли запрашиваемый файл bootstrap.min.css
         if (ctx.File.Name.Equals("bootstrap.min.css", StringComparison.OrdinalIgnoreCase))
         {
            // Устанавливаем время кэширования в 7 дней
            ctx.Context.Response.Headers.Append("Cache-Control", "public, max-age=604800");
         }
      }
   });
}
else
{
   app.UseExceptionHandler("/Error");
}


app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
