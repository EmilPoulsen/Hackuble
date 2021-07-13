using System;
using System.Collections.Generic;
using System.Text;

namespace Hackuble.Commands
{

    public abstract class AbstractCommand
    {
        public AbstractCommand()
        {
            //this.Arguments = new List<IAbstractArgument>();
        }

        public abstract string Name { get; }
        public virtual string Author => "Unkown";
        public virtual string Description => "";
        public virtual string Accent => "#FF96AD";
        public virtual void RegisterInputArguments(DataAccess dataAccess) { }
        public abstract string CommandLineName { get; }
        public abstract CommandStatus RunCommand(Context context, DataAccess dataAccess);
    }
}