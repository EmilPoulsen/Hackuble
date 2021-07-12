using System;
using System.Collections.Generic;
using System.Text;

namespace CompileBlazorInBlazor.Demo
{

    public abstract class AbstractCommand
    {
        public AbstractCommand()
        {
            //this.Arguments = new List<IAbstractArgument>();
        }

        public abstract string Name { get; }
        public abstract string Author { get; }
        public abstract string Description { get; }
        public abstract string Accent { get; }
        public abstract void RegisterInputArguments(DataAccess dataAccess);
        public abstract string CommandLineName { get; }
        public abstract CommandStatus RunCommand(Context context, DataAccess dataAccess);
    }
}