using System;

class Program {
    static void Main(String[] args) {
        String formatName1 = Console.ReadLine(), formatName2 = Console.ReadLine();
        AFormat format = FormatFactory.CreateFormat(formatName1);

        if (formatName1 == formatName2) {
            format.Output(new ConsoleOutput());
        } else {
            format.TransformTo(formatName2).Output(new ConsoleOutput());
        }
    }
}