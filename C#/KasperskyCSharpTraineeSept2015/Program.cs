using System;
using System.IO;
using System.Collections.Generic;

class Program {
    private static String GetAnagramString(String word) {
        Char[] chars = word.ToLowerInvariant().ToCharArray();
        Array.Sort(chars);
        return new String(chars);
    }
    private static void IndexStringFromStringReader(StringReader sr) {
        String word;
        var structure = new List<KeyValuePair<String, String>>();
        while ((word = sr.ReadLine()) != null) {
            structure.Add(new KeyValuePair<String, String>(GetAnagramString(word), word));
        }
        foreach (var element in structure) {
            Console.WriteLine(String.Format("{0} {1}", element.Key, element.Value));
        }
    }
    static void Main(String[] args) {
        String inputString = "абак\n" +
            "ток\n" +
            "вертикаль\n" +
            "кто\n" +
            "кильватер\n" +
            "колун\n" +
            "кот\n" +
            "уклон\n";
        StringReader sr = new StringReader(inputString);
        IndexStringFromStringReader(sr);
    }
}
