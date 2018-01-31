using System;
using System.Dynamic;
using System.Threading.Tasks;

namespace Metaparticle.Storage
{
    public class MetaparticleStorage
    {
        private static dynamic staticScopedData = new ScopedObject(new MetaparticleFileStorage());

        private readonly IMetaparticleStorage _storage;
        
        private readonly dynamic _scopedData;

        public MetaparticleStorage(IMetaparticleStorage storage)
        {
            _storage = storage;
            _scopedData = new ScopedObject(_storage);
        }

        public async Task<object> Scoped(string name, Func<dynamic, object> fn)
        {
            var result = fn(_scopedData);
            await Task.Delay(1000);
            return result;
        }
    }
}
