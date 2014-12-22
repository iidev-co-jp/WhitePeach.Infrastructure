using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhitePeach.Infrastructure.Caching
{
    /// <summary>
    /// 判別可能な<see cref="WhitePeach.Infrastructure.Caching.ITranslator&lt;T&gt;"/>を表します
    /// </summary>
    public interface ITranslatorIdentity
    {
        /// <summary>
        /// <see cref="WhitePeach.Infrastructure.Caching.ITranslator&lt;T&gt;"/>を判別する値取得します。
        /// </summary>
        string Identity { get; }
    }
}
