using CSharpBlazorCICDSkeleton.Frontend.Entities;

namespace CSharpBlazorCICDSkeleton.Frontend.Services.Interfaces;

public interface INavMenuLinksService
{
    public abstract List<NavMenuLinkEntity> GetNavMenuLinks();
}
