using Addressbook.Application.Handlers;
using Addressbook.Application.Mappings;
using Addressbook.Application.Validations;
using Addressbook.Domain;
using Addressbook.Domain.Repositories;
using Addressbook.Models.Dtos;
using Addressbook.Service.Services;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AddressbookContext>(options =>
    options
    .UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnectionString"),
      b => b.MigrationsAssembly(typeof(AddressbookContext).Assembly.FullName)));

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

builder.Services.AddScoped<IAddressBookRepository, AddressBookRepository>();
builder.Services.AddScoped<IAddressbookService, AddressbookService>();
builder.Services.AddScoped<IAddressbookHandler, AddressbookHandler>();
builder.Services.AddTransient<IValidator<IpAddressBookDto>, AddressBookDtoValidator>();
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

app.UseCors(builder =>
{
    builder.AllowAnyOrigin();
    builder.AllowAnyMethod();
    builder.AllowAnyHeader();
});

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AddressbookContext>();
    db.Database.Migrate();
}

app.Run();
