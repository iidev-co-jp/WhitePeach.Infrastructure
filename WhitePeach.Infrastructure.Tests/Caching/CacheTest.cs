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

        private Mock<ICacheDrive> UseUserDefinedCacheDriveMapping_MappedDriveMock()
        {
            var mappedDriveMock = new Mock<ICacheDrive>();
            mappedDriveMock.Setup(_ => _.Contains("one" + "identity")).Returns(true);
            mappedDriveMock.Setup(_ => _.Get<int>("one" + "identity")).Returns(new CacheEntry<int>() { Key = "one", Value = 1234567 });
            return mappedDriveMock;
        }

        private Mock<ICacheDrive> UseUserDefinedCacheDriveMapping_DefaultDriveMock()
        {
            var defaultDriveMock = new Mock<ICacheDrive>();
            defaultDriveMock.Setup(_ => _.Contains("one" + "identity")).Returns(true);
            defaultDriveMock.Setup(_ => _.Get<int>("one" + "identity")).Returns(new CacheEntry<int>() { Key = "one", Value = 7654321 });
            return defaultDriveMock;
        }

        private Mock<IExpirePolicy> UseUserDefinedCacheDriveMapping_DefaultPolicyMock()
        {
            var defaultPolicyMock = new Mock<IExpirePolicy>();
            defaultPolicyMock.Setup(_ => _.Validate(It.IsAny<CacheEntry<int>>())).Returns(true);
            return defaultPolicyMock;
        }

        private void UseUserDefinedCacheDriveMapping_Assert(int result, Mock<ICacheDrive> defaultDriveMock, Mock<ICacheDrive> mappedDriveMock)
        {
            Assert.Equal(1234567, result);
            mappedDriveMock.VerifyAll();
            defaultDriveMock.Verify(_ => _.Contains("one" + "identity"), Times.Never());
            defaultDriveMock.Verify(_ => _.Get<int>("one" + "identity"), Times.Never());
        }

        private void UseUserDefinedCacheDriveMapping_Assert(int[] result, Mock<ICacheDrive> defaultDriveMock, Mock<ICacheDrive> mappedDriveMock)
        {
            Assert.Equal(new[] { 1234567 }, result);
            mappedDriveMock.VerifyAll();
            defaultDriveMock.Verify(_ => _.Contains("one" + "identity"), Times.Never());
            defaultDriveMock.Verify(_ => _.Get<int>("one" + "identity"), Times.Never());
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheCore.CacheDriveMapping" />に定義されている場合はそれを使用する
        /// </summary>
        [Fact]
        public void UseUserDefinedCacheDriveMapping_Simplex()
        {
            var mappedDriveMock = UseUserDefinedCacheDriveMapping_MappedDriveMock();
            var defaultDriveMock = UseUserDefinedCacheDriveMapping_DefaultDriveMock();
            var defaultPolicyMock = UseUserDefinedCacheDriveMapping_DefaultPolicyMock();


            var cache = new Cache(new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { typeof(KeyLengthSimplexTranslator).FullName, mappedDriveMock.Object } } });
            var result = cache.Get("one", "identity", new KeyLengthSimplexTranslator());

            UseUserDefinedCacheDriveMapping_Assert(result, defaultDriveMock, mappedDriveMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheCore.CacheDriveMapping" />に定義されている場合はそれを使用する
        /// </summary>
        [Fact]
        public void UseUserDefinedCacheDriveMapping_Bulk_Simplex()
        {
            var mappedDriveMock = UseUserDefinedCacheDriveMapping_MappedDriveMock();
            var defaultDriveMock = UseUserDefinedCacheDriveMapping_DefaultDriveMock();
            var defaultPolicyMock = UseUserDefinedCacheDriveMapping_DefaultPolicyMock();


            var cache = new Cache(new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { typeof(KeyLengthSimplexTranslator).FullName, mappedDriveMock.Object } } });
            var result = cache.Get(new[] { "one" }, "identity", new KeyLengthSimplexTranslator());

            UseUserDefinedCacheDriveMapping_Assert(result, defaultDriveMock, mappedDriveMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheCore.CacheDriveMapping" />に定義されている場合はそれを使用する
        /// </summary>
        [Fact]
        public void UseUserDefinedCacheDriveMapping_Vectorized()
        {
            var mappedDriveMock = UseUserDefinedCacheDriveMapping_MappedDriveMock();
            var defaultDriveMock = UseUserDefinedCacheDriveMapping_DefaultDriveMock();
            var defaultPolicyMock = UseUserDefinedCacheDriveMapping_DefaultPolicyMock();


            var cache = new Cache(new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { typeof(KeyLengthVectorizedTranslator).FullName, mappedDriveMock.Object } } });
            var result = cache.Get("one", "identity", new KeyLengthVectorizedTranslator());

            UseUserDefinedCacheDriveMapping_Assert(result, defaultDriveMock, mappedDriveMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheCore.CacheDriveMapping" />に定義されている場合はそれを使用する
        /// </summary>
        [Fact]
        public void UseUserDefinedCacheDriveMapping_Bulk_Vectorized()
        {
            var mappedDriveMock = UseUserDefinedCacheDriveMapping_MappedDriveMock();
            var defaultDriveMock = UseUserDefinedCacheDriveMapping_DefaultDriveMock();
            var defaultPolicyMock = UseUserDefinedCacheDriveMapping_DefaultPolicyMock();


            var cache = new Cache(new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { typeof(KeyLengthVectorizedTranslator).FullName, mappedDriveMock.Object } } });
            var result = cache.Get(new[] { "one" }, "identity", new KeyLengthVectorizedTranslator());

            UseUserDefinedCacheDriveMapping_Assert(result, defaultDriveMock, mappedDriveMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheCore.CacheDriveMapping" />に定義されている場合はそれを使用する
        /// </summary>
        [Fact]
        public void UseUserDefinedCacheDriveMapping_Optimized()
        {
            var mappedDriveMock = UseUserDefinedCacheDriveMapping_MappedDriveMock();
            var defaultDriveMock = UseUserDefinedCacheDriveMapping_DefaultDriveMock();
            var defaultPolicyMock = UseUserDefinedCacheDriveMapping_DefaultPolicyMock();


            var cache = new Cache(new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { typeof(KeyLengthOptimizedTranslator).FullName, mappedDriveMock.Object } } });
            var result = cache.Get("one", "identity", new KeyLengthOptimizedTranslator());

            UseUserDefinedCacheDriveMapping_Assert(result, defaultDriveMock, mappedDriveMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheCore.CacheDriveMapping" />に定義されている場合はそれを使用する
        /// </summary>
        [Fact]
        public void UseUserDefinedCacheDriveMapping_Bulk_Optimized()
        {
            var mappedDriveMock = UseUserDefinedCacheDriveMapping_MappedDriveMock();
            var defaultDriveMock = UseUserDefinedCacheDriveMapping_DefaultDriveMock();
            var defaultPolicyMock = UseUserDefinedCacheDriveMapping_DefaultPolicyMock();


            var cache = new Cache(new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { typeof(KeyLengthOptimizedTranslator).FullName, mappedDriveMock.Object } } });
            var result = cache.Get(new[] { "one" }, "identity", new KeyLengthOptimizedTranslator());

            UseUserDefinedCacheDriveMapping_Assert(result, defaultDriveMock, mappedDriveMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheCore.CacheDriveMapping" />に定義されている場合はそれを使用する
        /// </summary>
        [Fact]
        public void UseUserDefinedCacheDriveMapping_IdenticalSimplex()
        {
            var mappedDriveMock = UseUserDefinedCacheDriveMapping_MappedDriveMock();
            var defaultDriveMock = UseUserDefinedCacheDriveMapping_DefaultDriveMock();
            var defaultPolicyMock = UseUserDefinedCacheDriveMapping_DefaultPolicyMock();


            var cache = new Cache(new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { typeof(KeyLengthIdenticalSimplexTranslator).FullName, mappedDriveMock.Object } } });
            var result = cache.Get("one", new KeyLengthIdenticalSimplexTranslator("identity"));

            UseUserDefinedCacheDriveMapping_Assert(result, defaultDriveMock, mappedDriveMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheCore.CacheDriveMapping" />に定義されている場合はそれを使用する
        /// </summary>
        [Fact]
        public void UseUserDefinedCacheDriveMapping_Bulk_IdenticalSimplex()
        {
            var mappedDriveMock = UseUserDefinedCacheDriveMapping_MappedDriveMock();
            var defaultDriveMock = UseUserDefinedCacheDriveMapping_DefaultDriveMock();
            var defaultPolicyMock = UseUserDefinedCacheDriveMapping_DefaultPolicyMock();


            var cache = new Cache(new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { typeof(KeyLengthIdenticalSimplexTranslator).FullName, mappedDriveMock.Object } } });
            var result = cache.Get(new[] { "one" }, new KeyLengthIdenticalSimplexTranslator("identity"));

            UseUserDefinedCacheDriveMapping_Assert(result, defaultDriveMock, mappedDriveMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheCore.CacheDriveMapping" />に定義されている場合はそれを使用する
        /// </summary>
        [Fact]
        public void UseUserDefinedCacheDriveMapping_IdenticalVectorized()
        {
            var mappedDriveMock = UseUserDefinedCacheDriveMapping_MappedDriveMock();
            var defaultDriveMock = UseUserDefinedCacheDriveMapping_DefaultDriveMock();
            var defaultPolicyMock = UseUserDefinedCacheDriveMapping_DefaultPolicyMock();


            var cache = new Cache(new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { typeof(KeyLengthIdenticalVectorizedTranslator).FullName, mappedDriveMock.Object } } });
            var result = cache.Get("one", new KeyLengthIdenticalVectorizedTranslator("identity"));

            UseUserDefinedCacheDriveMapping_Assert(result, defaultDriveMock, mappedDriveMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheCore.CacheDriveMapping" />に定義されている場合はそれを使用する
        /// </summary>
        [Fact]
        public void UseUserDefinedCacheDriveMapping_Bulk_IdenticalVectorized()
        {
            var mappedDriveMock = UseUserDefinedCacheDriveMapping_MappedDriveMock();
            var defaultDriveMock = UseUserDefinedCacheDriveMapping_DefaultDriveMock();
            var defaultPolicyMock = UseUserDefinedCacheDriveMapping_DefaultPolicyMock();


            var cache = new Cache(new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { typeof(KeyLengthIdenticalVectorizedTranslator).FullName, mappedDriveMock.Object } } });
            var result = cache.Get(new[] { "one" }, new KeyLengthIdenticalVectorizedTranslator("identity"));

            UseUserDefinedCacheDriveMapping_Assert(result, defaultDriveMock, mappedDriveMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheCore.CacheDriveMapping" />に定義されている場合はそれを使用する
        /// </summary>
        [Fact]
        public void UseUserDefinedCacheDriveMapping_IdenticalOptimized()
        {
            var mappedDriveMock = UseUserDefinedCacheDriveMapping_MappedDriveMock();
            var defaultDriveMock = UseUserDefinedCacheDriveMapping_DefaultDriveMock();
            var defaultPolicyMock = UseUserDefinedCacheDriveMapping_DefaultPolicyMock();


            var cache = new Cache(new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { typeof(KeyLengthIdenticalOptimizedTranslator).FullName, mappedDriveMock.Object } } });
            var result = cache.Get("one", new KeyLengthIdenticalOptimizedTranslator("identity"));

            UseUserDefinedCacheDriveMapping_Assert(result, defaultDriveMock, mappedDriveMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheCore.CacheDriveMapping" />に定義されている場合はそれを使用する
        /// </summary>
        [Fact]
        public void UseUserDefinedCacheDriveMapping_Bulk_IdenticalOptimized()
        {
            var mappedDriveMock = UseUserDefinedCacheDriveMapping_MappedDriveMock();
            var defaultDriveMock = UseUserDefinedCacheDriveMapping_DefaultDriveMock();
            var defaultPolicyMock = UseUserDefinedCacheDriveMapping_DefaultPolicyMock();


            var cache = new Cache(new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { typeof(KeyLengthIdenticalOptimizedTranslator).FullName, mappedDriveMock.Object } } });
            var result = cache.Get(new[] { "one" }, new KeyLengthIdenticalOptimizedTranslator("identity"));

            UseUserDefinedCacheDriveMapping_Assert(result, defaultDriveMock, mappedDriveMock);
        }

        //

        private Mock<ICacheDrive> UseDefaultDriveWhenDriveMappingNotDefined_DefaultDriveMock()
        {
            var defaultDriveMock = new Mock<ICacheDrive>();
            defaultDriveMock.Setup(_ => _.Contains("one" + "identity")).Returns(true);
            defaultDriveMock.Setup(_ => _.Get<int>("one" + "identity")).Returns(new CacheEntry<int>() { Key = "one", Value = 1234567 });
            return defaultDriveMock;
        }

        private Mock<ICacheDrive> UseDefaultDriveWhenDriveMappingNotDefined_MappedDriveMock()
        {
            var mappedDriveMock = new Mock<ICacheDrive>();
            mappedDriveMock.Setup(_ => _.Contains("one" + "identity")).Returns(true);
            mappedDriveMock.Setup(_ => _.Get<int>("one" + "identity")).Returns(new CacheEntry<int>() { Key = "one", Value = 7654321 });
            return mappedDriveMock;
        }

        private Mock<IExpirePolicy> UseDefaultDriveWhenDriveMappingNotDefined_DefaultPolicyMock()
        {
            var policyMock = new Mock<IExpirePolicy>();
            policyMock.Setup(_ => _.Validate(It.IsAny<CacheEntry<int>>())).Returns(true);
            return policyMock;
        }

        private void UseDefaultDriveWhenDriveMappingNotDefined_Assert(int result, Mock<ICacheDrive> defaultDriveMock, Mock<ICacheDrive> mappedDriveMock)
        {
            Assert.Equal(1234567, result);
            defaultDriveMock.VerifyAll();
            mappedDriveMock.Verify(_ => _.Contains("one" + "identity"), Times.Never());
            mappedDriveMock.Verify(_ => _.Get<int>("one" + "identity"), Times.Never());
        }


        private void UseDefaultDriveWhenDriveMappingNotDefined_Assert(int[] result, Mock<ICacheDrive> defaultDriveMock, Mock<ICacheDrive> mappedDriveMock)
        {
            Assert.Equal(new[] { 1234567 }, result);
            defaultDriveMock.VerifyAll();
            mappedDriveMock.Verify(_ => _.Contains("one" + "identity"), Times.Never());
            mappedDriveMock.Verify(_ => _.Get<int>("one" + "identity"), Times.Never());
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheCore.CacheDriveMapping" />に定義されていない場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// </summary>
        [Fact]
        public void UseDefaultDriveWhenDriveMappingNotDefined_Simplex()
        {
            var mappedDriveMock = UseDefaultDriveWhenDriveMappingNotDefined_MappedDriveMock();
            var defaultDriveMock = UseDefaultDriveWhenDriveMappingNotDefined_DefaultDriveMock();
            var defaultPolicyMock = UseDefaultDriveWhenDriveMappingNotDefined_DefaultPolicyMock();


            var cache = new Cache(new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { "invalid", mappedDriveMock.Object } } });
            var result = cache.Get("one", "identity", new KeyLengthSimplexTranslator());

            UseDefaultDriveWhenDriveMappingNotDefined_Assert(result, defaultDriveMock, mappedDriveMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheCore.CacheDriveMapping" />に定義されていない場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// </summary>
        [Fact]
        public void UseDefaultDriveWhenDriveMappingNotDefined_Bulk_Simplex()
        {
            var mappedDriveMock = UseDefaultDriveWhenDriveMappingNotDefined_MappedDriveMock();
            var defaultDriveMock = UseDefaultDriveWhenDriveMappingNotDefined_DefaultDriveMock();
            var defaultPolicyMock = UseDefaultDriveWhenDriveMappingNotDefined_DefaultPolicyMock();


            var cache = new Cache(new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { "invalid", mappedDriveMock.Object } } });
            var result = cache.Get(new[] { "one" }, "identity", new KeyLengthSimplexTranslator());

            UseDefaultDriveWhenDriveMappingNotDefined_Assert(result, defaultDriveMock, mappedDriveMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheCore.CacheDriveMapping" />に定義されていない場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// </summary>
        [Fact]
        public void UseDefaultDriveWhenDriveMappingNotDefined_Vectorized()
        {
            var mappedDriveMock = UseDefaultDriveWhenDriveMappingNotDefined_MappedDriveMock();
            var defaultDriveMock = UseDefaultDriveWhenDriveMappingNotDefined_DefaultDriveMock();
            var defaultPolicyMock = UseDefaultDriveWhenDriveMappingNotDefined_DefaultPolicyMock();


            var cache = new Cache(new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { "invalid", mappedDriveMock.Object } } });
            var result = cache.Get("one", "identity", new KeyLengthVectorizedTranslator());

            UseDefaultDriveWhenDriveMappingNotDefined_Assert(result, defaultDriveMock, mappedDriveMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheCore.CacheDriveMapping" />に定義されていない場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// </summary>
        [Fact]
        public void UseDefaultDriveWhenDriveMappingNotDefined_Bulk_Vectorized()
        {
            var mappedDriveMock = UseDefaultDriveWhenDriveMappingNotDefined_MappedDriveMock();
            var defaultDriveMock = UseDefaultDriveWhenDriveMappingNotDefined_DefaultDriveMock();
            var defaultPolicyMock = UseDefaultDriveWhenDriveMappingNotDefined_DefaultPolicyMock();


            var cache = new Cache(new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { "invalid", mappedDriveMock.Object } } });
            var result = cache.Get(new[] { "one" }, "identity", new KeyLengthVectorizedTranslator());

            UseDefaultDriveWhenDriveMappingNotDefined_Assert(result, defaultDriveMock, mappedDriveMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheCore.CacheDriveMapping" />に定義されていない場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// </summary>
        [Fact]
        public void UseDefaultDriveWhenDriveMappingNotDefined_Optimized()
        {
            var mappedDriveMock = UseDefaultDriveWhenDriveMappingNotDefined_MappedDriveMock();
            var defaultDriveMock = UseDefaultDriveWhenDriveMappingNotDefined_DefaultDriveMock();
            var defaultPolicyMock = UseDefaultDriveWhenDriveMappingNotDefined_DefaultPolicyMock();


            var cache = new Cache(new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { "invalid", mappedDriveMock.Object } } });
            var result = cache.Get("one", "identity", new KeyLengthOptimizedTranslator());

            UseDefaultDriveWhenDriveMappingNotDefined_Assert(result, defaultDriveMock, mappedDriveMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheCore.CacheDriveMapping" />に定義されていない場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// </summary>
        [Fact]
        public void UseDefaultDriveWhenDriveMappingNotDefined_Bulk_Optimized()
        {
            var mappedDriveMock = UseDefaultDriveWhenDriveMappingNotDefined_MappedDriveMock();
            var defaultDriveMock = UseDefaultDriveWhenDriveMappingNotDefined_DefaultDriveMock();
            var defaultPolicyMock = UseDefaultDriveWhenDriveMappingNotDefined_DefaultPolicyMock();


            var cache = new Cache(new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { "invalid", mappedDriveMock.Object } } });
            var result = cache.Get(new[] { "one" }, "identity", new KeyLengthOptimizedTranslator());

            UseDefaultDriveWhenDriveMappingNotDefined_Assert(result, defaultDriveMock, mappedDriveMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheCore.CacheDriveMapping" />に定義されていない場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// </summary>
        [Fact]
        public void UseDefaultDriveWhenDriveMappingNotDefined_IdenticalSimplex()
        {
            var mappedDriveMock = UseDefaultDriveWhenDriveMappingNotDefined_MappedDriveMock();
            var defaultDriveMock = UseDefaultDriveWhenDriveMappingNotDefined_DefaultDriveMock();
            var defaultPolicyMock = UseDefaultDriveWhenDriveMappingNotDefined_DefaultPolicyMock();


            var cache = new Cache(new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { "invalid", mappedDriveMock.Object } } });
            var result = cache.Get("one", new KeyLengthIdenticalSimplexTranslator("identity"));

            UseDefaultDriveWhenDriveMappingNotDefined_Assert(result, defaultDriveMock, mappedDriveMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheCore.CacheDriveMapping" />に定義されていない場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// </summary>
        [Fact]
        public void UseDefaultDriveWhenDriveMappingNotDefined_Bulk_IdenticalSimplex()
        {
            var mappedDriveMock = UseDefaultDriveWhenDriveMappingNotDefined_MappedDriveMock();
            var defaultDriveMock = UseDefaultDriveWhenDriveMappingNotDefined_DefaultDriveMock();
            var defaultPolicyMock = UseDefaultDriveWhenDriveMappingNotDefined_DefaultPolicyMock();


            var cache = new Cache(new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { "invalid", mappedDriveMock.Object } } });
            var result = cache.Get(new[] { "one" }, new KeyLengthIdenticalSimplexTranslator("identity"));

            UseDefaultDriveWhenDriveMappingNotDefined_Assert(result, defaultDriveMock, mappedDriveMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheCore.CacheDriveMapping" />に定義されていない場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// </summary>
        [Fact]
        public void UseDefaultDriveWhenDriveMappingNotDefined_IdenticalVectorized()
        {
            var mappedDriveMock = UseDefaultDriveWhenDriveMappingNotDefined_MappedDriveMock();
            var defaultDriveMock = UseDefaultDriveWhenDriveMappingNotDefined_DefaultDriveMock();
            var defaultPolicyMock = UseDefaultDriveWhenDriveMappingNotDefined_DefaultPolicyMock();


            var cache = new Cache(new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { "invalid", mappedDriveMock.Object } } });
            var result = cache.Get("one", new KeyLengthIdenticalVectorizedTranslator("identity"));

            UseDefaultDriveWhenDriveMappingNotDefined_Assert(result, defaultDriveMock, mappedDriveMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheCore.CacheDriveMapping" />に定義されていない場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// </summary>
        [Fact]
        public void UseDefaultDriveWhenDriveMappingNotDefined_Bulk_IdenticalVectorized()
        {
            var mappedDriveMock = UseDefaultDriveWhenDriveMappingNotDefined_MappedDriveMock();
            var defaultDriveMock = UseDefaultDriveWhenDriveMappingNotDefined_DefaultDriveMock();
            var defaultPolicyMock = UseDefaultDriveWhenDriveMappingNotDefined_DefaultPolicyMock();


            var cache = new Cache(new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { "invalid", mappedDriveMock.Object } } });
            var result = cache.Get(new[] { "one" }, new KeyLengthIdenticalVectorizedTranslator("identity"));

            UseDefaultDriveWhenDriveMappingNotDefined_Assert(result, defaultDriveMock, mappedDriveMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheCore.CacheDriveMapping" />に定義されていない場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// </summary>
        [Fact]
        public void UseDefaultDriveWhenDriveMappingNotDefined_IdenticalOptimized()
        {
            var mappedDriveMock = UseDefaultDriveWhenDriveMappingNotDefined_MappedDriveMock();
            var defaultDriveMock = UseDefaultDriveWhenDriveMappingNotDefined_DefaultDriveMock();
            var defaultPolicyMock = UseDefaultDriveWhenDriveMappingNotDefined_DefaultPolicyMock();


            var cache = new Cache(new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { "invalid", mappedDriveMock.Object } } });
            var result = cache.Get("one", new KeyLengthIdenticalOptimizedTranslator("identity"));

            UseDefaultDriveWhenDriveMappingNotDefined_Assert(result, defaultDriveMock, mappedDriveMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheCore.CacheDriveMapping" />に定義されていない場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// </summary>
        [Fact]
        public void UseDefaultDriveWhenDriveMappingNotDefined_Bulk_IdenticalOptimized()
        {
            var mappedDriveMock = UseDefaultDriveWhenDriveMappingNotDefined_MappedDriveMock();
            var defaultDriveMock = UseDefaultDriveWhenDriveMappingNotDefined_DefaultDriveMock();
            var defaultPolicyMock = UseDefaultDriveWhenDriveMappingNotDefined_DefaultPolicyMock();


            var cache = new Cache(new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { "invalid", mappedDriveMock.Object } } });
            var result = cache.Get(new[] { "one" }, new KeyLengthIdenticalOptimizedTranslator("identity"));

            UseDefaultDriveWhenDriveMappingNotDefined_Assert(result, defaultDriveMock, mappedDriveMock);
        }

        //

        /// <summary>
        /// 保存先は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// </summary>
        [Fact]
        public void UseDefaultDriveWhenDriveMappingNotDefined_FuncSingle()
        {
            var mappedDriveMock = UseDefaultDriveWhenDriveMappingNotDefined_MappedDriveMock();
            var defaultDriveMock = UseDefaultDriveWhenDriveMappingNotDefined_DefaultDriveMock();
            var defaultPolicyMock = UseDefaultDriveWhenDriveMappingNotDefined_DefaultPolicyMock();


            var cache = new Cache(new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object)
            {
                CacheDriveMapping = new Dictionary<string, ICacheDrive> { { typeof(KeyLengthSimplexTranslator).FullName, mappedDriveMock.Object } }
            });
            var result = cache.Get("one", "identity", new KeyLengthSimplexTranslator().Get);

            UseDefaultDriveWhenDriveMappingNotDefined_Assert(result, defaultDriveMock, mappedDriveMock);
        }

        /// <summary>
        /// 保存先は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// </summary>
        [Fact]
        public void UseDefaultDriveWhenDriveMappingNotDefined_Bulk_FuncSingle()
        {
            var mappedDriveMock = UseDefaultDriveWhenDriveMappingNotDefined_MappedDriveMock();
            var defaultDriveMock = UseDefaultDriveWhenDriveMappingNotDefined_DefaultDriveMock();
            var defaultPolicyMock = UseDefaultDriveWhenDriveMappingNotDefined_DefaultPolicyMock();


            var cache = new Cache(new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object)
            {
                CacheDriveMapping = new Dictionary<string, ICacheDrive> { { typeof(KeyLengthSimplexTranslator).FullName, mappedDriveMock.Object } }
            });
            var result = cache.Get(new[] { "one" }, "identity", new KeyLengthSimplexTranslator().Get);

            UseDefaultDriveWhenDriveMappingNotDefined_Assert(result, defaultDriveMock, mappedDriveMock);
        }

        /// <summary>
        /// 保存先は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// </summary>
        [Fact]
        public void UseDefaultDriveWhenDriveMappingNotDefined_FuncVectorized()
        {
            var mappedDriveMock = UseDefaultDriveWhenDriveMappingNotDefined_MappedDriveMock();
            var defaultDriveMock = UseDefaultDriveWhenDriveMappingNotDefined_DefaultDriveMock();
            var defaultPolicyMock = UseDefaultDriveWhenDriveMappingNotDefined_DefaultPolicyMock();


            var cache = new Cache(new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object)
            {
                CacheDriveMapping = new Dictionary<string, ICacheDrive> { { typeof(KeyLengthVectorizedTranslator).FullName, mappedDriveMock.Object } }
            });
            var result = cache.Get("one", "identity", new KeyLengthVectorizedTranslator().Get);

            UseDefaultDriveWhenDriveMappingNotDefined_Assert(result, defaultDriveMock, mappedDriveMock);
        }

        /// <summary>
        /// 保存先は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// </summary>
        [Fact]
        public void UseDefaultDriveWhenDriveMappingNotDefined_Bulk_FuncVectorized()
        {
            var mappedDriveMock = UseDefaultDriveWhenDriveMappingNotDefined_MappedDriveMock();
            var defaultDriveMock = UseDefaultDriveWhenDriveMappingNotDefined_DefaultDriveMock();
            var defaultPolicyMock = UseDefaultDriveWhenDriveMappingNotDefined_DefaultPolicyMock();


            var cache = new Cache(new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object)
            {
                CacheDriveMapping = new Dictionary<string, ICacheDrive> { { typeof(KeyLengthVectorizedTranslator).FullName, mappedDriveMock.Object } }
            });
            var result = cache.Get(new[] { "one" }, "identity", new KeyLengthVectorizedTranslator().Get);

            UseDefaultDriveWhenDriveMappingNotDefined_Assert(result, defaultDriveMock, mappedDriveMock);
        }

        //  Policy Test

        private Mock<ICacheDrive> UseUserDefinedPolicyMapping_DefaultDriveMock()
        {
            var defaultDriveMock = new Mock<ICacheDrive>();
            defaultDriveMock.Setup(_ => _.Contains("one" + "identity")).Returns(true);
            defaultDriveMock.Setup(_ => _.Get<int>("one" + "identity")).Returns(new CacheEntry<int>() { Key = "one", Value = 1234567 });
            return defaultDriveMock;
        }

        private Mock<IExpirePolicy> UseUserDefinedPolicyMapping_DefaulPolicyMock()
        {
            var policyMock = new Mock<IExpirePolicy>();
            policyMock.Setup(_ => _.Validate(It.IsAny<CacheEntry<int>>())).Returns(true);
            return policyMock;
        }

        private Mock<IExpirePolicy> UseUserDefinedPolicyMapping_CustomPolicyMock()
        {
            var policyMock = new Mock<IExpirePolicy>();
            policyMock.Setup(_ => _.Validate(It.IsAny<CacheEntry<int>>())).Returns(false);
            return policyMock;
        }

        private void UseUserDefinedPolicyMapping_Assert(int result, Mock<IExpirePolicy> defaultPolicyMock, Mock<IExpirePolicy> customPolicyMock)
        {
            Assert.Equal(3, result);
            defaultPolicyMock.Verify(_ => _.Validate(It.IsAny<CacheEntry<int>>()), Times.Never());
            customPolicyMock.VerifyAll();
        }

        private void UseUserDefinedPolicyMapping_Assert(int[] result, Mock<IExpirePolicy> defaultPolicyMock, Mock<IExpirePolicy> customPolicyMock)
        {
            Assert.Equal(new[] { 3 }, result);
            defaultPolicyMock.Verify(_ => _.Validate(It.IsAny<CacheEntry<int>>()), Times.Never());
            customPolicyMock.VerifyAll();
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されている場合はそれを使用します。
        /// </summary>
        [Fact]
        public void UseUserDefinedPolicyMapping_Simplex()
        {
            var defaultDriveMock = UseUserDefinedPolicyMapping_DefaultDriveMock();
            var defaultPolicyMock = UseUserDefinedPolicyMapping_DefaulPolicyMock();
            var customPolicyMock = UseUserDefinedPolicyMapping_CustomPolicyMock();

            var cache = new Cache(new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy>() { { typeof(KeyLengthSimplexTranslator).FullName, customPolicyMock.Object } } });
            var result = cache.Get("one", "identity", new KeyLengthSimplexTranslator());

            UseUserDefinedPolicyMapping_Assert(result, defaultPolicyMock, customPolicyMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されている場合はそれを使用します。
        /// </summary>
        [Fact]
        public void UseUserDefinedPolicyMapping_Bulk_Simplex()
        {
            var defaultDriveMock = UseUserDefinedPolicyMapping_DefaultDriveMock();
            var defaultPolicyMock = UseUserDefinedPolicyMapping_DefaulPolicyMock();
            var customPolicyMock = UseUserDefinedPolicyMapping_CustomPolicyMock();

            var cache = new Cache(new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy>() { { typeof(KeyLengthSimplexTranslator).FullName, customPolicyMock.Object } } });
            var result = cache.Get(new[] { "one" }, "identity", new KeyLengthSimplexTranslator());

            UseUserDefinedPolicyMapping_Assert(result, defaultPolicyMock, customPolicyMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されている場合はそれを使用します。
        /// </summary>
        [Fact]
        public void UseUserDefinedPolicyMapping_Vectorized()
        {
            var defaultDriveMock = UseUserDefinedPolicyMapping_DefaultDriveMock();
            var defaultPolicyMock = UseUserDefinedPolicyMapping_DefaulPolicyMock();
            var customPolicyMock = UseUserDefinedPolicyMapping_CustomPolicyMock();

            var cache = new Cache(new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy>() { { typeof(KeyLengthVectorizedTranslator).FullName, customPolicyMock.Object } } });
            var result = cache.Get("one", "identity", new KeyLengthVectorizedTranslator());

            UseUserDefinedPolicyMapping_Assert(result, defaultPolicyMock, customPolicyMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されている場合はそれを使用します。
        /// </summary>
        [Fact]
        public void UseUserDefinedPolicyMapping_Bulk_Vectorized()
        {
            var defaultDriveMock = UseUserDefinedPolicyMapping_DefaultDriveMock();
            var defaultPolicyMock = UseUserDefinedPolicyMapping_DefaulPolicyMock();
            var customPolicyMock = UseUserDefinedPolicyMapping_CustomPolicyMock();

            var cache = new Cache(new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy>() { { typeof(KeyLengthVectorizedTranslator).FullName, customPolicyMock.Object } } });
            var result = cache.Get(new[] { "one" }, "identity", new KeyLengthVectorizedTranslator());

            UseUserDefinedPolicyMapping_Assert(result, defaultPolicyMock, customPolicyMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されている場合はそれを使用します。
        /// </summary>
        [Fact]
        public void UseUserDefinedPolicyMapping_Optimized()
        {
            var defaultDriveMock = UseUserDefinedPolicyMapping_DefaultDriveMock();
            var defaultPolicyMock = UseUserDefinedPolicyMapping_DefaulPolicyMock();
            var customPolicyMock = UseUserDefinedPolicyMapping_CustomPolicyMock();

            var cache = new Cache(new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy>() { { typeof(KeyLengthOptimizedTranslator).FullName, customPolicyMock.Object } } });
            var result = cache.Get("one", "identity", new KeyLengthOptimizedTranslator());

            UseUserDefinedPolicyMapping_Assert(result, defaultPolicyMock, customPolicyMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されている場合はそれを使用します。
        /// </summary>
        [Fact]
        public void UseUserDefinedPolicyMapping_Bulk_Optimized()
        {
            var defaultDriveMock = UseUserDefinedPolicyMapping_DefaultDriveMock();
            var defaultPolicyMock = UseUserDefinedPolicyMapping_DefaulPolicyMock();
            var customPolicyMock = UseUserDefinedPolicyMapping_CustomPolicyMock();

            var cache = new Cache(new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy>() { { typeof(KeyLengthOptimizedTranslator).FullName, customPolicyMock.Object } } });
            var result = cache.Get(new[] { "one" }, "identity", new KeyLengthOptimizedTranslator());

            UseUserDefinedPolicyMapping_Assert(result, defaultPolicyMock, customPolicyMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されている場合はそれを使用します。
        /// </summary>
        [Fact]
        public void UseUserDefinedPolicyMapping_IdenticalSimplex()
        {
            var defaultDriveMock = UseUserDefinedPolicyMapping_DefaultDriveMock();
            var defaultPolicyMock = UseUserDefinedPolicyMapping_DefaulPolicyMock();
            var customPolicyMock = UseUserDefinedPolicyMapping_CustomPolicyMock();

            var cache = new Cache(new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy>() { { typeof(KeyLengthIdenticalSimplexTranslator).FullName, customPolicyMock.Object } } });
            var result = cache.Get("one", new KeyLengthIdenticalSimplexTranslator("identity"));

            UseUserDefinedPolicyMapping_Assert(result, defaultPolicyMock, customPolicyMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されている場合はそれを使用します。
        /// </summary>
        [Fact]
        public void UseUserDefinedPolicyMapping_Bulk_IdenticalSimplex()
        {
            var defaultDriveMock = UseUserDefinedPolicyMapping_DefaultDriveMock();
            var defaultPolicyMock = UseUserDefinedPolicyMapping_DefaulPolicyMock();
            var customPolicyMock = UseUserDefinedPolicyMapping_CustomPolicyMock();

            var cache = new Cache(new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy>() { { typeof(KeyLengthIdenticalSimplexTranslator).FullName, customPolicyMock.Object } } });
            var result = cache.Get(new[] { "one" }, new KeyLengthIdenticalSimplexTranslator("identity"));

            UseUserDefinedPolicyMapping_Assert(result, defaultPolicyMock, customPolicyMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されている場合はそれを使用します。
        /// </summary>
        [Fact]
        public void UseUserDefinedPolicyMapping_IdenticalVectorized()
        {
            var defaultDriveMock = UseUserDefinedPolicyMapping_DefaultDriveMock();
            var defaultPolicyMock = UseUserDefinedPolicyMapping_DefaulPolicyMock();
            var customPolicyMock = UseUserDefinedPolicyMapping_CustomPolicyMock();

            var cache = new Cache(new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy>() { { typeof(KeyLengthIdenticalVectorizedTranslator).FullName, customPolicyMock.Object } } });
            var result = cache.Get("one", new KeyLengthIdenticalVectorizedTranslator("identity"));

            UseUserDefinedPolicyMapping_Assert(result, defaultPolicyMock, customPolicyMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されている場合はそれを使用します。
        /// </summary>
        [Fact]
        public void UseUserDefinedPolicyMapping_Bulk_IdenticalVectorized()
        {
            var defaultDriveMock = UseUserDefinedPolicyMapping_DefaultDriveMock();
            var defaultPolicyMock = UseUserDefinedPolicyMapping_DefaulPolicyMock();
            var customPolicyMock = UseUserDefinedPolicyMapping_CustomPolicyMock();

            var cache = new Cache(new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy>() { { typeof(KeyLengthIdenticalVectorizedTranslator).FullName, customPolicyMock.Object } } });
            var result = cache.Get(new[] { "one" }, new KeyLengthIdenticalVectorizedTranslator("identity"));

            UseUserDefinedPolicyMapping_Assert(result, defaultPolicyMock, customPolicyMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されている場合はそれを使用します。
        /// </summary>
        [Fact]
        public void UseUserDefinedPolicyMapping_IdenticalOptimized()
        {
            var defaultDriveMock = UseUserDefinedPolicyMapping_DefaultDriveMock();
            var defaultPolicyMock = UseUserDefinedPolicyMapping_DefaulPolicyMock();
            var customPolicyMock = UseUserDefinedPolicyMapping_CustomPolicyMock();

            var cache = new Cache(new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy>() { { typeof(KeyLengthIdenticalOptimizedTranslator).FullName, customPolicyMock.Object } } });
            var result = cache.Get("one", new KeyLengthIdenticalOptimizedTranslator("identity"));

            UseUserDefinedPolicyMapping_Assert(result, defaultPolicyMock, customPolicyMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されている場合はそれを使用します。
        /// </summary>
        [Fact]
        public void UseUserDefinedPolicyMapping_Bulk_IdenticalOptimized()
        {
            var defaultDriveMock = UseUserDefinedPolicyMapping_DefaultDriveMock();
            var defaultPolicyMock = UseUserDefinedPolicyMapping_DefaulPolicyMock();
            var customPolicyMock = UseUserDefinedPolicyMapping_CustomPolicyMock();

            var cache = new Cache(new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy>() { { typeof(KeyLengthIdenticalOptimizedTranslator).FullName, customPolicyMock.Object } } });
            var result = cache.Get(new[] { "one" }, new KeyLengthIdenticalOptimizedTranslator("identity"));

            UseUserDefinedPolicyMapping_Assert(result, defaultPolicyMock, customPolicyMock);
        }

        //

        private Mock<ICacheDrive> UseDefaultPolicyMapping_DefaultDriveMock()
        {
            var defaultDriveMock = new Mock<ICacheDrive>();
            defaultDriveMock.Setup(_ => _.Contains("one" + "identity")).Returns(true);
            defaultDriveMock.Setup(_ => _.Get<int>("one" + "identity")).Returns(new CacheEntry<int>() { Key = "one", Value = 1234567 });
            return defaultDriveMock;
        }

        private Mock<IExpirePolicy> UseDefaultPolicyMapping_DefaulPolicyMock()
        {
            var policyMock = new Mock<IExpirePolicy>();
            policyMock.Setup(_ => _.Validate(It.IsAny<CacheEntry<int>>())).Returns(true);
            return policyMock;
        }

        private Mock<IExpirePolicy> UseDefaultPolicyMapping_CustomPolicyMock()
        {
            var policyMock = new Mock<IExpirePolicy>();
            policyMock.Setup(_ => _.Validate(It.IsAny<CacheEntry<int>>())).Returns(false);
            return policyMock;
        }

        private void UseDefaultPolicyMapping_Assert(int result, Mock<IExpirePolicy> defaultPolicyMock, Mock<IExpirePolicy> customPolicyMock)
        {
            Assert.Equal(1234567, result);
            defaultPolicyMock.VerifyAll();
            customPolicyMock.Verify(_ => _.Validate(It.IsAny<CacheEntry<int>>()), Times.Never());
        }

        private void UseDefaultPolicyMapping_Assert(int[] result, Mock<IExpirePolicy> defaultPolicyMock, Mock<IExpirePolicy> customPolicyMock)
        {
            Assert.Equal(new[] { 1234567 }, result);
            defaultPolicyMock.VerifyAll();
            customPolicyMock.Verify(_ => _.Validate(It.IsAny<CacheEntry<int>>()), Times.Never());
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されてない場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// </summary>
        [Fact]
        public void UseDefaultPolicyMapping_Simplex()
        {
            var defaultDriveMock = UseDefaultPolicyMapping_DefaultDriveMock();
            var defaultPolicyMock = UseDefaultPolicyMapping_DefaulPolicyMock();
            var customPolicyMock = UseDefaultPolicyMapping_CustomPolicyMock();

            var cache = new Cache(new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy>() { { "invalid", customPolicyMock.Object } } });
            var result = cache.Get("one", "identity", new KeyLengthSimplexTranslator());

            UseDefaultPolicyMapping_Assert(result, defaultPolicyMock, customPolicyMock);

        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されてない場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// </summary>
        [Fact]
        public void UseDefaultPolicyMapping_Bulk_Simplex()
        {
            var defaultDriveMock = UseDefaultPolicyMapping_DefaultDriveMock();
            var defaultPolicyMock = UseDefaultPolicyMapping_DefaulPolicyMock();
            var customPolicyMock = UseDefaultPolicyMapping_CustomPolicyMock();

            var cache = new Cache(new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy>() { { "invalid", customPolicyMock.Object } } });
            var result = cache.Get(new[] { "one" }, "identity", new KeyLengthSimplexTranslator());

            UseDefaultPolicyMapping_Assert(result, defaultPolicyMock, customPolicyMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されてない場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// </summary>
        [Fact]
        public void UseDefaultPolicyMapping_Vectorized()
        {
            var defaultDriveMock = UseDefaultPolicyMapping_DefaultDriveMock();
            var defaultPolicyMock = UseDefaultPolicyMapping_DefaulPolicyMock();
            var customPolicyMock = UseDefaultPolicyMapping_CustomPolicyMock();

            var cache = new Cache(new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy>() { { "invalid", customPolicyMock.Object } } });
            var result = cache.Get("one", "identity", new KeyLengthVectorizedTranslator());

            UseDefaultPolicyMapping_Assert(result, defaultPolicyMock, customPolicyMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されてない場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// </summary>
        [Fact]
        public void UseDefaultPolicyMapping_Bulk_Vectorized()
        {
            var defaultDriveMock = UseDefaultPolicyMapping_DefaultDriveMock();
            var defaultPolicyMock = UseDefaultPolicyMapping_DefaulPolicyMock();
            var customPolicyMock = UseDefaultPolicyMapping_CustomPolicyMock();

            var cache = new Cache(new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy>() { { "invalid", customPolicyMock.Object } } });
            var result = cache.Get(new[] { "one" }, "identity", new KeyLengthVectorizedTranslator());

            UseDefaultPolicyMapping_Assert(result, defaultPolicyMock, customPolicyMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されてない場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// </summary>
        [Fact]
        public void UseDefaultPolicyMapping_Optimized()
        {
            var defaultDriveMock = UseDefaultPolicyMapping_DefaultDriveMock();
            var defaultPolicyMock = UseDefaultPolicyMapping_DefaulPolicyMock();
            var customPolicyMock = UseDefaultPolicyMapping_CustomPolicyMock();

            var cache = new Cache(new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy>() { { "invalid", customPolicyMock.Object } } });
            var result = cache.Get("one", "identity", new KeyLengthOptimizedTranslator());

            UseDefaultPolicyMapping_Assert(result, defaultPolicyMock, customPolicyMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されてない場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// </summary>
        [Fact]
        public void UseDefaultPolicyMapping_Bulk_Optimized()
        {
            var defaultDriveMock = UseDefaultPolicyMapping_DefaultDriveMock();
            var defaultPolicyMock = UseDefaultPolicyMapping_DefaulPolicyMock();
            var customPolicyMock = UseDefaultPolicyMapping_CustomPolicyMock();

            var cache = new Cache(new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy>() { { "invalid", customPolicyMock.Object } } });
            var result = cache.Get(new[] { "one" }, "identity", new KeyLengthOptimizedTranslator());

            UseDefaultPolicyMapping_Assert(result, defaultPolicyMock, customPolicyMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されてない場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// </summary>
        [Fact]
        public void UseDefaultPolicyMapping_IdenticalSimplex()
        {
            var defaultDriveMock = UseDefaultPolicyMapping_DefaultDriveMock();
            var defaultPolicyMock = UseDefaultPolicyMapping_DefaulPolicyMock();
            var customPolicyMock = UseDefaultPolicyMapping_CustomPolicyMock();

            var cache = new Cache(new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy>() { { "invalid", customPolicyMock.Object } } });
            var result = cache.Get("one", new KeyLengthIdenticalSimplexTranslator("identity"));

            UseDefaultPolicyMapping_Assert(result, defaultPolicyMock, customPolicyMock);

        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されてない場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// </summary>
        [Fact]
        public void UseDefaultPolicyMapping_Bulk_IdenticalSimplex()
        {
            var defaultDriveMock = UseDefaultPolicyMapping_DefaultDriveMock();
            var defaultPolicyMock = UseDefaultPolicyMapping_DefaulPolicyMock();
            var customPolicyMock = UseDefaultPolicyMapping_CustomPolicyMock();

            var cache = new Cache(new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy>() { { "invalid", customPolicyMock.Object } } });
            var result = cache.Get(new[] { "one" }, new KeyLengthIdenticalSimplexTranslator("identity"));

            UseDefaultPolicyMapping_Assert(result, defaultPolicyMock, customPolicyMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されてない場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// </summary>
        [Fact]
        public void UseDefaultPolicyMapping_IdenticalVectorized()
        {
            var defaultDriveMock = UseDefaultPolicyMapping_DefaultDriveMock();
            var defaultPolicyMock = UseDefaultPolicyMapping_DefaulPolicyMock();
            var customPolicyMock = UseDefaultPolicyMapping_CustomPolicyMock();

            var cache = new Cache(new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy>() { { "invalid", customPolicyMock.Object } } });
            var result = cache.Get("one", new KeyLengthIdenticalVectorizedTranslator("identity"));

            UseDefaultPolicyMapping_Assert(result, defaultPolicyMock, customPolicyMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されてない場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// </summary>
        [Fact]
        public void UseDefaultPolicyMapping_Bulk_IdenticalVectorized()
        {
            var defaultDriveMock = UseDefaultPolicyMapping_DefaultDriveMock();
            var defaultPolicyMock = UseDefaultPolicyMapping_DefaulPolicyMock();
            var customPolicyMock = UseDefaultPolicyMapping_CustomPolicyMock();

            var cache = new Cache(new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy>() { { "invalid", customPolicyMock.Object } } });
            var result = cache.Get(new[] { "one" }, new KeyLengthIdenticalVectorizedTranslator("identity"));

            UseDefaultPolicyMapping_Assert(result, defaultPolicyMock, customPolicyMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されてない場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// </summary>
        [Fact]
        public void UseDefaultPolicyMapping_IdenticalOptimized()
        {
            var defaultDriveMock = UseDefaultPolicyMapping_DefaultDriveMock();
            var defaultPolicyMock = UseDefaultPolicyMapping_DefaulPolicyMock();
            var customPolicyMock = UseDefaultPolicyMapping_CustomPolicyMock();

            var cache = new Cache(new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy>() { { "invalid", customPolicyMock.Object } } });
            var result = cache.Get("one", new KeyLengthIdenticalOptimizedTranslator("identity"));

            UseDefaultPolicyMapping_Assert(result, defaultPolicyMock, customPolicyMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されてない場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// </summary>
        [Fact]
        public void UseDefaultPolicyMapping_Bulk_IdenticalOptimized()
        {
            var defaultDriveMock = UseDefaultPolicyMapping_DefaultDriveMock();
            var defaultPolicyMock = UseDefaultPolicyMapping_DefaulPolicyMock();
            var customPolicyMock = UseDefaultPolicyMapping_CustomPolicyMock();

            var cache = new Cache(new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy>() { { "invalid", customPolicyMock.Object } } });
            var result = cache.Get(new[] { "one" }, new KeyLengthIdenticalOptimizedTranslator("identity"));

            UseDefaultPolicyMapping_Assert(result, defaultPolicyMock, customPolicyMock);
        }

        //

        /// <summary>
        /// ポリシーは<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// </summary>
        [Fact]
        public void UseDefaultPolicyMapping_FuncSingle()
        {
            var defaultDriveMock = UseDefaultPolicyMapping_DefaultDriveMock();
            var defaultPolicyMock = UseDefaultPolicyMapping_DefaulPolicyMock();
            var customPolicyMock = UseDefaultPolicyMapping_CustomPolicyMock();

            var cache = new Cache(new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy>() { { "invalid", customPolicyMock.Object } } });
            var result = cache.Get("one", "identity", new KeyLengthSimplexTranslator().Get);

            UseDefaultPolicyMapping_Assert(result, defaultPolicyMock, customPolicyMock);
        }

        /// <summary>
        /// ポリシーは<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// </summary>
        [Fact]
        public void UseDefaultPolicyMapping_Bulk_FuncSingle()
        {
            var defaultDriveMock = UseDefaultPolicyMapping_DefaultDriveMock();
            var defaultPolicyMock = UseDefaultPolicyMapping_DefaulPolicyMock();
            var customPolicyMock = UseDefaultPolicyMapping_CustomPolicyMock();

            var cache = new Cache(new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy>() { { "invalid", customPolicyMock.Object } } });
            var result = cache.Get(new[] { "one" }, "identity", new KeyLengthSimplexTranslator().Get);

            UseDefaultPolicyMapping_Assert(result, defaultPolicyMock, customPolicyMock);
        }

        /// <summary>
        /// ポリシーは<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// </summary>
        [Fact]
        public void UseDefaultPolicyMapping_FuncVectorized()
        {
            var defaultDriveMock = UseDefaultPolicyMapping_DefaultDriveMock();
            var defaultPolicyMock = UseDefaultPolicyMapping_DefaulPolicyMock();
            var customPolicyMock = UseDefaultPolicyMapping_CustomPolicyMock();

            var cache = new Cache(new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy>() { { "invalid", customPolicyMock.Object } } });
            var result = cache.Get("one", "identity", new KeyLengthVectorizedTranslator().Get);

            UseDefaultPolicyMapping_Assert(result, defaultPolicyMock, customPolicyMock);
        }

        /// <summary>
        /// ポリシーは<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// </summary>
        [Fact]
        public void UseDefaultPolicyMapping_Bulk_FuncVectorized()
        {
            var defaultDriveMock = UseDefaultPolicyMapping_DefaultDriveMock();
            var defaultPolicyMock = UseDefaultPolicyMapping_DefaulPolicyMock();
            var customPolicyMock = UseDefaultPolicyMapping_CustomPolicyMock();

            var cache = new Cache(new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy>() { { "invalid", customPolicyMock.Object } } });
            var result = cache.Get(new[] { "one" }, "identity", new KeyLengthVectorizedTranslator().Get);

            UseDefaultPolicyMapping_Assert(result, defaultPolicyMock, customPolicyMock);
        }

        // Fetch Test

        /// <summary>
        /// キー、トランスレータ識別子、トランスレータを指定し、キャッシュを取得します。
        /// </summary>
        [Fact]
        public void Fetch_Simplex()
        {
            var cache = new Cache();

            var result = cache.Get("key", "identity", new KeyLengthSimplexTranslator());

            Assert.Equal(3, result);
        }

        /// <summary>
        /// キー、トランスレータ識別子、トランスレータを指定し、キャッシュを取得します。
        /// </summary>
        [Fact]
        public void Fetch_Vectorized()
        {
            var cache = new Cache();

            var result = cache.Get("key", "identity", new KeyLengthVectorizedTranslator());

            Assert.Equal(3, result);
        }

        /// <summary>
        /// キー、トランスレータ識別子、トランスレータを指定し、キャッシュを取得します。
        /// </summary>
        [Fact]
        public void Fetch_Optimized()
        {
            var cache = new Cache();

            var result = cache.Get("key", "identity", new KeyLengthOptimizedTranslator());

            Assert.Equal(3, result);
        }

        /// <summary>
        /// キー、トランスレータ識別子、トランスレータを指定し、キャッシュを取得します。
        /// </summary>
        [Fact]
        public void Fetch_FuncSingle()
        {
            var cache = new Cache();

            var result = cache.Get("key", "identity", new KeyLengthSimplexTranslator().Get);

            Assert.Equal(3, result);
        }

        /// <summary>
        /// キー、トランスレータ識別子、トランスレータを指定し、キャッシュを取得します。
        /// </summary>
        [Fact]
        public void Fetch_FuncVectorized()
        {
            var cache = new Cache();

            var result = cache.Get("key", "identity", new KeyLengthVectorizedTranslator().Get);

            Assert.Equal(3, result);
        }

        /// <summary>
        /// キー、トランスレータを指定し、キャッシュを取得します。
        /// </summary>
        [Fact]
        public void Fetch_IdenticalSimplex()
        {
            var cache = new Cache();

            var result = cache.Get("key", new KeyLengthIdenticalSimplexTranslator("identity"));

            Assert.Equal(3, result);
        }

        /// <summary>
        /// キー、トランスレータを指定し、キャッシュを取得します。
        /// </summary>
        [Fact]
        public void Fetch_IdenticalSVectorized()
        {
            var cache = new Cache();

            var result = cache.Get("key", new KeyLengthIdenticalVectorizedTranslator("identity"));

            Assert.Equal(3, result);
        }

        /// <summary>
        /// キー、トランスレータを指定し、キャッシュを取得します。
        /// </summary>
        [Fact]
        public void Fetch_IdenticalSOptimized()
        {
            var cache = new Cache();

            var result = cache.Get("key", new KeyLengthIdenticalOptimizedTranslator("identity"));

            Assert.Equal(3, result);
        }

        //

        /// <summary>
        /// キー、トランスレータ識別子、トランスレータを指定し、キャッシュを取得します。
        /// </summary>
        [Fact]
        public void Fetch_Bulk_Simplex()
        {
            var cache = new Cache();

            var result = cache.Get(new[] { "k", "ke", "key" }, "identity", new KeyLengthSimplexTranslator());

            Assert.Equal(new[] { 1, 2, 3 }, result);
        }

        /// <summary>
        /// キー、トランスレータ識別子、トランスレータを指定し、キャッシュを取得します。
        /// </summary>
        [Fact]
        public void Fetch_Bulk_Vectorized()
        {
            var cache = new Cache();

            var result = cache.Get(new[] { "k", "ke", "key" }, "identity", new KeyLengthVectorizedTranslator());

            Assert.Equal(new[] { 1, 2, 3 }, result);
        }

        /// <summary>
        /// キー、トランスレータ識別子、トランスレータを指定し、キャッシュを取得します。
        /// </summary>
        [Fact]
        public void Fetch_Bulk_Optimized()
        {
            var cache = new Cache();

            var result = cache.Get(new[] { "k", "ke", "key" }, "identity", new KeyLengthOptimizedTranslator());

            Assert.Equal(new[] { 1, 2, 3 }, result);
        }

        /// <summary>
        /// キー、トランスレータ識別子、トランスレータを指定し、キャッシュを取得します。
        /// </summary>
        [Fact]
        public void Fetch_Bulk_FuncSingle()
        {
            var cache = new Cache();

            var result = cache.Get(new[] { "k", "ke", "key" }, "identity", new KeyLengthSimplexTranslator().Get);

            Assert.Equal(new[] { 1, 2, 3 }, result);
        }

        /// <summary>
        /// キー、トランスレータ識別子、トランスレータを指定し、キャッシュを取得します。
        /// </summary>
        [Fact]
        public void Fetch_Bulk_FuncVectorized()
        {
            var cache = new Cache();

            var result = cache.Get(new[] { "k", "ke", "key" }, "identity", new KeyLengthVectorizedTranslator().Get);

            Assert.Equal(new[] { 1, 2, 3 }, result);
        }

        /// <summary>
        /// キー、トランスレータを指定し、キャッシュを取得します。
        /// </summary>
        [Fact]
        public void Fetch_Bulk_IdenticalSimplex()
        {
            var cache = new Cache();

            var result = cache.Get(new[] { "k", "ke", "key" }, new KeyLengthIdenticalSimplexTranslator("identity"));

            Assert.Equal(new[] { 1, 2, 3 }, result);
        }

        /// <summary>
        /// キー、トランスレータを指定し、キャッシュを取得します。
        /// </summary>
        [Fact]
        public void Fetch_Bulk_IdenticalVectorized()
        {
            var cache = new Cache();

            var result = cache.Get(new[] { "k", "ke", "key" }, new KeyLengthIdenticalVectorizedTranslator("identity"));

            Assert.Equal(new[] { 1, 2, 3 }, result);
        }

        /// <summary>
        /// キー、トランスレータを指定し、キャッシュを取得します。
        /// </summary>
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
