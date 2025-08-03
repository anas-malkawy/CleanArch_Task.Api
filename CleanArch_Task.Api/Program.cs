using System;
using CleanArch_Task.Application.Service;
using CleanArch_Task.Domain.IRepo;
using CleanArch_Task.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddDbContext<BookingDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<CleanArch_Task.Application.IService.IService, CleanArch_Task.Application.Service.Service>();
builder.Services.AddScoped<CleanArch_Task.Domain.IRepo.IRepo, CleanArch_Task.Infrastructure.Repo.Repo>();

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
