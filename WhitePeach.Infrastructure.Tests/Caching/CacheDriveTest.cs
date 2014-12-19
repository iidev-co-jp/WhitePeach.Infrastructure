using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using WhitePeach.Infrastructure.Caching;
using Xunit;

namespace WhitePeach.Infrastructure.Tests.Caching
{
    public class CacheDriveTest
    {
        // Error Test

        [Fact]
        public void SerializerNullCheck()
        {
            var storageMock = new Mock<ICacheStorage<int>>();
            Assert.Throws<ArgumentNullException>(() => new CacheDrive<int>(null, storageMock.Object));
        }

        [Fact]
        public void StorageNullCheck()
        {
            var serializerMock = new Mock<ICacheSerializer<int>>();
            Assert.Throws<ArgumentNullException>(() => new CacheDrive<int>(serializerMock.Object, null));
        }

        //

        [Fact]
        public void Get()
        {
            var storageMock = new Mock<ICacheStorage<string>>();
            storageMock.Setup(_ => _.Get("key")).Returns((new CacheEntry<string> { Value = "got" }));
            var serializerMock = new Mock<ICacheSerializer<string>>();
            serializerMock.Setup(_ => _.Deserialize<string>("got")).Returns("got");

            var drive = new CacheDrive<string>(serializerMock.Object, storageMock.Object);
            var result = drive.Get<string>("key");
            Assert.Equal("got", result.Value);
        }
    }
}
