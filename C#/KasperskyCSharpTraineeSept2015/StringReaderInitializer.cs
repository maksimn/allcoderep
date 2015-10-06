using System;
using System.IO;
using System.Collections.Generic;

class StringReaderInitializer : IInitializer {
    public void Init(List<String> list) {
        String inputString = "абак\n" +
            "ток\n" +
            "вертикаль\n" +
            "кто\n" +
            "кильватер\n" +
            "колун\n" +
            "кот\n" +
            "уклон\n" +
            "эюя\n" + "эяю\n" + "яэю\n" + "zzzz\n" + "яяя\n";
        StringReader sr = new StringReader(inputString);
        String word;
        while ((word = sr.ReadLine()) != null) {
            list.Add(word);
        }
    }
}