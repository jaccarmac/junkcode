using LanguageExt;
using System;

using static LanguageExt.Prelude;

namespace free_monad_vs_typeclass
{
    public class ResourceWrapper : IDisposable {
        public void Dispose() {
            Console.WriteLine("Disposing");
        }

        public void Print(string output) {
            Console.WriteLine(output);
        }
    }
}
