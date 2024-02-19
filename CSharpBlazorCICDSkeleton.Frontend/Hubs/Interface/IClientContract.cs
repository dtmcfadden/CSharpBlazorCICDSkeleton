namespace CSharpBlazorCICDSkeleton.Frontend.Hubs.Interface;

public interface IClientContract
{
    Task SendMessage(string user, string message);
}