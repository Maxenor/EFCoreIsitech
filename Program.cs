using EFCoreIsitech.Data;
using EFCoreIsitech.Data.Interceptors;
using EFCoreIsitech.Data.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register HttpContextAccessor for current user information
builder.Services.AddHttpContextAccessor();

// Register CurrentUserService for auditing
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

// Register AuditInterceptor
builder.Services.AddScoped<AuditInterceptor>();

// Add MariaDB database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>((sp, options) => {
    var auditInterceptor = sp.GetService<AuditInterceptor>();
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    
    if (auditInterceptor != null)
    {
        options.AddInterceptors(auditInterceptor);
    }
});

// Add basic health checks
builder.Services.AddHealthChecks();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Apply database migrations at startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    
    // Apply pending migrations
    dbContext.Database.Migrate();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/health");

app.Run();
