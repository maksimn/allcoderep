using System;

public class ConsoleFindFormatInitializer : IFormatInitializer {
    public void Init(AFormat format) {
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