using System;

namespace Metaparticle.Storage.Exceptions
{
    public class StorageNotInitializedException : Exception
    {
        public StorageNotInitializedException()
            : this ("You must initialize the storage first.")
        {
        }
        
        public StorageNotInitializedException(string message) : base(message)
        {
        }
    }
}