using System;
using System.Collections.Generic;
using System.IO;

class FileOutput : IOutputable {
    private readonly String outFileName;
    public FileOutput(String fileName) {
        outFileName = fileName.Substring(0, fileName.LastIndexOf('.') - 1) + "_anagram.txt";
    }
    private void Write(List<String> list) {
        HashSet<String> set = new HashSet<String>();
        for (Int32 i = 0, j = 0; i < list.Count; ) {
            String anagramGroup = WordAnagramComparer.GetAnagramString(list[i]);
            for (j = 0; i + j <= list.Count; j++) {
                if (i + j == list.Count) {
                    PrintLineFromSet(set);
                    return;
                }
                if (AreWordsFromSameAnagramGroup(list[i], list[i + j])) {
                    set.Add(list[i + j]);
                } else {
                    i = i + j;
                    PrintLineFromSet(set);
                    break;
                }
            }
        }
    }
    public void Output(List<String> list) {
        if (File.Exists(outFileName)) {
            // Открыть файл и записать в него результат
        } else {
            // Создать файл и записать в него результат
        }        
    }
    private static void PrintLineFromSet(HashSet<String> set) {
        foreach (var word in set) {
            Console.Write(word + " ");
        }
        Console.Write("\n");
        set.Clear();
    }
    private static Boolean AreWordsFromSameAnagramGroup(String word1, String word2) {
        return WordAnagramComparer.GetAnagramString(word1) == WordAnagramComparer.GetAnagramString(word2);
    }
}
