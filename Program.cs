using Microsoft.EntityFrameworkCore;
using GlobomanticsSurveys.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddDbContext<SurveysContext>(
    options => options.UseSqlite(builder.Configuration.GetConnectionString("SurveysDatabase"))
);
builder.Services.AddHttpClient();
builder.Services.AddMemoryCache();
builder.Services.AddSession();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    Seed.Initialize(scope.ServiceProvider);
}

app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.MapRazorPages();
app.Run();
