using IoCThreatAnalyzer.Components;
using IoCThreatAnalyzer.Data;
using IoCThreatAnalyzer.Parsers;
using IoCThreatAnalyzer.Repositories;
using IoCThreatAnalyzer.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection")
        ?? "Data Source=ioc.db");
});

builder.Services.AddHttpClient();

builder.Services.AddScoped<WebFetchService>();

builder.Services.AddScoped<IocParser>();

builder.Services.AddScoped<IScanRepository, ScanRepository>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider
        .GetRequiredService<AppDbContext>();

    db.Database.Migrate();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();

app.UseAntiforgery();

app.MapGet("/report-malware", () =>
{
    return Results.Content(
        """
        <html>
        <body>

            <h1>Detected Malware Activity</h1>

            <h2>IPs</h2>

            <p>185.220.101.45</p>
            <p>45.9.148.23</p>
            <p>103.27.202.85</p>
            <p>154.42.1.5</p>

            <h2>Domains</h2>

            <p>evil434346.com</p>
            <p>malware-control.net</p>
            <p>darkpanel.cc</p>

            <h2>Emails</h2>

            <p>attacker@darkmail.com</p>
            <p>root@malware.rtr</p>

            <h2>URLs</h2>

            <p>http://malware.test/download</p>
            <p>http://evil-wedse.com/payload.exe</p>

            <h2>Hashes</h2>

            <p>d41d8cd98f00b204e9800998ecf8427e</p>

            <p>
            da39a3ee5e6b4b0d3255bfef95601890afd80709
            </p>

            <p>
            e3b0c44298fc1c149afbf4c8996fb924
            27ae41e4649b934ca495991b7852b855
            </p>

        </body>
        </html>
        """,
        "text/html");
});

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();