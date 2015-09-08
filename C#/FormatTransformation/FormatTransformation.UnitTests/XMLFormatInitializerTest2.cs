public class XMLFormatInitializerTest2 : IFormatInitializer {
    public void Init(AFormat format) {
        XMLFormat xml = (XMLFormat)format;
        xml.SetRoot("deo");
        xml.Append(0, new XMLFormat.DirNode { id = 36, name = "sek" });
        xml.Append(0, new XMLFormat.FileNode { id = 121, name = "try" });
        xml.Append(36, new XMLFormat.DirNode { id = 99, name = "uig" });
        xml.Append(36, new XMLFormat.FileNode { id = 2092, name = "qvg" });
        xml.Append(99, new XMLFormat.FileNode { id = 370, name = "xoj" });
    }
}