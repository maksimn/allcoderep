using System;

public class LogAnalyzer {
    public Boolean IsValidLogFileName(String fileName) {
        if (!fileName.EndsWith(".SLF")) {
            return false;
        }
        return true;
    }
}