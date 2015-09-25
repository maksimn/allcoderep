using System;
using System.Collections.Generic;
using DirNode = XMLFormat.DirNode;
using PathInfo = FindFormat.PathInfo;

public class XmlToFindTransformation : ITransformation {
    private List<PathInfo> pathInfo = new List<PathInfo>();
    public AFormat Transform(AFormat aFormat) {
        var xml = (XMLFormat)aFormat;
        DirNode root = (DirNode)xml.GetNode(0);
        pathInfo.Add(new PathInfo(root.name, root.id));
        WorkWithSubNodes(root, root.name);
        pathInfo.Sort();
        return new FindFormat { n = pathInfo.Count, pathInfo = pathInfo.ToArray() };
    }
    private void WorkWithSubNodes(DirNode dnode, String currPath) {
        for (Int32 i = 0; i < dnode.subNodes.Count; i++) {
            var subNode = dnode.subNodes[i];
            pathInfo.Add(new PathInfo(currPath + "/" + subNode.name, subNode.id));
            if (subNode is DirNode) {
                WorkWithSubNodes((DirNode)subNode, currPath + "/" + subNode.name);
            }
        }
    }
}