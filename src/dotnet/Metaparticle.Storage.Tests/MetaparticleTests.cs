using System;
using Metaparticle.Storage.Exceptions;
using Xunit;

namespace Metaparticle.Storage.Tests
{
    public class MetaparticleTests
    {
        [Fact]
        public async void GivenValidScopeAndFileStore_WhenScopedObjectValIsSet_ReturnValue()
        {
            // arrange
            var mpStorage = new MetaparticleStorage(new MetaparticleFileStorage());
            var val = 123;

            // act
            var result = await mpStorage.Scoped("global", (obj) => {
                obj.Val = val;
                return obj.Val;
            });

            // assert
            Assert.Equal(val, result);
        }

        [Fact]
        public async void GivenInvalidStorageSystem_WhenScopedIsCalled_ThrowsException()
        {
            // arrange
            var mpStorage = new MetaparticleStorage(null);
            
            // act
            // assert
            await Assert.ThrowsAsync<StorageNotInitializedException>(()=> mpStorage.Scoped("global", (obj) => { return null; }));
        }
    }
}
