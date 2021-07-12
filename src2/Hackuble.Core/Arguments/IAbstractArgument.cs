using System;
using System.Collections.Generic;
using System.Text;

namespace CompileBlazorInBlazor.Demo
{
    public interface IAbstractArgument
    {
        string Prompt { get; set; }
        string Description { get; set; }

        object DefaultValueUntyped { get; }
        bool TryPushValue(object data);

        bool GetData<T>(ref T data);
    }
}
