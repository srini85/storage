using System;
using System.Dynamic;
using System.Threading.Tasks;
using Metaparticle.Storage.Exceptions;

namespace Metaparticle.Storage
{
    public class MetaparticleStorage
    {
        private readonly IMetaparticleStorage _storage;

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
            dynamic resultantValue = null;
            var persistedData = await LoadScopeFromStorageAsync(name);
            persistedData.Data.Dirty = false;

            var result = fn(persistedData.Data);
            if (persistedData.Data.Dirty)
            {
                var success = await _storage.StoreAsync(name, persistedData);
                if (!success)
                {
                    await Task.Delay(100);
                    resultantValue = await LoadExecuteStore(name, fn);
                }
                if (resultantValue == null)
                    resultantValue = persistedData.Data;
            }
            
            return resultantValue;
        }

        private async Task<PersistedScopedObject> LoadScopeFromStorageAsync(string name)
        {
            PersistedScopedObject persistedScopedObject;
            while (true)
            {
                try 
                {
                    persistedScopedObject = await _storage.LoadAsync(name);
                    break;
                }
                catch
                {
                    Console.WriteLine("Error loading file. Will be retrying.");
                    await Task.Delay(100);
                }
            }

            return persistedScopedObject;
        }
    }
}
