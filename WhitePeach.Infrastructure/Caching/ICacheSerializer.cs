using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WhitePeach.Infrastructure.Caching
{
    /// <summary>
    /// キャッシュ対象のオブジェクトをシリアライズ・デシリアライズするシリアライザを表します。
    /// </summary>
    /// <typeparam name="T">シリアライズ後の型</typeparam>
    public interface ICacheSerializer<T>
    {
        /// <summary>
        /// オブジェクトをシリアライズします
        /// </summary>
        /// <typeparam name="Q">キャッシュの型</typeparam>
        /// <param name="cacheTarget">シリアライズ対象のオブジェクト</param>
        /// <returns>シリアライズされたオブジェクト</returns>
        T Serialize<Q>(Q cacheTarget);

        /// <summary>
        /// オブジェクトをデシリアライズします。
        /// </summary>
        /// <typeparam name="Q">キャッシュの型</typeparam>
        /// <param name="cachedObject">デシリアライズ対象のオブジェクト</param>
        /// <returns>デシリアライズされたオブジェクト</returns>
        Q Deserialize<Q>(T cachedObject);
    }
}
