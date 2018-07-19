namespace MrRondon.Services.Interfaces
{
    public interface IAppVersion
    {
        string GetVersion();
        int GetBuild();
    }
}