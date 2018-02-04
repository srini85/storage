namespace Metaparticle.Storage
{
    using System;
    
    public class PersistedScopedObject
    {
        public DateTime ModifiedTime { get;set; }

        public ScopedObject Data {  get;set; }
    }
}