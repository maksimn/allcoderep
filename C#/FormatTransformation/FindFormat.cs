using System;
using System.Linq;
using System.Text;

public class FindFormat : AFormat {
    public Int32 n;
    public Int32[] id;
    public String[] filepath;
    public FindFormat() {}
    public FindFormat(IFormatInitializer initializer) : base(initializer) {}
    public override Boolean Equals(Object obj) {
        return EqualityComparerHelper(this, obj, () => {
            FindFormat find = (FindFormat)obj;
            if (this.n == find.n && Enumerable.SequenceEqual(this.id, find.id) && 
                Enumerable.SequenceEqual(this.filepath, find.filepath)) {
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
            sb.Append(String.Format("{0} {1}\n", filepath[i], id[i]));
        }
        return sb.ToString();
    }
}
