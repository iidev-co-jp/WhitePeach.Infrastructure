using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhitePeach.Infrastructure.Caching
{

    /// <summary>
    /// 渡されたオブジェクトをそのまま返すシリアライザです。
    /// </summary>
    public class AsIsSerializer : ICacheSerializer<object>
    {
        /// <summary>
        /// オブジェクトを<see cref="object"/>に変換します。
        /// </summary>
        /// <typeparam name="Q">キャッシュ対象の型</typeparam>
        /// <param name="cacheTarget">変換対象のオブジェクト</param>
        /// <returns>
        /// 渡されたキャッシュオブジェクト
        /// </returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="cacheTarget" />がnullです。</exception>
        public object Serialize<Q>(Q cacheTarget)
        {
            if (cacheTarget == null) throw new ArgumentNullException("cacheTarget");
            return cacheTarget;
        }

        /// <summary>
        /// <see cref="object"/>を任意の型に変換します。
        /// </summary>
        /// <typeparam name="Q">キャッシュ対象の型</typeparam>
        /// <param name="cachedObject">キャッシュされたオブジェクト</param>
        /// <returns>
        /// 渡されたキャッシュされたオブジェクト
        /// </returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="cachedObject" />がnullです。</exception>
        /// <exception cref="System.InvalidOperationException">cacheObjectを指定された型に変換できません。</exception>
        public Q Deserialize<Q>(object cachedObject)
        {
            if (cachedObject == null) throw new ArgumentNullException("cachedObject");
            if (!(cachedObject is Q)) throw new InvalidOperationException("cacheObjectを指定された型に変換できません。");
            return (Q)cachedObject;
        }
    }
}
