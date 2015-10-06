using System;

class Program {
    static void Main(String[] args) {
        if (args.Length == 1) {
            //TaskSolver solver = new TaskSolver(new StringReaderInitializer());
            
            TaskSolver solver = new TaskSolver(new FileForReading(args[0]));            
            solver.Process();
            solver.Output(new FileOutput(args[0]));
            
            //solver.Output(new ConsoleOutputExample1());
        } else {
            Console.WriteLine("You should pass a parameter from the command line." +
                              "This parameter must be a file name or a full path to a text file.");
        }
    }
}