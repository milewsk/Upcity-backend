using Common.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Helpers
{
    public static class EnumExtension
    {
        public static string GetDescription(this Enum enumValue)
        {
            return EnumUtils.GetEnumDescription(enumValue);
        }
    }
}
