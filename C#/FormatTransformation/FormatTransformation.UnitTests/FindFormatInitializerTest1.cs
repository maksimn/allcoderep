using System;

public class FindFormatInitializerTest1 : IFormatInitializer {
    public void Init(AFormat format) {
        FindFormat find = (FindFormat)format;
        find.n = 2;
        find.filepath = new String[] { "site", "site/news" };
        find.id = new Int32[] { 0, 12 };
    }
}