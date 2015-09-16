using System;
using PathInfo = FindFormat.PathInfo;

public class FindFormatInitializerTest1 : IFormatInitializer {
    public void Init(AFormat format) {
        FindFormat find = (FindFormat)format;
        find.n = 2;
        find.pathInfo = new PathInfo[2];
        find.pathInfo[0] = new PathInfo("site", 0);
        find.pathInfo[1] = new PathInfo("site/news", 12);
    }
}