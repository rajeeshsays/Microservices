using Microsoft.EntityFrameworkCore;
using TransportService.DataAccess; // adjust namespace to where your DbContext lives
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Models;
var builder = WebApplication.CreateBuilder(args);

// ✅ ConfigureServices equivalent
var connectionString = builder.Configuration.GetConnectionString("MariaDbConnection");

if (string.IsNullOrEmpty(connectionString))
    throw new Exception("MariaDbConnection is missing!");


// Register DbContext with MariaDB
builder.Services.AddDbContext<TransportServiceDBContext>(options =>
    options.UseMySql(
        connectionString,
        ServerVersion.AutoDetect(connectionString),
        mySqlOptions =>
        {
            mySqlOptions.EnableRetryOnFailure();
        }
    )
);



// Add framework services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMemoryCache();
builder.Services.AddSwaggerGen();

var app = builder.Build();


app.UseCors(options =>
{
    options.WithOrigins("http://localhost:3000","http://192.168.1.4:3000")
           .AllowAnyHeader()
           .AllowAnyMethod();
});

// ✅ Apply migrations automatically at startup (optional, but useful in dev/staging)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TransportServiceDBContext>();
    db.Database.Migrate();
}

// Configure middleware pipeline
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}
app.UseAuthorization();
app.MapControllers();

app.Run();
