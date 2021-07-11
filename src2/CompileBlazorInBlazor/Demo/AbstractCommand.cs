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
        public T CurrentValue { get; set; }

        public T DefaultValue { get; set; }
        public string Prompt { get; set; }
        public string Description { get; set; }

        public object DefaultValueUntyped => this.DefaultValue;

        public bool GetData<T1>(ref T1 data)
        {
            if (typeof(T1) == typeof(T))
            {
                var temoObj = (object)this.CurrentValue;
                data = (T1)temoObj;
                return true;
            }
            else
            {
                return false;
            }
        }

        public abstract void RenderArgumentInput();
    }

    public interface IAbstractArgument
    {
        public string Prompt { get; set; }
        public string Description { get; set; }

        public object DefaultValueUntyped { get; }

        bool GetData<T>(ref T data);
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

    public class IntegerArgument : AbstractArgument<int>
    {

        public IntegerArgument(string prompt, string description, int defaultValue)
            : base(prompt, description, defaultValue)
        {

        }

        public override void RenderArgumentInput()
        {
            throw new NotImplementedException();
        }
    }

    public class TextArgument : AbstractArgument<string>
    {

        public TextArgument(string prompt, string description, string defaultValue)
            : base(prompt, description, defaultValue)
        {

        }

        public override void RenderArgumentInput()
        {
            throw new NotImplementedException();
        }
    }
}