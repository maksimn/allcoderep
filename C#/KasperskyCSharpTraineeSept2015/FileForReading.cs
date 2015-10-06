using System;
using System.Collections.Generic;
using System.IO;

class FileForReading : IInitializer {
    private readonly String fileName;
    public FileForReading(String fileName) {
        this.fileName = fileName;
    }
    public void Init(List<String> list) {
        using (StreamReader streamReader = new StreamReader(fileName)) {
            String word;
            while ((word = streamReader.ReadLine()) != null) {
                list.Add(word);
            }
        }        
    }
}