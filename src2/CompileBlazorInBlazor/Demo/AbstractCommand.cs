using System;
using System.Collections.Generic;
using System.Text;

namespace CompileBlazorInBlazor.Demo
{
    public abstract class AbstractCommand
    {
        private List<IAbstractArgument> Arguments { get; set; }
        public abstract string Name { get; }
        public abstract string Author { get; }
        public abstract string Description { get; }
        public abstract System.Drawing.Color Accent { get; }
        public abstract void RegisterInputArguments();
        public abstract string CommandLineName { get; }
        public abstract void RunCommand(Context context);

        public int RequestArgument(Type T, string prompt, string description)
        {
            //this.Arguments.Add(new AbstractArgument<T>());
            return (this.Arguments.Count - 1);
        }
    }

    public abstract class AbstractArgument<T> : IAbstractArgument
    {
        public abstract string Prompt { get; set; }
        public abstract string Description { get; set; }
        public abstract void RenderArgumentInput();
    }

    public interface IAbstractArgument
    {
        public abstract string Prompt { get; set; }
        public abstract string Description { get; set; }
    }
}