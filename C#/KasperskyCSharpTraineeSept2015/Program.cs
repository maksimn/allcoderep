using System;
using System.IO;
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

interface IInitializer {
    void Init(List<String> list);
}

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
            "эюя\n";
        StringReader sr = new StringReader(inputString);
        String word;
        while ((word = sr.ReadLine()) != null) {
            list.Add(word);
        }
    }
}

class TaskSolver {
    private List<String> list;
    public TaskSolver(IInitializer initializer) {
        list = new List<String>();
        initializer.Init(list);
    }
    public void Process() {
        list.Sort(new WordAnagramComparer());
    }
    public void Output(IOutputable iout) {
        iout.Output(list);
    }
}

interface IOutputable {
    void Output(List<String> list);
}

class ConsoleOutputExample1 : IOutputable {
    public void Output(List<String> list) {
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

class Program {
    static void Main(String[] args) {
        TaskSolver solver = new TaskSolver(new StringReaderInitializer());
        solver.Process();
        solver.Output(new ConsoleOutputExample1());
    }
}