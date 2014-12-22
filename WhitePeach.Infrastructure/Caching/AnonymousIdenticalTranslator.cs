using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhitePeach.Infrastructure.Caching
{
    class AnonymousIdenticalTranslator<T> : AnonymousTranslator<T>, IIdenticalOptimizedTranslator<T>
    {
        private readonly string _identity;

        public AnonymousIdenticalTranslator(string translatorIdentity, Func<string, T> translator)
            : base(translator)
        {
            _identity = translatorIdentity;
        }

        public AnonymousIdenticalTranslator(string translatorIdentity, Func<string[], T[]> translator)
            : base(translator)
        {
            _identity = translatorIdentity;
        }

        public string Identity
        {
            get { return _identity; }
        }
    }
}
