using System;
using System.Text;

public class FindFormat : AFormat {
    public Int32 n;
    public Int32[] id;
    public String[] filepath;
    public FindFormat(IFormatInitializer initializer) : base(initializer) { }
    public override string ToString() {
        StringBuilder sb = new StringBuilder();
        sb.Append(n.ToString() + "\n");
        for (Int32 i = 0; i < n; i++) {
            sb.Append(String.Format("{0} {1}\n", filepath[i], id[i]));
        }
        return sb.ToString();
    }
}
