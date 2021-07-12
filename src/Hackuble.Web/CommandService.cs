using CompileBlazorInBlazor.Demo;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompileBlazorInBlazor;

namespace Hackuble.Web
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

            //System.Diagnostics.Trace.WriteLine($"Number of cubes: {context.Cubes.Count}");

            ParseContext(context);

        }

        public AbstractCommand FindCommand(string commandLineName)
        {
            foreach (AbstractCommand c in this.Commands)
            {
                //System.Diagnostics.Trace.WriteLine($"Checking against Command {c.CommandLineName}");
                if (c.CommandLineName == commandLineName)
                {
                    System.Diagnostics.Trace.WriteLine($"Found Command {c.CommandLineName}!");
                    return c;
                }
            }
            System.Diagnostics.Trace.WriteLine($"Command {commandLineName} not found");
            return null;
        }

        public void RunJSONCommand(string json, Context context)
        {
            DataAccess dataAccess = new CompileBlazorInBlazor.Demo.DataAccess();
            CommandObject co = Newtonsoft.Json.JsonConvert.DeserializeObject<CommandObject>(json);
            AbstractCommand command = this.FindCommand(co.command);
            command.RegisterInputArguments(dataAccess);
            //System.Diagnostics.Trace.WriteLine("Command: " + co.command + ", argCount: " + co.data.Length.ToString());
            if (!dataAccess.PushDataFromObjArray(co.data))
            {
                System.Diagnostics.Trace.WriteLine($"Unable to push arguments");
            }
            dataAccess.PushDataFromObjArray(co.data);
            //System.Diagnostics.Trace.WriteLine("Running " + command.CommandLineName);
            if (command != null)
            {
                RunCommand(command, context, dataAccess);
            }
        }

        public async void ParseContext(Context context)
        {

            foreach (var cube in context.Cubes)
            {
                await CommandService.InvokeJS("addCube", new object[] { cube.X, cube.Y, cube.Z, cube.Width, cube.Depth, cube.Height, cube.Material });
            }
            foreach (var sphere in context.Spheres)
            {
                await CommandService.InvokeJS("addSphere", new object[] { sphere.Radius, sphere.U, sphere.V, sphere.Material });
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
