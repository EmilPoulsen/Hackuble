using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CompileBlazorInBlazor.Demo;
using Microsoft.AspNetCore.Blazor.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.JSInterop;

namespace CompileBlazorInBlazor
{
    public class CompileService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _uriHelper;
        public List<string> CompileLog { get; set; }
        private List<MetadataReference> references { get; set; }
        private CommandService _commandService;


        public CompileService(HttpClient http, NavigationManager uriHelper, CommandService commandService)
        {
            _http = http;
            _uriHelper = uriHelper;
            _commandService = commandService;
        }

        public async Task Init()
        {
            if (references == null)
            {
                references = new List<MetadataReference>();
                foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    if (assembly.IsDynamic)
                    {
                        continue;
                    }
                    var name = assembly.GetName().Name + ".dll";
                    Console.WriteLine(name);
                    references.Add(
                        MetadataReference.CreateFromStream(
                            await this._http.GetStreamAsync(_uriHelper.BaseUri+ "/_framework/_bin/" + name)));
                }
            }
        }


        public async Task<Type> CompileBlazor(string code)
        {
            CompileLog.Add("Create fileSystem");

            var fileSystem = new EmptyRazorProjectFileSystem();

            CompileLog.Add("Create engine");
            //            Microsoft.AspNetCore.Blazor.Build.
            
                        var engine = RazorProjectEngine.Create(RazorConfiguration.Create(RazorLanguageVersion.Version_3_0, "Blazor", new RazorExtension[0]), fileSystem, b =>
            {
                //                RazorExtensions.Register(b);


//                b.SetRootNamespace(DefaultRootNamespace);

                // Turn off checksums, we're testing code generation.
//                b.Features.Add(new SuppressChecksum());

//                if (LineEnding != null)
//                {
//                    b.Phases.Insert(0, new ForceLineEndingPhase(LineEnding));
//                }

                // Including MVC here so that we can find any issues that arise from mixed MVC + Components.
//                Microsoft.AspNetCore.Mvc.Razor.Extensions.RazorExtensions.Register(b);
//
//                // Features that use Roslyn are mandatory for components
//                Microsoft.CodeAnalysis.Razor.CompilerFeatures.Register(b);
//
//                b.Features.Add(new CompilationTagHelperFeature());
//                b.Features.Add(new DefaultMetadataReferenceFeature()
//                {
//                    References = references,
//                });



            });


            CompileLog.Add("Create file");
            var file = new MemoryRazorProjectItem(code, true, "/App", "/App/App.razor");
            CompileLog.Add("File process and GetCSharpDocument");
            var doc = engine.Process(file).GetCSharpDocument();
            CompileLog.Add("Get GeneratedCode");
            var csCode = doc.GeneratedCode;

            CompileLog.Add("Read Diagnostics");
            foreach (var diagnostic in doc.Diagnostics)
            {
                CompileLog.Add(diagnostic.ToString());
            }

            if (doc.Diagnostics.Any(i => i.Severity == RazorDiagnosticSeverity.Error))
            {
                return null;
            }

            CompileLog.Add(csCode);

            CompileLog.Add("Compile assembly");
            var assembly = await Compile(csCode);

            if (assembly != null)
            {
                CompileLog.Add("Search Blazor component");
                return assembly.GetExportedTypes().FirstOrDefault(i => i.IsSubclassOf(typeof(ComponentBase)));
            }

            return null;
        }


        public async Task<Assembly> Compile(string code)
        {
            await Init();

            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(code, new CSharpParseOptions(LanguageVersion.Preview));
            foreach (var diagnostic in syntaxTree.GetDiagnostics())
            {
                CompileLog.Add(diagnostic.ToString());
            }

            if (syntaxTree.GetDiagnostics().Any(i => i.Severity == DiagnosticSeverity.Error))
            {
                CompileLog.Add("Parse SyntaxTree Error!");
                return null;
            }

            CompileLog.Add("Parse SyntaxTree Success");

            CSharpCompilation compilation = CSharpCompilation.Create("CompileBlazorInBlazor.Demo", new[] {syntaxTree},
                references, new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            using (MemoryStream stream = new MemoryStream())
            {
                EmitResult result = compilation.Emit(stream);

                foreach (var diagnostic in result.Diagnostics)
                {
                    CompileLog.Add(diagnostic.ToString());
                }

                if (!result.Success)
                {
                    CompileLog.Add("Compilation error");
                    return null;
                }

                CompileLog.Add("Compilation success!");

                stream.Seek(0, SeekOrigin.Begin);

//                var context = new CollectibleAssemblyLoadContext();
                Assembly assemby = AppDomain.CurrentDomain.Load(stream.ToArray());
                return assemby;
            }
        }



        public async Task<Type> CompileOnly(string code)
        {
            await Init();

            var assemby = await this.Compile(code);
            if (assemby != null)
            {
                return assemby.GetExportedTypes().FirstOrDefault();
            }

            return null;
        }

        public AbstractCommand CreateRunClass(Type type)
        {
            var instance = Activator.CreateInstance(type) as AbstractCommand;
            return instance;
        }



        //public string RunCompiled(Type type, Context context, CommandObject command)
        //{
        //    //if (string.IsNullOrEmpty(command.command)) return null;
        //    //var methodInfo = type.GetMethod(command.command);
        //    //var instance = Activator.CreateInstance(type);
        //    //if (command.data.Length > 0) return (string)methodInfo.Invoke(instance, command.data);
        //    //else return (string)methodInfo.Invoke(instance, null);

        //    System.Diagnostics.Trace.WriteLine($"Here's the type: {type.FullName}");

        //    if (!typeof(AbstractCommand).IsAssignableFrom(type))
        //    {
        //        return "";
        //    }

        //    var instance = Activator.CreateInstance(type) as AbstractCommand;

        //    //Context context = new Context();

        //    _commandService.RunCommand(instance, context, null);

        //    return "";

        //}
    }

    public class CommandObject
    {
        public string command { get; set; }
        public object[] data { get; set; }
    }
}


////--------------------------------------------------------

//using System.Text;
//using Microsoft.JSInterop;
//using System.Threading.Tasks;

//        namespace CompileBlazorInBlazor.Demo
//{
//    public class RunClass
//    {
//        public string Run(string name)
//        {
//            var sb = new StringBuilder();
//            for (int i = 0; i < 5; i++)
//            {
//                sb.AppendLine($"{i}) Hello, {name}!");
//            }

//            return sb.ToString();
//        }
//        public async Task<object> RunB()
//        {
//            await CompileService.InvokeJS("clickCube", new object[] { });
//            return null;
//        }
//    }
//}
