using System;
using System.Linq;
using System.Text;

public class FindFormat : AFormat {
    public Int32 n;
    public class PathInfo : IComparable<PathInfo> {
        public Int32 id;
        public String filepath;
        public PathInfo(String path, Int32 id) {
            filepath = path;
            this.id = id;
        }
        public override Boolean Equals(Object obj) {
            return EqualityComparerHelper(this, obj, () => {
                PathInfo pathInfo = (PathInfo)obj;
                if (this.id == pathInfo.id && this.filepath == pathInfo.filepath) {
                    return true;
                }
                return false;
            });
        }
        public override Int32 GetHashCode() {
            return base.GetHashCode();
        }
        public Int32 CompareTo(PathInfo other) {
            return id - other.id;
        }
    }
    public PathInfo[] pathInfo;
    public FindFormat() {}
    public FindFormat(IFormatInitializer initializer) : base(initializer) {}
    public String GetPath(Int32 i) {
        return pathInfo[i].filepath;
    }
    public Int32 GetId(Int32 i) {
        return pathInfo[i].id;
    }
    public override Boolean Equals(Object obj) {
        return EqualityComparerHelper(this, obj, () => {
            FindFormat find = (FindFormat)obj;
            if (this.n == find.n && Enumerable.SequenceEqual(this.pathInfo, find.pathInfo)) {
                return true;
            }
            return false;
        });
    }
    public override Int32 GetHashCode() {
        return base.GetHashCode();
    }
    public override String ToString() {
        StringBuilder sb = new StringBuilder();
        sb.Append(n.ToString() + "\n");
        for (Int32 i = 0; i < n; i++) {
            sb.Append(String.Format("{0} {1}\n", pathInfo[i].filepath, pathInfo[i].id));
        }
        return sb.ToString();
    }
    protected override string Name {
        get {
            return "find";
        }
    }
}
