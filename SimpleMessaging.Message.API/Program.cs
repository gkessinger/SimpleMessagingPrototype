using Microsoft.EntityFrameworkCore;
using SimpleMessaging.Message.Repository.Interfaces;
using SimpleMessaging.Message.Repository.SQL;
using SimpleMessaging.Message.Repository.SQL.Data;

var builder = WebApplication.CreateBuilder(args);

// Add repositor context

var connectionString = builder.Configuration.GetConnectionString("MessageRepositoryDbContextConnection") ?? throw new InvalidOperationException("Connection string 'MessageRepositoryDbContextConnection' not found.");

builder.Services.AddDbContext<MessageRepositoryDbContext>(options => options.UseSqlServer(connectionString)); ;

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IMesssageRepository, MesssageRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();