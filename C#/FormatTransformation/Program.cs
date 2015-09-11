using System;

class Program {
    static void Main(String[] args) {
        XMLFormat xml = new XMLFormat(new XmlFormatFromConsole());

        string s = xml.ToString();
        xml.Output(new ConsoleOutput());
    }
}