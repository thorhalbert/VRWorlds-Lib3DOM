using System;
using Wasmtime;

namespace wasmExperiment
{
    class Program
    {
        static void proc_exit(int code)
        {

        }

        static int fd_write2(Caller caller, int fd, int iovs_ptr, int iovs_len, int nwritten)
        {
            //var str = caller.GetMemory("mem");

            //var val = str.ReadInt32(iovs_ptr);
            //var s = str.ReadNullTerminatedString(val);

            return 0;
        }
        static int fd_write(int fd, int iovs_ptr, int iovs_len, int nwritten)
        {
            Console.WriteLine($"fd_write({fd},{iovs_ptr:x},{iovs_len},{nwritten:x}");

            //var b = MemoryBase.ReadByte(iovs_ptr);
            //var b1 = MemoryBase.ReadByte(iovs_ptr+1);
            //var b2 = MemoryBase.ReadByte(iovs_ptr + 2);

            //var st = MemoryBase.ReadString(iovs_ptr, iovs_len);
            //var sn = MemoryBase.ReadNullTerminatedString(iovs_ptr);

            var ptr = MemoryBase.ReadInt32(iovs_ptr);

            var str = MemoryBase.ReadNullTerminatedString(ptr);

            return 0;
        }
        static int fd_prestat_get(int a, int b)
        {
            return 0;
        }
        static int fd_prestat_dir_name(int a, int b, int c)
        {
            return 0;
        }
        static int environ_sizes_get(int a, int b)
        {
            return 0;
        }
        static int environ_get(int a, int b)
        {
            return 0;
        }

        static Wasmtime.Externs.ExternMemory MemoryBase;

        const string preview = "wasi_snapshot_preview1";

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var was = "test1.wasm";

            using var host = new Host();

            var global = host.DefineMutableGlobal("", "global", 1);

            host.DefineFunction<int>(preview, "proc_exit", proc_exit);
            //host.DefineFunction<int,int,int,int, int>(preview, "fd_write", fd_write);
            host.DefineFunction<Caller,int,int,int,int,int>(preview, "fd_write", fd_write2);
            host.DefineFunction<int, int, int>(preview, "fd_prestat_get", fd_prestat_get);
            host.DefineFunction<int, int, int, int>(preview, "fd_prestat_dir_name", fd_prestat_dir_name);
            host.DefineFunction<int, int, int>(preview, "environ_sizes_get", environ_sizes_get);
            host.DefineFunction<int, int, int>(preview, "environ_get", environ_get);

            using var module = host.LoadModule(was);

            using dynamic instance = host.Instantiate(module);

            MemoryBase = (Wasmtime.Externs.ExternMemory) instance.Externs.Memories[0];
            
            instance.main(0, 0);
        }
    }
}
