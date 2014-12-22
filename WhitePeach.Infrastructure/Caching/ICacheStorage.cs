using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WhitePeach.Infrastructure.Caching
{
    /// <summary>
    /// キャッシュを保存するストレージを表します。
    /// </summary>
    /// <typeparam name="T">キャッシュ格納時の型</typeparam>
    public interface ICacheStorage<T>
    {
        /// <summary>
        /// キーから <see cref="WhitePeach.Infrastructure.Caching.CacheEntry&lt;T&gt;"/> を取得します。
        /// </summary>
        /// <param name="key"> <see cref="WhitePeach.Infrastructure.Caching.CacheEntry&lt;T&gt;"/> を取得するためのキー</param>
        /// <returns>キーに一致した <see cref="WhitePeach.Infrastructure.Caching.CacheEntry&lt;T&gt;"/> </returns>
        CacheEntry<T> Get(string key);

        /// <summary>
        /// キーに一致するキャッシュが格納されているか判断します。
        /// </summary>
        /// <param name="key"><see cref="WhitePeach.Infrastructure.Caching.ICacheStorage&lt;T&gt;"/>内で検索するキー</param>
        /// <returns>キーに対応するキャッシュが <see cref="WhitePeach.Infrastructure.Caching.ICacheStorage&lt;T&gt;"/> に存在する場合は true。それ以外の場合は false。</returns>
        bool Contains(string key);

        /// <summary>
        /// 指定した <see cref="WhitePeach.Infrastructure.Caching.CacheEntry&lt;T&gt;"/> が存在しなければ追加、存在すれば現在のエントリでアップデートします。
        /// </summary>
        /// <param name="entry">追加または更新するエントリ</param>
        void AddOrUpdate(CacheEntry<T> entry);


        /// <summary>
        /// キーに一致するキャッシュを削除します。
        /// </summary>
        /// <param name="key">キャッシュを検索するためのキー</param>
        /// <returns>削除が成功した場合はtrue。失敗または見つからなかった場合はfalse。</returns>
        bool Remove(string key);
    }
}
