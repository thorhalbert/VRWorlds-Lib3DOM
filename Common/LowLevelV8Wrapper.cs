﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.ClearScript.V8;

namespace Common
{
   
    class LowLevelV8Wrapper : IDisposable
    {
        V8ScriptEngine engine = null;
        Dictionary<string, V8Script> methodCache = new Dictionary<string, V8Script>();

        public LowLevelV8Wrapper()
        {
            engine = new V8ScriptEngine("name", 
                V8ScriptEngineFlags.DisableGlobalMembers | V8ScriptEngineFlags.EnableDebugging,
                8080);     // We need to pass in a name, and a unique debug port
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).

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
        // ~LowLevelV8Wrapper()
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
