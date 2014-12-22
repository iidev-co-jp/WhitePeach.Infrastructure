using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhitePeach.Infrastructure.Caching;
using Xunit;

namespace WhitePeach.Infrastructure.Tests.Caching
{
    public class CacheStorageTest
    {
        //ここにStorageの生成関数を追加する
        Func<ICacheStorage<int>>[] StorageGenerator = new Func<ICacheStorage<int>>[] { () => new OnMemoryCacheStorage<int>() };

        [Fact]
        public void GetValueAndDate()
        {
            foreach (var gen in StorageGenerator)
            {
                GetValueAndDateCore(gen());
            }
        }

        public void GetValueAndDateCore(ICacheStorage<int> storage)
        {
            var now = DateTime.Now;
            storage.AddOrUpdate(new CacheEntry<int>() { Key = "get", Value = 12345, UpdateTime = now });
            var entry = storage.Get("get");

            //シリアライズされるためAssert.Sameで比較しない
            Assert.Equal(now, entry.UpdateTime);
            Assert.Equal(12345, entry.Value);
        }

        /// <summary>
        /// キーがない場合にGetしたら例外を出すべき
        /// </summary>
        [Fact]
        public void GetFailOnNotContainsKey()
        {
            foreach (var gen in StorageGenerator)
            {
                GetFailOnNotContainsKeyCore(gen());
            }
        }

        public void GetFailOnNotContainsKeyCore(ICacheStorage<int> storage)
        {
            Assert.Throws<KeyNotFoundException>(() => storage.Get("throw"));
        }

        [Fact]
        public void ContainsKey()
        {
            foreach (var gen in StorageGenerator)
            {
                ContainsKeyCore(gen());
            }
        }

        public void ContainsKeyCore(ICacheStorage<int> storage)
        {
            storage.AddOrUpdate(new CacheEntry<int>() { Key = "contains" });

            Assert.True(storage.Contains("contains"));
        }

        [Fact]
        public void NotContainsKey()
        {
            foreach (var gen in StorageGenerator)
            {
                NotContainsKeyCore(gen());
            }
        }

        public void NotContainsKeyCore(ICacheStorage<int> storage)
        {
            storage.AddOrUpdate(new CacheEntry<int>() { Key = "contains" });

            Assert.False(storage.Contains("not contains"));
        }

        [Fact]
        public void RemoveContaisKey()
        {
            foreach (var gen in StorageGenerator)
            {
                RemoveContaisKeyCore(gen());
            }
        }

        public void RemoveContaisKeyCore(ICacheStorage<int> storage)
        {
            storage.AddOrUpdate(new CacheEntry<int> { Key = "remove" });

            Assert.True(storage.Remove("remove"));
        }

        [Fact]
        public void RemoveNotContaisKey()
        {
            foreach (var gen in StorageGenerator)
            {
                RemoveNotContaisKeyCore(gen());
            }
        }

        public void RemoveNotContaisKeyCore(ICacheStorage<int> storage)
        {
            storage.AddOrUpdate(new CacheEntry<int> { Key = "remove" });

            Assert.False(storage.Remove("not contains"));
        }

        [Fact]
        public void UpdateEntry()
        {
            foreach (var gen in StorageGenerator)
            {
                UpdateEntryCore(gen());
            }
        }

        public void UpdateEntryCore(ICacheStorage<int> storage)
        {
            var now = DateTime.Now;
            storage.AddOrUpdate(new CacheEntry<int> { Key = "update", Value = 1, UpdateTime = now });
            storage.AddOrUpdate(new CacheEntry<int> { Key = "update", Value = 2, UpdateTime = now.AddTicks(1) });

            var updatedEntry = storage.Get("update");

            Assert.Equal(2, updatedEntry.Value);
            Assert.Equal(now.AddTicks(1), updatedEntry.UpdateTime);
        }

        /// <summary>
        /// 異なるキーが異なるエントリに干渉しない
        /// </summary>
        [Fact]
        public void NoInterpositionOnUpdateEntry()
        {
            foreach (var gen in StorageGenerator)
            {
                NoInterpositionOnUpdateEntryCore(gen());
            }
        }

        public void NoInterpositionOnUpdateEntryCore(ICacheStorage<int> storage)
        {
            storage.AddOrUpdate(new CacheEntry<int>() { Key = "key1", Value = 111 });
            storage.AddOrUpdate(new CacheEntry<int>() { Key = "key2", Value = 222 });
           
            var entry = storage.Get("key1");
            var entry2 = storage.Get("key2");

            Assert.Equal(111, entry.Value);
            Assert.Equal(222, entry2.Value);
        }

    }
}
