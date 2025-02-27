using BookManagementBackend;
using BookManagementBackend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

// Register DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// CORS Configuration
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
	options.AddPolicy(MyAllowSpecificOrigins, policy =>
	{
		policy.WithOrigins("https://localhost:7154", "http://localhost:7154") // Blazor frontend URLs
			  .AllowAnyHeader()
			  .AllowAnyMethod()
			  .AllowCredentials(); // Required if using authentication/cookies
	});
});

var app = builder.Build();

// Enable Swagger in Development Mode
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(c =>
	{
		c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
		c.RoutePrefix = string.Empty; // Swagger at root URL
	});
}

app.UseHttpsRedirection();
app.UseRouting(); // Ensure UseRouting is called before UseCors
app.UseCors(MyAllowSpecificOrigins); // Enable CORS Middleware
app.UseAuthorization();
app.MapControllers();
app.Run();