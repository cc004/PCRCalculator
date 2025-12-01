using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityEngine
{
    public class CoroutineRunner
    {
#if ENABLE_THREADING
        [ThreadStatic]
#endif
        public static List<IEnumerator> running = new List<IEnumerator>();
#if ENABLE_THREADING
        [ThreadStatic]
#endif
        public static List<IEnumerator> pending = new List<IEnumerator>();
#if ENABLE_THREADING
        public static void Initialize()
        {
            running = new();
            pending = new();
        }
#endif
        public static void AppendCoroutine(IEnumerator routine)
        {
            pending.Add(routine);
        }
        public static void AppendCoroutine(Action updater)
        {
            IEnumerator Routine()
            {
                for (; ; )
                {
                    updater();
                    yield return null;
                }
            }
            AppendCoroutine(Routine());
        }

        public static void Update()
        {
            foreach (var routine in running)
            {
                if (routine.MoveNext())
                {
                    if (routine.Current is IEnumerator coroutine)
                    {
                        IEnumerator combinedRoutine()
                        {
                            while (coroutine.MoveNext())
                                yield return coroutine.Current;

                            while (routine.MoveNext())
                                yield return routine.Current;
                        }
                        pending.Add(combinedRoutine());
                    }
                    else
                        pending.Add(routine);
                }
                
            }
            running.Clear();
            (running, pending) = (pending, running);
        }
    }
}
