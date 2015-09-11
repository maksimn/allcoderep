public class ConsoleOutput : IOutput {
    public void Output(AFormat aFormat) {
        System.Console.Write(aFormat.ToString());
    }
}