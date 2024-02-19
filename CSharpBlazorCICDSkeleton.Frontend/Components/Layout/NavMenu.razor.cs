using CSharpBlazorCICDSkeleton.Frontend.Entities;
using CSharpBlazorCICDSkeleton.Frontend.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace CSharpBlazorCICDSkeleton.Frontend.Components.Layout;

public partial class NavMenu
{
    [Inject]
    private INavMenuLinksService NavMenuLinksService { get; set; }

    public IEnumerable<NavMenuLinkEntity> NavMenuLinks { get; set; } = [];

    protected override void OnInitialized()
    {
        base.OnInitialized();
        if (NavMenuLinksService is not null)
        {
            NavMenuLinks = NavMenuLinksService.GetNavMenuLinks();
        }
    }
}
