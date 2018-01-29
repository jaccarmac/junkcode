using LanguageExt;
using LanguageExt.TypeClasses;

using static LanguageExt.Prelude;

namespace free_monad_vs_typeclass {
    public interface ResourcePrinterTypeclass {
        Unit Print(ResourceWrapper resource, string output);
        ResourceWrapper AcquireResource();
    }

    public struct ResourcePrinterTypeclassImpl : ResourcePrinterTypeclass {
        public Unit Print(ResourceWrapper resource, string output) =>
            fun(() => resource.Print(output))();
        
        public ResourceWrapper AcquireResource() =>
            new ResourceWrapper();
    }
}
