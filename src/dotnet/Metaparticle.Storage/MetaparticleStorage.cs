using System;
using System.Dynamic;
using System.Threading.Tasks;
using Metaparticle.Storage.Exceptions;

namespace Metaparticle.Storage
{
    public class MetaparticleStorage
    {
        private readonly IMetaparticleStorage _storage;
        
        private dynamic _scopedData;

        public MetaparticleStorage(IMetaparticleStorage storage)
        {
            _storage = storage;
        }

        public async Task<object> Scoped(string name, Func<dynamic, object> fn)
        {
            ValidateStorage();
            
            return await LoadExecuteStore(name, fn);
        }

        private void ValidateStorage()
        {
            if (_storage == null)
                throw new StorageNotInitializedException();
        }

        private async Task<object> LoadExecuteStore(string name, Func<dynamic, object> fn)
        {
            _scopedData = await _storage.LoadAsync(name);
            _scopedData.Dirty = false;

            var result = fn(_scopedData);
            if (_scopedData.Dirty)
            {
                var success = await _storage.StoreAsync(name, _scopedData);
                if (!success)
                {
                    await Task.Delay(100);
                    await LoadExecuteStore(name, fn);
                }
            }
            
            return result;
        }
    }
}
