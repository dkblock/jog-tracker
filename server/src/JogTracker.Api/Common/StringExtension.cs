﻿namespace JogTracker.Api.Common
{
    public static class StringExtension
    {
        public static string ToLowerCamelCase(this string str) =>
            string.IsNullOrEmpty(str) || str.Length < 2
            ? str
            : char.ToLowerInvariant(str[0]) + str.Substring(1);
    }
}
