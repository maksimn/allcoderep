using System;
using System.IO;
using System.Collections.Generic;

class Program {
    private static String GetAnagramString(String word) {
        Char[] chars = word.ToCharArray(); 
        Array.Sort(chars);
        return new String(chars);
    }
    private static void IndexStringFromStringReader(StringReader sr) {
        String word;
        //var sortedByAnagramString = new SortedSet();
        while ((word = sr.ReadLine()) != null) {
            Console.WriteLine(word + " " + GetAnagramString(word));
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
