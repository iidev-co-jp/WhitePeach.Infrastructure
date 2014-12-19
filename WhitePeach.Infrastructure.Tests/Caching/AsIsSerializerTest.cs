using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhitePeach.Infrastructure.Caching;
using Xunit;

namespace WhitePeach.Infrastructure.Tests.Caching
{
    public class AsIsSerializerTest
    {
        class Sample
        {
            public int Value { get; set; }
        }

        [Fact]
        public void SerializePrimitiveType()
        {
            var serializer = new AsIsSerializer();
            var result = serializer.Serialize<int>(2000);
            Assert.Equal(2000, result);
        }

        [Fact]
        public void SerializeReferenceType()
        {
            var serializer = new AsIsSerializer();
            var data = new Sample { Value = int.MaxValue };
            var result = serializer.Serialize<Sample>(data);
            Assert.Same(data, result); //AsIsはそのまま値を返すので、アドレスは同じになる
        }

        [Fact]
        public void DeserializePrimitiveType()
        {
            var serializer = new AsIsSerializer();
            var result = serializer.Deserialize<int>(2000);
            Assert.Equal(2000, result);
        }

        [Fact]
        public void DeserializeReferenceType()
        {
            var serializer = new AsIsSerializer();
            var data = new Sample { Value = int.MaxValue };
            var result = serializer.Deserialize<Sample>(data);
            Assert.Same(data, result); //AsIsはそのまま値を返すので、アドレスは同じになる
        }

        // Error Test

        [Fact]
        public void InvalidTypeConvert()
        {
            var serializer = new AsIsSerializer();
            Assert.Throws<InvalidOperationException>(() => serializer.Deserialize<int>(new Sample()));
        }

        [Fact]
        public void SerializeNullCheck()
        {
            var serializer = new AsIsSerializer();
            Assert.Throws<ArgumentNullException>(() => serializer.Serialize<Sample>(null));
        }

        [Fact]
        public void DeserializeNullCheck()
        {
            var serializer = new AsIsSerializer();
            Assert.Throws<ArgumentNullException>(() => serializer.Deserialize<Sample>(null));
        }
    }
}
