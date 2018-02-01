using System.Threading.Tasks;

namespace Metaparticle.Storage
{
    public interface IMetaparticleStorage
    {
        Task ConfigureAsync();

        Task ShutdownAsync();

        Task<bool> StoreAsync(string name, dynamic data);

        Task<dynamic> LoadAsync(string name);
    }
}
