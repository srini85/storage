using System;
using Xunit;

namespace Metaparticle.Storage.Tests
{
    public class MetaparticleTests
    {
        [Fact]
        public void GivenDataDoesntExist_WhenMetaparticleDataIsRequested_ThrowsDoesNotExist()
        {
            // arrange
            bool testPassed = false;

            // act
            try
            {
                var result = Metaparticle.Scoped().Data;
            }
            catch
            {
                testPassed = true;
            }
            
            // assert
            Assert.True(testPassed, "Didnt throw as expected");
        }

        [Fact]
        public void GivenDataDoesntExist_SetData_ValueIsReturned()
        {
            // arrange
            var setValue = "Hello World";

            // act
            Metaparticle.Scoped().Data = setValue;

            // assert
            Assert.Equal(setValue, Metaparticle.Scoped().Data);
        }
    }
}
