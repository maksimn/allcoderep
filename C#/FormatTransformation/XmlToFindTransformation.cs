using System;
using System.Collections.Generic;
using DirNode = XMLFormat.DirNode;
using PathInfo = FindFormat.PathInfo;

public class XmlToFindTransformation : ITransformation {
    private List<PathInfo> pathInfo = new List<PathInfo>();
    public AFormat Transform(AFormat aFormat) {
        var xml = (XMLFormat)aFormat;
        TraverseAndBuildingFindFormat((DirNode)xml.GetNode(0), String.Empty);
        pathInfo.Sort();
        return new FindFormat { n = pathInfo.Count, pathInfo = pathInfo.ToArray() };
    }
    private void WorkWithRootNode(DirNode dnode) {
        pathInfo.Add(new PathInfo(dnode.name, dnode.id));
        WorkWithSubNodes(dnode, dnode.name);
    }
    private void TraverseAndBuildingFindFormat(DirNode dnode, String currPath) {
        if (dnode.id == 0) {
            WorkWithRootNode(dnode);
        } else {
            WorkWithSubNodes(dnode, currPath);
        }
    }
    private void WorkWithSubNodes(DirNode dnode, String currPath) {
        for (Int32 i = 0; i < dnode.subNodes.Count; i++) {
            var subNode = dnode.subNodes[i];
            pathInfo.Add(new PathInfo(currPath + "/" + subNode.name, subNode.id));
            if (subNode is DirNode) {
                TraverseAndBuildingFindFormat((DirNode)subNode, currPath + "/" + subNode.name);
            }
        }
    }
}