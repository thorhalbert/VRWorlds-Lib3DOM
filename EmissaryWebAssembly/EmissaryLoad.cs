using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Wasmtime;
using Wasmtime.Externs;

namespace EmissaryWebAssembly
{
    class EmissaryLoad : IDisposable
    {
        EmissaryEngine engine;

        internal Module module { get; set; }
        internal Instance instance { get; set; }
        internal ExternMemory memory { get; set; }
        internal EmissaryAllocator allocator { get; set; }

        public EmissaryLoad(EmissaryEngine eng)
        {
            engine = eng;
        }

        public void Load(string name)
        {
            module = engine.store.CreateModule("markdown.wasm");
            instance = module.Instantiate(new EmissaryHost());

            memory = instance.Externs.Memories.SingleOrDefault() ??
              throw new InvalidOperationException("Module must export a memory.");

            allocator = new EmissaryAllocator(memory, instance.Externs.Functions);
        }

  

            //(var inputAddress, var inputLength) = allocator.AllocateString(MarkdownSource);

            //try
            //{
            //    object[] results = (instance as dynamic).render(inputAddress, inputLength);

            //    var outputAddress = (int)results[0];
            //    var outputLength = (int)results[1];

            //    try
            //    {
            //        Console.WriteLine(memory.ReadString(outputAddress, outputLength));
            //    }
            //    finally
            //    {
            //        allocator.Free(outputAddress, outputLength);
            //    }
            //}
            //finally
            //{
            //    allocator.Free(inputAddress, inputLength);
            //}
        

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (instance != null)
                        instance.Dispose();
                    instance = null;
                    if (module != null)
                        module.Dispose();
                    module = null;
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~EmissaryLoad()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

    }
}
