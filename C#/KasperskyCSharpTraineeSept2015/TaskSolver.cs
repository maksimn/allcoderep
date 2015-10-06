using System;
using System.Collections.Generic;

class TaskSolver {
    private List<String> list;
    public TaskSolver(IInitializer initializer) {
        list = new List<String>();
        initializer.Init(list);
    }
    public void Process() {
        list.Sort(new WordAnagramComparer());
    }
    public void Output(IOutputable iout) {
        iout.Output(list);
    }
}
