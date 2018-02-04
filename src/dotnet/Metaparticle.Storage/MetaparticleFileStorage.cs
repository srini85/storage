using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using System.Dynamic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Metaparticle.Storage
{
    public class MetaparticleFileStorage : IMetaparticleStorage
    {
        private readonly MetaparticleFileStorageConfig _config;

        public MetaparticleFileStorage(MetaparticleFileStorageConfig config)
        {
            _config = config;
        }

        Dictionary<string, ScopedObject> valueDictionary = new Dictionary<string, ScopedObject>();

        public async Task ConfigureAsync()
        {
            throw new System.NotImplementedException();
        }

        public async Task<PersistedScopedObject> LoadAsync(string name)
        {
            
            FileInfo fileInfo;
            var file = FullPathToFile(name);

            try
            {
                fileInfo = new FileInfo(file);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
            JObject json;

            using (FileStream asyncFileStream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true))
            {
                json = await JObject.LoadAsync(new JsonTextReader(new StreamReader(asyncFileStream)));
            }
            

            return new PersistedScopedObject { ModifiedTime = fileInfo.LastWriteTimeUtc, Data = json.ToObject<ScopedObject>() };
        }

        public async Task ShutdownAsync()
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> StoreAsync(string name, PersistedScopedObject data)
        {
            var file = FullPathToFile(name);
            var lastWriteTime = File.GetLastWriteTimeUtc(file);
            if (lastWriteTime != data.ModifiedTime)
            {
                return false;
            }

            try
            {
                using (StreamWriter writer = File.CreateText(file))
                {
                    await writer.WriteAsync(JsonConvert.SerializeObject(data.Data.GetProperties()));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error writing to file. {e.Message}");
                return false;
            }
            return await Task.FromResult(true);
        }

        private string FullPathToFile(string name)
        {
            return Path.Combine(_config.Directory, $"{name}.json");
        }
    }
}