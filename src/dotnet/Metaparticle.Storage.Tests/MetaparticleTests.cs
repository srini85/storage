using System;
using Xunit;
using FluentAssertions;

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
    }
}
