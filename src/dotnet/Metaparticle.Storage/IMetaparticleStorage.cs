using System.Threading.Tasks;

namespace Metaparticle.Storage
{
    public interface IMetaparticleStorage
    {
        Task ConfigureAsync();

        Task ShutdownAsync();

        Task StoreAsync(string name, object data);

        Task<object> LoadAsync(string name);
    }
}
