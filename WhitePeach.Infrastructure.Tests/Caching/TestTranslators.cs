using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhitePeach.Infrastructure.Caching;

namespace WhitePeach.Infrastructure.Tests.Caching
{
    class KeyLengthSimplexTranslator : ISimplexTranslator<int>
    {
        public int Get(string key)
        {
            return key.Length;
        }
    }

    class KeyLengthVectorizedTranslator : IVectorizedTranslator<int>
    {

        public int[] Get(string[] keys)
        {
            return keys.Select(_ => _.Length).ToArray();
        }
    }

    class KeyLengthOptimizedTranslator : IOptimizedTranslator<int>
    {
        public int Get(string key)
        {
            return key.Length;
        }

        public int[] Get(string[] keys)
        {
            return keys.Select(_ => _.Length).ToArray();
        }
    }

    class KeyLengthIdenticalSimplexTranslator : IIdenticalSimplexTranslator<int>
    {
        public KeyLengthIdenticalSimplexTranslator(string identity)
        {
            Identity = identity;
        }

        public int Get(string key)
        {
            return key.Length;
        }

        public string Identity { get; private set; }
    }

    class KeyLengthIdenticalVectorizedTranslator : IIdenticalVectorizedTranslator<int>
    {
        public KeyLengthIdenticalVectorizedTranslator(string identity)
        {
            Identity = identity;
        }

        public int[] Get(string[] keys)
        {
            return keys.Select(_ => _.Length).ToArray();
        }

        public string Identity { get; private set; }
    }

    class KeyLengthIdenticalOptimizedTranslator : IIdenticalOptimizedTranslator<int>
    {
        public KeyLengthIdenticalOptimizedTranslator(string identity)
        {
            Identity = identity;
        }

        public int Get(string key)
        {
            return key.Length;
        }

        public int[] Get(string[] keys)
        {
            return keys.Select(_ => _.Length).ToArray();
        }

        public string Identity { get; private set; }
    }
}
