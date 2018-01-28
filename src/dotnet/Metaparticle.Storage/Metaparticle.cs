using System;
using System.Dynamic;

namespace Metaparticle.Storage
{
    public class Metaparticle
    {
        private static dynamic scopedData = new ExpandoObject();

        public static dynamic Scoped(string scope = "")
        {
            return scopedData;
        }
    }
}
