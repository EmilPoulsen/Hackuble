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
        //public abstract System.Drawing.Color Accent { get; }
        public abstract void RegisterInputArguments(DataAccess dataAccess);
        public abstract string CommandLineName { get; }
        public abstract CommandStatus RunCommand(Context context, DataAccess dataAccess);

        public int RequestArgument(Type T, string prompt, string description)
        {
            //this.Arguments.Add(new AbstractArgument<T>());
            //return (this.Arguments.Count - 1);
            return -1;
        }
    }

    public abstract class AbstractArgument<T> : IAbstractArgument
    {
        public AbstractArgument(string prompt, string description, T defaultValue)
        {
            this.Prompt = prompt;
            this.Description = description;
            this.DefaultValue = defaultValue;
        }

        public T DefaultValue { get; set; }
        public string Prompt { get; set; }
        public string Description { get; set; }

        public object DefaultValueUntyped => this.DefaultValue;

        public abstract void RenderArgumentInput();
    }

    public interface IAbstractArgument
    {
        public string Prompt { get; set; }
        public string Description { get; set; }

        public object DefaultValueUntyped { get; }
    }

    public class NumberArgument : AbstractArgument<double>
    {

        public NumberArgument(string prompt, string description, double defaultValue)
            :base(prompt, description, defaultValue)
        {

        }

        public override void RenderArgumentInput()
        {
            throw new NotImplementedException();
        }
    }
}