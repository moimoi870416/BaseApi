using System;

namespace Base.Api.Enums
{
    [Flags]
    public enum MemberStatus
    {
        None = 0,
        Suspend = 1,
        Closed = 2,
        Deleted = 4,
        Cash = 8,
        TestMode = 16,
        Locked = 32,
        Api = 256,
        ReadOnly = 512,
        SuspiciousTagByCustomer = 1024,
        WalkIn = 2048,
        StockSystem = 4096,
        CompanyCap = 8192,
        LockStockTransfer = 16384
    }
}