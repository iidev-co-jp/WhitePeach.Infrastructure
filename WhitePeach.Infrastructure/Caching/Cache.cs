using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhitePeach.Infrastructure.Caching
{
    /// <summary>
    /// <see cref="WhitePeach.Infrastructure.Caching.CacheCore" />は高機能だが使用方法が難解なため、
    /// 通常使用するまたは使用しやすいAPIのみに限定した<see cref="WhitePeach.Infrastructure.Caching.CacheCore" />のラッパークラスです。
    /// </summary>
    public class Cache
    {
        private static readonly CacheCore Globalcache = new CacheCore(new CacheDrive<object>(new AsIsSerializer(), new OnMemoryCacheStorage<object>()), new TimeSpanExpirePolicy(new TimeSpan(1, 0, 0)));

        /// <summary>
        /// アプリケーション全体で共有されるキャッシュを取得します。
        /// デフォルトではすべてメモリ上にキャッシュされます。
        /// </summary>
        /// <value>
        /// The global cache.
        /// </value>
        public static CacheCore GlobalCache { get { return Globalcache; } }

        private readonly CacheCore _baseCache;

        /// <summary>
        /// このクラスがラップしているキャッシュシステムを取得します。
        /// </summary>
        /// <value>
        /// ラップ対象のキャッシュシステム
        /// </value>
        public CacheCore BaseCache { get { return _baseCache; } }

        /// <summary>
        /// すべてのキャッシュをオンメモリに格納する <see cref="Cache" /> の新しいインスタンスを初期化します。
        /// </summary>
        public Cache()
            : this(new CacheCore(new CacheDrive<object>(new AsIsSerializer(), new OnMemoryCacheStorage<object>()), new TimeSpanExpirePolicy(new TimeSpan(1, 0, 0))))
        {
        }

        /// <summary>
        /// 指定した <see cref="CacheCore" /> で <see cref="Cache" /> の新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="baseCache">ラップする <see cref="CacheCore" /></param>
        /// <exception cref="System.ArgumentNullException"><paramref name="baseCache" />がnullです。</exception>
        public Cache(CacheCore baseCache)
        {
            if (baseCache == null) throw new ArgumentNullException("baseCache");
            _baseCache = baseCache;
        }

        /// <summary>
        /// 指定したキーからキャッシュを取得します。
        /// </summary>
        /// <typeparam name="T">キャッシュ対象の型</typeparam>
        /// <param name="key">キャッシュを取得するためのキー</param>
        /// <param name="translator">変換に使用するトランスレータ</param>
        /// <returns>
        /// キャッシュされたオブジェクト
        /// </returns>
        public T Get<T>(string key, IIdenticalOptimizedTranslator<T> translator) { return BaseCache.Get(key, translator); }

        /// <summary>
        /// 指定したキーからキャッシュを取得します。
        /// </summary>
        /// <typeparam name="T">キャッシュ対象の型</typeparam>
        /// <param name="key">キャッシュを取得するためのキー</param>
        /// <param name="translator">変換に使用するトランスレータ</param>
        /// <returns>
        /// キャッシュされたオブジェクト
        /// </returns>
        public T Get<T>(string key, IIdenticalSimplexTranslator<T> translator) { return BaseCache.Get(key, translator); }

        /// <summary>
        /// 指定したキーからキャッシュを取得します。
        /// </summary>
        /// <typeparam name="T">キャッシュ対象の型</typeparam>
        /// <param name="key">キャッシュを取得するためのキー</param>
        /// <param name="translator">変換に使用するトランスレータ</param>
        /// <returns>
        /// キャッシュされたオブジェクト
        /// </returns>
        public T Get<T>(string key, IIdenticalVectorizedTranslator<T> translator) { return BaseCache.Get(key, translator); }

        /// <summary>
        /// 指定したキーからキャッシュを取得します。
        /// </summary>
        /// <typeparam name="T">キャッシュ対象の型</typeparam>
        /// <param name="key">キャッシュを取得するためのキー</param>
        /// <param name="translatorIdentity">トランスレータを識別するためのキー</param>
        /// <param name="translator">変換に使用するトランスレータ</param>
        /// <returns>
        /// キャッシュされたオブジェクト
        /// </returns>
        public T Get<T>(string key, string translatorIdentity, IOptimizedTranslator<T> translator) { return BaseCache.Get(key, translatorIdentity, translator); }

        /// <summary>
        /// 指定したキーからキャッシュを取得します。
        /// </summary>
        /// <typeparam name="T">キャッシュ対象の型</typeparam>
        /// <param name="key">キャッシュを取得するためのキー</param>
        /// <param name="translatorIdentity">トランスレータを識別するためのキー</param>
        /// <param name="translator">変換に使用するトランスレータ</param>
        /// <returns>
        /// キャッシュされたオブジェクト
        /// </returns>
        public T Get<T>(string key, string translatorIdentity, ISimplexTranslator<T> translator) { return BaseCache.Get(key, translatorIdentity, translator); }

        /// <summary>
        /// 指定したキーからキャッシュを取得します。
        /// </summary>
        /// <typeparam name="T">キャッシュ対象の型</typeparam>
        /// <param name="key">キャッシュを取得するためのキー</param>
        /// <param name="translatorIdentity">トランスレータを識別するためのキー</param>
        /// <param name="translator">変換に使用するトランスレータ</param>
        /// <returns>
        /// キャッシュされたオブジェクト
        /// </returns>
        public T Get<T>(string key, string translatorIdentity, IVectorizedTranslator<T> translator) { return BaseCache.Get(key, translatorIdentity, translator); }

        /// <summary>
        /// 指定したキーからキャッシュを取得します。
        /// </summary>
        /// <typeparam name="T">キャッシュ対象の型</typeparam>
        /// <param name="keys">キャッシュを取得するためのキー</param>
        /// <param name="translator">変換に使用するトランスレータ</param>
        /// <returns>
        /// キャッシュされたオブジェクト
        /// </returns>
        public T[] Get<T>(string[] keys, IIdenticalOptimizedTranslator<T> translator) { return BaseCache.Get(keys, translator); }

        /// <summary>
        /// 指定したキーからキャッシュを取得します。
        /// </summary>
        /// <typeparam name="T">キャッシュ対象の型</typeparam>
        /// <param name="keys">キャッシュを取得するためのキー</param>
        /// <param name="translator">変換に使用するトランスレータ</param>
        /// <returns>
        /// キャッシュされたオブジェクト
        /// </returns>
        public T[] Get<T>(string[] keys, IIdenticalSimplexTranslator<T> translator) { return BaseCache.Get(keys, translator); }

        /// <summary>
        /// 指定したキーからキャッシュを取得します。
        /// </summary>
        /// <typeparam name="T">キャッシュ対象の型</typeparam>
        /// <param name="keys">キャッシュを取得するためのキー</param>
        /// <param name="translator">変換に使用するトランスレータ</param>
        /// <returns>
        /// キャッシュされたオブジェクト
        /// </returns>
        public T[] Get<T>(string[] keys, IIdenticalVectorizedTranslator<T> translator) { return BaseCache.Get(keys, translator); }

        /// <summary>
        /// 指定したキーからキャッシュを取得します。
        /// </summary>
        /// <typeparam name="T">キャッシュ対象の型</typeparam>
        /// <param name="keys">キャッシュを取得するためのキー</param>
        /// <param name="translatorIdentity">トランスレータを識別するためのキー</param>
        /// <param name="translator">変換に使用するトランスレータ</param>
        /// <returns>
        /// キャッシュされたオブジェクト
        /// </returns>
        public T[] Get<T>(string[] keys, string translatorIdentity, IOptimizedTranslator<T> translator) { return BaseCache.Get(keys, translatorIdentity, translator); }

        /// <summary>
        /// 指定したキーからキャッシュを取得します。
        /// </summary>
        /// <typeparam name="T">キャッシュ対象の型</typeparam>
        /// <param name="keys">キャッシュを取得するためのキー</param>
        /// <param name="translatorIdentity">トランスレータを識別するためのキー</param>
        /// <param name="translator">変換に使用するトランスレータ</param>
        /// <returns>
        /// キャッシュされたオブジェクト
        /// </returns>
        public T[] Get<T>(string[] keys, string translatorIdentity, ISimplexTranslator<T> translator) { return BaseCache.Get(keys, translatorIdentity, translator); }

        /// <summary>
        /// 指定したキーからキャッシュを取得します。
        /// </summary>
        /// <typeparam name="T">キャッシュ対象の型</typeparam>
        /// <param name="keys">キャッシュを取得するためのキー</param>
        /// <param name="translatorIdentity">トランスレータを識別するためのキー</param>
        /// <param name="translator">変換に使用するトランスレータ</param>
        /// <returns>
        /// キャッシュされたオブジェクト
        /// </returns>
        public T[] Get<T>(string[] keys, string translatorIdentity, IVectorizedTranslator<T> translator) { return BaseCache.Get(keys, translatorIdentity, translator); }

        /// <summary>
        /// 指定したキーからキャッシュを取得します。
        /// キャッシュの格納先は <see cref="Cache.BaseCache" /> のデフォルトストレージの設定に従います。
        /// </summary>
        /// <typeparam name="T">キャッシュ対象の型</typeparam>
        /// <param name="key">キャッシュを取得するためのキー</param>
        /// <param name="translatorIdentity">トランスレータを識別するためのキー</param>
        /// <param name="translator">変換に使用するトランスレータ</param>
        /// <returns>
        /// キャッシュされたオブジェクト
        /// </returns>
        public T Get<T>(string key, string translatorIdentity, Func<string, T> translator) { return Get(key, new AnonymousIdenticalTranslator<T>(translatorIdentity, translator)); }

        /// <summary>
        /// 指定したキーからキャッシュを取得します。
        /// キャッシュの格納先は <see cref="Cache.BaseCache" /> のデフォルトストレージの設定に従います。
        /// </summary>
        /// <typeparam name="T">キャッシュ対象の型</typeparam>
        /// <param name="key">キャッシュを取得するためのキー</param>
        /// <param name="translatorIdentity">トランスレータを識別するためのキー</param>
        /// <param name="translator">変換に使用するトランスレータ</param>
        /// <returns>
        /// キャッシュされたオブジェクト
        /// </returns>
        public T Get<T>(string key, string translatorIdentity, Func<string[], T[]> translator) { return Get(key, new AnonymousIdenticalTranslator<T>(translatorIdentity, translator)); }

        /// <summary>
        /// 指定したキーからキャッシュを取得します。
        /// キャッシュの格納先は <see cref="Cache.BaseCache" /> のデフォルトストレージの設定に従います。
        /// </summary>
        /// <typeparam name="T">キャッシュ対象の型</typeparam>
        /// <param name="keys">キャッシュを取得するためのキー</param>
        /// <param name="translatorIdentity">トランスレータを識別するためのキー</param>
        /// <param name="translator">変換に使用するトランスレータ</param>
        /// <returns>
        /// キャッシュされたオブジェクト
        /// </returns>
        public T[] Get<T>(string[] keys, string translatorIdentity, Func<string, T> translator) { return Get(keys, new AnonymousIdenticalTranslator<T>(translatorIdentity, translator)); }

        /// <summary>
        /// 指定したキーからキャッシュを取得します。
        /// キャッシュの格納先は <see cref="Cache.BaseCache" /> のデフォルトストレージの設定に従います。
        /// </summary>
        /// <typeparam name="T">キャッシュ対象の型</typeparam>
        /// <param name="keys">キャッシュを取得するためのキー</param>
        /// <param name="translatorIdentity">トランスレータを識別するためのキー</param>
        /// <param name="translator">変換に使用するトランスレータ</param>
        /// <returns>
        /// キャッシュされたオブジェクト
        /// </returns>
        public T[] Get<T>(string[] keys, string translatorIdentity, Func<string[], T[]> translator) { return Get(keys, new AnonymousIdenticalTranslator<T>(translatorIdentity, translator)); }
    }
}
