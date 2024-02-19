namespace CSharpBlazorCICDSkeleton.Frontend.Entities;

public class NavMenuLinkEntity
{
    public string? DisplayName { get; init; }
    public string ClassName { get; init; } = "nav-link";
    public string? HrefValue { get; init; }
    public bool IsVisible { get; init; } = true;
}
