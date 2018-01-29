using LanguageExt;
using System;

using static LanguageExt.Prelude;

namespace free_monad_vs_typeclass
{   
    public abstract class ResourcePrinterFree<T> {
        public class Return : ResourcePrinterFree<T> {
            public T Value;
            public Return(T value) {
                Value = value;
            }
        }

        public class Print : ResourcePrinterFree<T> {
            public string Output;
            public Func<ResourcePrinterFree<T>> Next;
            public Print(string output, Func<ResourcePrinterFree<T>> next) {
                Output = output;
                Next = next;
            }
        }

        public class AcquireResource : ResourcePrinterFree<T> {
            public Func<ResourcePrinterFree<T>> Next;
            public AcquireResource(Func<ResourcePrinterFree<T>> next) {
                Next = next;
            }
        }
    }

    public static class ResourcePrinterFreeExtensions {
        public static T Map<T>(this ResourcePrinterFree<T> op, Func<ResourcePrinterFree<T>, T> f) =>
            f(op);
    }

    public static class FreeInterpreter {
        public static Unit Interpret(ResourcePrinterFree<Unit> op) =>
            Interpret(None, op);

        public static Unit Interpret(Option<ResourceWrapper> resource, ResourcePrinterFree<Unit> op) =>
            resource.Map(_ => op).IfNone(new ResourcePrinterFree<Unit>.AcquireResource(() => op)).Apply(o =>
                  o is ResourcePrinterFree<Unit>.Return r ? r.Value
                : o is ResourcePrinterFree<Unit>.Print p ? resource.Map(fun((ResourceWrapper rs) => rs.Print(p.Output))).Apply(_ => Interpret(resource, p.Next()))
                : o is ResourcePrinterFree<Unit>.AcquireResource a ? use(new ResourceWrapper(), rw => Interpret(rw, a.Next()))
                : throw new InvalidOperationException()
            );
    }
}
