using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace RoslynDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var code = @"
            public class HiJ
            {
                public void Test(string v)
                {
                    System.Console.WriteLine($""Hi, {v}!"");
                }
            }";
            var jit = new Jit();
            var syntaxTree = jit.ParseToSyntaxTree(code);
            var compilation = jit.BuildCompilation(syntaxTree);
            var assembly = jit.ComplieToAssembly(compilation);

            foreach (var item in assembly.GetTypes())
            {
                Console.WriteLine(item.FullName);
                item.GetMethod("Test").Invoke(Activator.CreateInstance(item), new object[] { "joker" });
            }

        }


    }

    public class Jit
    {
        public SyntaxTree ParseToSyntaxTree(string code)
        {
            var parseOptions = new CSharpParseOptions(LanguageVersion.Latest, preprocessorSymbols: new[] { "RELEASE" });

            return CSharpSyntaxTree.ParseText(code, parseOptions);
        }

        public CSharpCompilation BuildCompilation(SyntaxTree syntaxTree)
        {
            var compilationOptions = new CSharpCompilationOptions(
                    concurrentBuild: true,
                    metadataImportOptions: MetadataImportOptions.All,
                    outputKind: OutputKind.DynamicallyLinkedLibrary,
                    allowUnsafe: true,
                    platform: Platform.AnyCpu,
                    checkOverflow: false,
                    assemblyIdentityComparer: DesktopAssemblyIdentityComparer.Default
                );

            var referenecs = AppDomain.CurrentDomain.GetAssemblies()
                .Where(i => !i.IsDynamic && !string.IsNullOrWhiteSpace(i.Location))
                .Distinct()
                .Select(i => MetadataReference.CreateFromFile(i.Location));

            return CSharpCompilation.Create("code.cs", new SyntaxTree[] { syntaxTree });
        }

        public Assembly ComplieToAssembly(CSharpCompilation compilation)
        {
            using (var stream = new MemoryStream())
            {
                var result = compilation.Emit(stream);
                if (result.Success)
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    return AssemblyLoadContext.Default.LoadFromStream(stream);
                }
                else
                {
                    throw new Exception(result.Diagnostics.Select(i => i.ToString()).DefaultIfEmpty().Aggregate((i, j) => i + j));
                }
            }
        }
    }

}
