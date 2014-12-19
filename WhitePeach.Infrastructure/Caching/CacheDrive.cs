using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhitePeach.Infrastructure.Caching
{
    /// <summary>
    /// <see cref="WhitePeach.Infrastructure.Caching.ICacheSerializer&lt;T&gt;" /> と
    /// <see cref="WhitePeach.Infrastructure.Caching.CacheDrive&lt;T&gt;" /> を使用してキャッシュをの保存を行います。
    /// </summary>
    /// <typeparam name="T"><see cref="WhitePeach.Infrastructure.Caching.CacheEntry&lt;T&gt;" /></typeparam>
    public class CacheDrive<T> : ICacheDrive
    {
        readonly ICacheSerializer<T> _serializer;

        readonly ICacheStorage<T> _cacheStorage;

        /// <summary>
        /// シリアライザとストレージを使用して、<see cref="WhitePeach.Infrastructure.Caching.CacheDrive&lt;T&gt;" /> の新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="serializer">キャッシュを保存できるように変換するシリアライザ</param>
        /// <param name="cacheStorage">変換されたキャッシュを保存するストレージ</param>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="serializer"/>または<paramref name="cacheStorage"/>がnullです。 </exception>
        public CacheDrive(ICacheSerializer<T> serializer, ICacheStorage<T> cacheStorage)
        {
            if (serializer == null) throw new ArgumentNullException("serializer");
            if (cacheStorage == null) throw new ArgumentNullException("cacheStorage");
            _serializer = serializer;
            _cacheStorage = cacheStorage;
        }

        /// <summary>
        /// キーから <see cref="WhitePeach.Infrastructure.Caching.CacheEntry&lt;T&gt;"/> を取得します。
        /// キーに一致するものがなかった場合の動作は保証されません。
        /// </summary>
        /// <typeparam name="V">キャッシュ対象の型</typeparam>
        /// <param name="key">キャッシュを取得するためのキー</param>
        /// <returns>キーに対応するエントリ</returns>
        public CacheEntry<V> Get<V>(string key)
        {
            var entry = _cacheStorage.Get(key);
            return new CacheEntry<V> { Key = key, Value = _serializer.Deserialize<V>(entry.Value), UpdateTime = entry.UpdateTime };
        }

        /// <summary>
        /// キーに一致するキャッシュが格納されているか判断します。
        /// </summary>
        /// <param name="key"><see cref="WhitePeach.Infrastructure.Caching.ICacheDrive" />内で検索するキー</param>
        /// <returns>
        /// キーに対応するキャッシュが <see cref="WhitePeach.Infrastructure.Caching.ICacheDrive" /> に存在する場合は true。それ以外の場合は false。
        /// </returns>
        public bool Contains(string key)
        {
            return _cacheStorage.Contains(key);
        }

        /// <summary>
        /// 指定した <see cref="WhitePeach.Infrastructure.Caching.CacheEntry&lt;T&gt;" />が存在しなければ追加、存在すれば現在のエントリでアップデートします。
        /// </summary>
        /// <typeparam name="V">キャッシュ対象の型</typeparam>
        /// <param name="entry">追加・更新をしたいエントリ</param>
        public void AddOrUpdate<V>(CacheEntry<V> entry)
        {
            _cacheStorage.AddOrUpdate(new CacheEntry<T> { Key = entry.Key, Value = _serializer.Serialize(entry.Value), UpdateTime = entry.UpdateTime });
        }
    }
}
