using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhitePeach.Infrastructure.Caching
{
    /// <summary>
    /// キャッシュの実装を提供します。
    /// </summary>
    public class CacheCore
    {
        private Dictionary<string, IExpirePolicy> _expirePolicyMapping = new Dictionary<string, IExpirePolicy>();

        /// <summary>
        /// キャッシュの有効ポリシーを取得または設定します。
        /// キーは <see cref="WhitePeach.Infrastructure.Caching.ITranslator&lt;T&gt;" /> の <see cref="System.Type.FullName" /> になります。
        /// </summary>
        /// <value>
        /// キャッシュの有効ポリシー
        /// </value>
        /// <exception cref="System.InvalidOperationException">値がnullです。</exception>
        public Dictionary<string, IExpirePolicy> ExpirePolicyMapping
        {
            get { return _expirePolicyMapping; }
            set
            {
                if (value == null) throw new InvalidOperationException("nullにすることはできません");
                _expirePolicyMapping = value;
            }
        }

        private Dictionary<string, ICacheDrive> _cacheDriveMapping = new Dictionary<string, ICacheDrive>();

        /// <summary>
        /// キャッシュドライブのマッピングポリシーを取得または設定します。
        /// キーは <see cref="WhitePeach.Infrastructure.Caching.ITranslator&lt;T&gt;" /> の <see cref="System.Type.FullName" /> になります。
        /// </summary>
        /// <value>
        /// キャッシュドライブのマッピングポリシー
        /// </value>
        /// <exception cref="System.InvalidOperationException">値がnullです。</exception>
        public Dictionary<string, ICacheDrive> CacheDriveMapping
        {
            get { return _cacheDriveMapping; }
            set
            {
                if (value == null) throw new InvalidOperationException("nullにすることはできません");
                _cacheDriveMapping = value;
            }
        }

        private IExpirePolicy _defaultExpirePolicy;

        /// <summary>
        /// <see cref="WhitePeach.Infrastructure.Caching.ITranslator&lt;T&gt;" /> に対応する <see cref="WhitePeach.Infrastructure.Caching.ICacheDrive" /> が指定されていなかった場合に使用する
        /// <see cref="WhitePeach.Infrastructure.Caching.IExpirePolicy" /> を取得または設定します。
        /// </summary>
        /// <value>
        /// デフォルト使用する <see cref="WhitePeach.Infrastructure.Caching.IExpirePolicy" />
        /// </value>
        /// <exception cref="System.InvalidOperationException">値がnullです。</exception>
        public IExpirePolicy DefaultExpirePolicy
        {
            get { return _defaultExpirePolicy; }
            set
            {
                if (value == null) throw new InvalidOperationException("nullにすることはできません");
                _defaultExpirePolicy = value;
            }
        }

        private ICacheDrive _defaultCacheDrive;

        /// <summary>
        /// <see cref="WhitePeach.Infrastructure.Caching.ITranslator&lt;T&gt;" />に対応する <see cref="WhitePeach.Infrastructure.Caching.ICacheDrive" /> が指定されていなかった場合に使用する
        /// <see cref="WhitePeach.Infrastructure.Caching.ICacheDrive" /> を取得または設定します。
        /// </summary>
        /// <value>
        /// デフォルト使用する <see cref="WhitePeach.Infrastructure.Caching.ICacheDrive" />
        /// </value>
        /// <exception cref="System.InvalidOperationException">値がnullです。</exception>
        public ICacheDrive DefaultCacheDrive
        {
            get { return _defaultCacheDrive; }
            set
            {
                if (value == null) throw new InvalidOperationException("nullにすることはできません");
                _defaultCacheDrive = value;
            }
        }

        /// <summary>
        /// デフォルトのキャッシュドライブとポリシーを指定して、<see cref="WhitePeach.Infrastructure.Caching.CacheCore" /> の新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="defaultCacheDrive">対応する <see cref="WhitePeach.Infrastructure.Caching.ICacheDrive" /> がない場合デフォルトで使用される <see cref="WhitePeach.Infrastructure.Caching.ICacheDrive" /></param>
        /// <param name="defaultExpirePolicy">対応する <see cref="WhitePeach.Infrastructure.Caching.IExpirePolicy" /> がない場合デフォルトで使用される　<see cref="WhitePeach.Infrastructure.Caching.IExpirePolicy" /></param>
        public CacheCore(ICacheDrive defaultCacheDrive, IExpirePolicy defaultExpirePolicy)
        {
            if (defaultCacheDrive == null) throw new ArgumentNullException("defaultCacheDrive");
            if (defaultExpirePolicy == null) throw new ArgumentNullException("defaultExpirePolicy");
            DefaultCacheDrive = defaultCacheDrive;
            DefaultExpirePolicy = defaultExpirePolicy;
        }

        /// <summary>
        /// キー、トランスレータを指定し、キャッシュを取得します。
        /// キーに対応するエントリがキャッシュに存在した場合は<see cref="IExpirePolicy" />でエントリの有効性を検証後、有効であれば既存のエントリが取得されます。有効でない場合は新規取得され、既存のキャッシュエントリがアップデートされます。
        /// キーに対応するエントリがキャッシュに存在しない場合は新規取得され、キャッシュに追加されます。
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheDriveMapping" />に定義されている場合はそれを使用し、それ以外の場合は<see cref="DefaultCacheDrive" />が使用されます。
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="ExpirePolicyMapping" />に定義されている場合はそれを使用し、それ以外の場合は<see cref="DefaultCacheDrive" />が使用されます。
        /// 使用される<see cref="ICacheDrive" />や<see cref="IExpirePolicy" />が例外を出した場合は、<paramref name="translator" />から値を取得しそれを返します。
        /// </summary>
        /// <typeparam name="T">キャッシュ対象の型</typeparam>
        /// <param name="key">キャッシュを取得するためのキー</param>
        /// <param name="translator">変換に使用するトランスレータ</param>
        /// <returns>
        /// キャッシュされたオブジェクト
        /// </returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="translator" />がnullです。</exception>
        public T Get<T>(string key, IIdenticalSimplexTranslator<T> translator)
        {
            if (translator == null) throw new ArgumentNullException("translator");
            return Get(key, translator.Identity, translator.Get, GetCacheDrive(translator), GetExpirePolicy(translator));
        }

        /// <summary>
        /// キー、トランスレータ、保存先を指定し、キャッシュを取得します。
        /// キーに対応するエントリがキャッシュに存在した場合は<see cref="IExpirePolicy" />でエントリの有効性を検証後、有効であれば既存のエントリが取得されます。有効でない場合は新規取得され、既存のキャッシュエントリがアップデートされます。
        /// キーに対応するエントリがキャッシュに存在しない場合は新規取得され、キャッシュに追加されます。
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="ExpirePolicyMapping" />に定義されている場合はそれを使用し、それ以外の場合は<see cref="DefaultCacheDrive" />が使用されます。
        /// 使用される<see cref="ICacheDrive" />や<see cref="IExpirePolicy" />が例外を出した場合は、<paramref name="translator" />から値を取得しそれを返します。
        /// </summary>
        /// <typeparam name="T">キャッシュ対象の型</typeparam>
        /// <param name="key">キャッシュを取得するためのキー</param>
        /// <param name="translator">変換に使用するトランスレータ</param>
        /// <param name="cacheDrive">キャッシュを保存するするストレージ</param>
        /// <returns>
        /// キャッシュされたオブジェクト
        /// </returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="translator" />がnullです。</exception>
        public T Get<T>(string key, IIdenticalSimplexTranslator<T> translator, ICacheDrive cacheDrive)
        {
            if (translator == null) throw new ArgumentNullException("translator");
            return Get(key, translator.Identity, translator.Get, cacheDrive, GetExpirePolicy(translator));
        }

        /// <summary>
        /// キー、トランスレータ、ポリシーを指定し、キャッシュを取得します。
        /// キーに対応するエントリがキャッシュに存在した場合は<see cref="IExpirePolicy" />でエントリの有効性を検証後、有効であれば既存のエントリが取得されます。有効でない場合は新規取得され、既存のキャッシュエントリがアップデートされます。
        /// キーに対応するエントリがキャッシュに存在しない場合は新規取得され、キャッシュに追加されます。
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheDriveMapping" />に定義されている場合はそれを使用し、それ以外の場合は<see cref="DefaultCacheDrive" />が使用されます。
        /// 使用される<see cref="ICacheDrive" />や<see cref="IExpirePolicy" />が例外を出した場合は、<paramref name="translator" />から値を取得しそれを返します。
        /// </summary>
        /// <typeparam name="T">キャッシュ対象の型</typeparam>
        /// <param name="key">キャッシュを取得するためのキー</param>
        /// <param name="translator">変換に使用するトランスレータ</param>
        /// <param name="expirePolicy">キャッシュの有効期限ポリシー</param>
        /// <returns>
        /// キャッシュされたオブジェクト
        /// </returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="translator"/>がnullです。</exception>
        public T Get<T>(string key, IIdenticalSimplexTranslator<T> translator, IExpirePolicy expirePolicy)
        {
            if (translator == null) throw new ArgumentNullException("translator");
            return Get(key, translator.Identity, translator.Get, GetCacheDrive(translator), expirePolicy);
        }

        //

        /// <summary>
        /// キー、トランスレータを指定し、キャッシュを取得します。
        /// キーに対応するエントリがキャッシュに存在した場合は<see cref="IExpirePolicy" />でエントリの有効性を検証後、有効であれば既存のエントリが取得されます。有効でない場合は新規取得され、既存のキャッシュエントリがアップデートされます。
        /// キーに対応するエントリがキャッシュに存在しない場合は新規取得され、キャッシュに追加されます。
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheDriveMapping" />に定義されている場合はそれを使用し、それ以外の場合は<see cref="DefaultCacheDrive" />が使用されます。
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="ExpirePolicyMapping" />に定義されている場合はそれを使用し、それ以外の場合は<see cref="DefaultCacheDrive" />が使用されます。
        /// 使用される<see cref="ICacheDrive" />や<see cref="IExpirePolicy" />が例外を出した場合は、<paramref name="translator" />から値を取得しそれを返します。
        /// </summary>
        /// <typeparam name="T">キャッシュ対象の型</typeparam>
        /// <param name="key">キャッシュを取得するためのキー</param>
        /// <param name="translator">変換に使用するトランスレータ</param>
        /// <returns>
        /// キャッシュされたオブジェクト
        /// </returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="translator"/>がnullです。</exception>
        public T Get<T>(string key, IIdenticalVectorizedTranslator<T> translator)
        {
            if (translator == null) throw new ArgumentNullException("translator");
            return Get(key, translator.Identity, translator.Get, GetCacheDrive(translator), GetExpirePolicy(translator));
        }

        /// <summary>
        /// キー、トランスレータ、保存先を指定し、キャッシュを取得します。
        /// キーに対応するエントリがキャッシュに存在した場合は<see cref="IExpirePolicy" />でエントリの有効性を検証後、有効であれば既存のエントリが取得されます。有効でない場合は新規取得され、既存のキャッシュエントリがアップデートされます。
        /// キーに対応するエントリがキャッシュに存在しない場合は新規取得され、キャッシュに追加されます。
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="ExpirePolicyMapping" />に定義されている場合はそれを使用し、それ以外の場合は<see cref="DefaultCacheDrive" />が使用されます。
        /// 使用される<see cref="ICacheDrive" />や<see cref="IExpirePolicy" />が例外を出した場合は、<paramref name="translator" />から値を取得しそれを返します。
        /// </summary>
        /// <typeparam name="T">キャッシュ対象の型</typeparam>
        /// <param name="key">キャッシュを取得するためのキー</param>
        /// <param name="translator">変換に使用するトランスレータ</param>
        /// <param name="cacheDrive">キャッシュを保存するするストレージ</param>
        /// <returns>
        /// キャッシュされたオブジェクト
        /// </returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="translator"/>がnullです。</exception>
        public T Get<T>(string key, IIdenticalVectorizedTranslator<T> translator, ICacheDrive cacheDrive)
        {
            if (translator == null) throw new ArgumentNullException("translator");
            return Get(key, translator.Identity, translator.Get, cacheDrive, GetExpirePolicy(translator));
        }

        /// <summary>
        /// キー、トランスレータ、ポリシーを指定し、キャッシュを取得します。
        /// キーに対応するエントリがキャッシュに存在した場合は<see cref="IExpirePolicy" />でエントリの有効性を検証後、有効であれば既存のエントリが取得されます。有効でない場合は新規取得され、既存のキャッシュエントリがアップデートされます。
        /// キーに対応するエントリがキャッシュに存在しない場合は新規取得され、キャッシュに追加されます。
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheDriveMapping" />に定義されている場合はそれを使用し、それ以外の場合は<see cref="DefaultCacheDrive" />が使用されます。
        /// 使用される<see cref="ICacheDrive" />や<see cref="IExpirePolicy" />が例外を出した場合は、<paramref name="translator" />から値を取得しそれを返します。
        /// </summary>
        /// <typeparam name="T">キャッシュ対象の型</typeparam>
        /// <param name="key">キャッシュを取得するためのキー</param>
        /// <param name="translator">変換に使用するトランスレータ</param>
        /// <param name="expirePolicy">キャッシュの有効期限ポリシー</param>
        /// <returns>
        /// キャッシュされたオブジェクト
        /// </returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="translator"/>がnullです。</exception>
        public T Get<T>(string key, IIdenticalVectorizedTranslator<T> translator, IExpirePolicy expirePolicy)
        {
            if (translator == null) throw new ArgumentNullException("translator");
            return Get(key, translator.Identity, translator.Get, GetCacheDrive(translator), expirePolicy);
        }

        //

        /// <summary>
        /// キー、トランスレータを指定し、キャッシュを取得します。
        /// キーに対応するエントリがキャッシュに存在した場合は<see cref="IExpirePolicy" />でエントリの有効性を検証後、有効であれば既存のエントリが取得されます。有効でない場合は新規取得され、既存のキャッシュエントリがアップデートされます。
        /// キーに対応するエントリがキャッシュに存在しない場合は新規取得され、キャッシュに追加されます。
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheDriveMapping" />に定義されている場合はそれを使用し、それ以外の場合は<see cref="DefaultCacheDrive" />が使用されます。
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="ExpirePolicyMapping" />に定義されている場合はそれを使用し、それ以外の場合は<see cref="DefaultCacheDrive" />が使用されます。
        /// 使用される<see cref="ICacheDrive" />や<see cref="IExpirePolicy" />が例外を出した場合は、<paramref name="translator" />から値を取得しそれを返します。
        /// </summary>
        /// <typeparam name="T">キャッシュ対象の型</typeparam>
        /// <param name="key">キャッシュを取得するためのキー</param>
        /// <param name="translator">変換に使用するトランスレータ</param>
        /// <returns>
        /// キャッシュされたオブジェクト
        /// </returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="translator"/>がnullです。</exception>
        public T Get<T>(string key, IIdenticalOptimizedTranslator<T> translator)
        {
            if (translator == null) throw new ArgumentNullException("translator");
            return Get(key, translator.Identity, (Func<string, T>)translator.Get, GetCacheDrive(translator), GetExpirePolicy(translator));
        }

        /// <summary>
        /// キー、トランスレータ、保存先を指定し、キャッシュを取得します。
        /// キーに対応するエントリがキャッシュに存在した場合は<see cref="IExpirePolicy" />でエントリの有効性を検証後、有効であれば既存のエントリが取得されます。有効でない場合は新規取得され、既存のキャッシュエントリがアップデートされます。
        /// キーに対応するエントリがキャッシュに存在しない場合は新規取得され、キャッシュに追加されます。
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="ExpirePolicyMapping" />に定義されている場合はそれを使用し、それ以外の場合は<see cref="DefaultCacheDrive" />が使用されます。
        /// 使用される<see cref="ICacheDrive" />や<see cref="IExpirePolicy" />が例外を出した場合は、<paramref name="translator" />から値を取得しそれを返します。
        /// </summary>
        /// <typeparam name="T">キャッシュ対象の型</typeparam>
        /// <param name="key">キャッシュを取得するためのキー</param>
        /// <param name="translator">変換に使用するトランスレータ</param>
        /// <param name="cacheDrive">キャッシュを保存するするストレージ</param>
        /// <returns>
        /// キャッシュされたオブジェクト
        /// </returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="translator"/>がnullです。</exception>
        public T Get<T>(string key, IIdenticalOptimizedTranslator<T> translator, ICacheDrive cacheDrive)
        {
            if (translator == null) throw new ArgumentNullException("translator");
            return Get(key, translator.Identity, (Func<string, T>)translator.Get, cacheDrive, GetExpirePolicy(translator));
        }

        /// <summary>
        /// キー、トランスレータ、ポリシーを指定し、キャッシュを取得します。
        /// キーに対応するエントリがキャッシュに存在した場合は<see cref="IExpirePolicy" />でエントリの有効性を検証後、有効であれば既存のエントリが取得されます。有効でない場合は新規取得され、既存のキャッシュエントリがアップデートされます。
        /// キーに対応するエントリがキャッシュに存在しない場合は新規取得され、キャッシュに追加されます。
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheDriveMapping" />に定義されている場合はそれを使用し、それ以外の場合は<see cref="DefaultCacheDrive" />が使用されます。
        /// 使用される<see cref="ICacheDrive" />や<see cref="IExpirePolicy" />が例外を出した場合は、<paramref name="translator" />から値を取得しそれを返します。
        /// </summary>
        /// <typeparam name="T">キャッシュ対象の型</typeparam>
        /// <param name="key">キャッシュを取得するためのキー</param>
        /// <param name="translator">変換に使用するトランスレータ</param>
        /// <param name="expirePolicy">キャッシュの有効期限ポリシー</param>
        /// <returns>
        /// キャッシュされたオブジェクト
        /// </returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="translator"/>がnullです。</exception>
        public T Get<T>(string key, IIdenticalOptimizedTranslator<T> translator, IExpirePolicy expirePolicy)
        {
            if (translator == null) throw new ArgumentNullException("translator");
            return Get(key, translator.Identity, (Func<string, T>)translator.Get, GetCacheDrive(translator), expirePolicy);
        }

        //

        /// <summary>
        /// キー、トランスレータ識別子、トランスレータを指定し、キャッシュを取得します。
        /// キーに対応するエントリがキャッシュに存在した場合は<see cref="IExpirePolicy" />でエントリの有効性を検証後、有効であれば既存のエントリが取得されます。有効でない場合は新規取得され、既存のキャッシュエントリがアップデートされます。
        /// キーに対応するエントリがキャッシュに存在しない場合は新規取得され、キャッシュに追加されます。
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheDriveMapping" />に定義されている場合はそれを使用し、それ以外の場合は<see cref="DefaultCacheDrive" />が使用されます。
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="ExpirePolicyMapping" />に定義されている場合はそれを使用し、それ以外の場合は<see cref="DefaultCacheDrive" />が使用されます。
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
        /// <exception cref="System.ArgumentNullException"><paramref name="translator" />がnullです。</exception>
        public T Get<T>(string key, string translatorIdentity, IOptimizedTranslator<T> translator)
        {
            if (translator == null) throw new ArgumentNullException("translator");
            return Get(key, translatorIdentity, (Func<string, T>)translator.Get, GetCacheDrive(translator), GetExpirePolicy(translator));
        }

        /// <summary>
        /// キー、トランスレータ識別子、トランスレータ、保存先を指定し、キャッシュを取得します。
        /// キーに対応するエントリがキャッシュに存在した場合は<see cref="IExpirePolicy" />でエントリの有効性を検証後、有効であれば既存のエントリが取得されます。有効でない場合は新規取得され、既存のキャッシュエントリがアップデートされます。
        /// キーに対応するエントリがキャッシュに存在しない場合は新規取得され、キャッシュに追加されます。
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="ExpirePolicyMapping" />に定義されている場合はそれを使用し、それ以外の場合は<see cref="DefaultCacheDrive" />が使用されます。
        /// <paramref name="translatorIdentity" />はトランスレータを区別するための識別子として使用されます。同じ機能を持つトランスレータを使用した場合でも、トランスレータ識別子が異なる場合はキャッシュのエントリは異なるものとして扱われます。
        /// 使用される<see cref="ICacheDrive" />や<see cref="IExpirePolicy" />が例外を出した場合は、<paramref name="translator" />から値を取得しそれを返します。
        /// </summary>
        /// <typeparam name="T">キャッシュ対象の型</typeparam>
        /// <param name="key">キャッシュを取得するためのキー</param>
        /// <param name="translatorIdentity">トランスレータを識別するためのキー</param>
        /// <param name="translator">変換に使用するトランスレータ</param>
        /// <param name="cacheDrive">キャッシュを保存するするストレージ</param>
        /// <returns>
        /// キャッシュされたオブジェクト
        /// </returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="translator"/>がnullです。</exception>B
        public T Get<T>(string key, string translatorIdentity, IOptimizedTranslator<T> translator, ICacheDrive cacheDrive)
        {
            if (translator == null) throw new ArgumentNullException("translator");
            return Get(key, translatorIdentity, (Func<string, T>)translator.Get, cacheDrive, GetExpirePolicy(translator));
        }

        /// <summary>
        /// キー、トランスレータ識別子、トランスレータ、ポリシーを指定し、キャッシュを取得します。
        /// キーに対応するエントリがキャッシュに存在した場合は<see cref="IExpirePolicy" />でエントリの有効性を検証後、有効であれば既存のエントリが取得されます。有効でない場合は新規取得され、既存のキャッシュエントリがアップデートされます。
        /// キーに対応するエントリがキャッシュに存在しない場合は新規取得され、キャッシュに追加されます。
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheDriveMapping" />に定義されている場合はそれを使用し、それ以外の場合は<see cref="DefaultCacheDrive" />が使用されます。
        /// <paramref name="translatorIdentity" />はトランスレータを区別するための識別子として使用されます。同じ機能を持つトランスレータを使用した場合でも、トランスレータ識別子が異なる場合はキャッシュのエントリは異なるものとして扱われます。
        /// 使用される<see cref="ICacheDrive" />や<see cref="IExpirePolicy" />が例外を出した場合は、<paramref name="translator" />から値を取得しそれを返します。
        /// </summary>
        /// <typeparam name="T">キャッシュ対象の型</typeparam>
        /// <param name="key">キャッシュを取得するためのキー</param>
        /// <param name="translatorIdentity">トランスレータを識別するためのキー</param>
        /// <param name="translator">変換に使用するトランスレータ</param>
        /// <param name="expirePolicy">キャッシュの有効期限ポリシー</param>
        /// <returns>
        /// キャッシュされたオブジェクト
        /// </returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="translator" />がnullです。</exception>
        public T Get<T>(string key, string translatorIdentity, IOptimizedTranslator<T> translator, IExpirePolicy expirePolicy)
        {
            if (translator == null) throw new ArgumentNullException("translator");
            return Get(key, translatorIdentity, (Func<string, T>)translator.Get, GetCacheDrive(translator), expirePolicy);
        }

        //

        /// <summary>
        /// キー、トランスレータ識別子、トランスレータを指定し、キャッシュを取得します。
        /// キーに対応するエントリがキャッシュに存在した場合は<see cref="IExpirePolicy" />でエントリの有効性を検証後、有効であれば既存のエントリが取得されます。有効でない場合は新規取得され、既存のキャッシュエントリがアップデートされます。
        /// キーに対応するエントリがキャッシュに存在しない場合は新規取得され、キャッシュに追加されます。
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheDriveMapping" />に定義されている場合はそれを使用し、それ以外の場合は<see cref="DefaultCacheDrive" />が使用されます。
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="ExpirePolicyMapping" />に定義されている場合はそれを使用し、それ以外の場合は<see cref="DefaultCacheDrive" />が使用されます。
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
        /// <exception cref="System.ArgumentNullException"><paramref name="translator" />がnullです。</exception>
        public T Get<T>(string key, string translatorIdentity, ISimplexTranslator<T> translator)
        {
            if (translator == null) throw new ArgumentNullException("translator");
            return Get(key, translatorIdentity, translator.Get, GetCacheDrive(translator), GetExpirePolicy(translator));
        }

        /// <summary>
        /// キー、トランスレータ識別子、トランスレータ、保存先を指定し、キャッシュを取得します。
        /// キーに対応するエントリがキャッシュに存在した場合は<see cref="IExpirePolicy" />でエントリの有効性を検証後、有効であれば既存のエントリが取得されます。有効でない場合は新規取得され、既存のキャッシュエントリがアップデートされます。
        /// キーに対応するエントリがキャッシュに存在しない場合は新規取得され、キャッシュに追加されます。
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="ExpirePolicyMapping" />に定義されている場合はそれを使用し、それ以外の場合は<see cref="DefaultCacheDrive" />が使用されます。
        /// <paramref name="translatorIdentity" />はトランスレータを区別するための識別子として使用されます。同じ機能を持つトランスレータを使用した場合でも、トランスレータ識別子が異なる場合はキャッシュのエントリは異なるものとして扱われます。
        /// 使用される<see cref="ICacheDrive" />や<see cref="IExpirePolicy" />が例外を出した場合は、<paramref name="translator" />から値を取得しそれを返します。
        /// </summary>
        /// <typeparam name="T">キャッシュ対象の型</typeparam>
        /// <param name="key">キャッシュを取得するためのキー</param>
        /// <param name="translatorIdentity">トランスレータを識別するためのキー</param>
        /// <param name="translator">変換に使用するトランスレータ</param>
        /// <param name="cacheDrive">キャッシュを保存するするストレージ</param>
        /// <returns>
        /// キャッシュされたオブジェクト
        /// </returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="translator" />がnullです。</exception>
        public T Get<T>(string key, string translatorIdentity, ISimplexTranslator<T> translator, ICacheDrive cacheDrive)
        {
            if (translator == null) throw new ArgumentNullException("translator");
            return Get(key, translatorIdentity, translator.Get, cacheDrive, GetExpirePolicy(translator));
        }

        /// <summary>
        /// キー、トランスレータ識別子、トランスレータ、ポリシーを指定し、キャッシュを取得します。
        /// キーに対応するエントリがキャッシュに存在した場合は<see cref="IExpirePolicy" />でエントリの有効性を検証後、有効であれば既存のエントリが取得されます。有効でない場合は新規取得され、既存のキャッシュエントリがアップデートされます。
        /// キーに対応するエントリがキャッシュに存在しない場合は新規取得され、キャッシュに追加されます。
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheDriveMapping" />に定義されている場合はそれを使用し、それ以外の場合は<see cref="DefaultCacheDrive" />が使用されます。
        /// <paramref name="translatorIdentity" />はトランスレータを区別するための識別子として使用されます。同じ機能を持つトランスレータを使用した場合でも、トランスレータ識別子が異なる場合はキャッシュのエントリは異なるものとして扱われます。
        /// 使用される<see cref="ICacheDrive" />や<see cref="IExpirePolicy" />が例外を出した場合は、<paramref name="translator" />から値を取得しそれを返します。
        /// </summary>
        /// <typeparam name="T">キャッシュ対象の型</typeparam>
        /// <param name="key">キャッシュを取得するためのキー</param>
        /// <param name="translatorIdentity">トランスレータを識別するためのキー</param>
        /// <param name="translator">変換に使用するトランスレータ</param>
        /// <param name="expirePolicy">キャッシュの有効期限ポリシー</param>
        /// <returns>
        /// キャッシュされたオブジェクト
        /// </returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="translator" />がnullです。</exception>
        public T Get<T>(string key, string translatorIdentity, ISimplexTranslator<T> translator, IExpirePolicy expirePolicy)
        {
            if (translator == null) throw new ArgumentNullException("translator");
            return Get(key, translatorIdentity, translator.Get, GetCacheDrive(translator), expirePolicy);
        }

        //

        /// <summary>
        /// キー、トランスレータ識別子、トランスレータを指定し、キャッシュを取得します。
        /// キーに対応するエントリがキャッシュに存在した場合は<see cref="IExpirePolicy" />でエントリの有効性を検証後、有効であれば既存のエントリが取得されます。有効でない場合は新規取得され、既存のキャッシュエントリがアップデートされます。
        /// キーに対応するエントリがキャッシュに存在しない場合は新規取得され、キャッシュに追加されます。
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheDriveMapping" />に定義されている場合はそれを使用し、それ以外の場合は<see cref="DefaultCacheDrive" />が使用されます。
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="ExpirePolicyMapping" />に定義されている場合はそれを使用し、それ以外の場合は<see cref="DefaultCacheDrive" />が使用されます。
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
        /// <exception cref="System.ArgumentNullException"><paramref name="translator" />がnullです。</exception>
        public T Get<T>(string key, string translatorIdentity, IVectorizedTranslator<T> translator)
        {
            if (translator == null) throw new ArgumentNullException("translator");
            return Get(key, translatorIdentity, translator.Get, GetCacheDrive(translator), GetExpirePolicy(translator));
        }

        /// <summary>
        /// キー、トランスレータ識別子、トランスレータ、保存先を指定し、キャッシュを取得します。
        /// キーに対応するエントリがキャッシュに存在した場合は<see cref="IExpirePolicy" />でエントリの有効性を検証後、有効であれば既存のエントリが取得されます。有効でない場合は新規取得され、既存のキャッシュエントリがアップデートされます。
        /// キーに対応するエントリがキャッシュに存在しない場合は新規取得され、キャッシュに追加されます。
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="ExpirePolicyMapping" />に定義されている場合はそれを使用し、それ以外の場合は<see cref="DefaultCacheDrive" />が使用されます。
        /// <paramref name="translatorIdentity" />はトランスレータを区別するための識別子として使用されます。同じ機能を持つトランスレータを使用した場合でも、トランスレータ識別子が異なる場合はキャッシュのエントリは異なるものとして扱われます。
        /// 使用される<see cref="ICacheDrive" />や<see cref="IExpirePolicy" />が例外を出した場合は、<paramref name="translator" />から値を取得しそれを返します。
        /// </summary>
        /// <typeparam name="T">キャッシュ対象の型</typeparam>
        /// <param name="key">キャッシュを取得するためのキー</param>
        /// <param name="translatorIdentity">トランスレータを識別するためのキー</param>
        /// <param name="translator">変換に使用するトランスレータ</param>
        /// <param name="cacheDrive">キャッシュを保存するするストレージ</param>
        /// <returns>
        /// キャッシュされたオブジェクト
        /// </returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="translator" />がnullです。</exception>
        public T Get<T>(string key, string translatorIdentity, IVectorizedTranslator<T> translator, ICacheDrive cacheDrive)
        {
            if (translator == null) throw new ArgumentNullException("translator");
            return Get(key, translatorIdentity, translator.Get, cacheDrive, GetExpirePolicy(translator));
        }

        /// <summary>
        /// キー、トランスレータ識別子、トランスレータ、ポリシーを指定し、キャッシュを取得します。
        /// キーに対応するエントリがキャッシュに存在した場合は<see cref="IExpirePolicy" />でエントリの有効性を検証後、有効であれば既存のエントリが取得されます。有効でない場合は新規取得され、既存のキャッシュエントリがアップデートされます。
        /// キーに対応するエントリがキャッシュに存在しない場合は新規取得され、キャッシュに追加されます。
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheDriveMapping" />に定義されている場合はそれを使用し、それ以外の場合は<see cref="DefaultCacheDrive" />が使用されます。
        /// <paramref name="translatorIdentity" />はトランスレータを区別するための識別子として使用されます。同じ機能を持つトランスレータを使用した場合でも、トランスレータ識別子が異なる場合はキャッシュのエントリは異なるものとして扱われます。
        /// 使用される<see cref="ICacheDrive" />や<see cref="IExpirePolicy" />が例外を出した場合は、<paramref name="translator" />から値を取得しそれを返します。
        /// </summary>
        /// <typeparam name="T">キャッシュ対象の型</typeparam>
        /// <param name="key">キャッシュを取得するためのキー</param>
        /// <param name="translatorIdentity">トランスレータを識別するためのキー</param>
        /// <param name="translator">変換に使用するトランスレータ</param>
        /// <param name="expirePolicy">キャッシュの有効期限ポリシー</param>
        /// <returns>
        /// キャッシュされたオブジェクト
        /// </returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="translator" />がnullです。</exception>
        public T Get<T>(string key, string translatorIdentity, IVectorizedTranslator<T> translator, IExpirePolicy expirePolicy)
        {
            if (translator == null) throw new ArgumentNullException("translator");
            return Get(key, translatorIdentity, translator.Get, GetCacheDrive(translator), expirePolicy);
        }

        ///////

        /// <summary>
        /// キー、トランスレータを指定し、キャッシュを取得します。
        /// キーに対応するエントリがキャッシュに存在した場合は<see cref="IExpirePolicy" />でエントリの有効性を検証後、有効であれば既存のエントリが取得されます。有効でない場合は新規取得され、既存のキャッシュエントリがアップデートされます。
        /// キーに対応するエントリがキャッシュに存在しない場合は新規取得され、キャッシュに追加されます。
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheDriveMapping" />に定義されている場合はそれを使用し、それ以外の場合は<see cref="DefaultCacheDrive" />が使用されます。
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="ExpirePolicyMapping" />に定義されている場合はそれを使用し、それ以外の場合は<see cref="DefaultCacheDrive" />が使用されます。
        /// 使用される<see cref="ICacheDrive" />や<see cref="IExpirePolicy" />が例外を出した場合は、<paramref name="translator" />から値を取得しそれを返します。
        /// </summary>
        /// <typeparam name="T">キャッシュ対象の型</typeparam>
        /// <param name="keys">キャッシュを取得するためのキー</param>
        /// <param name="translator">変換に使用するトランスレータ</param>
        /// <returns>
        /// キャッシュされたオブジェクト
        /// </returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="translator" />がnullです。</exception>
        public T[] Get<T>(string[] keys, IIdenticalSimplexTranslator<T> translator)
        {
            if (translator == null) throw new ArgumentNullException("translator");
            return Get(keys, translator.Identity, translator.Get, GetCacheDrive(translator), GetExpirePolicy(translator));
        }

        /// <summary>
        /// キー、トランスレータ、保存先を指定し、キャッシュを取得します。
        /// キーに対応するエントリがキャッシュに存在した場合は<see cref="IExpirePolicy" />でエントリの有効性を検証後、有効であれば既存のエントリが取得されます。有効でない場合は新規取得され、既存のキャッシュエントリがアップデートされます。
        /// キーに対応するエントリがキャッシュに存在しない場合は新規取得され、キャッシュに追加されます。
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="ExpirePolicyMapping" />に定義されている場合はそれを使用し、それ以外の場合は<see cref="DefaultCacheDrive" />が使用されます。
        /// 使用される<see cref="ICacheDrive" />や<see cref="IExpirePolicy" />が例外を出した場合は、<paramref name="translator" />から値を取得しそれを返します。
        /// </summary>
        /// <typeparam name="T">キャッシュ対象の型</typeparam>
        /// <param name="keys">キャッシュを取得するためのキー</param>
        /// <param name="translator">変換に使用するトランスレータ</param>
        /// <param name="cacheDrive">キャッシュを保存するするストレージ</param>
        /// <returns>
        /// キャッシュされたオブジェクト
        /// </returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="translator" />がnullです。</exception>
        public T[] Get<T>(string[] keys, IIdenticalSimplexTranslator<T> translator, ICacheDrive cacheDrive)
        {
            if (translator == null) throw new ArgumentNullException("translator");
            return Get(keys, translator.Identity, translator.Get, cacheDrive, GetExpirePolicy(translator));
        }

        /// <summary>
        /// キー、トランスレータ、ポリシーを指定し、キャッシュを取得します。
        /// キーに対応するエントリがキャッシュに存在した場合は<see cref="IExpirePolicy" />でエントリの有効性を検証後、有効であれば既存のエントリが取得されます。有効でない場合は新規取得され、既存のキャッシュエントリがアップデートされます。
        /// キーに対応するエントリがキャッシュに存在しない場合は新規取得され、キャッシュに追加されます。
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheDriveMapping" />に定義されている場合はそれを使用し、それ以外の場合は<see cref="DefaultCacheDrive" />が使用されます。
        /// 使用される<see cref="ICacheDrive" />や<see cref="IExpirePolicy" />が例外を出した場合は、<paramref name="translator" />から値を取得しそれを返します。
        /// </summary>
        /// <typeparam name="T">キャッシュ対象の型</typeparam>
        /// <param name="keys">キャッシュを取得するためのキー</param>
        /// <param name="translator">変換に使用するトランスレータ</param>
        /// <param name="expirePolicy">キャッシュの有効期限ポリシー</param>
        /// <returns>
        /// キャッシュされたオブジェクト
        /// </returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="translator" />がnullです。</exception>
        public T[] Get<T>(string[] keys, IIdenticalSimplexTranslator<T> translator, IExpirePolicy expirePolicy)
        {
            if (translator == null) throw new ArgumentNullException("translator");
            return Get(keys, translator.Identity, translator.Get, GetCacheDrive(translator), expirePolicy);
        }

        //

        /// <summary>
        /// キー、トランスレータを指定し、キャッシュを取得します。
        /// キーに対応するエントリがキャッシュに存在した場合は<see cref="IExpirePolicy" />でエントリの有効性を検証後、有効であれば既存のエントリが取得されます。有効でない場合は新規取得され、既存のキャッシュエントリがアップデートされます。
        /// キーに対応するエントリがキャッシュに存在しない場合は新規取得され、キャッシュに追加されます。
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheDriveMapping" />に定義されている場合はそれを使用し、それ以外の場合は<see cref="DefaultCacheDrive" />が使用されます。
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="ExpirePolicyMapping" />に定義されている場合はそれを使用し、それ以外の場合は<see cref="DefaultCacheDrive" />が使用されます。
        /// 使用される<see cref="ICacheDrive" />や<see cref="IExpirePolicy" />が例外を出した場合は、<paramref name="translator" />から値を取得しそれを返します。
        /// </summary>
        /// <typeparam name="T">キャッシュ対象の型</typeparam>
        /// <param name="keys">キャッシュを取得するためのキー</param>
        /// <param name="translator">変換に使用するトランスレータ</param>
        /// <returns>
        /// キャッシュされたオブジェクト
        /// </returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="translator" />がnullです。</exception>
        public T[] Get<T>(string[] keys, IIdenticalVectorizedTranslator<T> translator)
        {
            if (translator == null) throw new ArgumentNullException("translator");
            return Get(keys, translator.Identity, translator.Get, GetCacheDrive(translator), GetExpirePolicy(translator));
        }

        /// <summary>
        /// キー、トランスレータ、保存先を指定し、キャッシュを取得します。
        /// キーに対応するエントリがキャッシュに存在した場合は<see cref="IExpirePolicy" />でエントリの有効性を検証後、有効であれば既存のエントリが取得されます。有効でない場合は新規取得され、既存のキャッシュエントリがアップデートされます。
        /// キーに対応するエントリがキャッシュに存在しない場合は新規取得され、キャッシュに追加されます。
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="ExpirePolicyMapping" />に定義されている場合はそれを使用し、それ以外の場合は<see cref="DefaultCacheDrive" />が使用されます。
        /// 使用される<see cref="ICacheDrive" />や<see cref="IExpirePolicy" />が例外を出した場合は、<paramref name="translator" />から値を取得しそれを返します。
        /// </summary>
        /// <typeparam name="T">キャッシュ対象の型</typeparam>
        /// <param name="keys">キャッシュを取得するためのキー</param>
        /// <param name="translator">変換に使用するトランスレータ</param>
        /// <param name="cacheDrive">キャッシュを保存するするストレージ</param>
        /// <returns>
        /// キャッシュされたオブジェクト
        /// </returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="translator" />がnullです。</exception>
        public T[] Get<T>(string[] keys, IIdenticalVectorizedTranslator<T> translator, ICacheDrive cacheDrive)
        {
            if (translator == null) throw new ArgumentNullException("translator");
            return Get(keys, translator.Identity, translator.Get, cacheDrive, GetExpirePolicy(translator));
        }

        /// <summary>
        /// キー、トランスレータ、ポリシーを指定し、キャッシュを取得します。
        /// キーに対応するエントリがキャッシュに存在した場合は<see cref="IExpirePolicy" />でエントリの有効性を検証後、有効であれば既存のエントリが取得されます。有効でない場合は新規取得され、既存のキャッシュエントリがアップデートされます。
        /// キーに対応するエントリがキャッシュに存在しない場合は新規取得され、キャッシュに追加されます。
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheDriveMapping" />に定義されている場合はそれを使用し、それ以外の場合は<see cref="DefaultCacheDrive" />が使用されます。
        /// 使用される<see cref="ICacheDrive" />や<see cref="IExpirePolicy" />が例外を出した場合は、<paramref name="translator" />から値を取得しそれを返します。
        /// </summary>
        /// <typeparam name="T">キャッシュ対象の型</typeparam>
        /// <param name="keys">キャッシュを取得するためのキー</param>
        /// <param name="translator">変換に使用するトランスレータ</param>
        /// <param name="expirePolicy">キャッシュの有効期限ポリシー</param>
        /// <returns>
        /// キャッシュされたオブジェクト
        /// </returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="translator" />がnullです。</exception>
        public T[] Get<T>(string[] keys, IIdenticalVectorizedTranslator<T> translator, IExpirePolicy expirePolicy)
        {
            if (translator == null) throw new ArgumentNullException("translator");
            return Get(keys, translator.Identity, translator.Get, GetCacheDrive(translator), expirePolicy);
        }

        //

        /// <summary>
        /// キー、トランスレータを指定し、キャッシュを取得します。
        /// キーに対応するエントリがキャッシュに存在した場合は<see cref="IExpirePolicy" />でエントリの有効性を検証後、有効であれば既存のエントリが取得されます。有効でない場合は新規取得され、既存のキャッシュエントリがアップデートされます。
        /// キーに対応するエントリがキャッシュに存在しない場合は新規取得され、キャッシュに追加されます。
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheDriveMapping" />に定義されている場合はそれを使用し、それ以外の場合は<see cref="DefaultCacheDrive" />が使用されます。
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="ExpirePolicyMapping" />に定義されている場合はそれを使用し、それ以外の場合は<see cref="DefaultCacheDrive" />が使用されます。
        /// 使用される<see cref="ICacheDrive" />や<see cref="IExpirePolicy" />が例外を出した場合は、<paramref name="translator" />から値を取得しそれを返します。
        /// </summary>
        /// <typeparam name="T">キャッシュ対象の型</typeparam>
        /// <param name="keys">キャッシュを取得するためのキー</param>
        /// <param name="translator">変換に使用するトランスレータ</param>
        /// <returns>
        /// キャッシュされたオブジェクト
        /// </returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="translator" />がnullです。</exception>
        public T[] Get<T>(string[] keys, IIdenticalOptimizedTranslator<T> translator)
        {
            if (translator == null) throw new ArgumentNullException("translator");
            return Get(keys, translator.Identity, (Func<string[], T[]>)translator.Get, GetCacheDrive(translator), GetExpirePolicy(translator));
        }

        /// <summary>
        /// キー、トランスレータ、保存先を指定し、キャッシュを取得します。
        /// キーに対応するエントリがキャッシュに存在した場合は<see cref="IExpirePolicy" />でエントリの有効性を検証後、有効であれば既存のエントリが取得されます。有効でない場合は新規取得され、既存のキャッシュエントリがアップデートされます。
        /// キーに対応するエントリがキャッシュに存在しない場合は新規取得され、キャッシュに追加されます。
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="ExpirePolicyMapping" />に定義されている場合はそれを使用し、それ以外の場合は<see cref="DefaultCacheDrive" />が使用されます。
        /// 使用される<see cref="ICacheDrive" />や<see cref="IExpirePolicy" />が例外を出した場合は、<paramref name="translator" />から値を取得しそれを返します。
        /// </summary>
        /// <typeparam name="T">キャッシュ対象の型</typeparam>
        /// <param name="keys">キャッシュを取得するためのキー</param>
        /// <param name="translator">変換に使用するトランスレータ</param>
        /// <param name="cacheDrive">キャッシュを保存するするストレージ</param>
        /// <returns>
        /// キャッシュされたオブジェクト
        /// </returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="translator" />がnullです。</exception>
        public T[] Get<T>(string[] keys, IIdenticalOptimizedTranslator<T> translator, ICacheDrive cacheDrive)
        {
            if (translator == null) throw new ArgumentNullException("translator");
            return Get(keys, translator.Identity, (Func<string[], T[]>)translator.Get, cacheDrive, GetExpirePolicy(translator));
        }

        /// <summary>
        /// キー、トランスレータ、ポリシーを指定し、キャッシュを取得します。
        /// キーに対応するエントリがキャッシュに存在した場合は<see cref="IExpirePolicy" />でエントリの有効性を検証後、有効であれば既存のエントリが取得されます。有効でない場合は新規取得され、既存のキャッシュエントリがアップデートされます。
        /// キーに対応するエントリがキャッシュに存在しない場合は新規取得され、キャッシュに追加されます。
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheDriveMapping" />に定義されている場合はそれを使用し、それ以外の場合は<see cref="DefaultCacheDrive" />が使用されます。
        /// 使用される<see cref="ICacheDrive" />や<see cref="IExpirePolicy" />が例外を出した場合は、<paramref name="translator" />から値を取得しそれを返します。
        /// </summary>
        /// <typeparam name="T">キャッシュ対象の型</typeparam>
        /// <param name="keys">キャッシュを取得するためのキー</param>
        /// <param name="translator">変換に使用するトランスレータ</param>
        /// <param name="expirePolicy">キャッシュの有効期限ポリシー</param>
        /// <returns>
        /// キャッシュされたオブジェクト
        /// </returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="translator" />がnullです。</exception>
        public T[] Get<T>(string[] keys, IIdenticalOptimizedTranslator<T> translator, IExpirePolicy expirePolicy)
        {
            if (translator == null) throw new ArgumentNullException("translator");
            return Get(keys, translator.Identity, (Func<string[], T[]>)translator.Get, GetCacheDrive(translator), expirePolicy);
        }

        //

        /// <summary>
        /// キー、トランスレータ識別子、トランスレータを指定し、キャッシュを取得します。
        /// キーに対応するエントリがキャッシュに存在した場合は<see cref="IExpirePolicy" />でエントリの有効性を検証後、有効であれば既存のエントリが取得されます。有効でない場合は新規取得され、既存のキャッシュエントリがアップデートされます。
        /// キーに対応するエントリがキャッシュに存在しない場合は新規取得され、キャッシュに追加されます。
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheDriveMapping" />に定義されている場合はそれを使用し、それ以外の場合は<see cref="DefaultCacheDrive" />が使用されます。
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="ExpirePolicyMapping" />に定義されている場合はそれを使用し、それ以外の場合は<see cref="DefaultCacheDrive" />が使用されます。
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
        /// <exception cref="System.ArgumentNullException"><paramref name="translator" />がnullです。</exception>
        public T[] Get<T>(string[] keys, string translatorIdentity, IOptimizedTranslator<T> translator)
        {
            if (translator == null) throw new ArgumentNullException("translator");
            return Get(keys, translatorIdentity, (Func<string[], T[]>)translator.Get, GetCacheDrive(translator), GetExpirePolicy(translator));
        }

        /// <summary>
        /// キー、トランスレータ識別子、トランスレータ、保存先を指定し、キャッシュを取得します。
        /// キーに対応するエントリがキャッシュに存在した場合は<see cref="IExpirePolicy" />でエントリの有効性を検証後、有効であれば既存のエントリが取得されます。有効でない場合は新規取得され、既存のキャッシュエントリがアップデートされます。
        /// キーに対応するエントリがキャッシュに存在しない場合は新規取得され、キャッシュに追加されます。
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="ExpirePolicyMapping" />に定義されている場合はそれを使用し、それ以外の場合は<see cref="DefaultCacheDrive" />が使用されます。
        /// <paramref name="translatorIdentity" />はトランスレータを区別するための識別子として使用されます。同じ機能を持つトランスレータを使用した場合でも、トランスレータ識別子が異なる場合はキャッシュのエントリは異なるものとして扱われます。
        /// 使用される<see cref="ICacheDrive" />や<see cref="IExpirePolicy" />が例外を出した場合は、<paramref name="translator" />から値を取得しそれを返します。
        /// </summary>
        /// <typeparam name="T">キャッシュ対象の型</typeparam>
        /// <param name="keys">キャッシュを取得するためのキー</param>
        /// <param name="translatorIdentity">トランスレータを識別するためのキー</param>
        /// <param name="translator">変換に使用するトランスレータ</param>
        /// <param name="cacheDrive">キャッシュを保存するするストレージ</param>
        /// <returns>
        /// キャッシュされたオブジェクト
        /// </returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="translator" />がnullです。</exception>
        public T[] Get<T>(string[] keys, string translatorIdentity, IOptimizedTranslator<T> translator, ICacheDrive cacheDrive)
        {
            if (translator == null) throw new ArgumentNullException("translator");
            return Get(keys, translatorIdentity, (Func<string[], T[]>)translator.Get, cacheDrive, GetExpirePolicy(translator));
        }

        /// <summary>
        /// キー、トランスレータ識別子、トランスレータ、ポリシーを指定し、キャッシュを取得します。
        /// キーに対応するエントリがキャッシュに存在した場合は<see cref="IExpirePolicy" />でエントリの有効性を検証後、有効であれば既存のエントリが取得されます。有効でない場合は新規取得され、既存のキャッシュエントリがアップデートされます。
        /// キーに対応するエントリがキャッシュに存在しない場合は新規取得され、キャッシュに追加されます。
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheDriveMapping" />に定義されている場合はそれを使用し、それ以外の場合は<see cref="DefaultCacheDrive" />が使用されます。
        /// <paramref name="translatorIdentity" />はトランスレータを区別するための識別子として使用されます。同じ機能を持つトランスレータを使用した場合でも、トランスレータ識別子が異なる場合はキャッシュのエントリは異なるものとして扱われます。
        /// 使用される<see cref="ICacheDrive" />や<see cref="IExpirePolicy" />が例外を出した場合は、<paramref name="translator" />から値を取得しそれを返します。
        /// </summary>
        /// <typeparam name="T">キャッシュ対象の型</typeparam>
        /// <param name="keys">キャッシュを取得するためのキー</param>
        /// <param name="translatorIdentity">トランスレータを識別するためのキー</param>
        /// <param name="translator">変換に使用するトランスレータ</param>
        /// <param name="expirePolicy">キャッシュの有効期限ポリシー</param>
        /// <returns>
        /// キャッシュされたオブジェクト
        /// </returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="translator" />がnullです。</exception>
        public T[] Get<T>(string[] keys, string translatorIdentity, IOptimizedTranslator<T> translator, IExpirePolicy expirePolicy)
        {
            if (translator == null) throw new ArgumentNullException("translator");
            return Get(keys, translatorIdentity, (Func<string[], T[]>)translator.Get, GetCacheDrive(translator), expirePolicy);
        }

        //

        /// <summary>
        /// キー、トランスレータ識別子、トランスレータを指定し、キャッシュを取得します。
        /// キーに対応するエントリがキャッシュに存在した場合は<see cref="IExpirePolicy" />でエントリの有効性を検証後、有効であれば既存のエントリが取得されます。有効でない場合は新規取得され、既存のキャッシュエントリがアップデートされます。
        /// キーに対応するエントリがキャッシュに存在しない場合は新規取得され、キャッシュに追加されます。
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheDriveMapping" />に定義されている場合はそれを使用し、それ以外の場合は<see cref="DefaultCacheDrive" />が使用されます。
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="ExpirePolicyMapping" />に定義されている場合はそれを使用し、それ以外の場合は<see cref="DefaultCacheDrive" />が使用されます。
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
        /// <exception cref="System.ArgumentNullException"><paramref name="translator" />がnullです。</exception>
        public T[] Get<T>(string[] keys, string translatorIdentity, ISimplexTranslator<T> translator)
        {
            if (translator == null) throw new ArgumentNullException("translator");
            return Get(keys, translatorIdentity, translator.Get, GetCacheDrive(translator), GetExpirePolicy(translator));
        }

        /// <summary>
        /// キー、トランスレータ識別子、トランスレータ、保存先を指定し、キャッシュを取得します。
        /// キーに対応するエントリがキャッシュに存在した場合は<see cref="IExpirePolicy" />でエントリの有効性を検証後、有効であれば既存のエントリが取得されます。有効でない場合は新規取得され、既存のキャッシュエントリがアップデートされます。
        /// キーに対応するエントリがキャッシュに存在しない場合は新規取得され、キャッシュに追加されます。
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="ExpirePolicyMapping" />に定義されている場合はそれを使用し、それ以外の場合は<see cref="DefaultCacheDrive" />が使用されます。
        /// <paramref name="translatorIdentity" />はトランスレータを区別するための識別子として使用されます。同じ機能を持つトランスレータを使用した場合でも、トランスレータ識別子が異なる場合はキャッシュのエントリは異なるものとして扱われます。
        /// 使用される<see cref="ICacheDrive" />や<see cref="IExpirePolicy" />が例外を出した場合は、<paramref name="translator" />から値を取得しそれを返します。
        /// </summary>
        /// <typeparam name="T">キャッシュ対象の型</typeparam>
        /// <param name="keys">キャッシュを取得するためのキー</param>
        /// <param name="translatorIdentity">トランスレータを識別するためのキー</param>
        /// <param name="translator">変換に使用するトランスレータ</param>
        /// <param name="cacheDrive">キャッシュを保存するするストレージ</param>
        /// <returns>
        /// キャッシュされたオブジェクト
        /// </returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="translator" />がnullです。</exception>
        public T[] Get<T>(string[] keys, string translatorIdentity, ISimplexTranslator<T> translator, ICacheDrive cacheDrive)
        {
            if (translator == null) throw new ArgumentNullException("translator");
            return Get(keys, translatorIdentity, translator.Get, cacheDrive, GetExpirePolicy(translator));
        }

        /// <summary>
        /// キー、トランスレータ識別子、トランスレータ、ポリシーを指定し、キャッシュを取得します。
        /// キーに対応するエントリがキャッシュに存在した場合は<see cref="IExpirePolicy" />でエントリの有効性を検証後、有効であれば既存のエントリが取得されます。有効でない場合は新規取得され、既存のキャッシュエントリがアップデートされます。
        /// キーに対応するエントリがキャッシュに存在しない場合は新規取得され、キャッシュに追加されます。
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheDriveMapping" />に定義されている場合はそれを使用し、それ以外の場合は<see cref="DefaultCacheDrive" />が使用されます。
        /// <paramref name="translatorIdentity" />はトランスレータを区別するための識別子として使用されます。同じ機能を持つトランスレータを使用した場合でも、トランスレータ識別子が異なる場合はキャッシュのエントリは異なるものとして扱われます。
        /// 使用される<see cref="ICacheDrive" />や<see cref="IExpirePolicy" />が例外を出した場合は、<paramref name="translator" />から値を取得しそれを返します。
        /// </summary>
        /// <typeparam name="T">キャッシュ対象の型</typeparam>
        /// <param name="keys">キャッシュを取得するためのキー</param>
        /// <param name="translatorIdentity">トランスレータを識別するためのキー</param>
        /// <param name="translator">変換に使用するトランスレータ</param>
        /// <param name="expirePolicy">キャッシュの有効期限ポリシー</param>
        /// <returns>
        /// キャッシュされたオブジェクト
        /// </returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="translator" />がnullです。</exception>
        public T[] Get<T>(string[] keys, string translatorIdentity, ISimplexTranslator<T> translator, IExpirePolicy expirePolicy)
        {
            if (translator == null) throw new ArgumentNullException("translator");
            return Get(keys, translatorIdentity, translator.Get, GetCacheDrive(translator), expirePolicy);
        }

        //

        /// <summary>
        /// キー、トランスレータ識別子、トランスレータを指定し、キャッシュを取得します。
        /// キーに対応するエントリがキャッシュに存在した場合は<see cref="IExpirePolicy" />でエントリの有効性を検証後、有効であれば既存のエントリが取得されます。有効でない場合は新規取得され、既存のキャッシュエントリがアップデートされます。
        /// キーに対応するエントリがキャッシュに存在しない場合は新規取得され、キャッシュに追加されます。
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheDriveMapping" />に定義されている場合はそれを使用し、それ以外の場合は<see cref="DefaultCacheDrive" />が使用されます。
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="ExpirePolicyMapping" />に定義されている場合はそれを使用し、それ以外の場合は<see cref="DefaultCacheDrive" />が使用されます。
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
        /// <exception cref="System.ArgumentNullException"><paramref name="translator" />がnullです。</exception>
        public T[] Get<T>(string[] keys, string translatorIdentity, IVectorizedTranslator<T> translator)
        {
            if (translator == null) throw new ArgumentNullException("translator");
            return Get(keys, translatorIdentity, translator.Get, GetCacheDrive(translator), GetExpirePolicy(translator));
        }

        /// <summary>
        /// キー、トランスレータ識別子、トランスレータ、保存先を指定し、キャッシュを取得します。
        /// キーに対応するエントリがキャッシュに存在した場合は<see cref="IExpirePolicy" />でエントリの有効性を検証後、有効であれば既存のエントリが取得されます。有効でない場合は新規取得され、既存のキャッシュエントリがアップデートされます。
        /// キーに対応するエントリがキャッシュに存在しない場合は新規取得され、キャッシュに追加されます。
        /// <see cref="ITranslator{T}" />に対応するポリシーが<see cref="ExpirePolicyMapping" />に定義されている場合はそれを使用し、それ以外の場合は<see cref="DefaultCacheDrive" />が使用されます。
        /// <paramref name="translatorIdentity" />はトランスレータを区別するための識別子として使用されます。同じ機能を持つトランスレータを使用した場合でも、トランスレータ識別子が異なる場合はキャッシュのエントリは異なるものとして扱われます。
        /// 使用される<see cref="ICacheDrive" />や<see cref="IExpirePolicy" />が例外を出した場合は、<paramref name="translator" />から値を取得しそれを返します。
        /// </summary>
        /// <typeparam name="T">キャッシュ対象の型</typeparam>
        /// <param name="keys">キャッシュを取得するためのキー</param>
        /// <param name="translatorIdentity">トランスレータを識別するためのキー</param>
        /// <param name="translator">変換に使用するトランスレータ</param>
        /// <param name="cacheDrive">キャッシュを保存するするストレージ</param>
        /// <returns>
        /// キャッシュされたオブジェクト
        /// </returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="translator" />がnullです。</exception>
        public T[] Get<T>(string[] keys, string translatorIdentity, IVectorizedTranslator<T> translator, ICacheDrive cacheDrive)
        {
            if (translator == null) throw new ArgumentNullException("translator");
            return Get(keys, translatorIdentity, translator.Get, cacheDrive, GetExpirePolicy(translator));
        }

        /// <summary>
        /// キー、トランスレータ識別子、トランスレータ、ポリシーを指定し、キャッシュを取得します。
        /// キーに対応するエントリがキャッシュに存在した場合は<see cref="IExpirePolicy" />でエントリの有効性を検証後、有効であれば既存のエントリが取得されます。有効でない場合は新規取得され、既存のキャッシュエントリがアップデートされます。
        /// キーに対応するエントリがキャッシュに存在しない場合は新規取得され、キャッシュに追加されます。
        /// <see cref="ITranslator{T}" />に対応するドライブが<see cref="CacheDriveMapping" />に定義されている場合はそれを使用し、それ以外の場合は<see cref="DefaultCacheDrive" />が使用されます。
        /// <paramref name="translatorIdentity" />はトランスレータを区別するための識別子として使用されます。同じ機能を持つトランスレータを使用した場合でも、トランスレータ識別子が異なる場合はキャッシュのエントリは異なるものとして扱われます。
        /// 使用される<see cref="ICacheDrive" />や<see cref="IExpirePolicy" />が例外を出した場合は、<paramref name="translator" />から値を取得しそれを返します。
        /// </summary>
        /// <typeparam name="T">キャッシュ対象の型</typeparam>
        /// <param name="keys">キャッシュを取得するためのキー</param>
        /// <param name="translatorIdentity">トランスレータを識別するためのキー</param>
        /// <param name="translator">変換に使用するトランスレータ</param>
        /// <param name="expirePolicy">キャッシュの有効期限ポリシー</param>
        /// <returns>
        /// キャッシュされたオブジェクト
        /// </returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="translator" />がnullです。</exception>
        public T[] Get<T>(string[] keys, string translatorIdentity, IVectorizedTranslator<T> translator, IExpirePolicy expirePolicy)
        {
            if (translator == null) throw new ArgumentNullException("translator");
            return Get(keys, translatorIdentity, translator.Get, GetCacheDrive(translator), expirePolicy);
        }

        //

        /// <summary>
        /// キー、トランスレータ識別子、トランスレータ、保存先、ポリシーを指定し、キャッシュを取得します
        /// キーに対応するエントリがキャッシュに存在した場合は<see cref="IExpirePolicy" />でエントリの有効性を検証後、有効であれば既存のエントリが取得されます。有効でない場合は新規取得され、既存のキャッシュエントリがアップデートされます。
        /// キーに対応するエントリがキャッシュに存在しない場合は新規取得され、キャッシュに追加されます。
        /// <paramref name="translatorIdentity" />はトランスレータを区別するための識別子として使用されます。同じ機能を持つトランスレータを使用した場合でも、トランスレータ識別子が異なる場合はキャッシュのエントリは異なるものとして扱われます。
        /// <see cref="ICacheDrive" />や<see cref="IExpirePolicy" />が例外を出した場合は、<paramref name="translator" />から値を取得しそれを返します。
        /// </summary>
        /// <typeparam name="T">キャッシュ対象の型</typeparam>
        /// <param name="key">オブジェクトの取得のためのキー</param>
        /// <param name="translatorIdentity">トランスレータの判別のためのアイデンティティキー</param>
        /// <param name="translator">キーをオブジェクトに変換するトランスレータ</param>
        /// <param name="cacheDrive">キャッシュを保存する先</param>
        /// <param name="expirePolicy">キャッシュが有効か判断を行うポリシー</param>
        /// <returns>
        /// キャッシュされたまたは、新規に取得されたオブジェクト
        /// </returns>
        public static T Get<T>(string key, string translatorIdentity, Func<string, T> translator, ICacheDrive cacheDrive, IExpirePolicy expirePolicy)
        {
            return Get(new[] { key }, translatorIdentity, keys => keys.Select(translator).ToArray(), cacheDrive, expirePolicy).First();
        }

        /// <summary>
        /// キー、トランスレータ識別子、トランスレータ、保存先、ポリシーを指定し、キャッシュを取得します
        /// キーに対応するエントリがキャッシュに存在した場合は<see cref="IExpirePolicy" />でエントリの有効性を検証後、有効であれば既存のエントリが取得されます。有効でない場合は新規取得され、既存のキャッシュエントリがアップデートされます。
        /// キーに対応するエントリがキャッシュに存在しない場合は新規取得され、キャッシュに追加されます。
        /// <paramref name="translatorIdentity" />はトランスレータを区別するための識別子として使用されます。同じ機能を持つトランスレータを使用した場合でも、トランスレータ識別子が異なる場合はキャッシュのエントリは異なるものとして扱われます。
        /// <see cref="ICacheDrive" />や<see cref="IExpirePolicy" />が例外を出した場合は、<paramref name="translator" />から値を取得しそれを返します。
        /// このメソッドでは、<paramref name="translator" />が一括変換に最適化されている場合、パフォーマンスが出にくくなる可能性があります。
        /// </summary>
        /// <typeparam name="T">キャッシュ対象の型</typeparam>
        /// <param name="key">オブジェクトの取得のためのキー</param>
        /// <param name="translatorIdentity">トランスレータの判別のためのアイデンティティキー</param>
        /// <param name="translator">キーをオブジェクトに変換するトランスレータ</param>
        /// <param name="cacheDrive">キャッシュを保存する先</param>
        /// <param name="expirePolicy">キャッシュが有効か判断を行うポリシー</param>
        /// <returns>
        /// キャッシュされたまたは、新規に取得されたオブジェクト
        /// </returns>
        public static T Get<T>(string key, string translatorIdentity, Func<string[], T[]> translator, ICacheDrive cacheDrive, IExpirePolicy expirePolicy)
        {
            return Get(new[] { key }, translatorIdentity, translator, cacheDrive, expirePolicy).First();
        }

        /// <summary>
        /// キー、トランスレータ識別子、トランスレータ、保存先、ポリシーを指定し、キャッシュを取得します
        /// キーに対応するエントリがキャッシュに存在した場合は<see cref="IExpirePolicy" />でエントリの有効性を検証後、有効であれば既存のエントリが取得されます。有効でない場合は新規取得され、既存のキャッシュエントリがアップデートされます。
        /// キーに対応するエントリがキャッシュに存在しない場合は新規取得され、キャッシュに追加されます。
        /// 新規取得、更新対象のエントリ、更新対象でないエントリが同時に存在する場合は、新規取得、更新対象のエントリのみが新規取得されます。
        /// <paramref name="translatorIdentity" />はトランスレータを区別するための識別子として使用されます。同じ機能を持つトランスレータを使用した場合でも、トランスレータ識別子が異なる場合はキャッシュのエントリは異なるものとして扱われます。
        /// <see cref="ICacheDrive" />や<see cref="IExpirePolicy" />が例外を出した場合は、<paramref name="translator" />から値を取得しそれを返します。
        /// このメソッドでは、<paramref name="translator" />が変換が繰り返し呼ばれるため、<paramref name="translator" />の呼び出しにコストがかかる場合はパフォーマンスが出にくくなる可能性があります。
        /// </summary>
        /// <typeparam name="T">キャッシュ対象の型</typeparam>
        /// <param name="keys">オブジェクトの取得のためのキー</param>
        /// <param name="translatorIdentity">トランスレータの判別のためのアイデンティティキー</param>
        /// <param name="translator">キーをオブジェクトに変換するトランスレータ</param>
        /// <param name="cacheDrive">キャッシュを保存する先</param>
        /// <param name="expirePolicy">キャッシュが有効か判断を行うポリシー</param>
        /// <returns>
        /// キャッシュされたまたは、新規に取得されたオブジェクト
        /// </returns>
        public static T[] Get<T>(string[] keys, string translatorIdentity, Func<string, T> translator, ICacheDrive cacheDrive, IExpirePolicy expirePolicy)
        {
            return Get(keys, translatorIdentity, fkeys => fkeys.Select(translator).ToArray(), cacheDrive, expirePolicy);
        }

        /// <summary>
        /// キー、トランスレータ識別子、トランスレータ、保存先、ポリシーを指定し、キャッシュを取得します。
        /// キーに対応するエントリがキャッシュに存在した場合は<see cref="IExpirePolicy" />でエントリの有効性を検証後、有効であれば既存のエントリが取得されます。有効でない場合は新規取得され、既存のキャッシュエントリがアップデートされます。
        /// キーに対応するエントリがキャッシュに存在しない場合、新規取得されキャッシュに追加されます。
        /// 新規取得、更新対象のエントリ、更新対象でないエントリが同時に存在する場合は、新規取得、更新対象のエントリのみが新規取得されます。
        /// <paramref name="translatorIdentity" />はトランスレータを区別するための識別子として使用されます。同じ機能を持つトランスレータを使用した場合でも、トランスレータ識別子が異なる場合はキャッシュのエントリは異なるものとして扱われます。
        /// <see cref="ICacheDrive" />や<see cref="IExpirePolicy" />が例外を出した場合は、<paramref name="translator" />から値を取得しそれを返します。
        /// </summary>
        /// <typeparam name="T">キャッシュ対象の型</typeparam>
        /// <param name="keys">オブジェクトの取得のためのキー</param>
        /// <param name="translatorIdentity">トランスレータの判別のためのアイデンティティキー</param>
        /// <param name="translator">キーをオブジェクトに変換するトランスレータ</param>
        /// <param name="cacheDrive">キャッシュを保存する先</param>
        /// <param name="expirePolicy">キャッシュが有効か判断を行うポリシー</param>
        /// <returns>
        /// キャッシュされたまたは、新規に取得されたオブジェクト
        /// </returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="keys" />,<paramref name="translatorIdentity" />,<paramref name="translator" />,<paramref name="cacheDrive" />,<paramref name="expirePolicy" />のいずれかがnullです。</exception>
        public static T[] Get<T>(string[] keys, string translatorIdentity, Func<string[], T[]> translator, ICacheDrive cacheDrive, IExpirePolicy expirePolicy)
        {
            if (keys == null) throw new ArgumentNullException("keys");
            if (translatorIdentity == null) throw new ArgumentNullException("translatorIdentity");
            if (translator == null) throw new ArgumentNullException("translator");
            if (cacheDrive == null) throw new ArgumentNullException("cacheDrive");
            if (expirePolicy == null) throw new ArgumentNullException("expirePolicy");

            //キーに対応するエントリの取得と、キーがないもしくは更新が必要な場合はそのフラグを立てる
            var entryState = keys.Select(_ =>
            {
                var primaryKey = _ + translatorIdentity;
                var needupdate = true;
                CacheEntry<T> entry = null;
                //cacheDriveの操作に失敗する可能性を考慮
                try
                {
                    if (cacheDrive.Contains(primaryKey))
                    {
                        entry = cacheDrive.Get<T>(primaryKey);
                        needupdate = !expirePolicy.Validate(entry);
                    }
                }
                catch
                {
                    // エラーが発生しても取得だけは続行する
                }

                return new { PrimaryKey = primaryKey, Key = _, NeedUpdate = needupdate, Entry = entry };
            }).ToArray();

            //新規に取得する必要のあるキーの抽出
            var updateNeedkeys = entryState.Where(_ => _.NeedUpdate).Select(_ => _.Key).ToArray();
            //値の取得とキーのマッピング
            var entries = translator(updateNeedkeys).Zip(updateNeedkeys, (f, s) => new { Key = s, Value = f }).ToDictionary(_ => _.Key);

            //キャッシュの更新
            var newEntryState = entryState.Select(_ => new { _.PrimaryKey, IsRenew = entries.ContainsKey(_.Key), Value = entries.ContainsKey(_.Key) ? entries[_.Key].Value : _.Entry.Value }).ToArray();
            try
            {
                var updateTime = DateTime.Now;
                foreach (var item in newEntryState.Where(_ => _.IsRenew))
                {
                    cacheDrive.AddOrUpdate(new CacheEntry<T> { Key = item.PrimaryKey, Value = item.Value, UpdateTime = updateTime });
                }
            }
            catch
            {
                //キャッシュの更新に失敗しても値だけは返せるように
            }

            return newEntryState.Select(_ => _.Value).ToArray();
        }

        /// <summary>
        /// トランスレータに対応した有効期限ポリシーを返します。
        /// </summary>
        /// <typeparam name="T">キャッシュ対象の型</typeparam>
        /// <param name="translator">ポリシーを取得するトランスレータ</param>
        /// <returns>トランスレータに対応した有効期限ポリシー</returns>
        private IExpirePolicy GetExpirePolicy<T>(ITranslator<T> translator)
        {
            return ExpirePolicyMapping.ContainsKey(translator.GetType().FullName) ? ExpirePolicyMapping[translator.GetType().FullName] : DefaultExpirePolicy;
        }

        /// <summary>
        /// トランスレータに対応したキャッシュを保存するストレージを返します。
        /// </summary>
        /// <typeparam name="T">キャッシュ対象の型</typeparam>
        /// <param name="translator">ストレージを取得するトランスレータ</param>
        /// <returns>
        /// トランスレータに対応したキャッシュを保存するストレージ
        /// </returns>
        private ICacheDrive GetCacheDrive<T>(ITranslator<T> translator)
        {
            return CacheDriveMapping.ContainsKey(translator.GetType().FullName) ? CacheDriveMapping[translator.GetType().FullName] : DefaultCacheDrive;
        }

    }
}
