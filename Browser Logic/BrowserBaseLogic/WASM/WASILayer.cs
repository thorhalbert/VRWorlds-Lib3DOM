using System;
using System.Collections.Generic;
using System.Text;
using Wasmtime;

namespace VRWorlds.Browser.WASM
{
    public class WASILayer : wasi_snapshot_preview1
    {
        public void proc_exit(int code)
        {

        }

        public int fd_write2(Caller caller, int fd, int iovs_ptr, int iovs_len, int nwritten)
        {
            //var str = caller.GetMemory("mem");

            //var val = str.ReadInt32(iovs_ptr);
            //var s = str.ReadNullTerminatedString(val);

            return 0;
        }
        public int fd_write(int fd, int iovs_ptr, int iovs_len, int nwritten)
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
        public int fd_prestat_get(int a, int b)
        {
            return 0;
        }
        public int fd_prestat_dir_name(int a, int b, int c)
        {
            return 0;
        }
        public int environ_sizes_get(int a, int b)
        {
            return 0;
        }
        public int environ_get(int a, int b)
        {
            return 0;
        }
        errno wasi_snapshot_preview1.args_get(UIntPtr argv, UIntPtr argv_buf)
        {
            return errno.ERRNO_PERM;
        }
        errno wasi_snapshot_preview1.environ_get(UIntPtr environ, UIntPtr environ_buf)
        {
            return errno.ERRNO_PERM;
        }
        errno wasi_snapshot_preview1.fd_advise(uint fd, ulong offset, ulong len, Advice advice)
        {
            return errno.ERRNO_PERM;
        }
        errno wasi_snapshot_preview1.fd_allocate(uint fd, ulong offset, ulong len)
        {
            return errno.ERRNO_PERM;
        }
        errno wasi_snapshot_preview1.fd_close(uint fd)
        {
            return errno.ERRNO_PERM;
        }
        errno wasi_snapshot_preview1.fd_datasync(uint fd)
        {
            return errno.ERRNO_PERM;
        }
        errno wasi_snapshot_preview1.fd_fdstat_set_flags(uint fd, Fdflags flags)
        {
            return errno.ERRNO_PERM;
        }
        errno wasi_snapshot_preview1.fd_fdstat_set_rights(uint fd, Rights fs_rights_base, Rights fs_rights_inheriting)
        {
            return errno.ERRNO_PERM;
        }
        errno wasi_snapshot_preview1.fd_filestat_set_size(uint fd, ulong size)
        {
            return errno.ERRNO_PERM;
        }
        errno wasi_snapshot_preview1.fd_filestat_set_times(uint fd, ulong atim, ulong mtim, Fstflags fst_flags)
        {
            return errno.ERRNO_PERM;
        }
        errno wasi_snapshot_preview1.fd_prestat_dir_name(uint fd, UIntPtr path, uint path_len)
        {
            return errno.ERRNO_PERM;
        }
        errno wasi_snapshot_preview1.fd_renumber(uint fd, uint to)
        {
            return errno.ERRNO_PERM;
        }
        errno wasi_snapshot_preview1.fd_sync(uint fd)
        {
            return errno.ERRNO_PERM;
        }
        errno wasi_snapshot_preview1.path_create_directory(uint fd, string path)
        {
            return errno.ERRNO_PERM;
        }
        errno wasi_snapshot_preview1.path_filestat_set_times(uint fd, Lookupflags flags, string path, ulong atim, ulong mtim, Fstflags fstflags)
        {
            return errno.ERRNO_PERM;
        }
        errno wasi_snapshot_preview1.path_link(uint old_fs, Lookupflags old_flags, string old_path, uint new_fd, string new_path)
        {
            return errno.ERRNO_PERM;
        }
        errno wasi_snapshot_preview1.path_readlink(uint fd, string path, UIntPtr buf, uint buf_len)
        {
            return errno.ERRNO_PERM;
        }
        errno wasi_snapshot_preview1.path_remove_directory(uint fd, string path)
        {
            return errno.ERRNO_PERM;
        }
        errno wasi_snapshot_preview1.path_rename(uint fd, string old_path, uint new_fd, string new_path)
        {
            return errno.ERRNO_PERM;
        }
        errno wasi_snapshot_preview1.path_symlink(string old_path, uint fd, string new_path)
        {
            return errno.ERRNO_PERM;
        }
        errno wasi_snapshot_preview1.path_unlink_file(uint fd, string path)
        {
            return errno.ERRNO_PERM;
        }
        void wasi_snapshot_preview1.proc_exit(uint rval)
        {

        }
        errno wasi_snapshot_preview1.proc_raise(Signal sig)
        {
            return errno.ERRNO_PERM;
        }
        errno wasi_snapshot_preview1.sched_yield()
        {
            return errno.ERRNO_PERM;
        }
        errno wasi_snapshot_preview1.random_get(UIntPtr buf, uint buf_len)
        {
            return errno.ERRNO_PERM;
        }
        errno wasi_snapshot_preview1.sock_shutdown(uint fd, Sdflags how)
        {
            return errno.ERRNO_PERM;
        }
        Tuple<errno, uint, uint> wasi_snapshot_preview1.args_sizes_get()
        {
            throw new NotImplementedException();
        }
        Tuple<errno, uint, uint> wasi_snapshot_preview1.environ_sizes_get()
        {
            throw new NotImplementedException();
        }
        Tuple<errno, ulong> wasi_snapshot_preview1.clock_res_get(clockid id)
        {
            throw new NotImplementedException();
        }
        Tuple<errno, ulong> wasi_snapshot_preview1.clock_time_get(clockid id, ulong precision)
        {
            throw new NotImplementedException();
        }
        Tuple<errno, Fdstat> wasi_snapshot_preview1.fd_fdstat_get(uint fd)
        {
            throw new NotImplementedException();
        }
        Tuple<errno, Filestat> wasi_snapshot_preview1.fd_filestat_get(uint fd)
        {
            throw new NotImplementedException();
        }
        Tuple<errno, uint> wasi_snapshot_preview1.fd_pread(uint fd, Iovec[] iovs, ulong offset)
        {
            throw new NotImplementedException();
        }
        Tuple<errno, Prestat> wasi_snapshot_preview1.fd_prestat_get(uint fd)
        {
            throw new NotImplementedException();
        }
        Tuple<errno, uint> wasi_snapshot_preview1.fd_pwrite(uint fd, UIntPtr iovs, ulong offset)
        {
            throw new NotImplementedException();
        }

