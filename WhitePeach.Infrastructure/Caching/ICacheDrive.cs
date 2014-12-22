using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WhitePeach.Infrastructure.Caching
{
    /// <summary>
    /// キャッシュを保存する場所を表します。
    /// </summary>
    public interface ICacheDrive
    {
        /// <summary>
        /// キーから <see cref="WhitePeach.Infrastructure.Caching.CacheEntry&lt;T&gt;"/> を取得します。
        /// </summary>
        /// <typeparam name="T">取得したいキャッシュの値の型</typeparam>
        /// <param name="key">キャッシュを取得するためのキー</param>
        /// <returns>キーに対応するエントリ</returns>
        CacheEntry<T> Get<T>(string key);

        /// <summary>
        /// キーに一致するキャッシュが格納されているか判断します。
        /// </summary>
        /// <param name="key"><see cref="WhitePeach.Infrastructure.Caching.ICacheDrive"/>内で検索するキー</param>
        /// <returns>キーに対応するキャッシュが <see cref="WhitePeach.Infrastructure.Caching.ICacheDrive"/> に存在する場合は true。それ以外の場合は false。</returns>
        bool Contains(string key);

        /// <summary>
        /// </summary>
        /// <typeparam name="T">キャッシュ対象の型</typeparam>
        /// <param name="entry">追加・更新をしたいエントリ</param>
        void AddOrUpdate<T>(CacheEntry<T> entry);
    }
}
