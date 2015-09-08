using System;

public abstract class AFormat {
    protected AFormat() {}
    protected AFormat(IFormatInitializer initializer) {
        initializer.Init(this);
    }
    protected static Boolean IsNull(Object obj) {
        if (obj == null) {
            return true;
        }
        return false;
    }
    protected static Boolean AreSameTypes(Object o1, Object o2) {
        if (o1.GetType() == o2.GetType()) {
            return true;
        }
        return false;
    }
    protected static Boolean AreSame(Object o1, Object o2) {
        if (o1 == o2) {
            return true;
        }
        return false;
    }
    protected static Boolean EqualityComparerHelper(Object obj1, Object obj2, Func<Boolean> cmp) {
        if (IsNull(obj2)) {
            return false;
        }
        if (AreSame(obj1, obj2)) {
            return true;
        }
        if (!AreSameTypes(obj1, obj2)) {
            return false;
        }
        return cmp();
    }
}