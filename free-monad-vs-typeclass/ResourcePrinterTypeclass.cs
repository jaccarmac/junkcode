using LanguageExt;
using LanguageExt.TypeClasses;

using static LanguageExt.Prelude;

namespace free_monad_vs_typeclass {
    public interface ResourcePrinterTypeclass<MA, A> {
        MB Print<MonadB, MB>(ResourceWrapper resource, string output) where MonadB: struct, Monad<MB, Unit>;
        MB AcquireResource<MonadB, MB>() where MonadB: struct, Monad<MB, ResourceWrapper>;
    }

    public struct ResourcePrinterTypeclassUnit : ResourcePrinterTypeclass<Identity<Unit>, Unit> {
        public MB Print<MonadB, MB>(ResourceWrapper resource, string output) where MonadB: struct, Monad<MB, Unit> =>
            default(MonadB).Return(fun(() => resource.Print(output))());
        
        public MB AcquireResource<MonadB, MB>() where MonadB: struct, Monad<MB, ResourceWrapper> =>
            default(MonadB).Return(new ResourceWrapper());
    }
}
