using System;
using System.Collections.Generic;
using System.Linq;

public class XMLFormat : AFormat {
    private static Boolean IsNull(Object obj) {
        if (obj == null) {
            return true;
        }
        return false;
    }
    private static Boolean AreSameTypes(Object o1, Object o2) {
        if (o1.GetType() == o2.GetType()) {
            return true;
        }
        return false;
    }
    private static Boolean AreSame(Object o1, Object o2) {
        if (o1 == o2) {
            return true;
        }
        return false;
    }
    public class Node {
        public String name;
        public Int32 id;
        public override Boolean Equals(Object obj) {
            if (IsNull(obj)) {
                return false;
            }
            if (AreSame(this, obj)) {
                return true;
            }
            if (!AreSameTypes(this, obj)) {
                return false;
            }
            Node node = (Node)obj;
            if (this.id == node.id && this.name == node.name) {
                return true;
            }
            return false;
        }
    }
    public class DirNode : Node {
        public List<Node> subNodes;
        public override Boolean Equals(Object obj) {
            if (IsNull(obj)) {
                return false;
            }
            if (AreSame(this, obj)) {
                return true;
            }
            if (!AreSameTypes(this, obj)) {
                return false;
            }
            DirNode node = (DirNode)obj;
            if (!(this.id == node.id && this.name == node.name)) {
                return false;
            }
            Boolean areEqual = Enumerable.SequenceEqual(this.subNodes, node.subNodes);
            if (!areEqual) {
                return false;
            }
            return true;
        }
    }
    public class FileNode : Node {
        public override Boolean Equals(Object obj) {
            return base.Equals(obj);
        }
    }
    private DirNode root;
    public XMLFormat() {}
    public XMLFormat(IFormatInitializer initializer) : base(initializer) { }
    public void Append(Int32 id, Node nodeToAppend) {
        if (id == 0) {
            root.subNodes.Add(nodeToAppend);
        }
    }
    public void SetRoot(String name, Int32 id) {
        root = new DirNode() { name = name, id = id, subNodes = new List<Node>() };
    }
    public override Boolean Equals(Object obj) {
        if (obj == null || obj.GetType() != this.GetType()) {
            return false;
        }
        if (this == obj) {
            return true;
        }
        // Сначала надо написать код Equals для классов Node.

        return false;
    }
}