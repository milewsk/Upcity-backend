using Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Helpers
{
    public static class EnumExtension
    {
        public static string GetDescription(this Enum enumValue)
        {
            return EnumUtils.GetEnumDescription(enumValue);
        }
    }
}
