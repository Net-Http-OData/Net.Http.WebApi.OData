using System;

namespace NorthwindModel
{
    [Flags]
    public enum AccessLevel
    {
        None = 0,
        Read = 1,
        Write = 2,
        Delete = 4
    }
}
