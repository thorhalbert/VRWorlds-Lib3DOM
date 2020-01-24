using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Wasmtime;
using Wasmtime.Externs;

namespace EmissaryWebAssembly
{
    class EmissaryAllocator
    {
        private ExternMemory _memory;
        private ExternFunction _malloc;
        private ExternFunction _free;

        public EmissaryAllocator(ExternMemory memory, IReadOnlyList<ExternFunction> functions)
        {
            _memory = memory ??
                throw new ArgumentNullException(nameof(memory));

            _malloc = functions
                .Where(f => f.Name == "__wbindgen_malloc")
                .SingleOrDefault() ??
                    throw new ArgumentException("Unable to resolve malloc function.");

            _free = functions
                .Where(f => f.Name == "__wbindgen_free")
                .SingleOrDefault() ??
                    throw new ArgumentException("Unable to resolve free function.");
        }

        public int Allocate(int length)
        {
            return (int)_malloc.Invoke(length);
        }

        public (int Address, int Length) AllocateString(string str)
        {
            var length = Encoding.UTF8.GetByteCount(str);

            int addr = Allocate(length);

            _memory.WriteString(addr, str);

            return (addr, length);
        }

        public void Free(int address, int length)
        {
            _free.Invoke(address, length);
        }

        
    }
}
