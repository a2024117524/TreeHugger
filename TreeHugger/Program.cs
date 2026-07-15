using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using TreeHugger.Components;
using TreeHugger.Components.Account;
using TreeHugger.Data;
using TreeHugger.Data.Api;
using FluentMigrator.Runner;
using System.Net;
using System.Net.Mail;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("Properties/appsettings.json");            
builder.Services.AddMudServices();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddIdentityCore<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();
    
builder.Services.AddFluentMigratorCore()
    .ConfigureRunner(runner => runner
        .AddSQLite()
        .WithGlobalConnectionString(connectionString)
        .ScanIn(typeof(Program).Assembly).For.Migrations())
    .AddScoped<IMigrationRunner, MigrationRunner>();

var smtpSettings = builder.Configuration.GetSection("SmtpSettings");

builder.Services
    .AddFluentEmail(smtpSettings["User"])
    .AddSmtpSender(new SmtpClient(smtpSettings["Server"])
    {
        Port = int.Parse(smtpSettings["Port"]),
        Credentials = new NetworkCredential(smtpSettings["User"], smtpSettings["Password"]),
        EnableSsl = true
    });

builder.Services.AddSingleton<IEmailSender<IdentityUser>, EmailSender>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
    runner.MigrateUp();
}

if (app.Environment.IsDevelopment())
{
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapAdditionalIdentityEndpoints();
app.MapAchievementEndpoints();
app.MapChoiceEndpoints();
app.MapCommentEndpoints();
app.MapLessonEndpoints();
app.MapProfileEndpoints();
app.MapQuestionEndpoints();
app.MapQuizEndpoints();
app.MapReplyEndpoints();
app.MapStudentAchievementEndpoints();
app.Run();