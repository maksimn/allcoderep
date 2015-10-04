using System;
using System.IO;
using System.Collections.Generic;

class WordAnagramComparer : IComparer<KeyValuePair<String, String>> {
    public Int32 Compare(KeyValuePair<String, String> kv1, KeyValuePair<String, String> kv2) {
        return String.Compare(kv1.Key, kv2.Key, StringComparison.Ordinal);
    }
}

interface IInitializer {
    void Init(List<KeyValuePair<String, String>> list);
}

class StringReaderInitializer : IInitializer {
    public void Init(List<KeyValuePair<String, String>> list) {
        String inputString = "абак\n" +
            "ток\n" +
            "вертикаль\n" +
            "кто\n" +
            "кильватер\n" +
            "колун\n" +
            "кот\n" +
            "уклон\n";
        StringReader sr = new StringReader(inputString);
        String word;
        while ((word = sr.ReadLine()) != null) {
            list.Add(new KeyValuePair<String, String>(GetAnagramString(word), word));
        }
    }
    private static String GetAnagramString(String word) {
        Char[] chars = word.ToLowerInvariant().ToCharArray();
        Array.Sort(chars);
        return new String(chars);
    }
}

class TaskSolver {
    private List<KeyValuePair<String, String>> list;
    public TaskSolver(IInitializer initializer) {
        list = new List<KeyValuePair<String, String>>();
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
    void Output(List<KeyValuePair<String, String>> list);
}

class ConsoleOutputExample1 : IOutputable {
    public void Output(List<KeyValuePair<String, String>> list) {
        foreach (var element in list) {
            Console.WriteLine(String.Format("{0} {1}", element.Key, element.Value));
        }
    }
}

class Program {
    static void Main(String[] args) {
        TaskSolver solver = new TaskSolver(new StringReaderInitializer());
        solver.Process();
        solver.Output(new ConsoleOutputExample1());
    }
}