using System;
using System.Collections.Generic;

public class XMLFormatInitializerTest1 : IFormatInitializer {
    public void Init(AFormat format) {
        XMLFormat xml = (XMLFormat)format;
        xml.SetRoot("site", 0);
        xml.Append(0, new XMLFormat.FileNode { id = 12, name = "news" });
    }
}