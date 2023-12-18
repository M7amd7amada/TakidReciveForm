using Microsoft.EntityFrameworkCore;

using TakidReciveForm.DataAccess.Data;
using TakidReciveForm.Domain.Helper;

namespace TakidReciveForm.Api.Extensions;

public static class BuilderExtensions
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("SqlServer");
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));

        builder.Services.AddControllers();
        builder.Services.AddAutoMapper(typeof(MappingProfile));

        builder.Services.RegisterServices();
    }
}
