using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace UnityEngine
{
    public static class Random
    {
        [Serializable]
        public struct State
        {
            public UnityRandom.State state;
        }

        public static State state
        {
            get =>
                new()
                {
                    state = random.state
                };
            set => random.state = value.state;
        }
#if ENABLE_THREADING
        [ThreadStatic]
        private static UnityRandom _random;

        public static UnityRandom random => _random ??= new();
#else
        public static UnityRandom random = new UnityRandom();
#endif
        public static int _seed = 0;

        public static int seed
        {
            get { return _seed; }
            set { _seed = value; random.InitState(_seed); }
        }

        public static void InitState(int seed)
        {
            random.InitState(seed);
        }

        public static int Range(int minInclusive, int maxExclusive)
        {
            return random.Range(minInclusive, maxExclusive);
        }
    }

}
