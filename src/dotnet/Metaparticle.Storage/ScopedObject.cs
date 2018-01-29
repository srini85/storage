using System;
using System.Dynamic;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Metaparticle.Storage
{
    public class ScopedObject : DynamicObject
    {
        private readonly IMetaparticleStorage _storage;
        public ScopedObject(IMetaparticleStorage storage)
        {
            _storage = storage;
        }
        
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = TryGetMemberAsync(binder.Name).GetAwaiter().GetResult();
            return true;
        }

        private async Task<object> TryGetMemberAsync(string name)
        {
            return await _storage.LoadAsync(name);
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            TrySetMemberAsync(binder.Name, value).GetAwaiter().GetResult();
            return true;
        }

        private async Task TrySetMemberAsync(string name, object value)
        {
            await _storage.StoreAsync(name, value);
        }

    }
}
