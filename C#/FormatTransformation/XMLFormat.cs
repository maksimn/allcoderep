using System;
using System.Collections.Generic;
using System.Linq;

public class XMLFormat : AFormat {
    public class Node {
        public String name;
        public Int32 id;
        public override Boolean Equals(Object obj) {
            return EqualityComparerHelper(this, obj, () => {
                Node node = (Node)obj;
                if (this.id == node.id && this.name == node.name) {
                    return true;
                }
                return false;
            });
        }
        public override Int32 GetHashCode() {
            return base.GetHashCode();
        }
    }
    public class DirNode : Node {
        public List<Node> subNodes = new List<Node>();
        public override Boolean Equals(Object obj) {
            return EqualityComparerHelper(this, obj, () => {
                DirNode node = (DirNode)obj;
                if (!(this.id == node.id && this.name == node.name)) {
                    return false;
                }
                return Enumerable.SequenceEqual(this.subNodes, node.subNodes);
            });            
        }
        public override Int32 GetHashCode() {
            return base.GetHashCode();
        }
    }
    public class FileNode : Node {}
    private DirNode root;
    public XMLFormat() {}
    public XMLFormat(IFormatInitializer initializer) : base(initializer) { }
    public void Append(Int32 id, Node nodeToAppend) {
        var node = GetNode(id);
        if (node is DirNode) {
            ((DirNode)node).subNodes.Add(nodeToAppend); 
        }
    }
    public Node GetNode(Int32 id) {
        Node nodeToSearch = null;
        Search(root, id, ref nodeToSearch);
        return nodeToSearch;
    }
    private void Search(DirNode n, Int32 id, ref Node res) {
        if (n.id == id) {
            res = n;
            return;
        }
        else {
            for (Int32 i = 0; i < n.subNodes.Count; i++) {
                Node subNode = n.subNodes[i]; 
                if (subNode.id == id) {
                    res = subNode;
                    return;
                }
                if (subNode is DirNode) {
                    Search((DirNode)subNode, id, ref res);
                }
            }
        }
    }
    public void SetRoot(String name) {
        root = new DirNode() { name = name, id = 0 };
    }
    public override Boolean Equals(Object obj) {
        return EqualityComparerHelper(this, obj, () => {
            if (root != null) {
                XMLFormat xml = (XMLFormat)obj;
                if (!root.Equals(xml.root)) {
                    return false;
                } else {
                    return Enumerable.SequenceEqual(root.subNodes, xml.root.subNodes);
                } 
            }
            return false;
        });
    }
    public override Int32 GetHashCode() {
        return base.GetHashCode();
    }
}