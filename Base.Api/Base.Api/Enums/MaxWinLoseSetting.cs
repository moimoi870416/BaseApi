using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Api.Enums
{
    public enum MaxWinSetting
    {
        Unlimited = 0,
        _3Times = 3,
        _5Times = 5,
        _10Times = 10
    }

    public enum MaxLoseSetting
    {
        Unlimited = 10,
        _80Percent = 8,
        _50Percent = 5,
        _30Percent = 3,
        _10Percent = 1

    }
}
