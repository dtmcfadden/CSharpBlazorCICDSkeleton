using CSharpBlazorCICDSkeleton.Frontend.Hubs;

namespace CSharpBlazorCICDSkeleton.Frontend.Register;

public static class RegisterMapHubs
{
    public static IEndpointRouteBuilder AddMapHubs(this IEndpointRouteBuilder builder)
    {
        builder.MapHub<ChatHub>("/chathub");

        return builder;
    }
}
