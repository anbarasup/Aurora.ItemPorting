using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Cognizant.ItemPorting.Client.Utilities
{
    public class Utility
    {
        public static T GetAttribute<T>(MethodBase methodBase)
        {
            return (T)methodBase.GetCustomAttributes(typeof(T), true)[0];
        }
    }
}
