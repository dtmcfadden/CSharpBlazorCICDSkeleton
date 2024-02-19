using CSharpBlazorCICDSkeleton.Frontend.Entities;
using CSharpBlazorCICDSkeleton.Frontend.Services.Interfaces;

namespace CSharpBlazorCICDSkeleton.Frontend.Services;

public class NavMenuLinksService : INavMenuLinksService
{
    private readonly List<NavMenuLinkEntity> PublicNavMenuLinks =
    [
        new() {
            DisplayName = "Counter",
            ClassName = "bi-plus-square-fill-nav-menu",
            HrefValue = "counter"
        },
        new() {
            DisplayName = "Weather",
            ClassName = "bi-list-nested-nav-menu",
            HrefValue = "weather"
        },
        new() {
            DisplayName = "Chat",
            ClassName = "bi-list-nested-nav-menu",
            HrefValue = "chat"
        }
    ];

    private List<NavMenuLinkEntity> CurrentNavMenuLinks { get; set; } = [];

    public List<NavMenuLinkEntity> GetNavMenuLinks()
    {
        if (CurrentNavMenuLinks.Count == 0)
        {
            CurrentNavMenuLinks = PublicNavMenuLinks;
        }
        return CurrentNavMenuLinks;
    }
}
