using System;
using DirNode = XMLFormat.DirNode;
using FileNode = XMLFormat.FileNode;

public class XmlFormatFromConsole : IFormatInitializer {
    private XMLFormat xml = null;
    public void Init(AFormat format) {
        xml = format as XMLFormat;
        ProcessDirectory(0);
    }
    private void ProcessDirectory(Int32 currDirId) {
        String line;
        String type, name;
        Int32 id;
        do {
            line = Console.ReadLine();
            if(line == null || line.EndsWith("</dir>")) {
                return;
            }
            var tuple = GetTypeNameIdTupleFromLine(line);
            type = tuple.Item1; 
            name = tuple.Item2; 
            id = tuple.Item3;
            if (id == 0) {
                xml.SetRoot(name);
                ProcessDirectory(0);
            } else {
                if (type == "dir") {
                    xml.Append(currDirId, new DirNode { id = id, name = name });
                    ProcessDirectory(id);
                } else if (type == "file") {
                    xml.Append(currDirId, new FileNode { id = id, name = name });
                }
            }
        } while(!line.EndsWith("</dir>"));
    }
    public static Tuple<String, String, Int32> GetTypeNameIdTupleFromLine(String line) {
        Int32 first = line.IndexOf('\''), second = line.IndexOf('\'', first + 1), 
            third = line.IndexOf('\'', second + 1), fourth = line.IndexOf('\'', third + 1);
        String type = line.Substring(line.IndexOf('<') + 1, 1);
        if (type == "f") {
            type = "file";
        } else if (type == "d") {
            type = "dir";
        } else {
            type = null;
        }
        String name = line.Substring(first + 1, second - first - 1);
        Int32 id = Convert.ToInt32(line.Substring(third + 1, fourth - third - 1));
        return new Tuple<String, String, Int32>(type, name, id);
    }
}
