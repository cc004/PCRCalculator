public class UnityRandom
{
    public struct State
    {
        public uint x, y, z, w;
    }
    public static State state;
    public static void InitState(int seed)
    {
        state.x = (uint)seed;
        state.y = (uint)(1812433253 * state.x + 1);
        state.z = (uint)(1812433253 * state.y + 1);
        state.w = (uint)(1812433253 * state.z + 1);
    }

    public static int Range(int min, int max)
    {
        return min + (int)(Random() % (max - min));
    }

    public static uint Random()
    {
        uint t = state.x, s = state.w;
        state.x = state.y;
        state.y = state.z;
        state.z = state.w;

        t ^= t << 11;
        t ^= t >> 8;
        state.w = t ^ s ^ (s >> 19);
        return state.w;
    }
}