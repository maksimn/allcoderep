using System;
using System.Collections.Generic;
using DirNode = XMLFormat.DirNode;

public class XmlToFindTransformation : ITransformation {
    private List<Int32> id = new List<Int32>();
    private List<String> path = new List<String>();
    public AFormat Transform(AFormat aFormat) {
        var xml = (XMLFormat)aFormat;
        Traverse((DirNode)xml.GetNode(0), String.Empty);
        BSort();
        return new FindFormat { n = id.Count, filepath = path.ToArray(), id = id.ToArray() };
    }
    private void Traverse(DirNode dnode, String currPath) {
        if (dnode.id == 0) {
            id.Add(dnode.id);
            path.Add(dnode.name);
            for (Int32 i = 0; i < dnode.subNodes.Count; i++) {
                var subNode = dnode.subNodes[i];
                id.Add(subNode.id);
                path.Add(dnode.name + "/" + subNode.name);
                if (subNode is DirNode) {
                    Traverse((DirNode)subNode, dnode.name + "/" + subNode.name);
                }
            }
        } else {
            for (Int32 i = 0; i < dnode.subNodes.Count; i++) {
                var subNode = dnode.subNodes[i];
                id.Add(subNode.id);
                path.Add(currPath + "/" + subNode.name);
                if (subNode is DirNode) {
                    Traverse((DirNode)subNode, currPath + "/" + subNode.name);
                }
            }
        }
    }
    private void BSort() {
        for (Int32 i = 0, n = id.Count; i < n; i++) {
            for (Int32 j = 0; j < n - 1; j++) {
                if (id[j] > id[j + 1]) {
                    Int32 t = id[j]; id[j] = id[j + 1]; id[j + 1] = t;
                    String ts = path[j]; path[j] = path[j + 1]; path[j + 1] = ts;
                }
            }
        }
    }
}