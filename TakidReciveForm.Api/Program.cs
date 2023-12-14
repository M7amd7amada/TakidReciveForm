using Microsoft.EntityFrameworkCore;

using TakidReciveForm.Api.Services;
using TakidReciveForm.DataAccess.Data;
using TakidReciveForm.DataAccess.Repositories;
using TakidReciveForm.Domain.Helper;
using TakidReciveForm.Domain.Interfaces;
using TakidReciveForm.Domain.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("SqlServer");

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddControllers();
builder.Services.AddScoped<IFormRepository, FormRepository>();
builder.Services.AddScoped<IImagesService, ImagesService>();
builder.Services.AddScoped<IAttachmentService, AttachmentService>();
builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();