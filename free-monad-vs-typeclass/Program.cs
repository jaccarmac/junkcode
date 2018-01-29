using LanguageExt;
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

            TypeclassProgram<ResourcePrinterTypeclassUnit, Unit>();
        }

        public static Unit TypeclassProgram<ResourcePrinterTypeclassT, T>() where ResourcePrinterTypeclassT: struct, ResourcePrinterTypeclass<T> =>
            use(default(ResourcePrinterTypeclassT).AcquireResource, r => default(ResourcePrinterTypeclassT).Print(r, "Hello, world!"));
    }
}
