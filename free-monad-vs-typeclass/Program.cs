using LanguageExt;
using LanguageExt.ClassInstances;
using System;

using static free_monad_vs_typeclass.ResourcePrinterTypeclassExtensions;
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

            TypeclassProgram<ResourcePrinterTypeclassAllCapsImpl>();
        }

        public static Unit TypeclassProgram<T>() where T: struct, ResourcePrinterTypeclass =>
            rpacquireResource<T, Unit>(r => rpprint<T>(r, "Hello, world!"));
    }
}
