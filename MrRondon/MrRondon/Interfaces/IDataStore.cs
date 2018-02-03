using SQLite.Net.Interop;

namespace MrRondon.Interfaces
{
    public interface IDataStore
    {
        string Path { get; }
        ISQLitePlatform Platform { get; }
    }
}