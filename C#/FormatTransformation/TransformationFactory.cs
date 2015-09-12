using System;

public static class TransformationFactory {
    public static ITransformation CreateTransformation(String from, String to) {
        if (from == "find" && to == "find") {
            return new FindToFindTransformation();
        } else if (from == "xml" && to == "find") {
            return new XmlToFindTransformation();
        } else if (from == "find" && to == "xml") {
            return new FindToXmlTransformation();
        } else {
            return null;
        }
    }
}
