using CSharpBlazorCICDSkeleton.Frontend.Services;
using CSharpBlazorCICDSkeleton.Frontend.Services.Interfaces;

namespace CSharpBlazorCICDSkeleton.Frontend.Register;

public static class RegisterServices
{
    public static IServiceCollection AddFrontendServices(this IServiceCollection services)
    {
        services.AddScoped<INavMenuLinksService, NavMenuLinksService>();

        return services;
    }
}
