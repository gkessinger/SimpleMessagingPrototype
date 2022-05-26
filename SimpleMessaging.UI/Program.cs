using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using SimpleMessaging.Message.Repository.Interfaces;
using SimpleMessaging.Message.Repository.SQL;
using SimpleMessaging.Message.Repository.SQL.Data;
using SimpleMessaging.UI.Data;

var builder = WebApplication.CreateBuilder(args);

var identityConnectionString = builder.Configuration.GetConnectionString("ApplicationIdentityConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationIdentityConnection' not found.");
var messageRepositoryConnection = builder.Configuration.GetConnectionString("MessageRepositoryDbContextConnection") ?? throw new InvalidOperationException("Connection string 'MessageRepositoryDbContextConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(identityConnectionString));;
builder.Services.AddDbContext<MessageRepositoryDbContext>(options => options.UseSqlServer(messageRepositoryConnection)); ;

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddScoped<IMesssageRepository, MesssageRepository>();

builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseStaticFiles();

app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"..\Documentation")),
    RequestPath = new PathString("")
});

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller}/{action}/"
    );
});

app.MapRazorPages();

app.Run();