using System;
using System.IO;
using System.Threading.Tasks;
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
            var directory = System.IO.Directory.GetCurrentDirectory();
            if (File.Exists(Path.Combine(directory, "globaltest.json")))
            {
                File.Delete(Path.Combine(directory, "globaltest.json"));
            }
            var mpStorage = new MetaparticleStorage(new MetaparticleFileStorage(new MetaparticleFileStorageConfig {Directory = directory}));

            // act
            var result = mpStorage.Scoped("globaltest", (scope) => {
                if (scope.Val == null)
                    scope.Val = 0;
                scope.Val++;
                return scope.Val;
            });

            var result2 = mpStorage.Scoped("globaltest", (scope) => {
                if (scope.Val == null)
                    scope.Val = 0;
                scope.Val++;
                return scope.Val;
            });

            var results = await Task.WhenAll(result, result2);

            // assert
            // confirm we have incremented twice
            Assert.Equal(3, (int)(results[0] as dynamic).Val + (int)(results[1] as dynamic).Val);
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
