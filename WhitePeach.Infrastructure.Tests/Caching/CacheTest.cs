using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhitePeach.Infrastructure.Caching;
using Xunit;

namespace WhitePeach.Infrastructure.Tests.Caching
{
    public class CacheTest
    {
        // Using Drive Test

        [Fact]
        public void UseUserDefinedCacheDriveMapping_Simplex()
        {
            var mappedDriveMock = new Mock<ICacheDrive>();
            mappedDriveMock.Setup(_ => _.Contains("one" + "identity")).Returns(true);
            mappedDriveMock.Setup(_ => _.Get<int>("one" + "identity")).Returns(new CacheEntry<int>() { Key = "one", Value = 1234567 });

            var defaultDriveMock = new Mock<ICacheDrive>();
            defaultDriveMock.Setup(_ => _.Contains("one" + "identity")).Returns(true);
            defaultDriveMock.Setup(_ => _.Get<int>("one" + "identity")).Returns(new CacheEntry<int>() { Key = "one", Value = 7654321 });

            var defaultPolicyMock = new Mock<IExpirePolicy>();
            defaultPolicyMock.Setup(_ => _.Validate(It.IsAny<CacheEntry<int>>())).Returns(true);


            var cache = new Cache(new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { typeof(KeyLengthSimplexTranslator).FullName, mappedDriveMock.Object } } });
            var result = cache.Get("one", "identity", new KeyLengthSimplexTranslator());

