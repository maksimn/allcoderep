using System;

public class LogAnalyzer {
    public Boolean IsValidLogFileName(String fileName) { 
        if(!fileName.EndsWith(".SLF")) {
            return false;
        }
        return true;
    }
}

public class FindFormat {
    public Int32 n;
    public Int32[] id;
    public String[] filepath;
    public FindFormat(IFormatInitializer initializer) {
        initializer.Init(this);
    }
}

public interface IFormatInitializer {
    void Init(Object format);
}

public class Test1FindFormatInitializer : IFormatInitializer {
    public void Init(Object format) {
        FindFormat find = (FindFormat)format;
        find.n = 2;
        find.filepath = new String[] { "site", "site/news" };
        find.id = new Int32[] { 0, 12 };
    }
}

public class ConsoleFindFormatInitializer : IFormatInitializer {
    public void Init(Object format) {
        FindFormat find = (FindFormat)format;
        String line = Console.ReadLine();
        find.n = Convert.ToInt32(line);
        find.filepath = new String[find.n];
        find.id = new Int32[find.n];
        for (Int32 i = 0; i < find.n; i++) {
            line = Console.ReadLine();
            String[] s = line.Split(new Char[] { ' ' });
            find.filepath[i] = s[0];
            find.id[i] = Convert.ToInt32(s[1]);
        }
    }
}

class Program {
    static void Main(String[] args) {
        FindFormat find = new FindFormat(new Test1FindFormatInitializer());
        Console.ReadLine();
    }
}