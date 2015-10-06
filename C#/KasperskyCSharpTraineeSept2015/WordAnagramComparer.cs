using System;
using System.Collections.Generic;

class WordAnagramComparer : IComparer<String> {
    public Int32 Compare(String str1, String str2) {
        return String.Compare(GetAnagramString(str1), GetAnagramString(str2), StringComparison.Ordinal);
    }
    public static String GetAnagramString(String word) {
        Char[] chars = word.ToLowerInvariant().ToCharArray();
        Array.Sort(chars);
        return new String(chars);
    }
}