using LanguageExt;
using LanguageExt.TypeClasses;
using System;

using static LanguageExt.Prelude;

namespace free_monad_vs_typeclass {
    public interface ResourcePrinterTypeclass {
        Unit Print(ResourceWrapper resource, string output);
        T AcquireResource<T>(Func<ResourceWrapper, T> f);
    }

    public struct ResourcePrinterTypeclassImpl : ResourcePrinterTypeclass {
        public Unit Print(ResourceWrapper resource, string output) =>
            fun(() => resource.Print(output))();
        
        public T AcquireResource<T>(Func<ResourceWrapper, T> f) =>
            use(new ResourceWrapper(), f);
    }

    public struct ResourcePrinterTypeclassAllCapsImpl : ResourcePrinterTypeclass {
        public Unit Print(ResourceWrapper resource, string output) =>
            fun(() => resource.Print(output.ToUpper()))();
        
        public T AcquireResource<T>(Func<ResourceWrapper, T> f) =>
            use(new ResourceWrapper(), f);
    }
}
