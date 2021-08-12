using System;
using System.Collections.Generic;
using System.Linq;

public static class ConvertDict
{
    public static Dictionary<string,int> ValueInt(object value)
    {
        return (value as Dictionary<string, object>).ToDictionary(k => k.Key, k => Convert.ToInt32(k.Value));
    }

    public static Dictionary<string,string> ValueStr(object value)
    {
        return (value as Dictionary<string, object>).ToDictionary(k => k.Key, k => Convert.ToString(k.Value));
    }

}