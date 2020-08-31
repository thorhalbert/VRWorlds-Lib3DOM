using System;
using System.Collections.Generic;
using System.Text;
using Wasmtime;

namespace EmissaryWebAssembly
{
    class EmissaryHost : IHost
    {
        public Instance Instance { get; set; }

        public EmissaryHost()
        {
            // Host class isn't here yet
            //var host = new Wasmtime.
        }

    }
}
