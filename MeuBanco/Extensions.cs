using System;

namespace MeuBanco
{
    public static class CustomExtensions
    {
        public static decimal ToDecimal(this string str)
        {
            decimal.TryParse(str, out var x);
            return x;
        }
        
        public static byte ToByte(this string key)
        {
            byte.TryParse(key, out var x);
            return x;
        }
    }
}