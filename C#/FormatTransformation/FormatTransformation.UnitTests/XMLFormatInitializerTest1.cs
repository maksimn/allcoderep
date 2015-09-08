public class XMLFormatInitializerTest1 : IFormatInitializer {
    public void Init(AFormat format) {
        XMLFormat xml = (XMLFormat)format;
        xml.SetRoot("site");
        xml.Append(0, new XMLFormat.FileNode { id = 12, name = "news" });
    }
}