            Assert.Equal(1234567, result);
            mappedDriveMock.VerifyAll();
            defaultDriveMock.Verify(_ => _.Contains("one" + "identity"), Times.Never());
            defaultDriveMock.Verify(_ => _.Get<int>("one" + "identity"), Times.Never());
        }

        [Fact]
        public void UseUserDefinedCacheDriveMapping_Bulk_Simplex()
        {
            var mappedDriveMock = new Mock<ICacheDrive>();
            mappedDriveMock.Setup(_ => _.Contains("one" + "identity")).Returns(true);
            mappedDriveMock.Setup(_ => _.Get<int>("one" + "identity")).Returns(new CacheEntry<int>() { Key = "one", Value = 1234567 });

            var defaultDriveMock = new Mock<ICacheDrive>();
            defaultDriveMock.Setup(_ => _.Contains("one" + "identity")).Returns(true);
            defaultDriveMock.Setup(_ => _.Get<int>("one" + "identity")).Returns(new CacheEntry<int>() { Key = "one", Value = 7654321 });

            var defaultPolicyMock = new Mock<IExpirePolicy>();
            defaultPolicyMock.Setup(_ => _.Validate(It.IsAny<CacheEntry<int>>())).Returns(true);


            var cache = new Cache(new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { typeof(KeyLengthSimplexTranslator).FullName, mappedDriveMock.Object } } });
            var result = cache.Get(new[] { "one" }, "identity", new KeyLengthSimplexTranslator());

            Assert.Equal(new[] { 1234567 }, result);
            mappedDriveMock.VerifyAll();
            defaultDriveMock.Verify(_ => _.Contains("one" + "identity"), Times.Never());
            defaultDriveMock.Verify(_ => _.Get<int>("one" + "identity"), Times.Never());
        }

        [Fact]
        public void UseUserDefinedCacheDriveMapping_Vectorized()
        {
            var mappedDriveMock = new Mock<ICacheDrive>();
            mappedDriveMock.Setup(_ => _.Contains("one" + "identity")).Returns(true);
            mappedDriveMock.Setup(_ => _.Get<int>("one" + "identity")).Returns(new CacheEntry<int>() { Key = "one", Value = 1234567 });

            var defaultDriveMock = new Mock<ICacheDrive>();
            defaultDriveMock.Setup(_ => _.Contains("one" + "identity")).Returns(true);
            defaultDriveMock.Setup(_ => _.Get<int>("one" + "identity")).Returns(new CacheEntry<int>() { Key = "one", Value = 7654321 });

            var defaultPolicyMock = new Mock<IExpirePolicy>();
            defaultPolicyMock.Setup(_ => _.Validate(It.IsAny<CacheEntry<int>>())).Returns(true);


            var cache = new Cache(new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { typeof(KeyLengthVectorizedTranslator).FullName, mappedDriveMock.Object } } });
            var result = cache.Get("one", "identity", new KeyLengthVectorizedTranslator());

            Assert.Equal(1234567, result);
            mappedDriveMock.VerifyAll();
            defaultDriveMock.Verify(_ => _.Contains("one" + "identity"), Times.Never());
            defaultDriveMock.Verify(_ => _.Get<int>("one" + "identity"), Times.Never());
        }

        [Fact]
        public void UseUserDefinedCacheDriveMapping_Bulk_Vectorized()
        {
            var mappedDriveMock = new Mock<ICacheDrive>();
            mappedDriveMock.Setup(_ => _.Contains("one" + "identity")).Returns(true);
            mappedDriveMock.Setup(_ => _.Get<int>("one" + "identity")).Returns(new CacheEntry<int>() { Key = "one", Value = 1234567 });

            var defaultDriveMock = new Mock<ICacheDrive>();
            defaultDriveMock.Setup(_ => _.Contains("one" + "identity")).Returns(true);
            defaultDriveMock.Setup(_ => _.Get<int>("one" + "identity")).Returns(new CacheEntry<int>() { Key = "one", Value = 7654321 });

            var defaultPolicyMock = new Mock<IExpirePolicy>();
            defaultPolicyMock.Setup(_ => _.Validate(It.IsAny<CacheEntry<int>>())).Returns(true);


            var cache = new Cache(new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { typeof(KeyLengthVectorizedTranslator).FullName, mappedDriveMock.Object } } });
            var result = cache.Get(new[] { "one" }, "identity", new KeyLengthVectorizedTranslator());

            Assert.Equal(new[] { 1234567 }, result);
            mappedDriveMock.VerifyAll();
            defaultDriveMock.Verify(_ => _.Contains("one" + "identity"), Times.Never());
            defaultDriveMock.Verify(_ => _.Get<int>("one" + "identity"), Times.Never());
        }

        [Fact]
        public void UseUserDefinedCacheDriveMapping_Optimized()
        {
            var mappedDriveMock = new Mock<ICacheDrive>();
            mappedDriveMock.Setup(_ => _.Contains("one" + "identity")).Returns(true);
            mappedDriveMock.Setup(_ => _.Get<int>("one" + "identity")).Returns(new CacheEntry<int>() { Key = "one", Value = 1234567 });

            var defaultDriveMock = new Mock<ICacheDrive>();
            defaultDriveMock.Setup(_ => _.Contains("one" + "identity")).Returns(true);
            defaultDriveMock.Setup(_ => _.Get<int>("one" + "identity")).Returns(new CacheEntry<int>() { Key = "one", Value = 7654321 });

            var defaultPolicyMock = new Mock<IExpirePolicy>();
            defaultPolicyMock.Setup(_ => _.Validate(It.IsAny<CacheEntry<int>>())).Returns(true);


            var cache = new Cache(new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { typeof(KeyLengthOptimizedTranslator).FullName, mappedDriveMock.Object } } });
            var result = cache.Get("one", "identity", new KeyLengthOptimizedTranslator());

            Assert.Equal(1234567, result);
            mappedDriveMock.VerifyAll();
            defaultDriveMock.Verify(_ => _.Contains("one" + "identity"), Times.Never());
            defaultDriveMock.Verify(_ => _.Get<int>("one" + "identity"), Times.Never());
        }

        [Fact]
        public void UseUserDefinedCacheDriveMapping_Bulk_Optimized()
        {
            var mappedDriveMock = new Mock<ICacheDrive>();
            mappedDriveMock.Setup(_ => _.Contains("one" + "identity")).Returns(true);
            mappedDriveMock.Setup(_ => _.Get<int>("one" + "identity")).Returns(new CacheEntry<int>() { Key = "one", Value = 1234567 });

            var defaultDriveMock = new Mock<ICacheDrive>();
            defaultDriveMock.Setup(_ => _.Contains("one" + "identity")).Returns(true);
            defaultDriveMock.Setup(_ => _.Get<int>("one" + "identity")).Returns(new CacheEntry<int>() { Key = "one", Value = 7654321 });

            var defaultPolicyMock = new Mock<IExpirePolicy>();
            defaultPolicyMock.Setup(_ => _.Validate(It.IsAny<CacheEntry<int>>())).Returns(true);


            var cache = new Cache(new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { typeof(KeyLengthOptimizedTranslator).FullName, mappedDriveMock.Object } } });
            var result = cache.Get(new[] { "one" }, "identity", new KeyLengthOptimizedTranslator());

            Assert.Equal(new[] { 1234567 }, result);
            mappedDriveMock.VerifyAll();
            defaultDriveMock.Verify(_ => _.Contains("one" + "identity"), Times.Never());
            defaultDriveMock.Verify(_ => _.Get<int>("one" + "identity"), Times.Never());
        }

        [Fact]
        public void UseUserDefinedCacheDriveMapping_IdenticalSimplex()
        {
            var mappedDriveMock = new Mock<ICacheDrive>();
            mappedDriveMock.Setup(_ => _.Contains("one" + "identity")).Returns(true);
            mappedDriveMock.Setup(_ => _.Get<int>("one" + "identity")).Returns(new CacheEntry<int>() { Key = "one", Value = 1234567 });

            var defaultDriveMock = new Mock<ICacheDrive>();
            defaultDriveMock.Setup(_ => _.Contains("one" + "identity")).Returns(true);
            defaultDriveMock.Setup(_ => _.Get<int>("one" + "identity")).Returns(new CacheEntry<int>() { Key = "one", Value = 7654321 });

            var defaultPolicyMock = new Mock<IExpirePolicy>();
            defaultPolicyMock.Setup(_ => _.Validate(It.IsAny<CacheEntry<int>>())).Returns(true);


            var cache = new Cache(new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { typeof(KeyLengthIdenticalSimplexTranslator).FullName, mappedDriveMock.Object } } });
            var result = cache.Get("one", new KeyLengthIdenticalSimplexTranslator("identity"));

            Assert.Equal(1234567, result);
            mappedDriveMock.VerifyAll();
            defaultDriveMock.Verify(_ => _.Contains("one" + "identity"), Times.Never());
            defaultDriveMock.Verify(_ => _.Get<int>("one" + "identity"), Times.Never());
        }

        [Fact]
        public void UseUserDefinedCacheDriveMapping_Bulk_IdenticalSimplex()
        {
            var mappedDriveMock = new Mock<ICacheDrive>();
            mappedDriveMock.Setup(_ => _.Contains("one" + "identity")).Returns(true);
            mappedDriveMock.Setup(_ => _.Get<int>("one" + "identity")).Returns(new CacheEntry<int>() { Key = "one", Value = 1234567 });

            var defaultDriveMock = new Mock<ICacheDrive>();
            defaultDriveMock.Setup(_ => _.Contains("one" + "identity")).Returns(true);
            defaultDriveMock.Setup(_ => _.Get<int>("one" + "identity")).Returns(new CacheEntry<int>() { Key = "one", Value = 7654321 });

            var defaultPolicyMock = new Mock<IExpirePolicy>();
            defaultPolicyMock.Setup(_ => _.Validate(It.IsAny<CacheEntry<int>>())).Returns(true);


            var cache = new Cache(new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { typeof(KeyLengthIdenticalSimplexTranslator).FullName, mappedDriveMock.Object } } });
            var result = cache.Get(new[] { "one" }, new KeyLengthIdenticalSimplexTranslator("identity"));

            Assert.Equal(new[] { 1234567 }, result);
            mappedDriveMock.VerifyAll();
            defaultDriveMock.Verify(_ => _.Contains("one" + "identity"), Times.Never());
            defaultDriveMock.Verify(_ => _.Get<int>("one" + "identity"), Times.Never());
        }

        [Fact]
        public void UseUserDefinedCacheDriveMapping_IdenticalVectorized()
        {
            var mappedDriveMock = new Mock<ICacheDrive>();
            mappedDriveMock.Setup(_ => _.Contains("one" + "identity")).Returns(true);
            mappedDriveMock.Setup(_ => _.Get<int>("one" + "identity")).Returns(new CacheEntry<int>() { Key = "one", Value = 1234567 });

            var defaultDriveMock = new Mock<ICacheDrive>();
            defaultDriveMock.Setup(_ => _.Contains("one" + "identity")).Returns(true);
            defaultDriveMock.Setup(_ => _.Get<int>("one" + "identity")).Returns(new CacheEntry<int>() { Key = "one", Value = 7654321 });

            var defaultPolicyMock = new Mock<IExpirePolicy>();
            defaultPolicyMock.Setup(_ => _.Validate(It.IsAny<CacheEntry<int>>())).Returns(true);


            var cache = new Cache(new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { typeof(KeyLengthIdenticalVectorizedTranslator).FullName, mappedDriveMock.Object } } });
            var result = cache.Get("one", new KeyLengthIdenticalVectorizedTranslator("identity"));

            Assert.Equal(1234567, result);
            mappedDriveMock.VerifyAll();
            defaultDriveMock.Verify(_ => _.Contains("one" + "identity"), Times.Never());
            defaultDriveMock.Verify(_ => _.Get<int>("one" + "identity"), Times.Never());
        }

        [Fact]
        public void UseUserDefinedCacheDriveMapping_Bulk_IdenticalVectorized()
        {
            var mappedDriveMock = new Mock<ICacheDrive>();
            mappedDriveMock.Setup(_ => _.Contains("one" + "identity")).Returns(true);
            mappedDriveMock.Setup(_ => _.Get<int>("one" + "identity")).Returns(new CacheEntry<int>() { Key = "one", Value = 1234567 });

            var defaultDriveMock = new Mock<ICacheDrive>();
            defaultDriveMock.Setup(_ => _.Contains("one" + "identity")).Returns(true);
            defaultDriveMock.Setup(_ => _.Get<int>("one" + "identity")).Returns(new CacheEntry<int>() { Key = "one", Value = 7654321 });

            var defaultPolicyMock = new Mock<IExpirePolicy>();
            defaultPolicyMock.Setup(_ => _.Validate(It.IsAny<CacheEntry<int>>())).Returns(true);


            var cache = new Cache(new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { typeof(KeyLengthIdenticalVectorizedTranslator).FullName, mappedDriveMock.Object } } });
            var result = cache.Get(new[] { "one" }, new KeyLengthIdenticalVectorizedTranslator("identity"));

            Assert.Equal(new[] { 1234567 }, result);
            mappedDriveMock.VerifyAll();
            defaultDriveMock.Verify(_ => _.Contains("one" + "identity"), Times.Never());
            defaultDriveMock.Verify(_ => _.Get<int>("one" + "identity"), Times.Never());
        }

        [Fact]
        public void UseUserDefinedCacheDriveMapping_IdenticalOptimized()
        {
            var mappedDriveMock = new Mock<ICacheDrive>();
            mappedDriveMock.Setup(_ => _.Contains("one" + "identity")).Returns(true);
            mappedDriveMock.Setup(_ => _.Get<int>("one" + "identity")).Returns(new CacheEntry<int>() { Key = "one", Value = 1234567 });

            var defaultDriveMock = new Mock<ICacheDrive>();
            defaultDriveMock.Setup(_ => _.Contains("one" + "identity")).Returns(true);
            defaultDriveMock.Setup(_ => _.Get<int>("one" + "identity")).Returns(new CacheEntry<int>() { Key = "one", Value = 7654321 });

            var defaultPolicyMock = new Mock<IExpirePolicy>();
            defaultPolicyMock.Setup(_ => _.Validate(It.IsAny<CacheEntry<int>>())).Returns(true);


            var cache = new Cache(new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { typeof(KeyLengthIdenticalOptimizedTranslator).FullName, mappedDriveMock.Object } } });
            var result = cache.Get("one", new KeyLengthIdenticalOptimizedTranslator("identity"));

            Assert.Equal(1234567, result);
            mappedDriveMock.VerifyAll();
            defaultDriveMock.Verify(_ => _.Contains("one" + "identity"), Times.Never());
            defaultDriveMock.Verify(_ => _.Get<int>("one" + "identity"), Times.Never());
        }

        [Fact]
        public void UseUserDefinedCacheDriveMapping_Bulk_IdenticalOptimized()
        {
            var mappedDriveMock = new Mock<ICacheDrive>();
            mappedDriveMock.Setup(_ => _.Contains("one" + "identity")).Returns(true);
            mappedDriveMock.Setup(_ => _.Get<int>("one" + "identity")).Returns(new CacheEntry<int>() { Key = "one", Value = 1234567 });

            var defaultDriveMock = new Mock<ICacheDrive>();
            defaultDriveMock.Setup(_ => _.Contains("one" + "identity")).Returns(true);
            defaultDriveMock.Setup(_ => _.Get<int>("one" + "identity")).Returns(new CacheEntry<int>() { Key = "one", Value = 7654321 });

            var defaultPolicyMock = new Mock<IExpirePolicy>();
            defaultPolicyMock.Setup(_ => _.Validate(It.IsAny<CacheEntry<int>>())).Returns(true);


            var cache = new Cache(new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { typeof(KeyLengthIdenticalOptimizedTranslator).FullName, mappedDriveMock.Object } } });
            var result = cache.Get(new[] { "one" }, new KeyLengthIdenticalOptimizedTranslator("identity"));

            Assert.Equal(new[] { 1234567 }, result);
            mappedDriveMock.VerifyAll();
            defaultDriveMock.Verify(_ => _.Contains("one" + "identity"), Times.Never());
            defaultDriveMock.Verify(_ => _.Get<int>("one" + "identity"), Times.Never());
        }

        //

        [Fact]
        public void UseDefaultDriveWhenDriveMappingNotDefined_Simplex()
        {
            var invalidDriveMock = new Mock<ICacheDrive>();
            invalidDriveMock.Setup(_ => _.Contains("one" + "identity")).Returns(true);
            invalidDriveMock.Setup(_ => _.Get<int>("one" + "identity")).Returns(new CacheEntry<int>() { Key = "one", Value = 7654321 });

            var defaultDriveMock = new Mock<ICacheDrive>();
            defaultDriveMock.Setup(_ => _.Contains("one" + "identity")).Returns(true);
            defaultDriveMock.Setup(_ => _.Get<int>("one" + "identity")).Returns(new CacheEntry<int>() { Key = "one", Value = 1234567 });

            var policyMock = new Mock<IExpirePolicy>();
            policyMock.Setup(_ => _.Validate(It.IsAny<CacheEntry<int>>())).Returns(true);


            var cache = new Cache(new CacheCore(defaultDriveMock.Object, policyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { "invalid", invalidDriveMock.Object } } });
            var result = cache.Get("one", "identity", new KeyLengthSimplexTranslator());

            Assert.Equal(1234567, result);
            defaultDriveMock.VerifyAll();
            invalidDriveMock.Verify(_ => _.Contains("one" + "identity"), Times.Never());
            invalidDriveMock.Verify(_ => _.Get<int>("one" + "identity"), Times.Never());
        }

        [Fact]
        public void UseDefaultDriveWhenDriveMappingNotDefined_Vectorized()
        {
            var invalidDriveMock = new Mock<ICacheDrive>();
            invalidDriveMock.Setup(_ => _.Contains("one" + "identity")).Returns(true);
            invalidDriveMock.Setup(_ => _.Get<int>("one" + "identity")).Returns(new CacheEntry<int>() { Key = "one", Value = 7654321 });

            var defaultDriveMock = new Mock<ICacheDrive>();
            defaultDriveMock.Setup(_ => _.Contains("one" + "identity")).Returns(true);
            defaultDriveMock.Setup(_ => _.Get<int>("one" + "identity")).Returns(new CacheEntry<int>() { Key = "one", Value = 1234567 });

            var policyMock = new Mock<IExpirePolicy>();
            policyMock.Setup(_ => _.Validate(It.IsAny<CacheEntry<int>>())).Returns(true);


            var cache = new Cache(new CacheCore(defaultDriveMock.Object, policyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { "invalid", invalidDriveMock.Object } } });
            var result = cache.Get("one", "identity", new KeyLengthVectorizedTranslator());

            Assert.Equal(1234567, result);
            defaultDriveMock.VerifyAll();
            invalidDriveMock.Verify(_ => _.Contains("one" + "identity"), Times.Never());
            invalidDriveMock.Verify(_ => _.Get<int>("one" + "identity"), Times.Never());
        }

        [Fact]
        public void UseDefaultDriveWhenDriveMappingNotDefined_Optimized()
        {
            var invalidDriveMock = new Mock<ICacheDrive>();
            invalidDriveMock.Setup(_ => _.Contains("one" + "identity")).Returns(true);
            invalidDriveMock.Setup(_ => _.Get<int>("one" + "identity")).Returns(new CacheEntry<int>() { Key = "one", Value = 7654321 });

            var defaultDriveMock = new Mock<ICacheDrive>();
            defaultDriveMock.Setup(_ => _.Contains("one" + "identity")).Returns(true);
            defaultDriveMock.Setup(_ => _.Get<int>("one" + "identity")).Returns(new CacheEntry<int>() { Key = "one", Value = 1234567 });

            var policyMock = new Mock<IExpirePolicy>();
            policyMock.Setup(_ => _.Validate(It.IsAny<CacheEntry<int>>())).Returns(true);


            var cache = new Cache(new CacheCore(defaultDriveMock.Object, policyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { "invalid", invalidDriveMock.Object } } });
            var result = cache.Get("one", "identity", new KeyLengthOptimizedTranslator());

            Assert.Equal(1234567, result);
            defaultDriveMock.VerifyAll();
            invalidDriveMock.Verify(_ => _.Contains("one" + "identity"), Times.Never());
            invalidDriveMock.Verify(_ => _.Get<int>("one" + "identity"), Times.Never());
        }

        [Fact]
        public void UseDefaultDriveWhenDriveMappingNotDefined_IdenticalSimplex()
        {
            var invalidDriveMock = new Mock<ICacheDrive>();
            invalidDriveMock.Setup(_ => _.Contains("one" + "identity")).Returns(true);
            invalidDriveMock.Setup(_ => _.Get<int>("one" + "identity")).Returns(new CacheEntry<int>() { Key = "one", Value = 7654321 });

            var defaultDriveMock = new Mock<ICacheDrive>();
            defaultDriveMock.Setup(_ => _.Contains("one" + "identity")).Returns(true);
            defaultDriveMock.Setup(_ => _.Get<int>("one" + "identity")).Returns(new CacheEntry<int>() { Key = "one", Value = 1234567 });

            var policyMock = new Mock<IExpirePolicy>();
            policyMock.Setup(_ => _.Validate(It.IsAny<CacheEntry<int>>())).Returns(true);


            var cache = new Cache(new CacheCore(defaultDriveMock.Object, policyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { "invalid", invalidDriveMock.Object } } });
            var result = cache.Get("one", new KeyLengthIdenticalSimplexTranslator("identity"));

            Assert.Equal(1234567, result);
            defaultDriveMock.VerifyAll();
            invalidDriveMock.Verify(_ => _.Contains("one" + "identity"), Times.Never());
            invalidDriveMock.Verify(_ => _.Get<int>("one" + "identity"), Times.Never());
        }

        [Fact]
        public void UseDefaultDriveWhenDriveMappingNotDefined_IdenticalVectorized()
        {
            var invalidDriveMock = new Mock<ICacheDrive>();
            invalidDriveMock.Setup(_ => _.Contains("one" + "identity")).Returns(true);
            invalidDriveMock.Setup(_ => _.Get<int>("one" + "identity")).Returns(new CacheEntry<int>() { Key = "one", Value = 7654321 });

            var defaultDriveMock = new Mock<ICacheDrive>();
            defaultDriveMock.Setup(_ => _.Contains("one" + "identity")).Returns(true);
            defaultDriveMock.Setup(_ => _.Get<int>("one" + "identity")).Returns(new CacheEntry<int>() { Key = "one", Value = 1234567 });

            var policyMock = new Mock<IExpirePolicy>();
            policyMock.Setup(_ => _.Validate(It.IsAny<CacheEntry<int>>())).Returns(true);


            var cache = new Cache(new CacheCore(defaultDriveMock.Object, policyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { "invalid", invalidDriveMock.Object } } });
            var result = cache.Get("one", new KeyLengthIdenticalVectorizedTranslator("identity"));

            Assert.Equal(1234567, result);
            defaultDriveMock.VerifyAll();
            invalidDriveMock.Verify(_ => _.Contains("one" + "identity"), Times.Never());
            invalidDriveMock.Verify(_ => _.Get<int>("one" + "identity"), Times.Never());
        }

        [Fact]
        public void UseDefaultDriveWhenDriveMappingNotDefined_IdenticalOptimized()
        {
            var invalidDriveMock = new Mock<ICacheDrive>();
            invalidDriveMock.Setup(_ => _.Contains("one" + "identity")).Returns(true);
            invalidDriveMock.Setup(_ => _.Get<int>("one" + "identity")).Returns(new CacheEntry<int>() { Key = "one", Value = 7654321 });

            var defaultDriveMock = new Mock<ICacheDrive>();
            defaultDriveMock.Setup(_ => _.Contains("one" + "identity")).Returns(true);
            defaultDriveMock.Setup(_ => _.Get<int>("one" + "identity")).Returns(new CacheEntry<int>() { Key = "one", Value = 1234567 });

            var policyMock = new Mock<IExpirePolicy>();
            policyMock.Setup(_ => _.Validate(It.IsAny<CacheEntry<int>>())).Returns(true);


            var cache = new Cache(new CacheCore(defaultDriveMock.Object, policyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { "invalid", invalidDriveMock.Object } } });
            var result = cache.Get("one", new KeyLengthIdenticalOptimizedTranslator("identity"));

            Assert.Equal(1234567, result);
            defaultDriveMock.VerifyAll();
            invalidDriveMock.Verify(_ => _.Contains("one" + "identity"), Times.Never());
            invalidDriveMock.Verify(_ => _.Get<int>("one" + "identity"), Times.Never());
        }

        //

        [Fact]
        public void UserDefaultDrive_FuncSingle()
        {
            var invalidDriveMock = new Mock<ICacheDrive>();
            invalidDriveMock.Setup(_ => _.Contains("one" + "identity")).Returns(true);
            invalidDriveMock.Setup(_ => _.Get<int>("one" + "identity")).Returns(new CacheEntry<int>() { Key = "one", Value = 7654321 });

            var defaultDriveMock = new Mock<ICacheDrive>();
            defaultDriveMock.Setup(_ => _.Contains("one" + "identity")).Returns(true);
            defaultDriveMock.Setup(_ => _.Get<int>("one" + "identity")).Returns(new CacheEntry<int>() { Key = "one", Value = 1234567 });

            var policyMock = new Mock<IExpirePolicy>();
            policyMock.Setup(_ => _.Validate(It.IsAny<CacheEntry<int>>())).Returns(true);


            var cache = new Cache(new CacheCore(defaultDriveMock.Object, policyMock.Object)
            {
                CacheDriveMapping = new Dictionary<string, ICacheDrive> { { typeof(KeyLengthSimplexTranslator).FullName, invalidDriveMock.Object } }
            });
            var result = cache.Get("one", "identity", new KeyLengthSimplexTranslator().Get);
            Assert.Equal(1234567, result);
            defaultDriveMock.VerifyAll();
            invalidDriveMock.Verify(_ => _.Contains("one" + "identity"), Times.Never());
            invalidDriveMock.Verify(_ => _.Get<int>("one" + "identity"), Times.Never());
        }

        [Fact]
        public void UserDefaultDrive_Bulk_FuncSingle()
        {
            var invalidDriveMock = new Mock<ICacheDrive>();
            invalidDriveMock.Setup(_ => _.Contains("one" + "identity")).Returns(true);
            invalidDriveMock.Setup(_ => _.Get<int>("one" + "identity")).Returns(new CacheEntry<int>() { Key = "one", Value = 7654321 });

            var defaultDriveMock = new Mock<ICacheDrive>();
            defaultDriveMock.Setup(_ => _.Contains("one" + "identity")).Returns(true);
            defaultDriveMock.Setup(_ => _.Get<int>("one" + "identity")).Returns(new CacheEntry<int>() { Key = "one", Value = 1234567 });

            var policyMock = new Mock<IExpirePolicy>();
            policyMock.Setup(_ => _.Validate(It.IsAny<CacheEntry<int>>())).Returns(true);


            var cache = new Cache(new CacheCore(defaultDriveMock.Object, policyMock.Object)
            {
                CacheDriveMapping = new Dictionary<string, ICacheDrive> { { typeof(KeyLengthSimplexTranslator).FullName, invalidDriveMock.Object } }
            });
            var result = cache.Get(new[] { "one" }, "identity", new KeyLengthSimplexTranslator().Get);
            Assert.Equal(new[] { 1234567 }, result);
            defaultDriveMock.VerifyAll();
            invalidDriveMock.Verify(_ => _.Contains("one" + "identity"), Times.Never());
            invalidDriveMock.Verify(_ => _.Get<int>("one" + "identity"), Times.Never());
        }

        [Fact]
        public void UserDefaultDrive_FuncVectorized()
        {
            var invalidDriveMock = new Mock<ICacheDrive>();
            invalidDriveMock.Setup(_ => _.Contains("one" + "identity")).Returns(true);
            invalidDriveMock.Setup(_ => _.Get<int>("one" + "identity")).Returns(new CacheEntry<int>() { Key = "one", Value = 7654321 });

            var defaultDriveMock = new Mock<ICacheDrive>();
            defaultDriveMock.Setup(_ => _.Contains("one" + "identity")).Returns(true);
            defaultDriveMock.Setup(_ => _.Get<int>("one" + "identity")).Returns(new CacheEntry<int>() { Key = "one", Value = 1234567 });

            var policyMock = new Mock<IExpirePolicy>();
            policyMock.Setup(_ => _.Validate(It.IsAny<CacheEntry<int>>())).Returns(true);


            var cache = new Cache(new CacheCore(defaultDriveMock.Object, policyMock.Object)
            {
                CacheDriveMapping = new Dictionary<string, ICacheDrive> { { typeof(KeyLengthVectorizedTranslator).FullName, invalidDriveMock.Object } }
            });
            var result = cache.Get("one", "identity", new KeyLengthVectorizedTranslator().Get);
            Assert.Equal(1234567, result);
            defaultDriveMock.VerifyAll();
            invalidDriveMock.Verify(_ => _.Contains("one" + "identity"), Times.Never());
            invalidDriveMock.Verify(_ => _.Get<int>("one" + "identity"), Times.Never());
        }

        [Fact]
        public void UserDefaultDrive_Bulk_FuncVectorized()
        {
            var invalidDriveMock = new Mock<ICacheDrive>();
            invalidDriveMock.Setup(_ => _.Contains("one" + "identity")).Returns(true);
            invalidDriveMock.Setup(_ => _.Get<int>("one" + "identity")).Returns(new CacheEntry<int>() { Key = "one", Value = 7654321 });

            var defaultDriveMock = new Mock<ICacheDrive>();
            defaultDriveMock.Setup(_ => _.Contains("one" + "identity")).Returns(true);
            defaultDriveMock.Setup(_ => _.Get<int>("one" + "identity")).Returns(new CacheEntry<int>() { Key = "one", Value = 1234567 });

            var policyMock = new Mock<IExpirePolicy>();
            policyMock.Setup(_ => _.Validate(It.IsAny<CacheEntry<int>>())).Returns(true);


            var cache = new Cache(new CacheCore(defaultDriveMock.Object, policyMock.Object)
            {
                CacheDriveMapping = new Dictionary<string, ICacheDrive> { { typeof(KeyLengthVectorizedTranslator).FullName, invalidDriveMock.Object } }
            });
            var result = cache.Get(new[] { "one" }, "identity", new KeyLengthVectorizedTranslator().Get);
            Assert.Equal(new[] { 1234567 }, result);
            defaultDriveMock.VerifyAll();
            invalidDriveMock.Verify(_ => _.Contains("one" + "identity"), Times.Never());
            invalidDriveMock.Verify(_ => _.Get<int>("one" + "identity"), Times.Never());
        }

        // Fetch Test

        [Fact]
        public void Fetch_Simplex()
        {
            var cache = new Cache();

            var result = cache.Get("key", "identity", new KeyLengthSimplexTranslator());

            Assert.Equal(3, result);
        }

        [Fact]
        public void Fetch_Vectorized()
        {
            var cache = new Cache();

            var result = cache.Get("key", "identity", new KeyLengthVectorizedTranslator());

            Assert.Equal(3, result);
        }

        [Fact]
        public void Fetch_Optimized()
        {
            var cache = new Cache();

            var result = cache.Get("key", "identity", new KeyLengthOptimizedTranslator());

            Assert.Equal(3, result);
        }

        [Fact]
        public void Fetch_FuncSingle()
        {
            var cache = new Cache();

            var result = cache.Get("key", "identity", new KeyLengthSimplexTranslator().Get);

            Assert.Equal(3, result);
        }

        [Fact]
        public void Fetch_FuncVectorized()
        {
            var cache = new Cache();

            var result = cache.Get("key", "identity", new KeyLengthVectorizedTranslator().Get);

            Assert.Equal(3, result);
        }

        [Fact]
        public void Fetch_IdenticalSimplex()
        {
            var cache = new Cache();

            var result = cache.Get("key", new KeyLengthIdenticalSimplexTranslator("identity"));

            Assert.Equal(3, result);
        }

        [Fact]
        public void Fetch_IdenticalSVectorized()
        {
            var cache = new Cache();

            var result = cache.Get("key", new KeyLengthIdenticalVectorizedTranslator("identity"));

            Assert.Equal(3, result);
        }

        [Fact]
        public void Fetch_IdenticalSOptimized()
        {
            var cache = new Cache();

            var result = cache.Get("key", new KeyLengthIdenticalOptimizedTranslator("identity"));

            Assert.Equal(3, result);
        }

        //

        [Fact]
        public void Fetch_Bulk_Simplex()
        {
            var cache = new Cache();

            var result = cache.Get(new[] { "k", "ke", "key" }, "identity", new KeyLengthSimplexTranslator());

            Assert.Equal(new[] { 1, 2, 3 }, result);
        }

        [Fact]
        public void Fetch_Bulk_Vectorized()
        {
            var cache = new Cache();

            var result = cache.Get(new[] { "k", "ke", "key" }, "identity", new KeyLengthVectorizedTranslator());

            Assert.Equal(new[] { 1, 2, 3 }, result);
        }

        [Fact]
        public void Fetch_Bulk_Optimized()
        {
            var cache = new Cache();

            var result = cache.Get(new[] { "k", "ke", "key" }, "identity", new KeyLengthOptimizedTranslator());

            Assert.Equal(new[] { 1, 2, 3 }, result);
        }

        [Fact]
        public void Fetch_Bulk_FuncSingle()
        {
            var cache = new Cache();

            var result = cache.Get(new[] { "k", "ke", "key" }, "identity", new KeyLengthSimplexTranslator().Get);

            Assert.Equal(new[] { 1, 2, 3 }, result);
        }

        [Fact]
        public void Fetch_Bulk_FuncVectorized()
        {
            var cache = new Cache();

            var result = cache.Get(new[] { "k", "ke", "key" }, "identity", new KeyLengthVectorizedTranslator().Get);

            Assert.Equal(new[] { 1, 2, 3 }, result);
        }

        [Fact]
        public void Fetch_Bulk_IdenticalSimplex()
        {
            var cache = new Cache();

            var result = cache.Get(new[] { "k", "ke", "key" }, new KeyLengthIdenticalSimplexTranslator("identity"));

            Assert.Equal(new[] { 1, 2, 3 }, result);
        }

        [Fact]
        public void Fetch_Bulk_IdenticalVectorized()
        {
            var cache = new Cache();

            var result = cache.Get(new[] { "k", "ke", "key" }, new KeyLengthIdenticalVectorizedTranslator("identity"));

            Assert.Equal(new[] { 1, 2, 3 }, result);
        }

        [Fact]
        public void Fetch_Bulk_IdenticalOptimized()
        {
            var cache = new Cache();

            var result = cache.Get(new[] { "k", "ke", "key" }, new KeyLengthIdenticalOptimizedTranslator("identity"));

            Assert.Equal(new[] { 1, 2, 3 }, result);
        }

        // 

        [Fact]
        public void GlobalCacheIsNotNull()
        {
            Assert.NotNull(Cache.GlobalCache);
        }

        // Error Test

        [Fact]
        public void CacheCoreNullCheck()
        {
            Assert.Throws<ArgumentNullException>(() => new Cache(null));
        }
    }
}
