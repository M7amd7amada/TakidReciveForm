using TakidReciveForm.DataAccess.Repositories;
using TakidReciveForm.Domain.Interfaces;
using TakidReciveForm.Domain.Services;

namespace TakidReciveForm.Api.Extensions;

public static class ServicesExtensions
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IFormRepository, FormRepository>();
        services.AddScoped<IAttachmentService, AttachmentService>();
    }
}