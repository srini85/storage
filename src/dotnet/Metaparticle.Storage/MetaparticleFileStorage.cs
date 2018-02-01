using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Dynamic;

namespace Metaparticle.Storage
{
    public class MetaparticleFileStorage : IMetaparticleStorage
    {
        Dictionary<string, ScopedObject> valueDictionary = new Dictionary<string, ScopedObject>();

        public async Task ConfigureAsync()
        {
            throw new System.NotImplementedException();
        }

        public async Task<dynamic> LoadAsync(string name)
        {
            if (!valueDictionary.Keys.Any(x=>x == name))
                return await Task.FromResult<dynamic>(new ScopedObject());
                
            return await Task.FromResult<dynamic>(valueDictionary[name]);
        }

        public async Task ShutdownAsync()
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> StoreAsync(string name, dynamic data)
        {
            await Task.Delay(500); // file write operation here
            return await Task.FromResult(true);
        }
    }
}