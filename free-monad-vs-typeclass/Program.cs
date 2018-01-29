using LanguageExt;
using LanguageExt.ClassInstances;
using System;

using static LanguageExt.Prelude;

namespace free_monad_vs_typeclass
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var freeProgram = new ResourcePrinterFree<Unit>.Print("Hello, world!", () =>
                new ResourcePrinterFree<Unit>.Return(unit));
            freeProgram.Map(FreeInterpreter.Interpret);

            TypeclassProgram<ResourcePrinterTypeclassImpl>();
        }

        public static Unit TypeclassProgram<ResourcePrinterTypeclassT>() where ResourcePrinterTypeclassT: struct, ResourcePrinterTypeclass =>
            use(default(ResourcePrinterTypeclassT).AcquireResource(), r => 
                default(ResourcePrinterTypeclassT).Print(r, "Hello, world!"));
    }
}