        Tuple<errno, uint> wasi_snapshot_preview1.fd_read(uint fd, UIntPtr iovs, ulong offset)
        {
            throw new NotImplementedException();
        }

        Tuple<errno, uint> wasi_snapshot_preview1.fd_readdir(uint fd, UIntPtr buf, uint buf_len, ulong cookie)
        {
            throw new NotImplementedException();
        }

        Tuple<errno, ulong> wasi_snapshot_preview1.fd_seed(uint fd, ulong offset, byte whence)
        {
            throw new NotImplementedException();
        }

        Tuple<errno, ulong> wasi_snapshot_preview1.fd_tell(uint fd)
        {
            throw new NotImplementedException();
        }

        Tuple<errno, uint> wasi_snapshot_preview1.fd_write(uint fd, UIntPtr iovs)
        {
            throw new NotImplementedException();
        }

        Tuple<errno, Filestat> wasi_snapshot_preview1.path_filestat_get(uint fd, Lookupflags flags, string path)
        {
            throw new NotImplementedException();
        }

        Tuple<errno, uint> wasi_snapshot_preview1.path_open(uint fd, Lookupflags dirflags, string path, Oflags oflags, Rights fs_rights_base, Rights fs_rights_inherting, Fdflags fdflags)
        {
            throw new NotImplementedException();
        }

        Tuple<errno, uint> wasi_snapshot_preview1.poll_oneoff(UIntPtr _in, UIntPtr _out, uint nsubscriptions)
        {
            throw new NotImplementedException();
        }

        Tuple<errno, uint, Roflags> wasi_snapshot_preview1.sock_recv(uint fd, UIntPtr si_data, Riflags ri_flags)
        {
            throw new NotImplementedException();
        }

        Tuple<errno, uint> wasi_snapshot_preview1.sock_send(uint fd, UIntPtr si_data, ushort si_flags)
        {
            throw new NotImplementedException();
        }

        public Wasmtime.Externs.ExternMemory MemoryBase { get; set; }
        public Wasmtime.Module Module { get; set; }
        public dynamic Instance { get; set; }

        const string preview = "wasi_snapshot_preview1";

        public WASILayer(string loadWasm)
        {
            var host = new HostBuilder()
                .WithMultiValue(true)
                .WithSIMD(true)
                .Build();

            var wasi = new WasiConfiguration()
                .WithInheritedStandardError()
                .WithInheritedStandardOutput();

            host.DefineWasi("wasi_snapshot_preview1", wasi);

            //global = host.DefineMutableGlobal("", "global", 1);

            WASIHelpers.BindAll(this, host);

            //host.DefineFunction<int>(preview, "proc_exit", proc_exit);
            //host.DefineFunction<int,int,int,int, int>(preview, "fd_write", fd_write);
            //host.DefineFunction<Caller, int, int, int, int, int>(preview, "fd_write", fd_write2);
            //host.DefineFunction<int, int, int>(preview, "fd_prestat_get", fd_prestat_get);
            //host.DefineFunction<int, int, int, int>(preview, "fd_prestat_dir_name", fd_prestat_dir_name);
            //host.DefineFunction<int, int, int>(preview, "environ_sizes_get", environ_sizes_get);
            //host.DefineFunction<int, int, int>(preview, "environ_get", environ_get);

            Module = host.LoadModule(loadWasm);

            Instance = host.Instantiate(Module);
            Instance.main(0,0);
           

            MemoryBase = (Wasmtime.Externs.ExternMemory)Instance.Externs.Memories[0];
        }
    }
}
