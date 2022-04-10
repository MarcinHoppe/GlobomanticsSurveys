using Microsoft.EntityFrameworkCore;
using GlobomanticsSurveys.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddDbContext<SurveysContext>(
    options => options.UseSqlite(builder.Configuration.GetConnectionString("SurveysDatabase"))
);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    Seed.Initialize(scope.ServiceProvider);
}

app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();
app.Run();
