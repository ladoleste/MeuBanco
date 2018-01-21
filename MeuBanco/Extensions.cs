using System;

namespace MeuBanco
{
    public static class CustomExtensions
    {
        public static int ToInt(this string str)
        {
            int.TryParse(str, out var x);
            return x;
        }
    }
}