using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhitePeach.Infrastructure.Caching
{
    /// <summary>
    /// 複数の<see cref="System.String"/>をキーとして複数の<typeparamref name="T"/>へ変換する機能を提供します。
    /// </summary>
    /// <typeparam name="T">キャッシュ対象の型</typeparam>
    public interface IVectorizedTranslator<T> : ITranslator<T>
    {
        /// <summary>
        ///  単一の<see cref="System.String"/>をキーとして単一<typeparamref name="T"/>へ変換します。
        ///  実装者はキーの順序と変換後のオブジェクトの順序を同一にする必要があります
        /// </summary>
        /// <param name="keys">Tへ変換するための複数のキー</param>
        /// <returns>複数のキーから変換された複数のオブジェクト</returns>
        T[] Get(string[] keys);
    }
}
