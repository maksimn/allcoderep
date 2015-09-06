public abstract class AFormat {
    protected AFormat() {}
    protected AFormat(IFormatInitializer initializer) {
        initializer.Init(this);
    }
}