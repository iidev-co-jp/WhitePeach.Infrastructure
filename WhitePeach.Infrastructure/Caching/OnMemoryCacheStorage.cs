using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhitePeach.Infrastructure.Caching
{
    /// <summary>
    /// すべてのキャッシュをメモリ上に任意の形式で格納するキャッシュストレージです。
    /// </summary>
    /// <typeparam name="T">キャッシュ格納時の型</typeparam>
    public class OnMemoryCacheStorage<T> : ICacheStorage<T>
    {
        private readonly ConcurrentDictionary<string, Tuple<object, DateTime>> _storage = new ConcurrentDictionary<string, Tuple<object, DateTime>>();

        /// <summary>
        /// キーから <see cref="WhitePeach.Infrastructure.Caching.CacheEntry&lt;T&gt;" /> を取得します。 
        /// </summary>
        /// <param name="key"><see cref="WhitePeach.Infrastructure.Caching.CacheEntry&lt;T&gt;" /> を取得するためのキー</param>
        /// <returns>
        /// キーに一致した <see cref="WhitePeach.Infrastructure.Caching.CacheEntry&lt;T&gt;" />
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public CacheEntry<T> Get(string key)
        {
            return new CacheEntry<T> { Key = key, Value = (T)_storage[key].Item1, UpdateTime = _storage[key].Item2 };
        }

        /// <summary>
        /// キーに一致するキャッシュが格納されているか判断します。
        /// </summary>
        /// <param name="key"><see cref="WhitePeach.Infrastructure.Caching.ICacheStorage&lt;T&gt;" />内で検索するキー</param>
        /// <returns>
        /// キーに対応するキャッシュが <see cref="WhitePeach.Infrastructure.Caching.ICacheStorage&lt;T&gt;" /> に存在する場合は true。それ以外の場合は false。
        /// </returns>
        public bool Contains(string key)
        {
            return _storage.ContainsKey(key);
        }

        /// <summary>
        /// 指定した <see cref="WhitePeach.Infrastructure.Caching.CacheEntry&lt;T&gt;"/> が存在しなければ追加、存在すれば現在のエントリでアップデートします。
        /// </summary>
        /// <param name="entry">追加または更新するエントリ</param>
        public void AddOrUpdate(CacheEntry<T> entry)
        {
            var val = Tuple.Create((object)entry.Value, entry.UpdateTime);
            _storage.AddOrUpdate(entry.Key, key => val, (key, existValue) => val);

        }

        /// <summary>
        /// キーに一致するキャッシュを削除します。
        /// </summary>
        /// <param name="key">キャッシュを検索するためのキー</param>
        /// <returns>削除が成功した場合はtrue。失敗または見つからなかった場合はfalse。</returns>
        public bool Remove(string key)
        {
            Tuple<object, DateTime> entry;
            return _storage.TryRemove(key, out entry);
        }
    }
}
