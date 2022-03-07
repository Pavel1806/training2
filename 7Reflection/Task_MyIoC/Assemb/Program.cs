using System;
using Mono.Cecil;
using Mono.Cecil.Cil;
using IoCSample;

namespace Assemb
{
    class Program
    {
        [Obsolete]
        static void Main(string[] args)
        {
            //string path = "D:\\VisualStudio\\repos\\training\\7Reflection\\Task_MyIoC\\IoCSample\\bin\\Debug\\IoCSample.dll";

            //var asm = AssemblyDefinition.ReadAssembly(path);

            //var writeLine = typeof(Console).GetMethod("WriteLine", new Type[]{typeof(string)});

            //var imported = asm.MainModule.Import(writeLine);

            //foreach (var typeDef in asm.MainModule.Types)
            //{
            //    foreach (var methodDef in typeDef.Methods)
            //    {
            //        var processor = methodDef.Body.GetILProcessor();

            //        string text = "kkorfepghjeihirt";

            //        var paramCall = processor.Create(OpCodes.Ldstr, text);

            //        var caller = processor.Create(OpCodes.Call, imported);

            //        processor.InsertAfter(methodDef.Body.Instructions[0], paramCall);
            //        processor.InsertBefore(paramCall, caller);
            //    }
            //}

            //asm.Write("patched.dll");
        }
    }
}
