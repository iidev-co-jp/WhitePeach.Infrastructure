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
        /// キー、トランスレータを指定し、キャッシュを取得します。
        /// キーに対応するエントリがキャッシュに存在した場合は<see cref="IExpirePolicy" />でエントリの有効性を検証後、有効であれば既存のエントリが取得されます。有効でない場合は新規取得され、既存のキャッシュエントリがアップデートされます。
        /// キーに対応するエントリがキャッシュに存在しない場合は新規取得され、キャッシュに追加されます。
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheCore.CacheDriveMapping" />に定義されている場合はそれを使用し、それ以外の場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されている場合はそれを使用し、それ以外の場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// 使用される<see cref="ICacheDrive" />や<see cref="IExpirePolicy" />が例外を出した場合は、<paramref name="translator" />から値を取得しそれを返します。
        /// </summary>
        /// <typeparam name="T">キャッシュ対象の型</typeparam>
        /// <param name="key">キャッシュを取得するためのキー</param>
        /// <param name="translator">変換に使用するトランスレータ</param>
        /// <returns>
        /// キャッシュされたオブジェクト
        /// </returns>
        public T Get<T>(string key, IIdenticalOptimizedTranslator<T> translator) { return BaseCache.Get(key, translator); }

        /// <summary>
        /// キー、トランスレータを指定し、キャッシュを取得します。
        /// キーに対応するエントリがキャッシュに存在した場合は<see cref="IExpirePolicy" />でエントリの有効性を検証後、有効であれば既存のエントリが取得されます。有効でない場合は新規取得され、既存のキャッシュエントリがアップデートされます。
        /// キーに対応するエントリがキャッシュに存在しない場合は新規取得され、キャッシュに追加されます。
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheCore.CacheDriveMapping" />に定義されている場合はそれを使用し、それ以外の場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されている場合はそれを使用し、それ以外の場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// 使用される<see cref="ICacheDrive" />や<see cref="IExpirePolicy" />が例外を出した場合は、<paramref name="translator" />から値を取得しそれを返します。
        /// </summary>
        /// <typeparam name="T">キャッシュ対象の型</typeparam>
        /// <param name="key">キャッシュを取得するためのキー</param>
        /// <param name="translator">変換に使用するトランスレータ</param>
        /// <returns>
        /// キャッシュされたオブジェクト
        /// </returns>
        public T Get<T>(string key, IIdenticalSimplexTranslator<T> translator) { return BaseCache.Get(key, translator); }

        /// <summary>
        /// キー、トランスレータを指定し、キャッシュを取得します。
        /// キーに対応するエントリがキャッシュに存在した場合は<see cref="IExpirePolicy" />でエントリの有効性を検証後、有効であれば既存のエントリが取得されます。有効でない場合は新規取得され、既存のキャッシュエントリがアップデートされます。
        /// キーに対応するエントリがキャッシュに存在しない場合は新規取得され、キャッシュに追加されます。
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheCore.CacheDriveMapping" />に定義されている場合はそれを使用し、それ以外の場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されている場合はそれを使用し、それ以外の場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// 使用される<see cref="ICacheDrive" />や<see cref="IExpirePolicy" />が例外を出した場合は、<paramref name="translator" />から値を取得しそれを返します。
        /// </summary>
        /// <typeparam name="T">キャッシュ対象の型</typeparam>
        /// <param name="key">キャッシュを取得するためのキー</param>
        /// <param name="translator">変換に使用するトランスレータ</param>
        /// <returns>
        /// キャッシュされたオブジェクト
        /// </returns>
        public T Get<T>(string key, IIdenticalVectorizedTranslator<T> translator) { return BaseCache.Get(key, translator); }

        /// <summary>
        /// キー、トランスレータ識別子、トランスレータを指定し、キャッシュを取得します。
        /// キーに対応するエントリがキャッシュに存在した場合は<see cref="IExpirePolicy" />でエントリの有効性を検証後、有効であれば既存のエントリが取得されます。有効でない場合は新規取得され、既存のキャッシュエントリがアップデートされます。
        /// キーに対応するエントリがキャッシュに存在しない場合は新規取得され、キャッシュに追加されます。
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheCore.CacheDriveMapping" />に定義されている場合はそれを使用し、それ以外の場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されている場合はそれを使用し、それ以外の場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// <paramref name="translatorIdentity" />はトランスレータを区別するための識別子として使用されます。同じ機能を持つトランスレータを使用した場合でも、トランスレータ識別子が異なる場合はキャッシュのエントリは異なるものとして扱われます。
        /// 使用される<see cref="ICacheDrive" />や<see cref="IExpirePolicy" />が例外を出した場合は、<paramref name="translator" />から値を取得しそれを返します。
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
        /// キー、トランスレータ識別子、トランスレータを指定し、キャッシュを取得します。
        /// キーに対応するエントリがキャッシュに存在した場合は<see cref="IExpirePolicy" />でエントリの有効性を検証後、有効であれば既存のエントリが取得されます。有効でない場合は新規取得され、既存のキャッシュエントリがアップデートされます。
        /// キーに対応するエントリがキャッシュに存在しない場合は新規取得され、キャッシュに追加されます。
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheCore.CacheDriveMapping" />に定義されている場合はそれを使用し、それ以外の場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されている場合はそれを使用し、それ以外の場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// <paramref name="translatorIdentity" />はトランスレータを区別するための識別子として使用されます。同じ機能を持つトランスレータを使用した場合でも、トランスレータ識別子が異なる場合はキャッシュのエントリは異なるものとして扱われます。
        /// 使用される<see cref="ICacheDrive" />や<see cref="IExpirePolicy" />が例外を出した場合は、<paramref name="translator" />から値を取得しそれを返します。
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
        /// キー、トランスレータ識別子、トランスレータを指定し、キャッシュを取得します。
        /// キーに対応するエントリがキャッシュに存在した場合は<see cref="IExpirePolicy" />でエントリの有効性を検証後、有効であれば既存のエントリが取得されます。有効でない場合は新規取得され、既存のキャッシュエントリがアップデートされます。
        /// キーに対応するエントリがキャッシュに存在しない場合は新規取得され、キャッシュに追加されます。
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheCore.CacheDriveMapping" />に定義されている場合はそれを使用し、それ以外の場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されている場合はそれを使用し、それ以外の場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// <paramref name="translatorIdentity" />はトランスレータを区別するための識別子として使用されます。同じ機能を持つトランスレータを使用した場合でも、トランスレータ識別子が異なる場合はキャッシュのエントリは異なるものとして扱われます。
        /// 使用される<see cref="ICacheDrive" />や<see cref="IExpirePolicy" />が例外を出した場合は、<paramref name="translator" />から値を取得しそれを返します。
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
        /// キー、トランスレータを指定し、キャッシュを取得します。
        /// キーに対応するエントリがキャッシュに存在した場合は<see cref="IExpirePolicy" />でエントリの有効性を検証後、有効であれば既存のエントリが取得されます。有効でない場合は新規取得され、既存のキャッシュエントリがアップデートされます。
        /// キーに対応するエントリがキャッシュに存在しない場合は新規取得され、キャッシュに追加されます。
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheCore.CacheDriveMapping" />に定義されている場合はそれを使用し、それ以外の場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されている場合はそれを使用し、それ以外の場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// 使用される<see cref="ICacheDrive" />や<see cref="IExpirePolicy" />が例外を出した場合は、<paramref name="translator" />から値を取得しそれを返します。
        /// </summary>
        /// <typeparam name="T">キャッシュ対象の型</typeparam>
        /// <param name="keys">キャッシュを取得するためのキー</param>
        /// <param name="translator">変換に使用するトランスレータ</param>
        /// <returns>
        /// キャッシュされたオブジェクト
        /// </returns>
        public T[] Get<T>(string[] keys, IIdenticalOptimizedTranslator<T> translator) { return BaseCache.Get(keys, translator); }

        /// <summary>
        /// キー、トランスレータを指定し、キャッシュを取得します。
        /// キーに対応するエントリがキャッシュに存在した場合は<see cref="IExpirePolicy" />でエントリの有効性を検証後、有効であれば既存のエントリが取得されます。有効でない場合は新規取得され、既存のキャッシュエントリがアップデートされます。
        /// キーに対応するエントリがキャッシュに存在しない場合は新規取得され、キャッシュに追加されます。
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheCore.CacheDriveMapping" />に定義されている場合はそれを使用し、それ以外の場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されている場合はそれを使用し、それ以外の場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// 使用される<see cref="ICacheDrive" />や<see cref="IExpirePolicy" />が例外を出した場合は、<paramref name="translator" />から値を取得しそれを返します。
        /// </summary>
        /// <typeparam name="T">キャッシュ対象の型</typeparam>
        /// <param name="keys">キャッシュを取得するためのキー</param>
        /// <param name="translator">変換に使用するトランスレータ</param>
        /// <returns>
        /// キャッシュされたオブジェクト
        /// </returns>
        public T[] Get<T>(string[] keys, IIdenticalSimplexTranslator<T> translator) { return BaseCache.Get(keys, translator); }

        /// <summary>
        /// キー、トランスレータを指定し、キャッシュを取得します。
        /// キーに対応するエントリがキャッシュに存在した場合は<see cref="IExpirePolicy" />でエントリの有効性を検証後、有効であれば既存のエントリが取得されます。有効でない場合は新規取得され、既存のキャッシュエントリがアップデートされます。
        /// キーに対応するエントリがキャッシュに存在しない場合は新規取得され、キャッシュに追加されます。
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheCore.CacheDriveMapping" />に定義されている場合はそれを使用し、それ以外の場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されている場合はそれを使用し、それ以外の場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// 使用される<see cref="ICacheDrive" />や<see cref="IExpirePolicy" />が例外を出した場合は、<paramref name="translator" />から値を取得しそれを返します。
        /// </summary>
        /// <typeparam name="T">キャッシュ対象の型</typeparam>
        /// <param name="keys">キャッシュを取得するためのキー</param>
        /// <param name="translator">変換に使用するトランスレータ</param>
        /// <returns>
        /// キャッシュされたオブジェクト
        /// </returns>
        public T[] Get<T>(string[] keys, IIdenticalVectorizedTranslator<T> translator) { return BaseCache.Get(keys, translator); }

        /// <summary>
        /// キー、トランスレータ識別子、トランスレータを指定し、キャッシュを取得します。
        /// キーに対応するエントリがキャッシュに存在した場合は<see cref="IExpirePolicy" />でエントリの有効性を検証後、有効であれば既存のエントリが取得されます。有効でない場合は新規取得され、既存のキャッシュエントリがアップデートされます。
        /// キーに対応するエントリがキャッシュに存在しない場合は新規取得され、キャッシュに追加されます。
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheCore.CacheDriveMapping" />に定義されている場合はそれを使用し、それ以外の場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されている場合はそれを使用し、それ以外の場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// <paramref name="translatorIdentity" />はトランスレータを区別するための識別子として使用されます。同じ機能を持つトランスレータを使用した場合でも、トランスレータ識別子が異なる場合はキャッシュのエントリは異なるものとして扱われます。
        /// 使用される<see cref="ICacheDrive" />や<see cref="IExpirePolicy" />が例外を出した場合は、<paramref name="translator" />から値を取得しそれを返します。
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
        /// キー、トランスレータ識別子、トランスレータを指定し、キャッシュを取得します。
        /// キーに対応するエントリがキャッシュに存在した場合は<see cref="IExpirePolicy" />でエントリの有効性を検証後、有効であれば既存のエントリが取得されます。有効でない場合は新規取得され、既存のキャッシュエントリがアップデートされます。
        /// キーに対応するエントリがキャッシュに存在しない場合は新規取得され、キャッシュに追加されます。
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheCore.CacheDriveMapping" />に定義されている場合はそれを使用し、それ以外の場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されている場合はそれを使用し、それ以外の場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// <paramref name="translatorIdentity" />はトランスレータを区別するための識別子として使用されます。同じ機能を持つトランスレータを使用した場合でも、トランスレータ識別子が異なる場合はキャッシュのエントリは異なるものとして扱われます。
        /// 使用される<see cref="ICacheDrive" />や<see cref="IExpirePolicy" />が例外を出した場合は、<paramref name="translator" />から値を取得しそれを返します。
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
        /// キー、トランスレータ識別子、トランスレータを指定し、キャッシュを取得します。
        /// キーに対応するエントリがキャッシュに存在した場合は<see cref="IExpirePolicy" />でエントリの有効性を検証後、有効であれば既存のエントリが取得されます。有効でない場合は新規取得され、既存のキャッシュエントリがアップデートされます。
        /// キーに対応するエントリがキャッシュに存在しない場合は新規取得され、キャッシュに追加されます。
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheCore.CacheDriveMapping" />に定義されている場合はそれを使用し、それ以外の場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="CacheCore.ExpirePolicyMapping" />に定義されている場合はそれを使用し、それ以外の場合は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// <paramref name="translatorIdentity" />はトランスレータを区別するための識別子として使用されます。同じ機能を持つトランスレータを使用した場合でも、トランスレータ識別子が異なる場合はキャッシュのエントリは異なるものとして扱われます。
        /// 使用される<see cref="ICacheDrive" />や<see cref="IExpirePolicy" />が例外を出した場合は、<paramref name="translator" />から値を取得しそれを返します。
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
        /// キー、トランスレータ識別子、トランスレータを指定し、キャッシュを取得します。
        /// キーに対応するエントリがキャッシュに存在した場合は<see cref="IExpirePolicy" />でエントリの有効性を検証後、有効であれば既存のエントリが取得されます。有効でない場合は新規取得され、既存のキャッシュエントリがアップデートされます。
        /// キーに対応するエントリがキャッシュに存在しない場合は新規取得され、キャッシュに追加されます。
        /// 保存先は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// ポリシーは<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// <paramref name="translatorIdentity" />はトランスレータを区別するための識別子として使用されます。同じ機能を持つトランスレータを使用した場合でも、トランスレータ識別子が異なる場合はキャッシュのエントリは異なるものとして扱われます。
        /// 使用される<see cref="ICacheDrive" />や<see cref="IExpirePolicy" />が例外を出した場合は、<paramref name="translator" />から値を取得しそれを返します。
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
        /// キー、トランスレータ識別子、トランスレータを指定し、キャッシュを取得します。
        /// キーに対応するエントリがキャッシュに存在した場合は<see cref="IExpirePolicy" />でエントリの有効性を検証後、有効であれば既存のエントリが取得されます。有効でない場合は新規取得され、既存のキャッシュエントリがアップデートされます。
        /// キーに対応するエントリがキャッシュに存在しない場合は新規取得され、キャッシュに追加されます。
        /// 保存先は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// ポリシーは<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// <paramref name="translatorIdentity" />はトランスレータを区別するための識別子として使用されます。同じ機能を持つトランスレータを使用した場合でも、トランスレータ識別子が異なる場合はキャッシュのエントリは異なるものとして扱われます。
        /// 使用される<see cref="ICacheDrive" />や<see cref="IExpirePolicy" />が例外を出した場合は、<paramref name="translator" />から値を取得しそれを返します。
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
        /// キー、トランスレータ識別子、トランスレータを指定し、キャッシュを取得します。
        /// キーに対応するエントリがキャッシュに存在した場合は<see cref="IExpirePolicy" />でエントリの有効性を検証後、有効であれば既存のエントリが取得されます。有効でない場合は新規取得され、既存のキャッシュエントリがアップデートされます。
        /// キーに対応するエントリがキャッシュに存在しない場合は新規取得され、キャッシュに追加されます。
        /// 保存先は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// ポリシーは<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// <paramref name="translatorIdentity" />はトランスレータを区別するための識別子として使用されます。同じ機能を持つトランスレータを使用した場合でも、トランスレータ識別子が異なる場合はキャッシュのエントリは異なるものとして扱われます。
        /// 使用される<see cref="ICacheDrive" />や<see cref="IExpirePolicy" />が例外を出した場合は、<paramref name="translator" />から値を取得しそれを返します。
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
        /// キー、トランスレータ識別子、トランスレータを指定し、キャッシュを取得します。
        /// キーに対応するエントリがキャッシュに存在した場合は<see cref="IExpirePolicy" />でエントリの有効性を検証後、有効であれば既存のエントリが取得されます。有効でない場合は新規取得され、既存のキャッシュエントリがアップデートされます。
        /// キーに対応するエントリがキャッシュに存在しない場合は新規取得され、キャッシュに追加されます。
        /// 保存先は<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// ポリシーは<see cref="CacheCore.DefaultCacheDrive" />が使用されます。
        /// <paramref name="translatorIdentity" />はトランスレータを区別するための識別子として使用されます。同じ機能を持つトランスレータを使用した場合でも、トランスレータ識別子が異なる場合はキャッシュのエントリは異なるものとして扱われます。
        /// 使用される<see cref="ICacheDrive" />や<see cref="IExpirePolicy" />が例外を出した場合は、<paramref name="translator" />から値を取得しそれを返します。
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
