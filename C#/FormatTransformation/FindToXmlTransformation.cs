using System;
using System.Linq;
using DirNode = XMLFormat.DirNode;
using FileNode = XMLFormat.FileNode;

public class FindToXmlTransformation : ITransformation {
    private FindFormat find = null;
    private Char[] separ = new Char[] { '/' };
    private XMLFormat xml = null;
    public AFormat Transform(AFormat aFormat) {
        find = (FindFormat)aFormat;
        xml = new XMLFormat();
        xml.SetRoot(find.GetPath(0));
        var root = (DirNode)xml.GetNode(0);
        FindSubNodesAndAppend(root, 1, root.name);
        return xml;
    }
    private void FindSubNodesAndAppend(DirNode dnode, Int32 pos, String currPath) {
        Int32 lvl = currPath.Count(ch => ch == '/'); // Уровень вложенности текущей папки
        for (Int32 i = pos; i < find.n; i++) {
            var path = find.GetPath(i);
            Int32 theLvl = path.Count(ch => ch == '/');
            if (theLvl == lvl + 1 && path.StartsWith(currPath)) {
                String name = path.Substring(currPath.Length + 1);
                if(IsFile(path, i)) {
                    FileNode fnode = new FileNode { id = find.GetId(i), name = name };
                    xml.Append(dnode.id, fnode);
                } else {
                    DirNode node = new DirNode { id = find.GetId(i), name = name };
                    xml.Append(dnode.id, node);
                    FindSubNodesAndAppend(node, i + 1, currPath + "/" + name);
                }
            }
        }
    }
    private Boolean IsFile(String path, Int32 pos) {
        for (Int32 i = pos + 1; i < find.n; i++) {
            if (find.GetPath(i).StartsWith(path)) {
                return false;
            }
        }
        return true;
    }
}