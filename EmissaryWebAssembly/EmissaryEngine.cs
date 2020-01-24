using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Wasmtime;

namespace EmissaryWebAssembly
{
    public class EmissaryEngine : IDisposable
    {
        internal Engine engine { get; set; }
        internal Store store { get; set; }

        public EmissaryEngine()
        {
            engine = new Engine();
            store = engine.CreateStore();
        }


        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls



        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (store != null)
                        store.Dispose();
                    store = null;
                    if (engine != null)
                        engine.Dispose();
                    engine = null;
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~EmissaryEngine()
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
