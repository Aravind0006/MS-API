using Microsoft.EntityFrameworkCore;
using WebApplication1.Dbconnection;

var builder = WebApplication.CreateBuilder(args);

// Allow specific origins in CORS policy
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure CORS policy
builder.Services.AddCors(options =>
{
  options.AddPolicy(MyAllowSpecificOrigins, builder =>
  {
    builder.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
  });
});

// Configure Entity Framework Core with SQL Server
builder.Services.AddDbContext<UserDb>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQLConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Enable CORS using the configured policy
app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
