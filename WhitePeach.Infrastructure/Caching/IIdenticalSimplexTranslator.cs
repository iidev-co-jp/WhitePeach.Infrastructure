using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhitePeach.Infrastructure.Caching
{
    /// <summary>
    /// 判別可能であり<see cref="System.String"/>をキーとして<typeparamref name="T"/>へ変換する機能を提供します。
    /// </summary>
    /// <typeparam name="T">キャッシュ対象の型</typeparam>
    public interface IIdenticalSimplexTranslator<T> : ISimplexTranslator<T>, ITranslatorIdentity
    {
    }
}
