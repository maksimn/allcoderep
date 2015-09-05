using System;
using System.Collections.Generic;

public class XMLFormatInitializerTest1 : IFormatInitializer {
    public void Init(AFormat format) {
        XMLFormat xml = (XMLFormat)format;
        xml.root = new XMLFormat.DirNode();
        xml.root.id = 0;
        xml.root.name = "site";
        xml.root.subNodes = new List<XMLFormat.Node>();
        xml.root.subNodes.Add(new XMLFormat.FileNode() { id = 12, name = "news" });
    }
}