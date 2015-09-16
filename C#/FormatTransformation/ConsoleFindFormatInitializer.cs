using System;
using PathInfo = FindFormat.PathInfo;

public class ConsoleFindFormatInitializer : IFormatInitializer {
    public void Init(AFormat format) {
        FindFormat find = (FindFormat)format;
        String line = Console.ReadLine();
        find.n = Convert.ToInt32(line);
        find.pathInfo = new PathInfo[find.n];
        for (Int32 i = 0; i < find.n; i++) {
            line = Console.ReadLine();
            String[] s = line.Split(new Char[] { ' ' });
            find.pathInfo[i] = new PathInfo(s[0], Convert.ToInt32(s[1]));
        }
    }
}