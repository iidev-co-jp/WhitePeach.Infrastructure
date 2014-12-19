using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WhitePeach.Infrastructure.Caching
{
    /// <summary>
    /// キャッシュ内の個別のエントリーを表します。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CacheEntry<T>
    {
        /// <summary>
        /// キャッシュに関連付けられているキーを取得または設定します。
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// キャッシュの値を取得または設定します。
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// キャッシュが更新された時間を取得または設定します。
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}
