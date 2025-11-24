using System;

namespace Elements
{
#if !DONT_USE_EX
    public interface IWithEx : IComparable, IEquatable<IWithEx>
    {
        string ToString();
        double ToDouble();
        long ToLong();
        public string ToExpression(int hash);
    }
#endif
}
