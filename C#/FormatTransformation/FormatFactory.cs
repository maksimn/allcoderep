using System;

public static class FormatFactory {
    public static AFormat CreateFormat(String name) {
        if (name == "find") {
            return new FindFormat(new ConsoleFindFormatInitializer());
        } else if (name == "xml") {
            return new XMLFormat(new XmlFormatFromConsole());
        } else {
            return null;
        }
    }
}