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
            var mpStorage = new MetaparticleStorage(new MetaparticleFileStorage(new MetaparticleFileStorageConfig {Directory = "c:\\temp"}));
            var val = 1;

            // act
            var result = mpStorage.Scoped("global", (scope) => {
                scope.Val++;
                return scope.Val;
            });

            var result2 = mpStorage.Scoped("global", (scope) => {
                scope.Val++;
                return scope.Val;
            });

            var results = await System.Threading.Tasks.Task.WhenAll(result, result2);

            // assert
            Assert.Equal(val, results[0]);
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
