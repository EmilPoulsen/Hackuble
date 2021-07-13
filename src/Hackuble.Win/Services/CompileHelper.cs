using Hackuble.Commands;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Hackuble.Win.Services
{
    public class CompileHelper
    {
        private List<MetadataReference> references { get; set; }
        public List<string> CompileLog { get; set; }
        public void Init()
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
                            System.IO.File.OpenRead(Environment.CurrentDirectory + name)));
                }
            }
        }

        public Assembly Compile(string code)
        {
            Init();

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

            CSharpCompilation compilation = CSharpCompilation.Create("Hackuble.Web", new[] { syntaxTree },
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

                Assembly assemby = AppDomain.CurrentDomain.Load(stream.ToArray());
                return assemby;
            }
        }

        public Type CompileOnly(string code)
        {
            Init();

            var assemby = this.Compile(code);
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
    }

    public class CommandObject
    {
        public string command { get; set; }
        public object[] data { get; set; }
    }
}
