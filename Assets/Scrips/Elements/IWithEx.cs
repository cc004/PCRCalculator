using System;

namespace Elements
{
    public interface IWithEx : IComparable, IEquatable<IWithEx>
    {
        string ToString();
        double ToDouble();
        long ToLong();
        public string ToExpression(int hash);
    }
}
