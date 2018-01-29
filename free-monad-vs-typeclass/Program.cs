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

        public static Identity<Unit> TypeclassProgram<ResourcePrinterTypeclassT, T>() where ResourcePrinterTypeclassT: struct, ResourcePrinterTypeclass<Identity<T>, T> =>
            default(MIdentity<ResourceWrapper>).Bind<MIdentity<Unit>, Identity<Unit>, Unit>(
                default(ResourcePrinterTypeclassT).AcquireResource<MIdentity<ResourceWrapper>, Identity<ResourceWrapper>>(),
                r => use(r, _ => default(ResourcePrinterTypeclassT).Print<MIdentity<Unit>, Identity<Unit>>(r, "Hello, world!"))
            );
    }
}
