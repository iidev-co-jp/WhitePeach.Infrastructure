using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhitePeach.Infrastructure.Caching
{
    class AnonymousTranslator<T> : IOptimizedTranslator<T>
    {
        private readonly Func<string, T> _translator;

        private readonly Func<string[], T[]> _vectorizedTranslator;

        public AnonymousTranslator(Func<string, T> translator)
            : this(translator, keys => keys.Select(translator).ToArray())
        {
        }

        public AnonymousTranslator(Func<string[], T[]> translator)
            : this(key => translator(new[] { key }).First(), translator)
        {
        }

        public AnonymousTranslator(Func<string, T> translator, Func<string[], T[]> vectorizedTranslator)
        {
            _translator = translator;
            _vectorizedTranslator = vectorizedTranslator;
        }

        public T Get(string key)
        {
            return _translator(key);
        }

        public T[] Get(string[] keys)
        {
            return _vectorizedTranslator(keys);
        }
    }
}
