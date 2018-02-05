using System;
using System.Dynamic;
using System.Collections.Generic;

namespace Metaparticle.Storage
{
    public class ScopedObject : DynamicObject
    {
        public bool Dirty = false;
        private Dictionary<string, object> data = new Dictionary<string, object>();

        public Dictionary<string, object> GetProperties()
        {
            return data;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            data.TryGetValue(binder.Name, out result);
            return true;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            if (!data.ContainsKey(binder.Name))
                data.Add(binder.Name, value);
            else
                data[binder.Name] = value;

            Dirty = true;
            return true;
        }
    }
}
