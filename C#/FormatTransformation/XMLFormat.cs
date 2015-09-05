using System;
using System.Collections.Generic;

public class XMLFormat : AFormat {
    public class Node {
        public String name;
        public Int32 id;
    }
    public class DirNode : Node {
        public List<Node> subNodes;
    }
    public class FileNode : Node { }
    public DirNode root;
    public XMLFormat(IFormatInitializer initializer) : base(initializer) { }
    public void Append(Node node, Node nodeToAppend) {
        DirNode dirNode = (DirNode)node;
        dirNode.subNodes.Add(nodeToAppend);
    }
}