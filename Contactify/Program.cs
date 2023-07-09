using Contactify.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// we have given EF everything its needs to create an InMemory Database & also knows the tables bcoz we have given a DbSet
//builder.Services.AddDbContext<ContactifyDbContext>(options => options.UseInMemoryDatabase("ContactsDb"));

// replace our InMemory Database into SqlServer Database [ can change to postgresql or sqlite as per need ]
builder.Services.AddDbContext<ContactifyDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("ContactifyConnectionString")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
