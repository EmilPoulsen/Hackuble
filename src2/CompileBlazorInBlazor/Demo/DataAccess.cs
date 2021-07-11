using System;
using System.Collections.Generic;
using System.Text;

namespace CompileBlazorInBlazor.Demo
{
    public class DataAccess
    {
        public DataAccess()
        {
            this.Arguments = new List<IAbstractArgument>();
        }

        public List<IAbstractArgument> Arguments { get; set; }

        public void RegisterNumberArgument(string name, string description, double defaultValue)
        {
            this.Arguments.Add(new NumberArgument(name, description, defaultValue));
        }

        public bool GetData<T>(int index, ref T data)
        {
            if (index < this.Arguments.Count)
            {
                var arg = this.Arguments[index];
                if (arg.GetData<T>(ref data))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}