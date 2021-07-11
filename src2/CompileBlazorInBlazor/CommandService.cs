using CompileBlazorInBlazor.Demo;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompileBlazorInBlazor
{
    public class CommandService
    {
        public static IJSRuntime JSRuntime;

        public List<AbstractCommand> Commands { get; set; }

        public CommandService()
        {
            this.Commands = new List<AbstractCommand>();
        }

        public void AddCommand(AbstractCommand command)
        {
            this.Commands.Add(command);
        }

        public string GetSummary()
        {
            return $"Number of commands: {this.Commands.Count}";
        }

        public void RunCommand(AbstractCommand command, Context context, DataAccess dataAccess)
        {
            var status = command.RunCommand(context, dataAccess);

            System.Diagnostics.Trace.WriteLine($"Number of cubes: {context.Cubes.Count}");

            ParseContext(context);

        }

        public async void ParseContext(Context context)
        {

            foreach (var cube in context.Cubes)
            {
                await CommandService.InvokeJS("addCube", new object[] { cube.X, cube.Y, cube.Z, cube.Width, cube.Depth, cube.Height });
            }

            //await CompileService.InvokeJS("clickCube", new object[] { });
        }

        public async static Task InvokeJS(string functionName, object[] argumentsObject)
        {
            await JSRuntime.InvokeAsync<object>
            (functionName, argumentsObject);
        }
    }
}
