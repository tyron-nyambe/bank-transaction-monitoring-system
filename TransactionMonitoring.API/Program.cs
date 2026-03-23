using Microsoft.EntityFrameworkCore;
using TransactionMonitoring.API.Data;
using TransactionMonitoring.API.Services;

var builder = WebApplication.CreateBuilder(args);

// ----------------------
// Services registration
// ----------------------
builder.Services.AddControllers();
builder.Services.AddScoped<TransactionService>(); // one instance per request
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//MySQL DB connection
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    ));

//Enable CORS for frontend dashboard
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

var app = builder.Build();

// ----------------------
//Test DB connection (optional)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try
    {
        db.Database.OpenConnection();
        Console.WriteLine("✅ Database connection SUCCESSFUL");
        db.Database.CloseConnection();
    }
    catch (Exception ex)
    {
        Console.WriteLine("❌ Database connection FAILED");
        Console.WriteLine(ex.Message);
    }
}

// ----------------------
// Middleware / pipeline
// ----------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Global exception handler
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";

        var response = new
        {
            message = "An unexpected error occurred."
        };

        await context.Response.WriteAsJsonAsync(response);
    });
});

// Must come **after exception handler**
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();