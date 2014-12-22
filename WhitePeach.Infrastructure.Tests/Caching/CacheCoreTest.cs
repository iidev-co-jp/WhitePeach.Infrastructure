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
    public class CacheCoreTest
    {
        // Fetch Test

        /// <summary>
        /// キー、トランスレータを指定し、キャッシュを取得します。
        /// </summary>
        [Fact]
        public void Fetch_Simplex()
        {
            var defaultDriveMock = new Mock<ICacheDrive>();
            defaultDriveMock.Setup(_ => _.Contains(It.IsAny<string>())).Returns(false);

            var defaultPolicyMock = new Mock<IExpirePolicy>();
            defaultPolicyMock.Setup(_ => _.Validate(It.IsAny<CacheEntry<int>>())).Returns(true);

            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object);
            var result = core.Get("key", "identity", new KeyLengthSimplexTranslator());

            Assert.Equal(3, result);
        }


        /// <summary>
        /// キー、トランスレータを指定し、キャッシュを取得します。
        /// </summary>
        [Fact]
        public void Fetch_Vectorized()
        {
            var defaultDriveMock = new Mock<ICacheDrive>();
            defaultDriveMock.Setup(_ => _.Contains(It.IsAny<string>())).Returns(false);

            var defaultPolicyMock = new Mock<IExpirePolicy>();
            defaultPolicyMock.Setup(_ => _.Validate(It.IsAny<CacheEntry<int>>())).Returns(true);

            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object);
            var result = core.Get("key", "identity", new KeyLengthVectorizedTranslator());

            Assert.Equal(3, result);
        }

        /// <summary>
        /// キー、トランスレータを指定し、キャッシュを取得します。
        /// </summary>
        [Fact]
        public void Fetch_Optimized()
        {
            var defaultDriveMock = new Mock<ICacheDrive>();
            defaultDriveMock.Setup(_ => _.Contains(It.IsAny<string>())).Returns(false);

            var defaultPolicyMock = new Mock<IExpirePolicy>();
            defaultPolicyMock.Setup(_ => _.Validate(It.IsAny<CacheEntry<int>>())).Returns(true);

            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object);
            var result = core.Get("key", "identity", new KeyLengthOptimizedTranslator());

            Assert.Equal(3, result);
        }

        /// <summary>
        /// キー、トランスレータを指定し、キャッシュを取得します。
        /// </summary>
        [Fact]
        public void Fetch_IdenticalSimplex()
        {
            var defaultDriveMock = new Mock<ICacheDrive>();
            defaultDriveMock.Setup(_ => _.Contains(It.IsAny<string>())).Returns(false);

            var defaultPolicyMock = new Mock<IExpirePolicy>();
            defaultPolicyMock.Setup(_ => _.Validate(It.IsAny<CacheEntry<int>>())).Returns(true);

            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object);
            var result = core.Get("key", new KeyLengthIdenticalSimplexTranslator("identity"));

            Assert.Equal(3, result);
        }

        /// <summary>
        /// キー、トランスレータを指定し、キャッシュを取得します。
        /// </summary>
        [Fact]
        public void Fetch_IdenticalVectorized()
        {
            var defaultDriveMock = new Mock<ICacheDrive>();
            defaultDriveMock.Setup(_ => _.Contains(It.IsAny<string>())).Returns(false);

            var defaultPolicyMock = new Mock<IExpirePolicy>();
            defaultPolicyMock.Setup(_ => _.Validate(It.IsAny<CacheEntry<int>>())).Returns(true);

            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object);
            var result = core.Get("key", new KeyLengthIdenticalVectorizedTranslator("identity"));

            Assert.Equal(3, result);
        }

        /// <summary>
        /// キー、トランスレータを指定し、キャッシュを取得します。
        /// </summary>
        [Fact]
        public void Fetch_IdenticalOptimized()
        {
            var defaultDriveMock = new Mock<ICacheDrive>();
            defaultDriveMock.Setup(_ => _.Contains(It.IsAny<string>())).Returns(false);

            var defaultPolicyMock = new Mock<IExpirePolicy>();
            defaultPolicyMock.Setup(_ => _.Validate(It.IsAny<CacheEntry<int>>())).Returns(true);

            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object);
            var result = core.Get("key", new KeyLengthIdenticalOptimizedTranslator("identity"));

            Assert.Equal(3, result);
        }

        //


        /// <summary>
        /// キー、トランスレータを指定し、キャッシュを取得します。
        /// </summary>
        [Fact]
        public void Fetch_Bulk_Simplex()
        {
            var defaultDriveMock = new Mock<ICacheDrive>();
            defaultDriveMock.Setup(_ => _.Contains(It.IsAny<string>())).Returns(false);

            var defaultPolicyMock = new Mock<IExpirePolicy>();
            defaultPolicyMock.Setup(_ => _.Validate(It.IsAny<CacheEntry<int>>())).Returns(true);

            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object);
            var result = core.Get(new[] { "key" }, "identity", new KeyLengthSimplexTranslator());

            Assert.Equal(new[] { 3 }, result);
        }


        /// <summary>
        /// キー、トランスレータを指定し、キャッシュを取得します。
        /// </summary>
        [Fact]
        public void Fetch_Bulk_Vectorized()
        {
            var defaultDriveMock = new Mock<ICacheDrive>();
            defaultDriveMock.Setup(_ => _.Contains(It.IsAny<string>())).Returns(false);

            var defaultPolicyMock = new Mock<IExpirePolicy>();
            defaultPolicyMock.Setup(_ => _.Validate(It.IsAny<CacheEntry<int>>())).Returns(true);

            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object);
            var result = core.Get(new[] { "key" }, "identity", new KeyLengthVectorizedTranslator());

            Assert.Equal(new[] { 3 }, result);
        }


        /// <summary>
        /// キー、トランスレータを指定し、キャッシュを取得します。
        /// </summary>
        [Fact]
        public void Fetch_Bulk_Optimized()
        {
            var defaultDriveMock = new Mock<ICacheDrive>();
            defaultDriveMock.Setup(_ => _.Contains(It.IsAny<string>())).Returns(false);

            var defaultPolicyMock = new Mock<IExpirePolicy>();
            defaultPolicyMock.Setup(_ => _.Validate(It.IsAny<CacheEntry<int>>())).Returns(true);

            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object);
            var result = core.Get(new[] { "key" }, "identity", new KeyLengthOptimizedTranslator());

            Assert.Equal(new[] { 3 }, result);
        }

        /// <summary>
        /// キー、トランスレータを指定し、キャッシュを取得します。
        /// </summary>
        [Fact]
        public void Fetch_Bulk_IdenticalSimplex()
        {
            var defaultDriveMock = new Mock<ICacheDrive>();
            defaultDriveMock.Setup(_ => _.Contains(It.IsAny<string>())).Returns(false);

            var defaultPolicyMock = new Mock<IExpirePolicy>();
            defaultPolicyMock.Setup(_ => _.Validate(It.IsAny<CacheEntry<int>>())).Returns(true);

            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object);
            var result = core.Get(new[] { "key" }, new KeyLengthIdenticalSimplexTranslator("identity"));

            Assert.Equal(new[] { 3 }, result);
        }

        /// <summary>
        /// キー、トランスレータを指定し、キャッシュを取得します。
        /// </summary>
        [Fact]
        public void Fetch_Bulk_IdenticalVectorized()
        {
            var defaultDriveMock = new Mock<ICacheDrive>();
            defaultDriveMock.Setup(_ => _.Contains(It.IsAny<string>())).Returns(false);

            var defaultPolicyMock = new Mock<IExpirePolicy>();
            defaultPolicyMock.Setup(_ => _.Validate(It.IsAny<CacheEntry<int>>())).Returns(true);

            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object);
            var result = core.Get(new[] { "key" }, new KeyLengthIdenticalVectorizedTranslator("identity"));

            Assert.Equal(new[] { 3 }, result);
        }

        /// <summary>
        /// キー、トランスレータを指定し、キャッシュを取得します。
        /// </summary>
        [Fact]
        public void Fetch_Bulk_IdenticalOptimized()
        {
            var defaultDriveMock = new Mock<ICacheDrive>();
            defaultDriveMock.Setup(_ => _.Contains(It.IsAny<string>())).Returns(false);

            var defaultPolicyMock = new Mock<IExpirePolicy>();
            defaultPolicyMock.Setup(_ => _.Validate(It.IsAny<CacheEntry<int>>())).Returns(true);

            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object);
            var result = core.Get(new[] { "key" }, new KeyLengthIdenticalOptimizedTranslator("identity"));

            Assert.Equal(new[] { 3 }, result);
        }

        //Using Drive Test

        private void UseUserDefinedCacheDriveMapping_Assert(int result, Mock<ICacheDrive> mappedDriveMock, Mock<ICacheDrive> defaultDriveMock)
        {
            Assert.Equal(1234567, result);
            mappedDriveMock.VerifyAll();
            defaultDriveMock.Verify(_ => _.Contains("one" + "identity"), Times.Never());
            defaultDriveMock.Verify(_ => _.Get<int>("one" + "identity"), Times.Never());
        }

        private void UseUserDefinedCacheDriveMapping_Assert(int[] result, Mock<ICacheDrive> mappedDriveMock, Mock<ICacheDrive> defaultDriveMock)
        {
            Assert.Equal(new[] { 1234567 }, result);
            mappedDriveMock.VerifyAll();
            defaultDriveMock.Verify(_ => _.Contains("one" + "identity"), Times.Never());
            defaultDriveMock.Verify(_ => _.Get<int>("one" + "identity"), Times.Never());
        }

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

        private Mock<IExpirePolicy> UseUserDefinedCacheDriveMapping_CustomPolicyMock()
        {
            var customPolicy = new Mock<IExpirePolicy>();
            customPolicy.Setup(_ => _.Validate(It.IsAny<CacheEntry<int>>())).Returns(true);

            return customPolicy;
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


            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { typeof(KeyLengthSimplexTranslator).FullName, mappedDriveMock.Object } } };
            var result = core.Get("one", "identity", new KeyLengthSimplexTranslator());

            UseUserDefinedCacheDriveMapping_Assert(result, mappedDriveMock, defaultDriveMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheCore.CacheDriveMapping" />に定義されている場合はそれを使用する
        /// </summary>
        [Fact]
        public void UseUserDefinedCacheDriveMapping_SimplexWithCustomPolicy()
        {
            var mappedDriveMock = UseUserDefinedCacheDriveMapping_MappedDriveMock();
            var defaultDriveMock = UseUserDefinedCacheDriveMapping_DefaultDriveMock();
            var defaultPolicyMock = UseUserDefinedCacheDriveMapping_DefaultPolicyMock();
            var customPolicyMock = UseUserDefinedCacheDriveMapping_CustomPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { typeof(KeyLengthSimplexTranslator).FullName, mappedDriveMock.Object } } };
            var result = core.Get("one", "identity", new KeyLengthSimplexTranslator(), customPolicyMock.Object); //ドライブの定義はポリシーに依存していないことを確認

            UseUserDefinedCacheDriveMapping_Assert(result, mappedDriveMock, defaultDriveMock);
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

            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { typeof(KeyLengthVectorizedTranslator).FullName, mappedDriveMock.Object } } };
            var result = core.Get("one", "identity", new KeyLengthVectorizedTranslator());

            UseUserDefinedCacheDriveMapping_Assert(result, mappedDriveMock, defaultDriveMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheCore.CacheDriveMapping" />に定義されている場合はそれを使用する
        /// </summary>
        [Fact]
        public void UseUserDefinedCacheDriveMapping_VectorizedWithCustomPolicy()
        {
            var mappedDriveMock = UseUserDefinedCacheDriveMapping_MappedDriveMock();
            var defaultDriveMock = UseUserDefinedCacheDriveMapping_DefaultDriveMock();
            var defaultPolicyMock = UseUserDefinedCacheDriveMapping_DefaultPolicyMock();
            var customPolicyMock = UseUserDefinedCacheDriveMapping_CustomPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { typeof(KeyLengthVectorizedTranslator).FullName, mappedDriveMock.Object } } };
            var result = core.Get("one", "identity", new KeyLengthVectorizedTranslator(), customPolicyMock.Object); //ドライブの定義はポリシーに依存していないことを確認

            UseUserDefinedCacheDriveMapping_Assert(result, mappedDriveMock, defaultDriveMock);
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


            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { typeof(KeyLengthOptimizedTranslator).FullName, mappedDriveMock.Object } } };
            var result = core.Get("one", "identity", new KeyLengthOptimizedTranslator());

            UseUserDefinedCacheDriveMapping_Assert(result, mappedDriveMock, defaultDriveMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheCore.CacheDriveMapping" />に定義されている場合はそれを使用する
        /// </summary>
        [Fact]
        public void UseUserDefinedCacheDriveMapping_OptimizedWithCustomPolicy()
        {
            var mappedDriveMock = UseUserDefinedCacheDriveMapping_MappedDriveMock();
            var defaultDriveMock = UseUserDefinedCacheDriveMapping_DefaultDriveMock();
            var defaultPolicyMock = UseUserDefinedCacheDriveMapping_DefaultPolicyMock();
            var customPolicyMock = UseUserDefinedCacheDriveMapping_CustomPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { typeof(KeyLengthOptimizedTranslator).FullName, mappedDriveMock.Object } } };
            var result = core.Get("one", "identity", new KeyLengthOptimizedTranslator(), customPolicyMock.Object); //ドライブの定義はポリシーに依存していないことを確認

            UseUserDefinedCacheDriveMapping_Assert(result, mappedDriveMock, defaultDriveMock);
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


            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { typeof(KeyLengthIdenticalSimplexTranslator).FullName, mappedDriveMock.Object } } };
            var result = core.Get("one", new KeyLengthIdenticalSimplexTranslator("identity"));

            UseUserDefinedCacheDriveMapping_Assert(result, mappedDriveMock, defaultDriveMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheCore.CacheDriveMapping" />に定義されている場合はそれを使用する
        /// </summary>
        [Fact]
        public void UseUserDefinedCacheDriveMapping_IdenticalSimplexWithCustomPolicy()
        {
            var mappedDriveMock = UseUserDefinedCacheDriveMapping_MappedDriveMock();
            var defaultDriveMock = UseUserDefinedCacheDriveMapping_DefaultDriveMock();
            var defaultPolicyMock = UseUserDefinedCacheDriveMapping_DefaultPolicyMock();
            var customPolicyMock = UseUserDefinedCacheDriveMapping_CustomPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { typeof(KeyLengthIdenticalSimplexTranslator).FullName, mappedDriveMock.Object } } };
            var result = core.Get("one", new KeyLengthIdenticalSimplexTranslator("identity"), customPolicyMock.Object); //ドライブの定義はポリシーに依存していないことを確認

            UseUserDefinedCacheDriveMapping_Assert(result, mappedDriveMock, defaultDriveMock);
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


            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { typeof(KeyLengthIdenticalVectorizedTranslator).FullName, mappedDriveMock.Object } } };
            var result = core.Get("one", new KeyLengthIdenticalVectorizedTranslator("identity"));

            UseUserDefinedCacheDriveMapping_Assert(result, mappedDriveMock, defaultDriveMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheCore.CacheDriveMapping" />に定義されている場合はそれを使用する
        /// </summary>
        [Fact]
        public void UseUserDefinedCacheDriveMapping_IdenticalVectorizedWithCustomPolicy()
        {
            var mappedDriveMock = UseUserDefinedCacheDriveMapping_MappedDriveMock();
            var defaultDriveMock = UseUserDefinedCacheDriveMapping_DefaultDriveMock();
            var defaultPolicyMock = UseUserDefinedCacheDriveMapping_DefaultPolicyMock();
            var customPolicyMock = UseUserDefinedCacheDriveMapping_CustomPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { typeof(KeyLengthIdenticalVectorizedTranslator).FullName, mappedDriveMock.Object } } };
            var result = core.Get("one", new KeyLengthIdenticalVectorizedTranslator("identity"), customPolicyMock.Object); //ドライブの定義はポリシーに依存していないことを確認

            UseUserDefinedCacheDriveMapping_Assert(result, mappedDriveMock, defaultDriveMock);
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


            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { typeof(KeyLengthIdenticalOptimizedTranslator).FullName, mappedDriveMock.Object } } };
            var result = core.Get("one", new KeyLengthIdenticalOptimizedTranslator("identity"));

            UseUserDefinedCacheDriveMapping_Assert(result, mappedDriveMock, defaultDriveMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheCore.CacheDriveMapping" />に定義されている場合はそれを使用する
        /// </summary>
        [Fact]
        public void UseUserDefinedCacheDriveMapping_IdenticalOptimizedWithCustomPolicy()
        {
            var mappedDriveMock = UseUserDefinedCacheDriveMapping_MappedDriveMock();
            var defaultDriveMock = UseUserDefinedCacheDriveMapping_DefaultDriveMock();
            var defaultPolicyMock = UseUserDefinedCacheDriveMapping_DefaultPolicyMock();
            var customPolicyMock = UseUserDefinedCacheDriveMapping_CustomPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { typeof(KeyLengthIdenticalOptimizedTranslator).FullName, mappedDriveMock.Object } } };
            var result = core.Get("one", new KeyLengthIdenticalOptimizedTranslator("identity"), customPolicyMock.Object); //ドライブの定義はポリシーに依存していないことを確認

            UseUserDefinedCacheDriveMapping_Assert(result, mappedDriveMock, defaultDriveMock);
        }

        //

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheCore.CacheDriveMapping" />に定義されている場合はそれを使用する
        /// </summary>
        [Fact]
        public void UseUserDefinedCacheDriveMapping_Bulk_Simplex()
        {
            var mappedDriveMock = UseUserDefinedCacheDriveMapping_MappedDriveMock();
            var defaultDriveMock = UseUserDefinedCacheDriveMapping_DefaultDriveMock();
            var defaultPolicyMock = UseUserDefinedCacheDriveMapping_DefaultPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { typeof(KeyLengthSimplexTranslator).FullName, mappedDriveMock.Object } } };
            var result = core.Get(new[] { "one" }, "identity", new KeyLengthSimplexTranslator());

            UseUserDefinedCacheDriveMapping_Assert(result, mappedDriveMock, defaultDriveMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheCore.CacheDriveMapping" />に定義されている場合はそれを使用する
        /// </summary>
        [Fact]
        public void UseUserDefinedCacheDriveMapping_Bulk_SimplexWithCustomPolicy()
        {
            var mappedDriveMock = UseUserDefinedCacheDriveMapping_MappedDriveMock();
            var defaultDriveMock = UseUserDefinedCacheDriveMapping_DefaultDriveMock();
            var defaultPolicyMock = UseUserDefinedCacheDriveMapping_DefaultPolicyMock();
            var customPolicyMock = UseUserDefinedCacheDriveMapping_CustomPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { typeof(KeyLengthSimplexTranslator).FullName, mappedDriveMock.Object } } };
            var result = core.Get(new[] { "one" }, "identity", new KeyLengthSimplexTranslator(), customPolicyMock.Object); //ドライブの定義はポリシーに依存していないことを確認

            UseUserDefinedCacheDriveMapping_Assert(result, mappedDriveMock, defaultDriveMock);
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


            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { typeof(KeyLengthVectorizedTranslator).FullName, mappedDriveMock.Object } } };
            var result = core.Get(new[] { "one" }, "identity", new KeyLengthVectorizedTranslator());

            UseUserDefinedCacheDriveMapping_Assert(result, mappedDriveMock, defaultDriveMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheCore.CacheDriveMapping" />に定義されている場合はそれを使用する
        /// </summary>
        [Fact]
        public void UseUserDefinedCacheDriveMapping_Bulk_VectorizedWithCustomPolicy()
        {
            var mappedDriveMock = UseUserDefinedCacheDriveMapping_MappedDriveMock();
            var defaultDriveMock = UseUserDefinedCacheDriveMapping_DefaultDriveMock();
            var defaultPolicyMock = UseUserDefinedCacheDriveMapping_DefaultPolicyMock();
            var customPolicyMock = UseUserDefinedCacheDriveMapping_CustomPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { typeof(KeyLengthVectorizedTranslator).FullName, mappedDriveMock.Object } } };
            var result = core.Get(new[] { "one" }, "identity", new KeyLengthVectorizedTranslator(), customPolicyMock.Object); //ドライブの定義はポリシーに依存していないことを確認

            UseUserDefinedCacheDriveMapping_Assert(result, mappedDriveMock, defaultDriveMock);
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


            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { typeof(KeyLengthOptimizedTranslator).FullName, mappedDriveMock.Object } } };
            var result = core.Get(new[] { "one" }, "identity", new KeyLengthOptimizedTranslator());

            UseUserDefinedCacheDriveMapping_Assert(result, mappedDriveMock, defaultDriveMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheCore.CacheDriveMapping" />に定義されている場合はそれを使用する
        /// </summary>
        [Fact]
        public void UseUserDefinedCacheDriveMapping_Bulk_OptimizedWithCustomPolicy()
        {
            var mappedDriveMock = UseUserDefinedCacheDriveMapping_MappedDriveMock();
            var defaultDriveMock = UseUserDefinedCacheDriveMapping_DefaultDriveMock();
            var defaultPolicyMock = UseUserDefinedCacheDriveMapping_DefaultPolicyMock();
            var customPolicyMock = UseUserDefinedCacheDriveMapping_CustomPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { typeof(KeyLengthOptimizedTranslator).FullName, mappedDriveMock.Object } } };
            var result = core.Get(new[] { "one" }, "identity", new KeyLengthOptimizedTranslator(), customPolicyMock.Object); //ドライブの定義はポリシーに依存していないことを確認

            UseUserDefinedCacheDriveMapping_Assert(result, mappedDriveMock, defaultDriveMock);
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


            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { typeof(KeyLengthIdenticalSimplexTranslator).FullName, mappedDriveMock.Object } } };
            var result = core.Get(new[] { "one" }, new KeyLengthIdenticalSimplexTranslator("identity"));

            UseUserDefinedCacheDriveMapping_Assert(result, mappedDriveMock, defaultDriveMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheCore.CacheDriveMapping" />に定義されている場合はそれを使用する
        /// </summary>
        [Fact]
        public void UseUserDefinedCacheDriveMapping_Bulk_IdenticalSimplexWithCustomPolicy()
        {
            var mappedDriveMock = UseUserDefinedCacheDriveMapping_MappedDriveMock();
            var defaultDriveMock = UseUserDefinedCacheDriveMapping_DefaultDriveMock();
            var defaultPolicyMock = UseUserDefinedCacheDriveMapping_DefaultPolicyMock();
            var customPolicyMock = UseUserDefinedCacheDriveMapping_CustomPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { typeof(KeyLengthIdenticalSimplexTranslator).FullName, mappedDriveMock.Object } } };
            var result = core.Get(new[] { "one" }, new KeyLengthIdenticalSimplexTranslator("identity"), customPolicyMock.Object); //ドライブの定義はポリシーに依存していないことを確認

            UseUserDefinedCacheDriveMapping_Assert(result, mappedDriveMock, defaultDriveMock);
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


            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { typeof(KeyLengthIdenticalVectorizedTranslator).FullName, mappedDriveMock.Object } } };
            var result = core.Get(new[] { "one" }, new KeyLengthIdenticalVectorizedTranslator("identity"));

            UseUserDefinedCacheDriveMapping_Assert(result, mappedDriveMock, defaultDriveMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheCore.CacheDriveMapping" />に定義されている場合はそれを使用する
        /// </summary>
        [Fact]
        public void UseUserDefinedCacheDriveMapping_Bulk_IdenticalVectorizedWithCustomPolicy()
        {
            var mappedDriveMock = UseUserDefinedCacheDriveMapping_MappedDriveMock();
            var defaultDriveMock = UseUserDefinedCacheDriveMapping_DefaultDriveMock();
            var defaultPolicyMock = UseUserDefinedCacheDriveMapping_DefaultPolicyMock();
            var customPolicyMock = UseUserDefinedCacheDriveMapping_CustomPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { typeof(KeyLengthIdenticalVectorizedTranslator).FullName, mappedDriveMock.Object } } };
            var result = core.Get(new[] { "one" }, new KeyLengthIdenticalVectorizedTranslator("identity"), customPolicyMock.Object); //ドライブの定義はポリシーに依存していないことを確認

            UseUserDefinedCacheDriveMapping_Assert(result, mappedDriveMock, defaultDriveMock);
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


            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { typeof(KeyLengthIdenticalOptimizedTranslator).FullName, mappedDriveMock.Object } } };
            var result = core.Get(new[] { "one" }, new KeyLengthIdenticalOptimizedTranslator("identity"));

            UseUserDefinedCacheDriveMapping_Assert(result, mappedDriveMock, defaultDriveMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheCore.CacheDriveMapping" />に定義されている場合はそれを使用する
        /// </summary>
        [Fact]
        public void UseUserDefinedCacheDriveMapping_Bulk_IdenticalOptimizedWithCustomPolicy()
        {
            var mappedDriveMock = UseUserDefinedCacheDriveMapping_MappedDriveMock();
            var defaultDriveMock = UseUserDefinedCacheDriveMapping_DefaultDriveMock();
            var defaultPolicyMock = UseUserDefinedCacheDriveMapping_DefaultPolicyMock();
            var customPolicyMock = UseUserDefinedCacheDriveMapping_CustomPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { typeof(KeyLengthIdenticalOptimizedTranslator).FullName, mappedDriveMock.Object } } };
            var result = core.Get(new[] { "one" }, new KeyLengthIdenticalOptimizedTranslator("identity"), customPolicyMock.Object); //ドライブの定義はポリシーに依存していないことを確認

            UseUserDefinedCacheDriveMapping_Assert(result, mappedDriveMock, defaultDriveMock);
        }

        //

        private Mock<ICacheDrive> UseDefaultDriveWhenDriveMappingNotDefined_MappedDriveMock()
        {
            var invalidDriveMock = new Mock<ICacheDrive>();
            invalidDriveMock.Setup(_ => _.Contains("one" + "identity")).Returns(true);
            invalidDriveMock.Setup(_ => _.Get<int>("one" + "identity")).Returns(new CacheEntry<int>() { Key = "one", Value = 7654321 });
            return invalidDriveMock;
        }

        private Mock<ICacheDrive> UseDefaultDriveWhenDriveMappingNotDefined_DefaultDriveMock()
        {
            var defaultDriveMock = new Mock<ICacheDrive>();
            defaultDriveMock.Setup(_ => _.Contains("one" + "identity")).Returns(true);
            defaultDriveMock.Setup(_ => _.Get<int>("one" + "identity")).Returns(new CacheEntry<int>() { Key = "one", Value = 1234567 });
            return defaultDriveMock;
        }

        private Mock<IExpirePolicy> UseDefaultDriveWhenDriveMappingNotDefined_DefaultPolicyMock()
        {
            var policyMock = new Mock<IExpirePolicy>();
            policyMock.Setup(_ => _.Validate(It.IsAny<CacheEntry<int>>())).Returns(true);
            return policyMock;
        }

        private Mock<IExpirePolicy> UseDefaultDriveWhenDriveMappingNotDefined_CustomPolicyMock()
        {
            var customPolicyMock = new Mock<IExpirePolicy>();
            customPolicyMock.Setup(_ => _.Validate(It.IsAny<CacheEntry<int>>())).Returns(true);
            return customPolicyMock;
        }

        public void UseDefaultDriveWhenDriveMappingNotDefined_Assert(int result, Mock<ICacheDrive> defaultDriveMock, Mock<ICacheDrive> mappedDriveMock)
        {
            Assert.Equal(1234567, result);
            defaultDriveMock.VerifyAll();
            mappedDriveMock.Verify(_ => _.Contains("one" + "identity"), Times.Never());
            mappedDriveMock.Verify(_ => _.Get<int>("one" + "identity"), Times.Never());
        }

        public void UseDefaultDriveWhenDriveMappingNotDefined_Assert(int[] result, Mock<ICacheDrive> defaultDriveMock, Mock<ICacheDrive> mappedDriveMock)
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
            var policyMock = UseDefaultDriveWhenDriveMappingNotDefined_DefaultPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, policyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { "invalid", mappedDriveMock.Object } } };
            var result = core.Get("one", "identity", new KeyLengthSimplexTranslator());

            UseDefaultDriveWhenDriveMappingNotDefined_Assert(result, defaultDriveMock, mappedDriveMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheCore.CacheDriveMapping" />に定義されていない場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// </summary>
        [Fact]
        public void UseDefaultDriveWhenDriveMappingNotDefined_SimplexWithCustomPolicy()
        {
            var mappedDriveMock = UseDefaultDriveWhenDriveMappingNotDefined_MappedDriveMock();
            var defaultDriveMock = UseDefaultDriveWhenDriveMappingNotDefined_DefaultDriveMock();
            var policyMock = UseDefaultDriveWhenDriveMappingNotDefined_DefaultPolicyMock();
            var customPolicyMock = UseDefaultDriveWhenDriveMappingNotDefined_CustomPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, policyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { "invalid", mappedDriveMock.Object } } };
            var result = core.Get("one", "identity", new KeyLengthSimplexTranslator(), customPolicyMock.Object); //ドライブの定義はポリシーに依存していないことを確認

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
            var policyMock = UseDefaultDriveWhenDriveMappingNotDefined_DefaultPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, policyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { "invalid", mappedDriveMock.Object } } };
            var result = core.Get("one", "identity", new KeyLengthVectorizedTranslator());

            UseDefaultDriveWhenDriveMappingNotDefined_Assert(result, defaultDriveMock, mappedDriveMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheCore.CacheDriveMapping" />に定義されていない場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// </summary>
        [Fact]
        public void UseDefaultDriveWhenDriveMappingNotDefined_VectorizedWithCustomPolicy()
        {
            var mappedDriveMock = UseDefaultDriveWhenDriveMappingNotDefined_MappedDriveMock();
            var defaultDriveMock = UseDefaultDriveWhenDriveMappingNotDefined_DefaultDriveMock();
            var policyMock = UseDefaultDriveWhenDriveMappingNotDefined_DefaultPolicyMock();
            var customPolicyMock = UseDefaultDriveWhenDriveMappingNotDefined_CustomPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, policyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { "invalid", mappedDriveMock.Object } } };
            var result = core.Get("one", "identity", new KeyLengthVectorizedTranslator(), customPolicyMock.Object); //ドライブの定義はポリシーに依存していないことを確認

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
            var policyMock = UseDefaultDriveWhenDriveMappingNotDefined_DefaultPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, policyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { "invalid", mappedDriveMock.Object } } };
            var result = core.Get("one", "identity", new KeyLengthOptimizedTranslator());

            UseDefaultDriveWhenDriveMappingNotDefined_Assert(result, defaultDriveMock, mappedDriveMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheCore.CacheDriveMapping" />に定義されていない場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// </summary>
        [Fact]
        public void UseDefaultDriveWhenDriveMappingNotDefined_OptimizedWithCustomPolicy()
        {
            var mappedDriveMock = UseDefaultDriveWhenDriveMappingNotDefined_MappedDriveMock();
            var defaultDriveMock = UseDefaultDriveWhenDriveMappingNotDefined_DefaultDriveMock();
            var policyMock = UseDefaultDriveWhenDriveMappingNotDefined_DefaultPolicyMock();
            var customPolicyMock = UseDefaultDriveWhenDriveMappingNotDefined_CustomPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, policyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { "invalid", mappedDriveMock.Object } } };
            var result = core.Get("one", "identity", new KeyLengthOptimizedTranslator(), customPolicyMock.Object); //ドライブの定義はポリシーに依存していないことを確認

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
            var policyMock = UseDefaultDriveWhenDriveMappingNotDefined_DefaultPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, policyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { "invalid", mappedDriveMock.Object } } };
            var result = core.Get("one", new KeyLengthIdenticalSimplexTranslator("identity"));

            UseDefaultDriveWhenDriveMappingNotDefined_Assert(result, defaultDriveMock, mappedDriveMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheCore.CacheDriveMapping" />に定義されていない場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// </summary>
        [Fact]
        public void UseDefaultDriveWhenDriveMappingNotDefined_IdenticalSimplexWithCustomPolicy()
        {
            var mappedDriveMock = UseDefaultDriveWhenDriveMappingNotDefined_MappedDriveMock();
            var defaultDriveMock = UseDefaultDriveWhenDriveMappingNotDefined_DefaultDriveMock();
            var policyMock = UseDefaultDriveWhenDriveMappingNotDefined_DefaultPolicyMock();
            var customPolicyMock = UseDefaultDriveWhenDriveMappingNotDefined_CustomPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, policyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { "invalid", mappedDriveMock.Object } } };
            var result = core.Get("one", new KeyLengthIdenticalSimplexTranslator("identity"), customPolicyMock.Object); //ドライブの定義はポリシーに依存していないことを確認

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
            var policyMock = UseDefaultDriveWhenDriveMappingNotDefined_DefaultPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, policyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { "invalid", mappedDriveMock.Object } } };
            var result = core.Get("one", new KeyLengthIdenticalVectorizedTranslator("identity"));

            UseDefaultDriveWhenDriveMappingNotDefined_Assert(result, defaultDriveMock, mappedDriveMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheCore.CacheDriveMapping" />に定義されていない場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// </summary>
        [Fact]
        public void UseDefaultDriveWhenDriveMappingNotDefined_IdenticalVectorizedWithCustomPolicy()
        {
            var mappedDriveMock = UseDefaultDriveWhenDriveMappingNotDefined_MappedDriveMock();
            var defaultDriveMock = UseDefaultDriveWhenDriveMappingNotDefined_DefaultDriveMock();
            var policyMock = UseDefaultDriveWhenDriveMappingNotDefined_DefaultPolicyMock();
            var customPolicyMock = UseDefaultDriveWhenDriveMappingNotDefined_CustomPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, policyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { "invalid", mappedDriveMock.Object } } };
            var result = core.Get("one", new KeyLengthIdenticalVectorizedTranslator("identity"), customPolicyMock.Object); //ドライブの定義はポリシーに依存していないことを確認

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
            var policyMock = UseDefaultDriveWhenDriveMappingNotDefined_DefaultPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, policyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { "invalid", mappedDriveMock.Object } } };
            var result = core.Get("one", new KeyLengthIdenticalOptimizedTranslator("identity"));

            UseDefaultDriveWhenDriveMappingNotDefined_Assert(result, defaultDriveMock, mappedDriveMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheCore.CacheDriveMapping" />に定義されていない場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// </summary>
        [Fact]
        public void UseDefaultDriveWhenDriveMappingNotDefined_IdenticalOptimizedWithCustomPolicy()
        {
            var mappedDriveMock = UseDefaultDriveWhenDriveMappingNotDefined_MappedDriveMock();
            var defaultDriveMock = UseDefaultDriveWhenDriveMappingNotDefined_DefaultDriveMock();
            var policyMock = UseDefaultDriveWhenDriveMappingNotDefined_DefaultPolicyMock();
            var customPolicyMock = UseDefaultDriveWhenDriveMappingNotDefined_CustomPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, policyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { "invalid", mappedDriveMock.Object } } };
            var result = core.Get("one", new KeyLengthIdenticalOptimizedTranslator("identity"), customPolicyMock.Object); //ドライブの定義はポリシーに依存していないことを確認

            UseDefaultDriveWhenDriveMappingNotDefined_Assert(result, defaultDriveMock, mappedDriveMock);
        }

        //

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheCore.CacheDriveMapping" />に定義されていない場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// </summary>
        [Fact]
        public void UseDefaultDriveWhenDriveMappingNotDefined_Bulk_Simplex()
        {
            var mappedDriveMock = UseDefaultDriveWhenDriveMappingNotDefined_MappedDriveMock();
            var defaultDriveMock = UseDefaultDriveWhenDriveMappingNotDefined_DefaultDriveMock();
            var policyMock = UseDefaultDriveWhenDriveMappingNotDefined_DefaultPolicyMock();

            var core = new CacheCore(defaultDriveMock.Object, policyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { "invalid", mappedDriveMock.Object } } };
            var result = core.Get(new[] { "one" }, "identity", new KeyLengthSimplexTranslator());

            UseDefaultDriveWhenDriveMappingNotDefined_Assert(result, defaultDriveMock, mappedDriveMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheCore.CacheDriveMapping" />に定義されていない場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// </summary>
        [Fact]
        public void UseDefaultDriveWhenDriveMappingNotDefined_Bulk_SimplexWithCustomPolicy()
        {
            var mappedDriveMock = UseDefaultDriveWhenDriveMappingNotDefined_MappedDriveMock();
            var defaultDriveMock = UseDefaultDriveWhenDriveMappingNotDefined_DefaultDriveMock();
            var policyMock = UseDefaultDriveWhenDriveMappingNotDefined_DefaultPolicyMock();
            var customPolicyMock = UseDefaultDriveWhenDriveMappingNotDefined_CustomPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, policyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { "invalid", mappedDriveMock.Object } } };
            var result = core.Get(new[] { "one" }, "identity", new KeyLengthSimplexTranslator(), customPolicyMock.Object); //ドライブの定義はポリシーに依存していないことを確認

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
            var policyMock = UseDefaultDriveWhenDriveMappingNotDefined_DefaultPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, policyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { "invalid", mappedDriveMock.Object } } };
            var result = core.Get(new[] { "one" }, "identity", new KeyLengthVectorizedTranslator());

            UseDefaultDriveWhenDriveMappingNotDefined_Assert(result, defaultDriveMock, mappedDriveMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheCore.CacheDriveMapping" />に定義されていない場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// </summary>
        [Fact]
        public void UseDefaultDriveWhenDriveMappingNotDefined_Bulk_VectorizedWithCustomPolicy()
        {
            var mappedDriveMock = UseDefaultDriveWhenDriveMappingNotDefined_MappedDriveMock();
            var defaultDriveMock = UseDefaultDriveWhenDriveMappingNotDefined_DefaultDriveMock();
            var policyMock = UseDefaultDriveWhenDriveMappingNotDefined_DefaultPolicyMock();
            var customPolicyMock = UseDefaultDriveWhenDriveMappingNotDefined_CustomPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, policyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { "invalid", mappedDriveMock.Object } } };
            var result = core.Get(new[] { "one" }, "identity", new KeyLengthVectorizedTranslator(), customPolicyMock.Object); //ドライブの定義はポリシーに依存していないことを確認

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
            var policyMock = UseDefaultDriveWhenDriveMappingNotDefined_DefaultPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, policyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { "invalid", mappedDriveMock.Object } } };
            var result = core.Get(new[] { "one" }, "identity", new KeyLengthOptimizedTranslator());

            UseDefaultDriveWhenDriveMappingNotDefined_Assert(result, defaultDriveMock, mappedDriveMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheCore.CacheDriveMapping" />に定義されていない場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// </summary>
        [Fact]
        public void UseDefaultDriveWhenDriveMappingNotDefined_Bulk_OptimizedWithCustomPolicy()
        {
            var mappedDriveMock = UseDefaultDriveWhenDriveMappingNotDefined_MappedDriveMock();
            var defaultDriveMock = UseDefaultDriveWhenDriveMappingNotDefined_DefaultDriveMock();
            var policyMock = UseDefaultDriveWhenDriveMappingNotDefined_DefaultPolicyMock();
            var customPolicyMock = UseDefaultDriveWhenDriveMappingNotDefined_CustomPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, policyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { "invalid", mappedDriveMock.Object } } };
            var result = core.Get(new[] { "one" }, "identity", new KeyLengthOptimizedTranslator(), customPolicyMock.Object); //ドライブの定義はポリシーに依存していないことを確認

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
            var policyMock = UseDefaultDriveWhenDriveMappingNotDefined_DefaultPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, policyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { "invalid", mappedDriveMock.Object } } };
            var result = core.Get(new[] { "one" }, new KeyLengthIdenticalSimplexTranslator("identity"));

            UseDefaultDriveWhenDriveMappingNotDefined_Assert(result, defaultDriveMock, mappedDriveMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheCore.CacheDriveMapping" />に定義されていない場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// </summary>
        [Fact]
        public void UseDefaultDriveWhenDriveMappingNotDefined_Bulk_IdenticalSimplexWithCustomPolicy()
        {
            var mappedDriveMock = UseDefaultDriveWhenDriveMappingNotDefined_MappedDriveMock();
            var defaultDriveMock = UseDefaultDriveWhenDriveMappingNotDefined_DefaultDriveMock();
            var policyMock = UseDefaultDriveWhenDriveMappingNotDefined_DefaultPolicyMock();
            var customPolicyMock = UseDefaultDriveWhenDriveMappingNotDefined_CustomPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, policyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { "invalid", mappedDriveMock.Object } } };
            var result = core.Get(new[] { "one" }, new KeyLengthIdenticalSimplexTranslator("identity"), customPolicyMock.Object); //ドライブの定義はポリシーに依存していないことを確認

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
            var policyMock = UseDefaultDriveWhenDriveMappingNotDefined_DefaultPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, policyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { "invalid", mappedDriveMock.Object } } };
            var result = core.Get(new[] { "one" }, new KeyLengthIdenticalVectorizedTranslator("identity"));

            UseDefaultDriveWhenDriveMappingNotDefined_Assert(result, defaultDriveMock, mappedDriveMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheCore.CacheDriveMapping" />に定義されていない場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// </summary>
        [Fact]
        public void UseDefaultDriveWhenDriveMappingNotDefined_Bulk_IdenticalVectorizedWithCustomPolicy()
        {
            var mappedDriveMock = UseDefaultDriveWhenDriveMappingNotDefined_MappedDriveMock();
            var defaultDriveMock = UseDefaultDriveWhenDriveMappingNotDefined_DefaultDriveMock();
            var policyMock = UseDefaultDriveWhenDriveMappingNotDefined_DefaultPolicyMock();
            var customPolicyMock = UseDefaultDriveWhenDriveMappingNotDefined_CustomPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, policyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { "invalid", mappedDriveMock.Object } } };
            var result = core.Get(new[] { "one" }, new KeyLengthIdenticalVectorizedTranslator("identity"), customPolicyMock.Object); //ドライブの定義はポリシーに依存していないことを確認

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
            var policyMock = UseDefaultDriveWhenDriveMappingNotDefined_DefaultPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, policyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { "invalid", mappedDriveMock.Object } } };
            var result = core.Get(new[] { "one" }, new KeyLengthIdenticalOptimizedTranslator("identity"));

            UseDefaultDriveWhenDriveMappingNotDefined_Assert(result, defaultDriveMock, mappedDriveMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheCore.CacheDriveMapping" />に定義されていない場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// </summary>
        [Fact]
        public void UseDefaultDriveWhenDriveMappingNotDefined_Bulk_IdenticalOptimizedWithCustomPolicy()
        {
            var mappedDriveMock = UseDefaultDriveWhenDriveMappingNotDefined_MappedDriveMock();
            var defaultDriveMock = UseDefaultDriveWhenDriveMappingNotDefined_DefaultDriveMock();
            var policyMock = UseDefaultDriveWhenDriveMappingNotDefined_DefaultPolicyMock();
            var customPolicyMock = UseDefaultDriveWhenDriveMappingNotDefined_CustomPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, policyMock.Object) { CacheDriveMapping = new Dictionary<string, ICacheDrive> { { "invalid", mappedDriveMock.Object } } };
            var result = core.Get(new[] { "one" }, new KeyLengthIdenticalOptimizedTranslator("identity"), customPolicyMock.Object); //ドライブの定義はポリシーに依存していないことを確認

            UseDefaultDriveWhenDriveMappingNotDefined_Assert(result, defaultDriveMock, mappedDriveMock);
        }

        // Using Policy Test

        private Mock<ICacheDrive> UseUserDefinedPolicyMapping_DefaultDriveMock()
        {
            var driveMock = new Mock<ICacheDrive>();
            driveMock.Setup(_ => _.Contains("one" + "identity")).Returns(true);
            driveMock.Setup(_ => _.Get<int>("one" + "identity")).Returns(new CacheEntry<int> { Key = "one", Value = 1234567 });
            return driveMock;
        }

        private Mock<ICacheDrive> UseUserDefinedPolicyMapping_CustomDriveMock()
        {
            var customDriveMock = new Mock<ICacheDrive>();
            customDriveMock.Setup(_ => _.Contains("one" + "identity")).Returns(true);
            customDriveMock.Setup(_ => _.Get<int>("one" + "identity")).Returns(new CacheEntry<int>() { Key = "one", Value = 1234567 });
            return customDriveMock;
        }

        private Mock<IExpirePolicy> UseUserDefinedPolicyMapping_DefaultPolicyMock()
        {
            var defaultPolicyMock = new Mock<IExpirePolicy>();
            defaultPolicyMock.Setup(_ => _.Validate(It.IsAny<CacheEntry<int>>())).Returns(true);
            return defaultPolicyMock;
        }

        private Mock<IExpirePolicy> UseUserDefinedPolicyMapping_MappedPolicyMock()
        {
            var mappedPolicyMock = new Mock<IExpirePolicy>();
            mappedPolicyMock.Setup(_ => _.Validate(It.IsAny<CacheEntry<int>>())).Returns(false);
            return mappedPolicyMock;
        }

        private void UseUserDefinedPolicyMapping_Assert(int result, Mock<IExpirePolicy> defaultPolicyMock, Mock<IExpirePolicy> mappedPolicyMock)
        {
            Assert.Equal(3, result);
            defaultPolicyMock.Verify(_ => _.Validate(It.IsAny<CacheEntry<int>>()), Times.Never());
            mappedPolicyMock.VerifyAll();
        }

        private void UseUserDefinedPolicyMapping_Assert(int[] result, Mock<IExpirePolicy> defaultPolicyMock, Mock<IExpirePolicy> mappedPolicyMock)
        {
            Assert.Equal(new[] { 3 }, result);
            defaultPolicyMock.Verify(_ => _.Validate(It.IsAny<CacheEntry<int>>()), Times.Never());
            mappedPolicyMock.VerifyAll();
        }

        /// <summary>
        ///  <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されている場合はそれを使用します。
        /// </summary>
        [Fact]
        public void UseUserDefinedPolicyMapping_Simplex()
        {
            var defaultDriveMock = UseUserDefinedPolicyMapping_DefaultDriveMock();
            var defaultPolicyMock = UseUserDefinedPolicyMapping_DefaultPolicyMock();
            var mappedPolicyMock = UseUserDefinedPolicyMapping_MappedPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy> { { typeof(KeyLengthSimplexTranslator).FullName, mappedPolicyMock.Object } } };
            var result = core.Get("one", "identity", new KeyLengthSimplexTranslator());

            UseUserDefinedPolicyMapping_Assert(result, defaultPolicyMock, mappedPolicyMock);
        }

        /// <summary>
        ///  <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されている場合はそれを使用します。
        /// </summary>
        [Fact]
        public void UseUserDefinedPolicyMapping_SimplexWithCustomDrive()
        {
            var defaultDriveMock = UseUserDefinedPolicyMapping_DefaultDriveMock();
            var customDriveMock = UseUserDefinedPolicyMapping_CustomDriveMock();
            var defaultPolicyMock = UseUserDefinedPolicyMapping_DefaultPolicyMock();
            var mappedPolicyMock = UseUserDefinedPolicyMapping_MappedPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy> { { typeof(KeyLengthSimplexTranslator).FullName, mappedPolicyMock.Object } } };
            var result = core.Get("one", "identity", new KeyLengthSimplexTranslator(), customDriveMock.Object);

            UseUserDefinedPolicyMapping_Assert(result, defaultPolicyMock, mappedPolicyMock);
        }

        /// <summary>
        ///  <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されている場合はそれを使用します。
        /// </summary>
        [Fact]
        public void UseUserDefinedPolicyMapping_Vectorized()
        {
            var defaultDriveMock = UseUserDefinedPolicyMapping_DefaultDriveMock();
            var defaultPolicyMock = UseUserDefinedPolicyMapping_DefaultPolicyMock();
            var mappedPolicyMock = UseUserDefinedPolicyMapping_MappedPolicyMock();

            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy> { { typeof(KeyLengthVectorizedTranslator).FullName, mappedPolicyMock.Object } } };
            var result = core.Get("one", "identity", new KeyLengthVectorizedTranslator());

            UseUserDefinedPolicyMapping_Assert(result, defaultPolicyMock, mappedPolicyMock);
        }

        /// <summary>
        ///  <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されている場合はそれを使用します。
        /// </summary>
        [Fact]
        public void UseUserDefinedPolicyMapping_VectorizedWithCustomDrive()
        {
            var defaultDriveMock = UseUserDefinedPolicyMapping_DefaultDriveMock();
            var customDriveMock = UseUserDefinedPolicyMapping_CustomDriveMock();
            var defaultPolicyMock = UseUserDefinedPolicyMapping_DefaultPolicyMock();
            var mappedPolicyMock = UseUserDefinedPolicyMapping_MappedPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy> { { typeof(KeyLengthVectorizedTranslator).FullName, mappedPolicyMock.Object } } };
            var result = core.Get("one", "identity", new KeyLengthVectorizedTranslator(), customDriveMock.Object);

            UseUserDefinedPolicyMapping_Assert(result, defaultPolicyMock, mappedPolicyMock);
        }

        /// <summary>
        ///  <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されている場合はそれを使用します。
        /// </summary>
        [Fact]
        public void UseUserDefinedPolicyMapping_Optimized()
        {
            var defaultDriveMock = UseUserDefinedPolicyMapping_DefaultDriveMock();
            var defaultPolicyMock = UseUserDefinedPolicyMapping_DefaultPolicyMock();
            var mappedPolicyMock = UseUserDefinedPolicyMapping_MappedPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy> { { typeof(KeyLengthOptimizedTranslator).FullName, mappedPolicyMock.Object } } };
            var result = core.Get("one", "identity", new KeyLengthOptimizedTranslator());

            UseUserDefinedPolicyMapping_Assert(result, defaultPolicyMock, mappedPolicyMock);
        }

        /// <summary>
        ///  <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されている場合はそれを使用します。
        /// </summary>
        [Fact]
        public void UseUserDefinedPolicyMapping_OptimizedWithCustomDrive()
        {
            var defaultDriveMock = UseUserDefinedPolicyMapping_DefaultDriveMock();
            var customDriveMock = UseUserDefinedPolicyMapping_CustomDriveMock();
            var defaultPolicyMock = UseUserDefinedPolicyMapping_DefaultPolicyMock();
            var mappedPolicyMock = UseUserDefinedPolicyMapping_MappedPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy> { { typeof(KeyLengthOptimizedTranslator).FullName, mappedPolicyMock.Object } } };
            var result = core.Get("one", "identity", new KeyLengthOptimizedTranslator(), customDriveMock.Object);

            UseUserDefinedPolicyMapping_Assert(result, defaultPolicyMock, mappedPolicyMock);
        }

        /// <summary>
        ///  <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されている場合はそれを使用します。
        /// </summary>
        [Fact]
        public void UseUserDefinedPolicyMapping_IdenticalSimplex()
        {
            var defaultDriveMock = UseUserDefinedPolicyMapping_DefaultDriveMock();
            var defaultPolicyMock = UseUserDefinedPolicyMapping_DefaultPolicyMock();
            var mappedPolicyMock = UseUserDefinedPolicyMapping_MappedPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy> { { typeof(KeyLengthIdenticalSimplexTranslator).FullName, mappedPolicyMock.Object } } };
            var result = core.Get("one", new KeyLengthIdenticalSimplexTranslator("identity"));

            UseUserDefinedPolicyMapping_Assert(result, defaultPolicyMock, mappedPolicyMock);
        }

        /// <summary>
        ///  <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されている場合はそれを使用します。
        /// </summary>
        [Fact]
        public void UseUserDefinedPolicyMapping_IdenticalSimplexWithCustomDrive()
        {
            var defaultDriveMock = UseUserDefinedPolicyMapping_DefaultDriveMock();
            var customDriveMock = UseUserDefinedPolicyMapping_CustomDriveMock();
            var defaultPolicyMock = UseUserDefinedPolicyMapping_DefaultPolicyMock();
            var mappedPolicyMock = UseUserDefinedPolicyMapping_MappedPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy> { { typeof(KeyLengthIdenticalSimplexTranslator).FullName, mappedPolicyMock.Object } } };
            var result = core.Get("one", new KeyLengthIdenticalSimplexTranslator("identity"), customDriveMock.Object);

            UseUserDefinedPolicyMapping_Assert(result, defaultPolicyMock, mappedPolicyMock);
        }

        /// <summary>
        ///  <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されている場合はそれを使用します。
        /// </summary>
        [Fact]
        public void UseUserDefinedPolicyMapping_IdenticalVectorized()
        {
            var defaultDriveMock = UseUserDefinedPolicyMapping_DefaultDriveMock();
            var defaultPolicyMock = UseUserDefinedPolicyMapping_DefaultPolicyMock();
            var mappedPolicyMock = UseUserDefinedPolicyMapping_MappedPolicyMock();

            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy> { { typeof(KeyLengthIdenticalVectorizedTranslator).FullName, mappedPolicyMock.Object } } };
            var result = core.Get("one", new KeyLengthIdenticalVectorizedTranslator("identity"));

            UseUserDefinedPolicyMapping_Assert(result, defaultPolicyMock, mappedPolicyMock);
        }

        /// <summary>
        ///  <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されている場合はそれを使用します。
        /// </summary>
        [Fact]
        public void UseUserDefinedPolicyMapping_IdenticalVectorizedWithCustomDrive()
        {
            var defaultDriveMock = UseUserDefinedPolicyMapping_DefaultDriveMock();
            var customDriveMock = UseUserDefinedPolicyMapping_CustomDriveMock();
            var defaultPolicyMock = UseUserDefinedPolicyMapping_DefaultPolicyMock();
            var mappedPolicyMock = UseUserDefinedPolicyMapping_MappedPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy> { { typeof(KeyLengthIdenticalVectorizedTranslator).FullName, mappedPolicyMock.Object } } };
            var result = core.Get("one", new KeyLengthIdenticalVectorizedTranslator("identity"), customDriveMock.Object);

            UseUserDefinedPolicyMapping_Assert(result, defaultPolicyMock, mappedPolicyMock);
        }

        /// <summary>
        ///  <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されている場合はそれを使用します。
        /// </summary>
        [Fact]
        public void UseUserDefinedPolicyMapping_IdenticalOptimized()
        {
            var defaultDriveMock = UseUserDefinedPolicyMapping_DefaultDriveMock();
            var defaultPolicyMock = UseUserDefinedPolicyMapping_DefaultPolicyMock();
            var mappedPolicyMock = UseUserDefinedPolicyMapping_MappedPolicyMock();

            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy> { { typeof(KeyLengthIdenticalOptimizedTranslator).FullName, mappedPolicyMock.Object } } };
            var result = core.Get("one", new KeyLengthIdenticalOptimizedTranslator("identity"));

            UseUserDefinedPolicyMapping_Assert(result, defaultPolicyMock, mappedPolicyMock);
        }

        /// <summary>
        ///  <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されている場合はそれを使用します。
        /// </summary>
        [Fact]
        public void UseUserDefinedPolicyMapping_IdenticalOptimizedWithCustomDrive()
        {
            var defaultDriveMock = UseUserDefinedPolicyMapping_DefaultDriveMock();
            var customDriveMock = UseUserDefinedPolicyMapping_CustomDriveMock();
            var defaultPolicyMock = UseUserDefinedPolicyMapping_DefaultPolicyMock();
            var mappedPolicyMock = UseUserDefinedPolicyMapping_MappedPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy> { { typeof(KeyLengthIdenticalOptimizedTranslator).FullName, mappedPolicyMock.Object } } };
            var result = core.Get("one", new KeyLengthIdenticalOptimizedTranslator("identity"), customDriveMock.Object);

            UseUserDefinedPolicyMapping_Assert(result, defaultPolicyMock, mappedPolicyMock);
        }

        //

        /// <summary>
        ///  <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されている場合はそれを使用します。
        /// </summary>
        [Fact]
        public void UseUserDefinedPolicyMapping_Bulk_Simplex()
        {
            var defaultDriveMock = UseUserDefinedPolicyMapping_DefaultDriveMock();
            var defaultPolicyMock = UseUserDefinedPolicyMapping_DefaultPolicyMock();
            var mappedPolicyMock = UseUserDefinedPolicyMapping_MappedPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy> { { typeof(KeyLengthSimplexTranslator).FullName, mappedPolicyMock.Object } } };
            var result = core.Get(new[] { "one" }, "identity", new KeyLengthSimplexTranslator());

            UseUserDefinedPolicyMapping_Assert(result, defaultPolicyMock, mappedPolicyMock);
        }

        /// <summary>
        ///  <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されている場合はそれを使用します。
        /// </summary>
        [Fact]
        public void UseUserDefinedPolicyMapping_Bulk_SimplexWithCustomDrive()
        {
            var defaultDriveMock = UseUserDefinedPolicyMapping_DefaultDriveMock();
            var customDriveMock = UseUserDefinedPolicyMapping_CustomDriveMock();
            var defaultPolicyMock = UseUserDefinedPolicyMapping_DefaultPolicyMock();
            var mappedPolicyMock = UseUserDefinedPolicyMapping_MappedPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy> { { typeof(KeyLengthSimplexTranslator).FullName, mappedPolicyMock.Object } } };
            var result = core.Get(new[] { "one" }, "identity", new KeyLengthSimplexTranslator(), customDriveMock.Object);

            UseUserDefinedPolicyMapping_Assert(result, defaultPolicyMock, mappedPolicyMock);
        }

        /// <summary>
        ///  <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されている場合はそれを使用します。
        /// </summary>
        [Fact]
        public void UseUserDefinedPolicyMapping_Bulk_Vectorized()
        {
            var defaultDriveMock = UseUserDefinedPolicyMapping_DefaultDriveMock();
            var defaultPolicyMock = UseUserDefinedPolicyMapping_DefaultPolicyMock();
            var mappedPolicyMock = UseUserDefinedPolicyMapping_MappedPolicyMock();

            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy> { { typeof(KeyLengthVectorizedTranslator).FullName, mappedPolicyMock.Object } } };
            var result = core.Get(new[] { "one" }, "identity", new KeyLengthVectorizedTranslator());

            UseUserDefinedPolicyMapping_Assert(result, defaultPolicyMock, mappedPolicyMock);
        }

        /// <summary>
        ///  <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されている場合はそれを使用します。
        /// </summary>
        [Fact]
        public void UseUserDefinedPolicyMapping_Bulk_VectorizedWithCustomDrive()
        {
            var defaultDriveMock = UseUserDefinedPolicyMapping_DefaultDriveMock();
            var customDriveMock = UseUserDefinedPolicyMapping_CustomDriveMock();
            var defaultPolicyMock = UseUserDefinedPolicyMapping_DefaultPolicyMock();
            var mappedPolicyMock = UseUserDefinedPolicyMapping_MappedPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy> { { typeof(KeyLengthVectorizedTranslator).FullName, mappedPolicyMock.Object } } };
            var result = core.Get(new[] { "one" }, "identity", new KeyLengthVectorizedTranslator(), customDriveMock.Object);

            UseUserDefinedPolicyMapping_Assert(result, defaultPolicyMock, mappedPolicyMock);
        }

        /// <summary>
        ///  <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されている場合はそれを使用します。
        /// </summary>
        [Fact]
        public void UseUserDefinedPolicyMapping_Bulk_Optimized()
        {
            var defaultDriveMock = UseUserDefinedPolicyMapping_DefaultDriveMock();
            var defaultPolicyMock = UseUserDefinedPolicyMapping_DefaultPolicyMock();
            var mappedPolicyMock = UseUserDefinedPolicyMapping_MappedPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy> { { typeof(KeyLengthOptimizedTranslator).FullName, mappedPolicyMock.Object } } };
            var result = core.Get(new[] { "one" }, "identity", new KeyLengthOptimizedTranslator());

            UseUserDefinedPolicyMapping_Assert(result, defaultPolicyMock, mappedPolicyMock);
        }

        /// <summary>
        ///  <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されている場合はそれを使用します。
        /// </summary>
        [Fact]
        public void UseUserDefinedPolicyMapping_Bulk_OptimizedWithCustomDrive()
        {
            var defaultDriveMock = UseUserDefinedPolicyMapping_DefaultDriveMock();
            var customDriveMock = UseUserDefinedPolicyMapping_CustomDriveMock();
            var defaultPolicyMock = UseUserDefinedPolicyMapping_DefaultPolicyMock();
            var mappedPolicyMock = UseUserDefinedPolicyMapping_MappedPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy> { { typeof(KeyLengthOptimizedTranslator).FullName, mappedPolicyMock.Object } } };
            var result = core.Get(new[] { "one" }, "identity", new KeyLengthOptimizedTranslator(), customDriveMock.Object);

            UseUserDefinedPolicyMapping_Assert(result, defaultPolicyMock, mappedPolicyMock);
        }

        /// <summary>
        ///  <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されている場合はそれを使用します。
        /// </summary>
        [Fact]
        public void UseUserDefinedPolicyMapping_Bulk_IdenticalSimplex()
        {
            var defaultDriveMock = UseUserDefinedPolicyMapping_DefaultDriveMock();
            var defaultPolicyMock = UseUserDefinedPolicyMapping_DefaultPolicyMock();
            var mappedPolicyMock = UseUserDefinedPolicyMapping_MappedPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy> { { typeof(KeyLengthIdenticalSimplexTranslator).FullName, mappedPolicyMock.Object } } };
            var result = core.Get(new[] { "one" }, new KeyLengthIdenticalSimplexTranslator("identity"));

            UseUserDefinedPolicyMapping_Assert(result, defaultPolicyMock, mappedPolicyMock);
        }

        /// <summary>
        ///  <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されている場合はそれを使用します。
        /// </summary>
        [Fact]
        public void UseUserDefinedPolicyMapping_Bulk_IdenticalSimplexWithCustomDrive()
        {
            var defaultDriveMock = UseUserDefinedPolicyMapping_DefaultDriveMock();
            var customDriveMock = UseUserDefinedPolicyMapping_CustomDriveMock();
            var defaultPolicyMock = UseUserDefinedPolicyMapping_DefaultPolicyMock();
            var mappedPolicyMock = UseUserDefinedPolicyMapping_MappedPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy> { { typeof(KeyLengthIdenticalSimplexTranslator).FullName, mappedPolicyMock.Object } } };
            var result = core.Get(new[] { "one" }, new KeyLengthIdenticalSimplexTranslator("identity"), customDriveMock.Object);

            UseUserDefinedPolicyMapping_Assert(result, defaultPolicyMock, mappedPolicyMock);
        }

        /// <summary>
        ///  <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されている場合はそれを使用します。
        /// </summary>
        [Fact]
        public void UseUserDefinedPolicyMapping_Bulk_IdenticalVectorized()
        {
            var defaultDriveMock = UseUserDefinedPolicyMapping_DefaultDriveMock();
            var defaultPolicyMock = UseUserDefinedPolicyMapping_DefaultPolicyMock();
            var mappedPolicyMock = UseUserDefinedPolicyMapping_MappedPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy> { { typeof(KeyLengthIdenticalVectorizedTranslator).FullName, mappedPolicyMock.Object } } };
            var result = core.Get(new[] { "one" }, new KeyLengthIdenticalVectorizedTranslator("identity"));

            UseUserDefinedPolicyMapping_Assert(result, defaultPolicyMock, mappedPolicyMock);
        }

        /// <summary>
        ///  <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されている場合はそれを使用します。
        /// </summary>
        [Fact]
        public void UseUserDefinedPolicyMapping_Bulk_IdenticalVectorizedWithCustomDrive()
        {
            var defaultDriveMock = UseUserDefinedPolicyMapping_DefaultDriveMock();
            var customDriveMock = UseUserDefinedPolicyMapping_CustomDriveMock();
            var defaultPolicyMock = UseUserDefinedPolicyMapping_DefaultPolicyMock();
            var mappedPolicyMock = UseUserDefinedPolicyMapping_MappedPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy> { { typeof(KeyLengthIdenticalVectorizedTranslator).FullName, mappedPolicyMock.Object } } };
            var result = core.Get(new[] { "one" }, new KeyLengthIdenticalVectorizedTranslator("identity"), customDriveMock.Object);

            UseUserDefinedPolicyMapping_Assert(result, defaultPolicyMock, mappedPolicyMock);
        }

        /// <summary>
        ///  <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されている場合はそれを使用します。
        /// </summary>
        [Fact]
        public void UseUserDefinedPolicyMapping_Bulk_IdenticalOptimized()
        {
            var defaultDriveMock = UseUserDefinedPolicyMapping_DefaultDriveMock();
            var defaultPolicyMock = UseUserDefinedPolicyMapping_DefaultPolicyMock();
            var mappedPolicyMock = UseUserDefinedPolicyMapping_MappedPolicyMock();

            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy> { { typeof(KeyLengthIdenticalOptimizedTranslator).FullName, mappedPolicyMock.Object } } };
            var result = core.Get(new[] { "one" }, new KeyLengthIdenticalOptimizedTranslator("identity"));

            UseUserDefinedPolicyMapping_Assert(result, defaultPolicyMock, mappedPolicyMock);
        }

        /// <summary>
        ///  <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されている場合はそれを使用します。
        /// </summary>
        [Fact]
        public void UseUserDefinedPolicyMapping_Bulk_IdenticalOptimizedWithCustomDrive()
        {
            var defaultDriveMock = UseUserDefinedPolicyMapping_DefaultDriveMock();
            var customDriveMock = UseUserDefinedPolicyMapping_CustomDriveMock();
            var defaultPolicyMock = UseUserDefinedPolicyMapping_DefaultPolicyMock();
            var mappedPolicyMock = UseUserDefinedPolicyMapping_MappedPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy> { { typeof(KeyLengthIdenticalOptimizedTranslator).FullName, mappedPolicyMock.Object } } };
            var result = core.Get(new[] { "one" }, new KeyLengthIdenticalOptimizedTranslator("identity"), customDriveMock.Object);

            UseUserDefinedPolicyMapping_Assert(result, defaultPolicyMock, mappedPolicyMock);
        }

        //

        private Mock<ICacheDrive> UseDefaultPolicyWhenPolicyMappingNotDefined_DefaultDriveMock()
        {
            var driveMock = new Mock<ICacheDrive>();
            driveMock.Setup(_ => _.Contains("one" + "identity")).Returns(true);
            driveMock.Setup(_ => _.Get<int>("one" + "identity")).Returns(new CacheEntry<int>() { Key = "one", Value = 1234567 });
            return driveMock;
        }

        private Mock<ICacheDrive> UseDefaultPolicyWhenPolicyMappingNotDefined_CustomtDriveMock()
        {
            var customDriveMock = new Mock<ICacheDrive>();
            customDriveMock.Setup(_ => _.Contains("one" + "identity")).Returns(true);
            customDriveMock.Setup(_ => _.Get<int>("one" + "identity")).Returns(new CacheEntry<int>() { Key = "one", Value = 1234567 });
            return customDriveMock;
        }

        private Mock<IExpirePolicy> UseDefaultPolicyWhenPolicyMappingNotDefined_DefaultPolicyMock()
        {
            var defaultPolicyMock = new Mock<IExpirePolicy>();
            defaultPolicyMock.Setup(_ => _.Validate(It.IsAny<CacheEntry<int>>())).Returns(true);
            return defaultPolicyMock;
        }

        private Mock<IExpirePolicy> UseDefaultPolicyWhenPolicyMappingNotDefined_MappedPolicyMock()
        {
            var mappedPolicyMock = new Mock<IExpirePolicy>();
            mappedPolicyMock.Setup(_ => _.Validate(It.IsAny<CacheEntry<int>>())).Returns(false);
            return mappedPolicyMock;
        }

        private void UseDefaultPolicyWhenPolicyMappingNotDefined_Assert(int result, Mock<IExpirePolicy> defaultPolicyMock, Mock<IExpirePolicy> mappedPolicyMock)
        {
            Assert.Equal(1234567, result);
            defaultPolicyMock.VerifyAll();
            mappedPolicyMock.Verify(_ => _.Validate(It.IsAny<CacheEntry<int>>()), Times.Never());
        }

        private void UseDefaultPolicyWhenPolicyMappingNotDefined_Assert(int[] result, Mock<IExpirePolicy> defaultPolicyMock, Mock<IExpirePolicy> mappedPolicyMock)
        {
            Assert.Equal(new[] { 1234567 }, result);
            defaultPolicyMock.VerifyAll();
            mappedPolicyMock.Verify(_ => _.Validate(It.IsAny<CacheEntry<int>>()), Times.Never());
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されてない場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// </summary>
        [Fact]
        public void UseDefaultPolicyWhenPolicyMappingNotDefined_Simplex()
        {
            var defaultDriveMock = UseDefaultPolicyWhenPolicyMappingNotDefined_DefaultDriveMock();
            var defaultPolicyMock = UseDefaultPolicyWhenPolicyMappingNotDefined_DefaultPolicyMock();
            var mappedPolicyMock = UseDefaultPolicyWhenPolicyMappingNotDefined_MappedPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy> { { "invalid", mappedPolicyMock.Object } } };
            var result = core.Get("one", "identity", new KeyLengthSimplexTranslator());

            UseDefaultPolicyWhenPolicyMappingNotDefined_Assert(result, defaultPolicyMock, mappedPolicyMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されてない場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// </summary>
        [Fact]
        public void UseDefaultPolicyWhenPolicyMappingNotDefined_SimplexWithCustomDrive()
        {
            var defaultDriveMock = UseDefaultPolicyWhenPolicyMappingNotDefined_DefaultDriveMock();
            var customDriveMock = UseDefaultPolicyWhenPolicyMappingNotDefined_CustomtDriveMock();
            var defaultPolicyMock = UseDefaultPolicyWhenPolicyMappingNotDefined_DefaultPolicyMock();
            var mappedPolicyMock = UseDefaultPolicyWhenPolicyMappingNotDefined_MappedPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy> { { "invalid", mappedPolicyMock.Object } } };
            var result = core.Get("one", "identity", new KeyLengthSimplexTranslator(), customDriveMock.Object);

            UseDefaultPolicyWhenPolicyMappingNotDefined_Assert(result, defaultPolicyMock, mappedPolicyMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されてない場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// </summary>
        [Fact]
        public void UseDefaultPolicyWhenPolicyMappingNotDefined_Vectorized()
        {
            var defaultDriveMock = UseDefaultPolicyWhenPolicyMappingNotDefined_DefaultDriveMock();
            var defaultPolicyMock = UseDefaultPolicyWhenPolicyMappingNotDefined_DefaultPolicyMock();
            var mappedPolicyMock = UseDefaultPolicyWhenPolicyMappingNotDefined_MappedPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy> { { "invalid", mappedPolicyMock.Object } } };
            var result = core.Get("one", "identity", new KeyLengthSimplexTranslator());

            UseDefaultPolicyWhenPolicyMappingNotDefined_Assert(result, defaultPolicyMock, mappedPolicyMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されてない場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// </summary>
        [Fact]
        public void UseDefaultPolicyWhenPolicyMappingNotDefined_VectorizedWithCustomDrive()
        {
            var defaultDriveMock = UseDefaultPolicyWhenPolicyMappingNotDefined_DefaultDriveMock();
            var customDriveMock = UseDefaultPolicyWhenPolicyMappingNotDefined_CustomtDriveMock();
            var defaultPolicyMock = UseDefaultPolicyWhenPolicyMappingNotDefined_DefaultPolicyMock();
            var mappedPolicyMock = UseDefaultPolicyWhenPolicyMappingNotDefined_MappedPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy> { { "invalid", mappedPolicyMock.Object } } };
            var result = core.Get("one", "identity", new KeyLengthSimplexTranslator(), customDriveMock.Object);

            UseDefaultPolicyWhenPolicyMappingNotDefined_Assert(result, defaultPolicyMock, mappedPolicyMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されてない場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// </summary>
        [Fact]
        public void UseDefaultPolicyWhenPolicyMappingNotDefined_Optimized()
        {
            var defaultDriveMock = UseDefaultPolicyWhenPolicyMappingNotDefined_DefaultDriveMock();
            var defaultPolicyMock = UseDefaultPolicyWhenPolicyMappingNotDefined_DefaultPolicyMock();
            var mappedPolicyMock = UseDefaultPolicyWhenPolicyMappingNotDefined_MappedPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy> { { "invalid", mappedPolicyMock.Object } } };
            var result = core.Get("one", "identity", new KeyLengthOptimizedTranslator());

            UseDefaultPolicyWhenPolicyMappingNotDefined_Assert(result, defaultPolicyMock, mappedPolicyMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されてない場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// </summary>
        [Fact]
        public void UseDefaultPolicyWhenPolicyMappingNotDefined_OptimizedWithCustomDrive()
        {
            var defaultDriveMock = UseDefaultPolicyWhenPolicyMappingNotDefined_DefaultDriveMock();
            var customDriveMock = UseDefaultPolicyWhenPolicyMappingNotDefined_CustomtDriveMock();
            var defaultPolicyMock = UseDefaultPolicyWhenPolicyMappingNotDefined_DefaultPolicyMock();
            var mappedPolicyMock = UseDefaultPolicyWhenPolicyMappingNotDefined_MappedPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy> { { "invalid", mappedPolicyMock.Object } } };
            var result = core.Get("one", "identity", new KeyLengthOptimizedTranslator(), customDriveMock.Object);

            UseDefaultPolicyWhenPolicyMappingNotDefined_Assert(result, defaultPolicyMock, mappedPolicyMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されてない場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// </summary>
        [Fact]
        public void UseDefaultPolicyWhenPolicyMappingNotDefined_IdenticalSimplex()
        {
            var defaultDriveMock = UseDefaultPolicyWhenPolicyMappingNotDefined_DefaultDriveMock();
            var defaultPolicyMock = UseDefaultPolicyWhenPolicyMappingNotDefined_DefaultPolicyMock();
            var mappedPolicyMock = UseDefaultPolicyWhenPolicyMappingNotDefined_MappedPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy> { { "invalid", mappedPolicyMock.Object } } };
            var result = core.Get("one", new KeyLengthIdenticalSimplexTranslator("identity"));

            UseDefaultPolicyWhenPolicyMappingNotDefined_Assert(result, defaultPolicyMock, mappedPolicyMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されてない場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// </summary>
        [Fact]
        public void UseDefaultPolicyWhenPolicyMappingNotDefined_IdenticalSimplexWithCustomDrive()
        {
            var defaultDriveMock = UseDefaultPolicyWhenPolicyMappingNotDefined_DefaultDriveMock();
            var customDriveMock = UseDefaultPolicyWhenPolicyMappingNotDefined_CustomtDriveMock();
            var defaultPolicyMock = UseDefaultPolicyWhenPolicyMappingNotDefined_DefaultPolicyMock();
            var mappedPolicyMock = UseDefaultPolicyWhenPolicyMappingNotDefined_MappedPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy> { { "invalid", mappedPolicyMock.Object } } };
            var result = core.Get("one", new KeyLengthIdenticalSimplexTranslator("identity"), customDriveMock.Object);

            UseDefaultPolicyWhenPolicyMappingNotDefined_Assert(result, defaultPolicyMock, mappedPolicyMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されてない場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// </summary>
        [Fact]
        public void UseDefaultPolicyWhenPolicyMappingNotDefined_IdenticalVectorized()
        {
            var defaultDriveMock = UseDefaultPolicyWhenPolicyMappingNotDefined_DefaultDriveMock();
            var defaultPolicyMock = UseDefaultPolicyWhenPolicyMappingNotDefined_DefaultPolicyMock();
            var mappedPolicyMock = UseDefaultPolicyWhenPolicyMappingNotDefined_MappedPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy> { { "invalid", mappedPolicyMock.Object } } };
            var result = core.Get("one", new KeyLengthIdenticalSimplexTranslator("identity"));

            UseDefaultPolicyWhenPolicyMappingNotDefined_Assert(result, defaultPolicyMock, mappedPolicyMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されてない場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// </summary>
        [Fact]
        public void UseDefaultPolicyWhenPolicyMappingNotDefined_IdenticalVectorizedWithCustomDrive()
        {
            var defaultDriveMock = UseDefaultPolicyWhenPolicyMappingNotDefined_DefaultDriveMock();
            var customDriveMock = UseDefaultPolicyWhenPolicyMappingNotDefined_CustomtDriveMock();
            var defaultPolicyMock = UseDefaultPolicyWhenPolicyMappingNotDefined_DefaultPolicyMock();
            var mappedPolicyMock = UseDefaultPolicyWhenPolicyMappingNotDefined_MappedPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy> { { "invalid", mappedPolicyMock.Object } } };
            var result = core.Get("one", new KeyLengthIdenticalSimplexTranslator("identity"), customDriveMock.Object);

            UseDefaultPolicyWhenPolicyMappingNotDefined_Assert(result, defaultPolicyMock, mappedPolicyMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されてない場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// </summary>
        [Fact]
        public void UseDefaultPolicyWhenPolicyMappingNotDefined_IdenticalOptimized()
        {
            var defaultDriveMock = UseDefaultPolicyWhenPolicyMappingNotDefined_DefaultDriveMock();
            var defaultPolicyMock = UseDefaultPolicyWhenPolicyMappingNotDefined_DefaultPolicyMock();
            var mappedPolicyMock = UseDefaultPolicyWhenPolicyMappingNotDefined_MappedPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy> { { "invalid", mappedPolicyMock.Object } } };
            var result = core.Get("one", new KeyLengthIdenticalOptimizedTranslator("identity"));

            UseDefaultPolicyWhenPolicyMappingNotDefined_Assert(result, defaultPolicyMock, mappedPolicyMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されてない場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// </summary>
        [Fact]
        public void UseDefaultPolicyWhenPolicyMappingNotDefined_IdenticalOptimizedWithCustomDrive()
        {
            var defaultDriveMock = UseDefaultPolicyWhenPolicyMappingNotDefined_DefaultDriveMock();
            var customDriveMock = UseDefaultPolicyWhenPolicyMappingNotDefined_CustomtDriveMock();
            var defaultPolicyMock = UseDefaultPolicyWhenPolicyMappingNotDefined_DefaultPolicyMock();
            var mappedPolicyMock = UseDefaultPolicyWhenPolicyMappingNotDefined_MappedPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy> { { "invalid", mappedPolicyMock.Object } } };
            var result = core.Get("one", new KeyLengthIdenticalOptimizedTranslator("identity"), customDriveMock.Object);

            UseDefaultPolicyWhenPolicyMappingNotDefined_Assert(result, defaultPolicyMock, mappedPolicyMock);
        }

        //

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されてない場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// </summary>
        [Fact]
        public void UseDefaultPolicyWhenPolicyMappingNotDefined_Bulk_Simplex()
        {
            var defaultDriveMock = UseDefaultPolicyWhenPolicyMappingNotDefined_DefaultDriveMock();
            var defaultPolicyMock = UseDefaultPolicyWhenPolicyMappingNotDefined_DefaultPolicyMock();
            var mappedPolicyMock = UseDefaultPolicyWhenPolicyMappingNotDefined_MappedPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy> { { "invalid", mappedPolicyMock.Object } } };
            var result = core.Get(new[] { "one" }, "identity", new KeyLengthSimplexTranslator());

            UseDefaultPolicyWhenPolicyMappingNotDefined_Assert(result, defaultPolicyMock, mappedPolicyMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されてない場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// </summary>
        [Fact]
        public void UseDefaultPolicyWhenPolicyMappingNotDefined_Bulk_SimplexWithCustomDrive()
        {
            var defaultDriveMock = UseDefaultPolicyWhenPolicyMappingNotDefined_DefaultDriveMock();
            var customDriveMock = UseDefaultPolicyWhenPolicyMappingNotDefined_CustomtDriveMock();
            var defaultPolicyMock = UseDefaultPolicyWhenPolicyMappingNotDefined_DefaultPolicyMock();
            var mappedPolicyMock = UseDefaultPolicyWhenPolicyMappingNotDefined_MappedPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy> { { "invalid", mappedPolicyMock.Object } } };
            var result = core.Get(new[] { "one" }, "identity", new KeyLengthSimplexTranslator(), customDriveMock.Object);

            UseDefaultPolicyWhenPolicyMappingNotDefined_Assert(result, defaultPolicyMock, mappedPolicyMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されてない場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// </summary>
        [Fact]
        public void UseDefaultPolicyWhenPolicyMappingNotDefined_Bulk_Vectorized()
        {
            var defaultDriveMock = UseDefaultPolicyWhenPolicyMappingNotDefined_DefaultDriveMock();
            var defaultPolicyMock = UseDefaultPolicyWhenPolicyMappingNotDefined_DefaultPolicyMock();
            var mappedPolicyMock = UseDefaultPolicyWhenPolicyMappingNotDefined_MappedPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy> { { "invalid", mappedPolicyMock.Object } } };
            var result = core.Get(new[] { "one" }, "identity", new KeyLengthSimplexTranslator());

            UseDefaultPolicyWhenPolicyMappingNotDefined_Assert(result, defaultPolicyMock, mappedPolicyMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されてない場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// </summary>
        [Fact]
        public void UseDefaultPolicyWhenPolicyMappingNotDefined_Bulk_VectorizedWithCustomDrive()
        {
            var defaultDriveMock = UseDefaultPolicyWhenPolicyMappingNotDefined_DefaultDriveMock();
            var customDriveMock = UseDefaultPolicyWhenPolicyMappingNotDefined_CustomtDriveMock();
            var defaultPolicyMock = UseDefaultPolicyWhenPolicyMappingNotDefined_DefaultPolicyMock();
            var mappedPolicyMock = UseDefaultPolicyWhenPolicyMappingNotDefined_MappedPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy> { { "invalid", mappedPolicyMock.Object } } };
            var result = core.Get(new[] { "one" }, "identity", new KeyLengthSimplexTranslator(), customDriveMock.Object);

            UseDefaultPolicyWhenPolicyMappingNotDefined_Assert(result, defaultPolicyMock, mappedPolicyMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されてない場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// </summary>
        [Fact]
        public void UseDefaultPolicyWhenPolicyMappingNotDefined_Bulk_Optimized()
        {
            var defaultDriveMock = UseDefaultPolicyWhenPolicyMappingNotDefined_DefaultDriveMock();
            var defaultPolicyMock = UseDefaultPolicyWhenPolicyMappingNotDefined_DefaultPolicyMock();
            var mappedPolicyMock = UseDefaultPolicyWhenPolicyMappingNotDefined_MappedPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy> { { "invalid", mappedPolicyMock.Object } } };
            var result = core.Get(new[] { "one" }, "identity", new KeyLengthOptimizedTranslator());

            UseDefaultPolicyWhenPolicyMappingNotDefined_Assert(result, defaultPolicyMock, mappedPolicyMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されてない場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// </summary>
        [Fact]
        public void UseDefaultPolicyWhenPolicyMappingNotDefined_Bulk_OptimizedWithCustomDrive()
        {
            var defaultDriveMock = UseDefaultPolicyWhenPolicyMappingNotDefined_DefaultDriveMock();
            var customDriveMock = UseDefaultPolicyWhenPolicyMappingNotDefined_CustomtDriveMock();
            var defaultPolicyMock = UseDefaultPolicyWhenPolicyMappingNotDefined_DefaultPolicyMock();
            var mappedPolicyMock = UseDefaultPolicyWhenPolicyMappingNotDefined_MappedPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy> { { "invalid", mappedPolicyMock.Object } } };
            var result = core.Get(new[] { "one" }, "identity", new KeyLengthOptimizedTranslator(), customDriveMock.Object);

            UseDefaultPolicyWhenPolicyMappingNotDefined_Assert(result, defaultPolicyMock, mappedPolicyMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されてない場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// </summary>
        [Fact]
        public void UseDefaultPolicyWhenPolicyMappingNotDefined_Bulk_IdenticalSimplex()
        {
            var defaultDriveMock = UseDefaultPolicyWhenPolicyMappingNotDefined_DefaultDriveMock();
            var defaultPolicyMock = UseDefaultPolicyWhenPolicyMappingNotDefined_DefaultPolicyMock();
            var mappedPolicyMock = UseDefaultPolicyWhenPolicyMappingNotDefined_MappedPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy> { { "invalid", mappedPolicyMock.Object } } };
            var result = core.Get(new[] { "one" }, new KeyLengthIdenticalSimplexTranslator("identity"));

            UseDefaultPolicyWhenPolicyMappingNotDefined_Assert(result, defaultPolicyMock, mappedPolicyMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されてない場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// </summary>
        [Fact]
        public void UseDefaultPolicyWhenPolicyMappingNotDefined_Bulk_IdenticalSimplexWithCustomDrive()
        {
            var defaultDriveMock = UseDefaultPolicyWhenPolicyMappingNotDefined_DefaultDriveMock();
            var customDriveMock = UseDefaultPolicyWhenPolicyMappingNotDefined_CustomtDriveMock();
            var defaultPolicyMock = UseDefaultPolicyWhenPolicyMappingNotDefined_DefaultPolicyMock();
            var mappedPolicyMock = UseDefaultPolicyWhenPolicyMappingNotDefined_MappedPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy> { { "invalid", mappedPolicyMock.Object } } };
            var result = core.Get(new[] { "one" }, new KeyLengthIdenticalSimplexTranslator("identity"), customDriveMock.Object);

            UseDefaultPolicyWhenPolicyMappingNotDefined_Assert(result, defaultPolicyMock, mappedPolicyMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されてない場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// </summary>
        [Fact]
        public void UseDefaultPolicyWhenPolicyMappingNotDefined_Bulk_IdenticalVectorized()
        {
            var defaultDriveMock = UseDefaultPolicyWhenPolicyMappingNotDefined_DefaultDriveMock();
            var defaultPolicyMock = UseDefaultPolicyWhenPolicyMappingNotDefined_DefaultPolicyMock();
            var mappedPolicyMock = UseDefaultPolicyWhenPolicyMappingNotDefined_MappedPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy> { { "invalid", mappedPolicyMock.Object } } };
            var result = core.Get(new[] { "one" }, new KeyLengthIdenticalSimplexTranslator("identity"));

            UseDefaultPolicyWhenPolicyMappingNotDefined_Assert(result, defaultPolicyMock, mappedPolicyMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されてない場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// </summary>
        [Fact]
        public void UseDefaultPolicyWhenPolicyMappingNotDefined_Bulk_IdenticalVectorizedWithCustomDrive()
        {
            var defaultDriveMock = UseDefaultPolicyWhenPolicyMappingNotDefined_DefaultDriveMock();
            var customDriveMock = UseDefaultPolicyWhenPolicyMappingNotDefined_CustomtDriveMock();
            var defaultPolicyMock = UseDefaultPolicyWhenPolicyMappingNotDefined_DefaultPolicyMock();
            var mappedPolicyMock = UseDefaultPolicyWhenPolicyMappingNotDefined_MappedPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy> { { "invalid", mappedPolicyMock.Object } } };
            var result = core.Get(new[] { "one" }, new KeyLengthIdenticalSimplexTranslator("identity"), customDriveMock.Object);

            UseDefaultPolicyWhenPolicyMappingNotDefined_Assert(result, defaultPolicyMock, mappedPolicyMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されてない場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// </summary>
        [Fact]
        public void UseDefaultPolicyWhenPolicyMappingNotDefined_Bulk_IdenticalOptimized()
        {
            var defaultDriveMock = UseDefaultPolicyWhenPolicyMappingNotDefined_DefaultDriveMock();
            var defaultPolicyMock = UseDefaultPolicyWhenPolicyMappingNotDefined_DefaultPolicyMock();
            var mappedPolicyMock = UseDefaultPolicyWhenPolicyMappingNotDefined_MappedPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy> { { "invalid", mappedPolicyMock.Object } } };
            var result = core.Get(new[] { "one" }, new KeyLengthIdenticalOptimizedTranslator("identity"));

            UseDefaultPolicyWhenPolicyMappingNotDefined_Assert(result, defaultPolicyMock, mappedPolicyMock);
        }

        /// <summary>
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されてない場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// </summary>
        [Fact]
        public void UseDefaultPolicyWhenPolicyMappingNotDefined_Bulk_IdenticalOptimizedWithCustomDrive()
        {
            var defaultDriveMock = UseDefaultPolicyWhenPolicyMappingNotDefined_DefaultDriveMock();
            var customDriveMock = UseDefaultPolicyWhenPolicyMappingNotDefined_CustomtDriveMock();
            var defaultPolicyMock = UseDefaultPolicyWhenPolicyMappingNotDefined_DefaultPolicyMock();
            var mappedPolicyMock = UseDefaultPolicyWhenPolicyMappingNotDefined_MappedPolicyMock();


            var core = new CacheCore(defaultDriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = new Dictionary<string, IExpirePolicy> { { "invalid", mappedPolicyMock.Object } } };
            var result = core.Get(new[] { "one" }, new KeyLengthIdenticalOptimizedTranslator("identity"), customDriveMock.Object);

            UseDefaultPolicyWhenPolicyMappingNotDefined_Assert(result, defaultPolicyMock, mappedPolicyMock);
        }

        // Error Test

        [Fact]
        public void DrivieMappingNullError()
        {
            var defaultdriveMock = new Mock<ICacheDrive>();
            var defaultPolicyMock = new Mock<IExpirePolicy>();
            Assert.Throws<InvalidOperationException>(() => new CacheCore(defaultdriveMock.Object, defaultPolicyMock.Object) { CacheDriveMapping = null });
        }

        [Fact]
        public void PolicyMappingNullError()
        {
            var defaultdriveMock = new Mock<ICacheDrive>();
            var defaultPolicyMock = new Mock<IExpirePolicy>();
            Assert.Throws<InvalidOperationException>(() => new CacheCore(defaultdriveMock.Object, defaultPolicyMock.Object) { ExpirePolicyMapping = null });
        }

        [Fact]
        public void DefaultDriveNullError()
        {
            var defaultdriveMock = new Mock<ICacheDrive>();
            var defaultPolicyMock = new Mock<IExpirePolicy>();
            Assert.Throws<InvalidOperationException>(() => new CacheCore(defaultdriveMock.Object, defaultPolicyMock.Object).DefaultCacheDrive = null);
        }

        [Fact]
        public void DefaultPolicyNullError()
        {
            var defaultdriveMock = new Mock<ICacheDrive>();
            var defaultPolicyMock = new Mock<IExpirePolicy>();
            Assert.Throws<InvalidOperationException>(() => new CacheCore(defaultdriveMock.Object, defaultPolicyMock.Object).DefaultExpirePolicy = null);
        }

        [Fact]
        public void DefaultPolicyNullCheck()
        {
            var defaultdriveMock = new Mock<ICacheDrive>();
            Assert.Throws<ArgumentNullException>(() => new CacheCore(defaultdriveMock.Object, null));
        }

        [Fact]
        public void DefaultDriveNullCheck()
        {
            var defaultPolicyMock = new Mock<IExpirePolicy>();
            Assert.Throws<ArgumentNullException>(() => new CacheCore(null, defaultPolicyMock.Object));
        }

        //

        [Fact]
        public void GetTranslatorNullCheck_Simplex()
        {
            ISimplexTranslator<int> translator = null;
            var defaultdriveMock = new Mock<ICacheDrive>();
            var defaultPolicyMock = new Mock<IExpirePolicy>();
            var core = new CacheCore(defaultdriveMock.Object, defaultPolicyMock.Object);

            Assert.Throws<ArgumentNullException>(() => core.Get("", "", translator));
        }

        [Fact]
        public void GetTranslatorNullCheck_SimplexWithCacheDrive()
        {
            ISimplexTranslator<int> translator = null;
            ICacheDrive drive = null;
            var defaultdriveMock = new Mock<ICacheDrive>();
            var defaultPolicyMock = new Mock<IExpirePolicy>();
            var core = new CacheCore(defaultdriveMock.Object, defaultPolicyMock.Object);

            Assert.Throws<ArgumentNullException>(() => core.Get("", "", translator, drive));
        }

        [Fact]
        public void GetTranslatorNullCheck_SimplexWithExpirePolicy()
        {
            ISimplexTranslator<int> translator = null;
            IExpirePolicy policy = null;
            var defaultdriveMock = new Mock<ICacheDrive>();
            var defaultPolicyMock = new Mock<IExpirePolicy>();
            var core = new CacheCore(defaultdriveMock.Object, defaultPolicyMock.Object);

            Assert.Throws<ArgumentNullException>(() => core.Get("", "", translator, policy));
        }

        [Fact]
        public void GetTranslatorNullCheck_Vectorized()
        {
            IVectorizedTranslator<int> translator = null;
            var defaultdriveMock = new Mock<ICacheDrive>();
            var defaultPolicyMock = new Mock<IExpirePolicy>();
            var core = new CacheCore(defaultdriveMock.Object, defaultPolicyMock.Object);

            Assert.Throws<ArgumentNullException>(() => core.Get("", "", translator));
        }

        [Fact]
        public void GetTranslatorNullCheck_VectorizedWithCacheDrive()
        {
            IVectorizedTranslator<int> translator = null;
            ICacheDrive drive = null;
            var defaultdriveMock = new Mock<ICacheDrive>();
            var defaultPolicyMock = new Mock<IExpirePolicy>();
            var core = new CacheCore(defaultdriveMock.Object, defaultPolicyMock.Object);

            Assert.Throws<ArgumentNullException>(() => core.Get("", "", translator, drive));
        }

        [Fact]
        public void GetTranslatorNullCheck_VectorizedWithExpirePolicy()
        {
            IVectorizedTranslator<int> translator = null;
            IExpirePolicy policy = null;
            var defaultdriveMock = new Mock<ICacheDrive>();
            var defaultPolicyMock = new Mock<IExpirePolicy>();
            var core = new CacheCore(defaultdriveMock.Object, defaultPolicyMock.Object);

            Assert.Throws<ArgumentNullException>(() => core.Get("", "", translator, policy));
        }

        [Fact]
        public void GetTranslatorNullCheck_Optimized()
        {
            IOptimizedTranslator<int> translator = null;
            var defaultdriveMock = new Mock<ICacheDrive>();
            var defaultPolicyMock = new Mock<IExpirePolicy>();
            var core = new CacheCore(defaultdriveMock.Object, defaultPolicyMock.Object);

            Assert.Throws<ArgumentNullException>(() => core.Get("", "", translator));
        }

        [Fact]
        public void GetTranslatorNullCheck_OptimizedWithCacheDrive()
        {
            IOptimizedTranslator<int> translator = null;
            ICacheDrive drive = null;
            var defaultdriveMock = new Mock<ICacheDrive>();
            var defaultPolicyMock = new Mock<IExpirePolicy>();
            var core = new CacheCore(defaultdriveMock.Object, defaultPolicyMock.Object);

            Assert.Throws<ArgumentNullException>(() => core.Get("", "", translator, drive));
        }

        [Fact]
        public void GetTranslatorNullCheck_OptimizedWithExpirePolicy()
        {
            IOptimizedTranslator<int> translator = null;
            IExpirePolicy policy = null;
            var defaultdriveMock = new Mock<ICacheDrive>();
            var defaultPolicyMock = new Mock<IExpirePolicy>();
            var core = new CacheCore(defaultdriveMock.Object, defaultPolicyMock.Object);

            Assert.Throws<ArgumentNullException>(() => core.Get("", "", translator, policy));
        }

        [Fact]
        public void GetTranslatorNullCheck_IdenticalSimplex()
        {
            IIdenticalSimplexTranslator<int> translator = null;
            var defaultdriveMock = new Mock<ICacheDrive>();
            var defaultPolicyMock = new Mock<IExpirePolicy>();
            var core = new CacheCore(defaultdriveMock.Object, defaultPolicyMock.Object);

            Assert.Throws<ArgumentNullException>(() => core.Get("", translator));
        }

        [Fact]
        public void GetTranslatorNullCheck_IdenticalSimplexWithCacheDrive()
        {
            IIdenticalSimplexTranslator<int> translator = null;
            ICacheDrive drive = null;
            var defaultdriveMock = new Mock<ICacheDrive>();
            var defaultPolicyMock = new Mock<IExpirePolicy>();
            var core = new CacheCore(defaultdriveMock.Object, defaultPolicyMock.Object);

            Assert.Throws<ArgumentNullException>(() => core.Get("", translator, drive));
        }

        [Fact]
        public void GetTranslatorNullCheck_IdenticalSimplexWithExpirePolicy()
        {
            IIdenticalSimplexTranslator<int> translator = null;
            IExpirePolicy policy = null;
            var defaultdriveMock = new Mock<ICacheDrive>();
            var defaultPolicyMock = new Mock<IExpirePolicy>();
            var core = new CacheCore(defaultdriveMock.Object, defaultPolicyMock.Object);

            Assert.Throws<ArgumentNullException>(() => core.Get("", translator, policy));
        }

        [Fact]
        public void GetTranslatorNullCheck_IdenticalVectorized()
        {
            IIdenticalVectorizedTranslator<int> translator = null;
            var defaultdriveMock = new Mock<ICacheDrive>();
            var defaultPolicyMock = new Mock<IExpirePolicy>();
            var core = new CacheCore(defaultdriveMock.Object, defaultPolicyMock.Object);

            Assert.Throws<ArgumentNullException>(() => core.Get("", translator));
        }

        [Fact]
        public void GetTranslatorNullCheck_IdenticalVectorizedWithCacheDrive()
        {
            IIdenticalVectorizedTranslator<int> translator = null;
            ICacheDrive drive = null;
            var defaultdriveMock = new Mock<ICacheDrive>();
            var defaultPolicyMock = new Mock<IExpirePolicy>();
            var core = new CacheCore(defaultdriveMock.Object, defaultPolicyMock.Object);

            Assert.Throws<ArgumentNullException>(() => core.Get("", translator, drive));
        }

        [Fact]
        public void GetTranslatorNullCheck_IdenticalVectorizedWithExpirePolicy()
        {
            IIdenticalVectorizedTranslator<int> translator = null;
            IExpirePolicy policy = null;
            var defaultdriveMock = new Mock<ICacheDrive>();
            var defaultPolicyMock = new Mock<IExpirePolicy>();
            var core = new CacheCore(defaultdriveMock.Object, defaultPolicyMock.Object);

            Assert.Throws<ArgumentNullException>(() => core.Get("", translator, policy));
        }

        [Fact]
        public void GetTranslatorNullCheck_IdenticalOptimized()
        {
            IIdenticalOptimizedTranslator<int> translator = null;
            var defaultdriveMock = new Mock<ICacheDrive>();
            var defaultPolicyMock = new Mock<IExpirePolicy>();
            var core = new CacheCore(defaultdriveMock.Object, defaultPolicyMock.Object);

            Assert.Throws<ArgumentNullException>(() => core.Get("", translator));
        }

        [Fact]
        public void GetTranslatorNullCheck_IdenticalOptimizedWithCacheDrive()
        {
            IIdenticalOptimizedTranslator<int> translator = null;
            ICacheDrive drive = null;
            var defaultdriveMock = new Mock<ICacheDrive>();
            var defaultPolicyMock = new Mock<IExpirePolicy>();
            var core = new CacheCore(defaultdriveMock.Object, defaultPolicyMock.Object);

            Assert.Throws<ArgumentNullException>(() => core.Get("", translator, drive));
        }

        [Fact]
        public void GetTranslatorNullCheck_IdenticalOptimizedWithExpirePolicy()
        {
            IIdenticalOptimizedTranslator<int> translator = null;
            IExpirePolicy policy = null;
            var defaultdriveMock = new Mock<ICacheDrive>();
            var defaultPolicyMock = new Mock<IExpirePolicy>();
            var core = new CacheCore(defaultdriveMock.Object, defaultPolicyMock.Object);

            Assert.Throws<ArgumentNullException>(() => core.Get("", translator, policy));
        }

        //

        [Fact]
        public void GetTranslatorNullCheck_Bulk_Simplex()
        {
            ISimplexTranslator<int> translator = null;
            var defaultdriveMock = new Mock<ICacheDrive>();
            var defaultPolicyMock = new Mock<IExpirePolicy>();
            var core = new CacheCore(defaultdriveMock.Object, defaultPolicyMock.Object);

            Assert.Throws<ArgumentNullException>(() => core.Get(new[] { "" }, "", translator));
        }

        [Fact]
        public void GetTranslatorNullCheck_Bulk_SimplexWithCacheDrive()
        {
            ISimplexTranslator<int> translator = null;
            ICacheDrive drive = null;
            var defaultdriveMock = new Mock<ICacheDrive>();
            var defaultPolicyMock = new Mock<IExpirePolicy>();
            var core = new CacheCore(defaultdriveMock.Object, defaultPolicyMock.Object);

            Assert.Throws<ArgumentNullException>(() => core.Get(new[] { "" }, "", translator, drive));
        }

        [Fact]
        public void GetTranslatorNullCheck_Bulk_SimplexWithExpirePolicy()
        {
            ISimplexTranslator<int> translator = null;
            IExpirePolicy policy = null;
            var defaultdriveMock = new Mock<ICacheDrive>();
            var defaultPolicyMock = new Mock<IExpirePolicy>();
            var core = new CacheCore(defaultdriveMock.Object, defaultPolicyMock.Object);

            Assert.Throws<ArgumentNullException>(() => core.Get(new[] { "" }, "", translator, policy));
        }

        [Fact]
        public void GetTranslatorNullCheck_Bulk_Vectorized()
        {
            IVectorizedTranslator<int> translator = null;
            var defaultdriveMock = new Mock<ICacheDrive>();
            var defaultPolicyMock = new Mock<IExpirePolicy>();
            var core = new CacheCore(defaultdriveMock.Object, defaultPolicyMock.Object);

            Assert.Throws<ArgumentNullException>(() => core.Get(new[] { "" }, "", translator));
        }

        [Fact]
        public void GetTranslatorNullCheck_Bulk_VectorizedWithCacheDrive()
        {
            IVectorizedTranslator<int> translator = null;
            ICacheDrive drive = null;
            var defaultdriveMock = new Mock<ICacheDrive>();
            var defaultPolicyMock = new Mock<IExpirePolicy>();
            var core = new CacheCore(defaultdriveMock.Object, defaultPolicyMock.Object);

            Assert.Throws<ArgumentNullException>(() => core.Get(new[] { "" }, "", translator, drive));
        }

        [Fact]
        public void GetTranslatorNullCheck_Bulk_VectorizedWithExpirePolicy()
        {
            IVectorizedTranslator<int> translator = null;
            IExpirePolicy policy = null;
            var defaultdriveMock = new Mock<ICacheDrive>();
            var defaultPolicyMock = new Mock<IExpirePolicy>();
            var core = new CacheCore(defaultdriveMock.Object, defaultPolicyMock.Object);

            Assert.Throws<ArgumentNullException>(() => core.Get(new[] { "" }, "", translator, policy));
        }

        [Fact]
        public void GetTranslatorNullCheck_Bulk_Optimized()
        {
            IOptimizedTranslator<int> translator = null;
            var defaultdriveMock = new Mock<ICacheDrive>();
            var defaultPolicyMock = new Mock<IExpirePolicy>();
            var core = new CacheCore(defaultdriveMock.Object, defaultPolicyMock.Object);

            Assert.Throws<ArgumentNullException>(() => core.Get(new[] { "" }, "", translator));
        }

        [Fact]
        public void GetTranslatorNullCheck_Bulk_OptimizedWithCacheDrive()
        {
            IOptimizedTranslator<int> translator = null;
            ICacheDrive drive = null;
            var defaultdriveMock = new Mock<ICacheDrive>();
            var defaultPolicyMock = new Mock<IExpirePolicy>();
            var core = new CacheCore(defaultdriveMock.Object, defaultPolicyMock.Object);

            Assert.Throws<ArgumentNullException>(() => core.Get(new[] { "" }, "", translator, drive));
        }

        [Fact]
        public void GetTranslatorNullCheck_Bulk_OptimizedWithExpirePolicy()
        {
            IOptimizedTranslator<int> translator = null;
            IExpirePolicy policy = null;
            var defaultdriveMock = new Mock<ICacheDrive>();
            var defaultPolicyMock = new Mock<IExpirePolicy>();
            var core = new CacheCore(defaultdriveMock.Object, defaultPolicyMock.Object);

            Assert.Throws<ArgumentNullException>(() => core.Get(new[] { "" }, "", translator, policy));
        }

        [Fact]
        public void GetTranslatorNullCheck_Bulk_IdenticalSimplex()
        {
            IIdenticalSimplexTranslator<int> translator = null;
            var defaultdriveMock = new Mock<ICacheDrive>();
            var defaultPolicyMock = new Mock<IExpirePolicy>();
            var core = new CacheCore(defaultdriveMock.Object, defaultPolicyMock.Object);

            Assert.Throws<ArgumentNullException>(() => core.Get(new[] { "" }, translator));
        }

        [Fact]
        public void GetTranslatorNullCheck_Bulk_IdenticalSimplexWithCacheDrive()
        {
            IIdenticalSimplexTranslator<int> translator = null;
            ICacheDrive drive = null;
            var defaultdriveMock = new Mock<ICacheDrive>();
            var defaultPolicyMock = new Mock<IExpirePolicy>();
            var core = new CacheCore(defaultdriveMock.Object, defaultPolicyMock.Object);

            Assert.Throws<ArgumentNullException>(() => core.Get(new[] { "" }, translator, drive));
        }

        [Fact]
        public void GetTranslatorNullCheck_Bulk_IdenticalSimplexWithExpirePolicy()
        {
            IIdenticalSimplexTranslator<int> translator = null;
            IExpirePolicy policy = null;
            var defaultdriveMock = new Mock<ICacheDrive>();
            var defaultPolicyMock = new Mock<IExpirePolicy>();
            var core = new CacheCore(defaultdriveMock.Object, defaultPolicyMock.Object);

            Assert.Throws<ArgumentNullException>(() => core.Get(new[] { "" }, translator, policy));
        }

        [Fact]
        public void GetTranslatorNullCheck_Bulk_IdenticalVectorized()
        {
            IIdenticalVectorizedTranslator<int> translator = null;
            var defaultdriveMock = new Mock<ICacheDrive>();
            var defaultPolicyMock = new Mock<IExpirePolicy>();
            var core = new CacheCore(defaultdriveMock.Object, defaultPolicyMock.Object);

            Assert.Throws<ArgumentNullException>(() => core.Get(new[] { "" }, translator));
        }

        [Fact]
        public void GetTranslatorNullCheck_Bulk_IdenticalVectorizedWithCacheDrive()
        {
            IIdenticalVectorizedTranslator<int> translator = null;
            ICacheDrive drive = null;
            var defaultdriveMock = new Mock<ICacheDrive>();
            var defaultPolicyMock = new Mock<IExpirePolicy>();
            var core = new CacheCore(defaultdriveMock.Object, defaultPolicyMock.Object);

            Assert.Throws<ArgumentNullException>(() => core.Get(new[] { "" }, translator, drive));
        }

        [Fact]
        public void GetTranslatorNullCheck_Bulk_IdenticalVectorizedWithExpirePolicy()
        {
            IIdenticalVectorizedTranslator<int> translator = null;
            IExpirePolicy policy = null;
            var defaultdriveMock = new Mock<ICacheDrive>();
            var defaultPolicyMock = new Mock<IExpirePolicy>();
            var core = new CacheCore(defaultdriveMock.Object, defaultPolicyMock.Object);

            Assert.Throws<ArgumentNullException>(() => core.Get(new[] { "" }, translator, policy));
        }

        [Fact]
        public void GetTranslatorNullCheck_Bulk_IdenticalOptimized()
        {
            IIdenticalOptimizedTranslator<int> translator = null;
            var defaultdriveMock = new Mock<ICacheDrive>();
            var defaultPolicyMock = new Mock<IExpirePolicy>();
            var core = new CacheCore(defaultdriveMock.Object, defaultPolicyMock.Object);

            Assert.Throws<ArgumentNullException>(() => core.Get(new[] { "" }, translator));
        }

        [Fact]
        public void GetTranslatorNullCheck_Bulk_IdenticalOptimizedWithCacheDrive()
        {
            IIdenticalOptimizedTranslator<int> translator = null;
            ICacheDrive drive = null;
            var defaultdriveMock = new Mock<ICacheDrive>();
            var defaultPolicyMock = new Mock<IExpirePolicy>();
            var core = new CacheCore(defaultdriveMock.Object, defaultPolicyMock.Object);

            Assert.Throws<ArgumentNullException>(() => core.Get(new[] { "" }, translator, drive));
        }

        [Fact]
        public void GetTranslatorNullCheck_Bulk_IdenticalOptimizedWithExpirePolicy()
        {
            IIdenticalOptimizedTranslator<int> translator = null;
            IExpirePolicy policy = null;
            var defaultdriveMock = new Mock<ICacheDrive>();
            var defaultPolicyMock = new Mock<IExpirePolicy>();
            var core = new CacheCore(defaultdriveMock.Object, defaultPolicyMock.Object);

            Assert.Throws<ArgumentNullException>(() => core.Get(new[] { "" }, translator, policy));
        }

        //

        [Fact]
        public void GetLogicArgKeysNullCheck()
        {
            string[] keys = null;
            var driveMock = new Mock<ICacheDrive>();
            var policyMock = new Mock<IExpirePolicy>();

            Assert.Throws<ArgumentNullException>(() => { CacheCore.Get<string>(keys, "", (string[] _) => _, driveMock.Object, policyMock.Object); });
        }

        [Fact]
        public void GetLogicArgTranslatorIdentityNullCheck()
        {
            var driveMock = new Mock<ICacheDrive>();
            var policyMock = new Mock<IExpirePolicy>();

            Assert.Throws<ArgumentNullException>(() => { CacheCore.Get<string>(new string[] { }, null, (string[] _) => _, driveMock.Object, policyMock.Object); });
        }

        [Fact]
        public void GetLogicArgTranslatorNullCheck()
        {
            Func<string[], string[]> func = null;
            var driveMock = new Mock<ICacheDrive>();
            var policyMock = new Mock<IExpirePolicy>();

            Assert.Throws<ArgumentNullException>(() => { CacheCore.Get<string>(new string[] { }, "", func, driveMock.Object, policyMock.Object); });
        }

        [Fact]
        public void GetLogicArgCacheDriveNullCheck()
        {
            var policyMock = new Mock<IExpirePolicy>();

            Assert.Throws<ArgumentNullException>(() => { CacheCore.Get<string>(new string[] { }, "", (string[] _) => _, null, policyMock.Object); });
        }

        [Fact]
        public void GetLogicArgExpirePolicyNullCheck()
        {
            var driveMock = new Mock<ICacheDrive>();

            Assert.Throws<ArgumentNullException>(() => { CacheCore.Get<string>(new string[] { }, "", (string[] _) => _, driveMock.Object, null); });
        }

        // Logic Test

        /// <summary>
        ///<see cref="ICacheDrive" />や<see cref="IExpirePolicy" />が例外を出した場合は、<see cref="ITranslator{T}"/> />から値を取得しそれを返します。
        /// </summary>
        [Fact]
        public void RecoverWhenPolicyThrowExption_Simplex()
        {
            var driveMock = new Mock<ICacheDrive>();
            driveMock.Setup(_ => _.Contains("one" + "identity")).Returns(true);
            driveMock.Setup(_ => _.Get<int>("one" + "identity")).Returns(new CacheEntry<int>() { Key = "one", Value = 1234567 });

            var policyMock = new Mock<IExpirePolicy>();
            policyMock.Setup(_ => _.Validate(It.IsAny<CacheEntry<int>>())).Throws<Exception>();

            var result = CacheCore.Get("one", "identity", new KeyLengthSimplexTranslator().Get, driveMock.Object, policyMock.Object);

            Assert.Equal(3, result); //ポリシーの検証で落ちた場合は、新規取得される
            policyMock.Verify(_ => _.Validate(It.IsAny<CacheEntry<int>>())); // throwsが呼ばれたか
        }

        /// <summary>
        ///<see cref="ICacheDrive" />や<see cref="IExpirePolicy" />が例外を出した場合は、<see cref="ITranslator{T}"/> />から値を取得しそれを返します。
        /// </summary>
        [Fact]
        public void RecoverWhenPolicyThrowExption_Bulk_Simplex()
        {
            var driveMock = new Mock<ICacheDrive>();
            driveMock.Setup(_ => _.Contains("one" + "identity")).Returns(true);
            driveMock.Setup(_ => _.Get<int>("one" + "identity")).Returns(new CacheEntry<int>() { Key = "one", Value = 1234567 });

            var policyMock = new Mock<IExpirePolicy>();
            policyMock.Setup(_ => _.Validate(It.IsAny<CacheEntry<int>>())).Throws<Exception>();

            var result = CacheCore.Get(new[] { "one" }, "identity", new KeyLengthSimplexTranslator().Get, driveMock.Object, policyMock.Object);

            Assert.Equal(new[] { 3 }, result); //ポリシーの検証で落ちた場合は、新規取得される
            policyMock.Verify(_ => _.Validate(It.IsAny<CacheEntry<int>>())); // throwsが呼ばれたか
        }

        /// <summary>
        ///<see cref="ICacheDrive" />や<see cref="IExpirePolicy" />が例外を出した場合は、<see cref="ITranslator{T}"/> />から値を取得しそれを返します。
        /// </summary>
        [Fact]
        public void RecoverWhenPolicyThrowExption_Vectorized()
        {
            var driveMock = new Mock<ICacheDrive>();
            driveMock.Setup(_ => _.Contains("one" + "identity")).Returns(true);
            driveMock.Setup(_ => _.Get<int>("one" + "identity")).Returns(new CacheEntry<int>() { Key = "one", Value = 1234567 });

            var policyMock = new Mock<IExpirePolicy>();
            policyMock.Setup(_ => _.Validate(It.IsAny<CacheEntry<int>>())).Throws<Exception>();

            var result = CacheCore.Get("one", "identity", new KeyLengthVectorizedTranslator().Get, driveMock.Object, policyMock.Object);

            Assert.Equal(3, result); //ポリシーの検証で落ちた場合は、新規取得される
            policyMock.Verify(_ => _.Validate(It.IsAny<CacheEntry<int>>())); // throwsが呼ばれたか
        }

        /// <summary>
        ///<see cref="ICacheDrive" />や<see cref="IExpirePolicy" />が例外を出した場合は、<see cref="ITranslator{T}"/> />から値を取得しそれを返します。
        /// </summary>
        [Fact]
        public void RecoverWhenPolicyThrowExption_Bulk_Vectorized()
        {
            var driveMock = new Mock<ICacheDrive>();
            driveMock.Setup(_ => _.Contains("one" + "identity")).Returns(true);
            driveMock.Setup(_ => _.Get<int>("one" + "identity")).Returns(new CacheEntry<int>() { Key = "one", Value = 1234567 });

            var policyMock = new Mock<IExpirePolicy>();
            policyMock.Setup(_ => _.Validate(It.IsAny<CacheEntry<int>>())).Throws<Exception>();

            var result = CacheCore.Get(new[] { "one" }, "identity", new KeyLengthVectorizedTranslator().Get, driveMock.Object, policyMock.Object);

            Assert.Equal(new[] { 3 }, result); //ポリシーの検証で落ちた場合は、新規取得される
            policyMock.Verify(_ => _.Validate(It.IsAny<CacheEntry<int>>())); // throwsが呼ばれたか
        }

        //

        /// <summary>
        ///<see cref="ICacheDrive" />や<see cref="IExpirePolicy" />が例外を出した場合は、<see cref="ITranslator{T}"/> />から値を取得しそれを返します。
        /// </summary>
        [Fact]
        public void RecoverWhenCacheDriveThrowExption_Simplex()
        {
            var driveMock = new Mock<ICacheDrive>();
            driveMock.Setup(_ => _.Contains("one" + "identity")).Returns(true);
            driveMock.Setup(_ => _.Get<int>("one" + "identity")).Returns(new CacheEntry<int>() { Key = "one", Value = 1234567 });
            driveMock.Setup(_ => _.AddOrUpdate(It.IsAny<CacheEntry<int>>())).Throws<Exception>();


            var policyMock = new Mock<IExpirePolicy>();
            policyMock.Setup(_ => _.Validate(It.IsAny<CacheEntry<int>>())).Returns(false);

            var result = CacheCore.Get("one", "identity", new KeyLengthSimplexTranslator().Get, driveMock.Object, policyMock.Object);

            Assert.Equal(3, result); //ストレージのエントリの更新で落ちた場合は、新規取得される
            driveMock.Verify(_ => _.AddOrUpdate(It.IsAny<CacheEntry<int>>())); // throwsが呼ばれたか
        }

        /// <summary>
        ///<see cref="ICacheDrive" />や<see cref="IExpirePolicy" />が例外を出した場合は、<see cref="ITranslator{T}"/> />から値を取得しそれを返します。
        /// </summary>
        [Fact]
        public void RecoverWhenCacheDriveThrowExption_Bulk_Simplex()
        {
            var driveMock = new Mock<ICacheDrive>();
            driveMock.Setup(_ => _.Contains("one" + "identity")).Returns(true);
            driveMock.Setup(_ => _.Get<int>("one" + "identity")).Returns(new CacheEntry<int>() { Key = "one", Value = 1234567 });
            driveMock.Setup(_ => _.AddOrUpdate(It.IsAny<CacheEntry<int>>())).Throws<Exception>();


            var policyMock = new Mock<IExpirePolicy>();
            policyMock.Setup(_ => _.Validate(It.IsAny<CacheEntry<int>>())).Returns(false);

            var result = CacheCore.Get(new[] { "one" }, "identity", new KeyLengthSimplexTranslator().Get, driveMock.Object, policyMock.Object);

            Assert.Equal(new[] { 3 }, result); //ストレージのエントリの更新で落ちた場合は、新規取得される
            driveMock.Verify(_ => _.AddOrUpdate(It.IsAny<CacheEntry<int>>())); // throwsが呼ばれたか
        }

        /// <summary>
        ///<see cref="ICacheDrive" />や<see cref="IExpirePolicy" />が例外を出した場合は、<see cref="ITranslator{T}"/> />から値を取得しそれを返します。
        /// </summary>
        [Fact]
        public void RecoverWhenCacheDriveThrowExption_Vectorized()
        {
            var driveMock = new Mock<ICacheDrive>();
            driveMock.Setup(_ => _.Contains("one" + "identity")).Returns(true);
            driveMock.Setup(_ => _.Get<int>("one" + "identity")).Returns(new CacheEntry<int>() { Key = "one", Value = 1234567 });
            driveMock.Setup(_ => _.AddOrUpdate(It.IsAny<CacheEntry<int>>())).Throws<Exception>();


            var policyMock = new Mock<IExpirePolicy>();
            policyMock.Setup(_ => _.Validate(It.IsAny<CacheEntry<int>>())).Returns(false);

            var result = CacheCore.Get("one", "identity", new KeyLengthVectorizedTranslator().Get, driveMock.Object, policyMock.Object);

            Assert.Equal(3, result); //ストレージのエントリの更新で落ちた場合は、新規取得される
            driveMock.Verify(_ => _.AddOrUpdate(It.IsAny<CacheEntry<int>>())); // throwsが呼ばれたか
        }

        /// <summary>
        ///<see cref="ICacheDrive" />や<see cref="IExpirePolicy" />が例外を出した場合は、<see cref="ITranslator{T}"/> />から値を取得しそれを返します。
        /// </summary>
        [Fact]
        public void RecoverWhenCacheDriveThrowExption_Bulk_Vectorized()
        {
            var driveMock = new Mock<ICacheDrive>();
            driveMock.Setup(_ => _.Contains("one" + "identity")).Returns(true);
            driveMock.Setup(_ => _.Get<int>("one" + "identity")).Returns(new CacheEntry<int>() { Key = "one", Value = 1234567 });
            driveMock.Setup(_ => _.AddOrUpdate(It.IsAny<CacheEntry<int>>())).Throws<Exception>();


            var policyMock = new Mock<IExpirePolicy>();
            policyMock.Setup(_ => _.Validate(It.IsAny<CacheEntry<int>>())).Returns(false);

            var result = CacheCore.Get(new[] { "one" }, "identity", new KeyLengthVectorizedTranslator().Get, driveMock.Object, policyMock.Object);

            Assert.Equal(new[] { 3 }, result); //ストレージのエントリの更新で落ちた場合は、新規取得される
            driveMock.Verify(_ => _.AddOrUpdate(It.IsAny<CacheEntry<int>>())); // throwsが呼ばれたか
        }

        //

        /// <summary>
        /// キーに対応するエントリがキャッシュに存在した場合は<see cref="IExpirePolicy" />でエントリの有効性を検証後、有効でない場合は新規取得され、既存のキャッシュエントリがアップデートされます。
        /// </summary>
        [Fact]
        public void SingleEntryUpdate_Simplex()
        {
            var driveMock = new Mock<ICacheDrive>();
            driveMock.Setup(_ => _.Contains("one" + "identity")).Returns(true);
            driveMock.Setup(_ => _.Get<int>("one" + "identity")).Returns(new CacheEntry<int>() { Key = "one", Value = 11111 });
            driveMock.Setup(_ => _.AddOrUpdate(It.IsAny<CacheEntry<int>>()));

            var defaultPolicyMock = new Mock<IExpirePolicy>();
            defaultPolicyMock.Setup(_ => _.Validate(It.Is<CacheEntry<int>>(e => e.Key == "one"))).Returns(false);

            var result = CacheCore.Get("one", "identity", new KeyLengthSimplexTranslator().Get, driveMock.Object, defaultPolicyMock.Object);
            Assert.Equal(3, result);
        }

        /// <summary>
        /// キーに対応するエントリがキャッシュに存在した場合は<see cref="IExpirePolicy" />でエントリの有効性を検証後、有効でない場合は新規取得され、既存のキャッシュエントリがアップデートされます。
        /// </summary>
        [Fact]
        public void SingleEntryUpdate_Vectorized()
        {
            var driveMock = new Mock<ICacheDrive>();
            driveMock.Setup(_ => _.Contains("one" + "identity")).Returns(true);
            driveMock.Setup(_ => _.Get<int>("one" + "identity")).Returns(new CacheEntry<int>() { Key = "one", Value = 11111 });
            driveMock.Setup(_ => _.AddOrUpdate(It.IsAny<CacheEntry<int>>()));

            var defaultPolicyMock = new Mock<IExpirePolicy>();
            defaultPolicyMock.Setup(_ => _.Validate(It.Is<CacheEntry<int>>(e => e.Key == "one"))).Returns(false);

            var result = CacheCore.Get("one", "identity", new KeyLengthVectorizedTranslator().Get, driveMock.Object, defaultPolicyMock.Object);
            Assert.Equal(3, result);
        }

        /// <summary>
        /// キーに対応するエントリがキャッシュに存在した場合は<see cref="IExpirePolicy" />でエントリの有効性を検証後、有効でない場合は新規取得され、既存のキャッシュエントリがアップデートされます。
        /// </summary>
        [Fact]
        public void BulkEntryUpdateOrderd_Simplex()
        {
            var driveMock = new Mock<ICacheDrive>();
            driveMock.Setup(_ => _.Contains("one" + "identity")).Returns(true);
            driveMock.Setup(_ => _.Contains("two" + "identity")).Returns(true);
            driveMock.Setup(_ => _.Contains("three" + "identity")).Returns(true);
            driveMock.Setup(_ => _.Contains("four" + "identity")).Returns(true);
            driveMock.Setup(_ => _.Contains("five" + "identity")).Returns(true);

            driveMock.Setup(_ => _.Get<int>("one" + "identity")).Returns(new CacheEntry<int>() { Key = "one", Value = 11111 });
            driveMock.Setup(_ => _.Get<int>("two" + "identity")).Returns(new CacheEntry<int>() { Key = "two", Value = 22222 });
            driveMock.Setup(_ => _.Get<int>("three" + "identity")).Returns(new CacheEntry<int>() { Key = "three", Value = 33333 });
            driveMock.Setup(_ => _.Get<int>("four" + "identity")).Returns(new CacheEntry<int>() { Key = "four", Value = 44444 });
            driveMock.Setup(_ => _.Get<int>("five" + "identity")).Returns(new CacheEntry<int>() { Key = "five", Value = 55555 });

            driveMock.Setup(_ => _.AddOrUpdate(It.IsAny<CacheEntry<int>>()));


            var policyMock = new Mock<IExpirePolicy>();
            policyMock.Setup(_ => _.Validate(It.Is<CacheEntry<int>>(e => e.Key == "one"))).Returns(false);
            policyMock.Setup(_ => _.Validate(It.Is<CacheEntry<int>>(e => e.Key == "two"))).Returns(true);
            policyMock.Setup(_ => _.Validate(It.Is<CacheEntry<int>>(e => e.Key == "three"))).Returns(false);
            policyMock.Setup(_ => _.Validate(It.Is<CacheEntry<int>>(e => e.Key == "four"))).Returns(true);
            policyMock.Setup(_ => _.Validate(It.Is<CacheEntry<int>>(e => e.Key == "five"))).Returns(false);


            var result = CacheCore.Get(new[] { "one", "two", "three", "four", "five" }, "identity", new KeyLengthSimplexTranslator().Get, driveMock.Object, policyMock.Object);
            Assert.Equal(new[] { 3, 22222, 5, 44444, 4 }, result);//更新が交互に入っていても順序が保たれているか
        }

        /// <summary>
        /// キーに対応するエントリがキャッシュに存在した場合は<see cref="IExpirePolicy" />でエントリの有効性を検証後、有効でない場合は新規取得され、既存のキャッシュエントリがアップデートされます。
        /// </summary>
        [Fact]
        public void BulkEntryUpdateOrderd_Vectorized()
        {
            var driveMock = new Mock<ICacheDrive>();
            driveMock.Setup(_ => _.Contains("one" + "identity")).Returns(true);
            driveMock.Setup(_ => _.Contains("two" + "identity")).Returns(true);
            driveMock.Setup(_ => _.Contains("three" + "identity")).Returns(true);
            driveMock.Setup(_ => _.Contains("four" + "identity")).Returns(true);
            driveMock.Setup(_ => _.Contains("five" + "identity")).Returns(true);

            driveMock.Setup(_ => _.Get<int>("one" + "identity")).Returns(new CacheEntry<int>() { Key = "one", Value = 11111 });
            driveMock.Setup(_ => _.Get<int>("two" + "identity")).Returns(new CacheEntry<int>() { Key = "two", Value = 22222 });
            driveMock.Setup(_ => _.Get<int>("three" + "identity")).Returns(new CacheEntry<int>() { Key = "three", Value = 33333 });
            driveMock.Setup(_ => _.Get<int>("four" + "identity")).Returns(new CacheEntry<int>() { Key = "four", Value = 44444 });
            driveMock.Setup(_ => _.Get<int>("five" + "identity")).Returns(new CacheEntry<int>() { Key = "five", Value = 55555 });

            driveMock.Setup(_ => _.AddOrUpdate(It.IsAny<CacheEntry<int>>()));


            var policyMock = new Mock<IExpirePolicy>();
            policyMock.Setup(_ => _.Validate(It.Is<CacheEntry<int>>(e => e.Key == "one"))).Returns(false);
            policyMock.Setup(_ => _.Validate(It.Is<CacheEntry<int>>(e => e.Key == "two"))).Returns(true);
            policyMock.Setup(_ => _.Validate(It.Is<CacheEntry<int>>(e => e.Key == "three"))).Returns(false);
            policyMock.Setup(_ => _.Validate(It.Is<CacheEntry<int>>(e => e.Key == "four"))).Returns(true);
            policyMock.Setup(_ => _.Validate(It.Is<CacheEntry<int>>(e => e.Key == "five"))).Returns(false);


            var result = CacheCore.Get("one", "identity", new KeyLengthVectorizedTranslator().Get, driveMock.Object, policyMock.Object);
            Assert.Equal(3, result);//更新が交互に入っていても順序が保たれているか
        }

        //

        /// <summary>
        /// キーに対応するエントリがキャッシュに存在しない場合は新規取得され、キャッシュに追加されます。
        /// </summary>
        [Fact]
        public void SingleEntryFetchOrderd_Simplex()
        {
            var driveMock = new Mock<ICacheDrive>();
            driveMock.Setup(_ => _.Contains("one" + "identity")).Returns(false);
            driveMock.Setup(_ => _.Get<int>("five" + "identity")).Returns(new CacheEntry<int>() { Key = "one", Value = 11111 });
            driveMock.Setup(_ => _.AddOrUpdate(It.IsAny<CacheEntry<int>>()));

            var policyMock = new Mock<IExpirePolicy>();
            policyMock.Setup(_ => _.Validate(It.Is<CacheEntry<int>>(e => e.Key == "one"))).Returns(true);


            var result = CacheCore.Get("one", "identity", new KeyLengthSimplexTranslator().Get, driveMock.Object, policyMock.Object);
            Assert.Equal(3, result);//新規取得が交互に入っていても順序が保たれているか
            driveMock.Verify(_ => _.Get<int>("one" + "identity"), Times.Never());
        }

        /// <summary>
        /// キーに対応するエントリがキャッシュに存在しない場合は新規取得され、キャッシュに追加されます。
        /// </summary>
        [Fact]
        public void SignleEntryFetchOrderd_Vectorized()
        {
            var driveMock = new Mock<ICacheDrive>();
            driveMock.Setup(_ => _.Contains("one" + "identity")).Returns(false);
            driveMock.Setup(_ => _.Get<int>("one" + "identity")).Returns(new CacheEntry<int>() { Key = "one", Value = 11111 });
            driveMock.Setup(_ => _.AddOrUpdate(It.IsAny<CacheEntry<int>>()));

            var policyMock = new Mock<IExpirePolicy>();
            policyMock.Setup(_ => _.Validate(It.Is<CacheEntry<int>>(e => e.Key == "one"))).Returns(true);


            var result = CacheCore.Get("one", "identity", new KeyLengthVectorizedTranslator().Get, driveMock.Object, policyMock.Object);
            Assert.Equal(3, result);//新規取得が交互に入っていても順序が保たれているか
            driveMock.Verify(_ => _.Get<int>("one" + "identity"), Times.Never());
        }

        /// <summary>
        /// 更新対象のエントリ、更新対象でないエントリが同時に存在する場合は、更新対象のエントリのみが新規取得されます。
        /// </summary>
        [Fact]
        public void BulkEntryFetchOrderd_Simplex()
        {
            var driveMock = new Mock<ICacheDrive>();
            driveMock.Setup(_ => _.Contains("one" + "identity")).Returns(false);
            driveMock.Setup(_ => _.Contains("two" + "identity")).Returns(true);
            driveMock.Setup(_ => _.Contains("three" + "identity")).Returns(false);
            driveMock.Setup(_ => _.Contains("four" + "identity")).Returns(true);
            driveMock.Setup(_ => _.Contains("five" + "identity")).Returns(false);

            driveMock.Setup(_ => _.Get<int>("one" + "identity")).Returns(new CacheEntry<int> { Key = "one", Value = 11111 });
            driveMock.Setup(_ => _.Get<int>("two" + "identity")).Returns(new CacheEntry<int> { Key = "two", Value = 22222 });
            driveMock.Setup(_ => _.Get<int>("three" + "identity")).Returns(new CacheEntry<int> { Key = "three", Value = 33333 });
            driveMock.Setup(_ => _.Get<int>("four" + "identity")).Returns(new CacheEntry<int> { Key = "four", Value = 44444 });
            driveMock.Setup(_ => _.Get<int>("five" + "identity")).Returns(new CacheEntry<int> { Key = "five", Value = 55555 });
            driveMock.Setup(_ => _.AddOrUpdate(It.IsAny<CacheEntry<int>>()));


            var policyMock = new Mock<IExpirePolicy>();
            policyMock.Setup(_ => _.Validate(It.IsAny<CacheEntry<int>>())).Returns(true);


            var result = CacheCore.Get(new[] { "one", "two", "three", "four", "five" }, "identity", new KeyLengthSimplexTranslator().Get, driveMock.Object, policyMock.Object);
            Assert.Equal(new[] { 3, 22222, 5, 44444, 4 }, result);//新規取得が交互に入っていても順序が保たれているか
            driveMock.Verify(_ => _.Get<int>("one" + "identity"), Times.Never());
            driveMock.Verify(_ => _.Get<int>("three" + "identity"), Times.Never());
            driveMock.Verify(_ => _.Get<int>("five" + "identity"), Times.Never());
        }

        /// <summary>
        /// 更新対象のエントリ、更新対象でないエントリが同時に存在する場合は、更新対象のエントリのみが新規取得されます。
        /// </summary>
        [Fact]
        public void BulkEntryFetchOrderd_Vectorized()
        {
            var driveMock = new Mock<ICacheDrive>();
            driveMock.Setup(_ => _.Contains("one" + "identity")).Returns(false);
            driveMock.Setup(_ => _.Contains("two" + "identity")).Returns(true);
            driveMock.Setup(_ => _.Contains("three" + "identity")).Returns(false);
            driveMock.Setup(_ => _.Contains("four" + "identity")).Returns(true);
            driveMock.Setup(_ => _.Contains("five" + "identity")).Returns(false);

            driveMock.Setup(_ => _.Get<int>("one" + "identity")).Returns(new CacheEntry<int> { Key = "one", Value = 11111 });
            driveMock.Setup(_ => _.Get<int>("two" + "identity")).Returns(new CacheEntry<int> { Key = "two", Value = 22222 });
            driveMock.Setup(_ => _.Get<int>("three" + "identity")).Returns(new CacheEntry<int> { Key = "three", Value = 33333 });
            driveMock.Setup(_ => _.Get<int>("four" + "identity")).Returns(new CacheEntry<int> { Key = "four", Value = 44444 });
            driveMock.Setup(_ => _.Get<int>("five" + "identity")).Returns(new CacheEntry<int> { Key = "five", Value = 55555 });
            driveMock.Setup(_ => _.AddOrUpdate(It.IsAny<CacheEntry<int>>()));


            var policyMock = new Mock<IExpirePolicy>();
            policyMock.Setup(_ => _.Validate(It.IsAny<CacheEntry<int>>())).Returns(true);


            var result = CacheCore.Get(new[] { "one", "two", "three", "four", "five" }, "identity", new KeyLengthVectorizedTranslator().Get, driveMock.Object, policyMock.Object);
            Assert.Equal(new[] { 3, 22222, 5, 44444, 4 }, result);//新規取得が交互に入っていても順序が保たれているか
            driveMock.Verify(_ => _.Get<int>("one" + "identity"), Times.Never());
            driveMock.Verify(_ => _.Get<int>("three" + "identity"), Times.Never());
            driveMock.Verify(_ => _.Get<int>("five" + "identity"), Times.Never());
        }

        //

        /// <summary>
        /// 同じ機能を持つトランスレータを使用した場合でも、トランスレータ識別子が異なる場合はキャッシュのエントリは異なるものとして扱われます。
        /// </summary>
        [Fact]
        public void IdentityIsWorking()
        {
            var driveMock = new Mock<ICacheDrive>();
            driveMock.Setup(_ => _.Contains("one" + "identity")).Returns(true);
            driveMock.Setup(_ => _.Contains("one" + "identity_another")).Returns(true);
            driveMock.Setup(_ => _.Get<int>("one" + "identity")).Returns(new CacheEntry<int>() { Key = "one", Value = 11111 });
            driveMock.Setup(_ => _.Get<int>("one" + "identity_another")).Returns(new CacheEntry<int>() { Key = "one", Value = 22222 });

            var policyMock = new Mock<IExpirePolicy>();
            policyMock.Setup(_ => _.Validate(It.IsAny<CacheEntry<int>>())).Returns(true);

            var result1 = CacheCore.Get("one", "identity", new KeyLengthSimplexTranslator().Get, driveMock.Object, policyMock.Object);
            Assert.Equal(11111, result1);

            var result2 = CacheCore.Get("one", "identity_another", new KeyLengthSimplexTranslator().Get, driveMock.Object, policyMock.Object);
            Assert.Equal(22222, result2);
        }
    }
}
