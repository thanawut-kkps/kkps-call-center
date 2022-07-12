﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phatra.Core.Utilities
{
    public class EnumUtility
    {
        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
}
