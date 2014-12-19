using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhitePeach.Infrastructure.Caching
{
    /// <summary>
    /// キャッシュのが有効かどうか判断する機能を提供します。
    /// </summary>
    public interface IExpirePolicy
    {
        /// <summary>
        /// キャッシュが有効かどうかを判断します
        /// </summary>
        /// <typeparam name="T">キャッシュ対象の型</typeparam>
        /// <param name="entry">検証対象のエントリ</param>
        /// <returns>キャッシュが有効な場合はtrue。それ以外はfalse。</returns>
        bool Validate<T>(CacheEntry<T> entry);
    }
}
