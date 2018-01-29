using System;
using System.Dynamic;

namespace Metaparticle.Storage
{
    public class MetaparticleStorage
    {
        private static dynamic scopedData = new ScopedObject(new MetaparticleFileStorage());

        public static dynamic Scoped(string scope = "")
        {
            return scopedData;
        }
    }
}
