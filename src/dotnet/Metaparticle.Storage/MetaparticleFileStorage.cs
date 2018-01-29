using System.Collections.Generic;
using System.Threading.Tasks;

namespace Metaparticle.Storage
{
    public class MetaparticleFileStorage : IMetaparticleStorage
    {
        Dictionary<string, object> valueDictionary = new Dictionary<string, object>();

        public async Task ConfigureAsync()
        {
            throw new System.NotImplementedException();
        }

        public async Task<object> LoadAsync(string name)
        {
            return await Task.FromResult<object>(valueDictionary[name]);
        }

        public async Task ShutdownAsync()
        {
            throw new System.NotImplementedException();
        }

        public async Task StoreAsync(string name, object data)
        {
            valueDictionary.Add(name, data);
            await Task.CompletedTask;
        }
    }
}