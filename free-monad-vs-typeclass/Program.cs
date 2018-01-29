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

            TypeclassProgram<ResourcePrinterTypeclassUnit, Unit>();
        }

        public static Option<Unit> TypeclassProgram<ResourcePrinterTypeclassT, T>() where ResourcePrinterTypeclassT: struct, ResourcePrinterTypeclass<Option<T>, T> =>
            default(ResourcePrinterTypeclassT).AcquireResource<MOption<ResourceWrapper>, Option<ResourceWrapper>>().Bind(r => 
                use(r, _ => default(ResourcePrinterTypeclassT).Print<MOption<Unit>, Option<Unit>>(r, "Hello, world!")));
    }
}
