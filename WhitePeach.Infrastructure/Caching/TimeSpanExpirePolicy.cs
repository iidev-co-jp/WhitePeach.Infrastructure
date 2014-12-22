using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhitePeach.Infrastructure.Caching
{
    /// <summary>
    /// 最後にキャッシュがアップデートされた時間から、指定した経過時間内までを有効とするポリシーを表します。
    /// </summary>
    public class TimeSpanExpirePolicy : IExpirePolicy
    {
        /// <summary>
        ///有効とする経過時間を取得または設定します。
        /// </summary>
        /// <value>
        /// 有効とする経過時間
        /// </value>
        public TimeSpan MaxElapsedTime { get; set; }

        /// <summary>
        /// キャッシュの最終更新時間から有効とする経過時間を指定して、 <see cref="TimeSpanExpirePolicy"/> の新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="maxElapsedTime">キャッシュの最終更新時刻から有効とする経過時間</param>
        public TimeSpanExpirePolicy(TimeSpan maxElapsedTime)
        {
            MaxElapsedTime = maxElapsedTime;
        }

        /// <summary>
        /// キャッシュが有効かどうかを判断します
        /// </summary>
        /// <typeparam name="T">キャッシュ対象の型</typeparam>
        /// <param name="entry">検証対象のエントリ</param>
        /// <returns>
        /// キャッシュが有効な場合はtrue。それ以外はfalse。
        /// </returns>
        public bool Validate<T>(CacheEntry<T> entry)
        {
            return DateTime.Now <= entry.UpdateTime.Add(MaxElapsedTime);
        }
    }
}
