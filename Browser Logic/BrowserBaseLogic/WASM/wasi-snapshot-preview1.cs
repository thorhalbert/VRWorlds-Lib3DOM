using System;
using System.Collections.Generic;
using System.Text;

using size = System.UInt32;
using filesize = System.UInt64;
using timestamp = System.UInt64;
using fd = System.UInt32;
using device = System.UInt64;
using inode = System.UInt64;
using linkcount = System.UInt64;
using dircookie = System.UInt64;
using dirnamlen = System.UInt32;
using filedelta = System.UInt64;
using whence = System.Byte;
using exitcode = System.UInt32;
using siflags = System.UInt16;

namespace VRWorlds.Browser.WASM
{
    public enum clockid : UInt32
    {
        realtime = 0,
        monotonic = 1,
        process_cputime_id = 2,
        thread_cputime_id = 3
    }

    public enum errno : UInt16
    {
        /// Successful call
        ERRNO_SUCCESS = 0,
        /// Argument list too long.
        ERRNO_2BIG = 1,
        /// Permission denied.
        ERRNO_ACCES = 2,
        /// Address in use.
        ERRNO_ADDRINUSE = 3,
        /// Address not available.
        ERRNO_ADDRNOTAVAIL = 4,
        /// Address family not supported.
        ERRNO_AFNOSUPPORT = 5,
        /// Resource unavailable, or operation would block.
        ERRNO_AGAIN = 6,
        /// Connection already in progress.
        ERRNO_ALREADY = 7,
        /// Bad file descriptor.
        ERRNO_BADF = 8,
        /// Bad message.
        ERRNO_BADMSG = 9,
        /// Device or resource busy.
        ERRNO_BUSY = 10,
        /// Operation canceled.
        ERRNO_CANCELED = 11,
        /// No child processes.
        ERRNO_CHILD = 12,
        /// Connection aborted.
        ERRNO_CONNABORTED = 13,
        /// Connection refused.
        ERRNO_CONNREFUSED = 14,
        /// Connection reset.
        ERRNO_CONNRESET = 15,
        /// Resource deadlock would occur.
        ERRNO_DEADLK = 16,
        /// Destination address required.
        ERRNO_DESTADDRREQ = 17,
        /// Mathematics argument out of domain of function.
        ERRNO_DOM = 18,
        /// Reserved.
        ERRNO_DQUOT = 19,
        /// File exists.
        ERRNO_EXIST = 20,
        /// Bad address.
        ERRNO_FAULT = 21,
        /// File too large.
        ERRNO_FBIG = 22,
        /// Host is unreachable.
        ERRNO_HOSTUNREACH = 23,
        /// Identifier removed.
        ERRNO_IDRM = 24,
        /// Illegal byte sequence.
        ERRNO_ILSEQ = 25,
        /// Operation in progress.
        ERRNO_INPROGRESS = 26,
        /// Interrupted function.
        ERRNO_INTR = 27,
        /// Invalid argument.
        ERRNO_INVAL = 28,
        /// I/O error.
        ERRNO_IO = 29,
        /// Socket is connected.
        ERRNO_ISCONN = 30,
        /// Is a directory.
        ERRNO_ISDIR = 31,
        /// Too many levels of symbolic links.
        ERRNO_LOOP = 32,
        /// File descriptor value too large.
        ERRNO_MFILE = 33,
        /// Too many links.
        ERRNO_MLINK = 34,
        /// Message too large.
        ERRNO_MSGSIZE = 35,
        /// Reserved.
        ERRNO_MULTIHOP = 36,
        /// Filename too long.
        ERRNO_NAMETOOLONG = 37,
        /// Network is down.
        ERRNO_NETDOWN = 38,
        /// Connection aborted by network.
        ERRNO_NETRESET = 39,
        /// Network unreachable.
        ERRNO_NETUNREACH = 40,
        /// Too many files open in system.
        ERRNO_NFILE = 41,
        /// No buffer space available.
        ERRNO_NOBUFS = 42,
        /// No such device.
        ERRNO_NODEV = 43,
        /// No such file or directory.
        ERRNO_NOENT = 44,
        /// Executable file format error.
        ERRNO_NOEXEC = 45,
        /// No locks available.
        ERRNO_NOLCK = 46,
        /// Reserved.
        ERRNO_NOLINK = 47,
        /// Not enough space.
        ERRNO_NOMEM = 48,
        /// No message of the desired type.
        ERRNO_NOMSG = 49,
        /// Protocol not available.
        ERRNO_NOPROTOOPT = 50,
        /// No space left on device.
        ERRNO_NOSPC = 51,
        /// Function not supported.
        ERRNO_NOSYS = 52,
        /// The socket is not connected.
        ERRNO_NOTCONN = 53,
        /// Not a directory or a symbolic link to a directory.
        ERRNO_NOTDIR = 54,
        /// Directory not empty.
        ERRNO_NOTEMPTY = 55,
        /// State not recoverable.
        ERRNO_NOTRECOVERABLE = 56,
        /// Not a socket.
        ERRNO_NOTSOCK = 57,
        /// Not supported, or operation not supported on socket.
        ERRNO_NOTSUP = 58,
        /// Inappropriate I/O control operation.
        ERRNO_NOTTY = 59,
        /// No such device or address.
        ERRNO_NXIO = 60,
        /// Value too large to be stored in data type.
        ERRNO_OVERFLOW = 61,
        /// Previous owner died.
        ERRNO_OWNERDEAD = 62,
        /// Operation not permitted.
        ERRNO_PERM = 63,
        /// Broken pipe.
        ERRNO_PIPE = 64,
        /// Protocol error.
        ERRNO_PROTO = 65,
        /// Protocol not supported.
        ERRNO_PROTONOSUPPORT = 66,
        /// Protocol wrong type for socket.
        ERRNO_PROTOTYPE = 67,
        /// Result too large.
        ERRNO_RANGE = 68,
        /// Read-only file system.
        ERRNO_ROFS = 69,
        /// Invalid seek.
        ERRNO_SPIPE = 70,
        /// No such process.
        ERRNO_SRCH = 71,
        /// Reserved.
        ERRNO_STALE = 72,
        /// Connection timed out.
        ERRNO_TIMEDOUT = 73,
        /// Text file busy.
        ERRNO_TXTBSY = 74,
        /// Cross-device link.
        ERRNO_XDEV = 75,
        /// Extension= Capabilities insufficient.
        ERRNO_NOTCAPABLE = 76,
    }

    [Flags]
    public enum Rights : UInt64
    {
        /// The right to invoke `fd_datasync`.
        /// If `path_open` is set, includes the right to invoke
        /// `path_open` with `fdflag::dsync`.
        RIGHTS_FD_DATASYNC = 0x1,
        /// The right to invoke `fd_read` and `sock_recv`.
        /// If `rights::fd_seek` is set, includes the right to invoke `fd_pread`.
        RIGHTS_FD_READ = 0x2,
        /// The right to invoke `fd_seek`. This flag implies `rights::fd_tell`.
        RIGHTS_FD_SEEK = 0x4,
        /// The right to invoke `fd_fdstat_set_flags`.
        RIGHTS_FD_FDSTAT_SET_FLAGS = 0x8,
        /// The right to invoke `fd_sync`.
        /// If `path_open` is set, includes the right to invoke
        /// `path_open` with `fdflag::rsync` and `fdflag::dsync`.
        RIGHTS_FD_SYNC = 0x10,
        /// The right to invoke `fd_seek` in such a way that the file offset
        /// remains unaltered (i.e., `WHENCE_CUR` with offset zero), or to
        /// invoke `fd_tell`.
        RIGHTS_FD_TELL = 0x20,
        /// The right to invoke `fd_write` and `sock_send`.
        /// If `rights::fd_seek` is set, includes the right to invoke `fd_pwrite`.
        RIGHTS_FD_WRITE = 0x40,
        /// The right to invoke `fd_advise`.
        RIGHTS_FD_ADVISE = 0x80,
        /// The right to invoke `fd_allocate`.
        RIGHTS_FD_ALLOCATE = 0x100,
        /// The right to invoke `path_create_directory`.
        RIGHTS_PATH_CREATE_DIRECTORY = 0x200,
        /// If `path_open` is set, the right to invoke `path_open` with `oflags::creat`.
        RIGHTS_PATH_CREATE_FILE = 0x400,
        /// The right to invoke `path_link` with the file descriptor as the
        /// source directory.
        RIGHTS_PATH_LINK_SOURCE = 0x800,
        /// The right to invoke `path_link` with the file descriptor as the
        /// target directory.
        RIGHTS_PATH_LINK_TARGET = 0x1000,
        /// The right to invoke `path_open`.
        RIGHTS_PATH_OPEN = 0x2000,
        /// The right to invoke `fd_readdir`.
        RIGHTS_FD_READDIR = 0x4000,
        /// The right to invoke `path_readlink`.
        RIGHTS_PATH_READLINK = 0x8000,
        /// The right to invoke `path_rename` with the file descriptor as the source directory.
        RIGHTS_PATH_RENAME_SOURCE = 0x10000,
        /// The right to invoke `path_rename` with the file descriptor as the target directory.
        RIGHTS_PATH_RENAME_TARGET = 0x20000,
        /// The right to invoke `path_filestat_get`.
        RIGHTS_PATH_FILESTAT_GET = 0x40000,
        /// The right to change a file's size (there is no `path_filestat_set_size`).
        /// If `path_open` is set, includes the right to invoke `path_open` with `oflags::trunc`.
        RIGHTS_PATH_FILESTAT_SET_SIZE = 0x80000,
        /// The right to invoke `path_filestat_set_times`.
        RIGHTS_PATH_FILESTAT_SET_TIMES = 0x100000,
        /// The right to invoke `fd_filestat_get`.
        RIGHTS_FD_FILESTAT_GET = 0x200000,
        /// The right to invoke `fd_filestat_set_size`.
        RIGHTS_FD_FILESTAT_SET_SIZE = 0x400000,
        /// The right to invoke `fd_filestat_set_times`.
        RIGHTS_FD_FILESTAT_SET_TIMES = 0x800000,
        /// The right to invoke `path_symlink`.
        RIGHTS_PATH_SYMLINK = 0x1000000,
        /// The right to invoke `path_remove_directory`.
        RIGHTS_PATH_REMOVE_DIRECTORY = 0x2000000,
        /// The right to invoke `path_unlink_file`.
        RIGHTS_PATH_UNLINK_FILE = 0x4000000,
        /// If `rights::fd_read` is set, includes the right to invoke `poll_oneoff` to subscribe to `eventtype::fd_read`.
        /// If `rights::fd_write` is set, includes the right to invoke `poll_oneoff` to subscribe to `eventtype::fd_write`.
        RIGHTS_POLL_FD_READWRITE = 0x8000000,
        /// The right to invoke `sock_shutdown`.
        RIGHTS_SOCK_SHUTDOWN = 0x10000000,
    }

    public enum Signal : Byte
    {

        /// No signal. Note that POSIX has special semantics for `kill(pid, 0)`,
        /// so this value is reserved.
        SIGNAL_NONE = 0,
        /// Hangup.
        /// Action: Terminates the process.
        SIGNAL_HUP = 1,
        /// Terminate interrupt signal.
        /// Action: Terminates the process.
        SIGNAL_INT = 2,
        /// Terminal quit signal.
        /// Action: Terminates the process.
        SIGNAL_QUIT = 3,
        /// Illegal instruction.
        /// Action: Terminates the process.
        SIGNAL_ILL = 4,
        /// Trace/breakpoint trap.
        /// Action: Terminates the process.
        SIGNAL_TRAP = 5,
        /// Process abort signal.
        /// Action: Terminates the process.
        SIGNAL_ABRT = 6,
        /// Access to an undefined portion of a memory object.
        /// Action: Terminates the process.
        SIGNAL_BUS = 7,
        /// Erroneous arithmetic operation.
        /// Action: Terminates the process.
        SIGNAL_FPE = 8,
        /// Kill.
        /// Action: Terminates the process.
        SIGNAL_KILL = 9,
        /// User-defined signal 1.
        /// Action: Terminates the process.
        SIGNAL_USR1 = 10,
        /// Invalid memory reference.
        /// Action: Terminates the process.
        SIGNAL_SEGV = 11,
        /// User-defined signal 2.
        /// Action: Terminates the process.
        SIGNAL_USR2 = 12,
        /// Write on a pipe with no one to read it.
        /// Action: Ignored.
        SIGNAL_PIPE = 13,
        /// Alarm clock.
        /// Action: Terminates the process.
        SIGNAL_ALRM = 14,
        /// Termination signal.
        /// Action: Terminates the process.
        SIGNAL_TERM = 15,
        /// Child process terminated, stopped, or continued.
        /// Action: Ignored.
        SIGNAL_CHLD = 16,
        /// Continue executing, if stopped.
        /// Action: Continues executing, if stopped.
        SIGNAL_CONT = 17,
        /// Stop executing.
        /// Action: Stops executing.
        SIGNAL_STOP = 18,
        /// Terminal stop signal.
        /// Action: Stops executing.
        SIGNAL_TSTP = 19,
        /// Background process attempting read.
        /// Action: Stops executing.
        SIGNAL_TTIN = 20,
        /// Background process attempting write.
        /// Action: Stops executing.
        SIGNAL_TTOU = 21,
        /// High bandwidth data is available at a socket.
        /// Action: Ignored.
        SIGNAL_URG = 22,
        /// CPU time limit exceeded.
        /// Action: Terminates the process.
        SIGNAL_XCPU = 23,
        /// File size limit exceeded.
        /// Action: Terminates the process.
        SIGNAL_XFSZ = 24,
        /// Virtual timer expired.
        /// Action: Terminates the process.
        SIGNAL_VTALRM = 25,
        /// Profiling timer expired.
        /// Action: Terminates the process.
        SIGNAL_PROF = 26,
        /// Window changed.
        /// Action: Ignored.
        SIGNAL_WINCH = 27,
        /// I/O possible.
        /// Action: Terminates the process.
        SIGNAL_POLL = 28,
        /// Power failure.
        /// Action: Terminates the process.
        SIGNAL_PWR = 29,
        /// Bad system call.
        /// Action: Terminates the process.
        SIGNAL_SYS = 30,
    }

    public enum Advice : byte
    {
        /// The application has no advice to give on its behavior with respect to the specified data.
        ADVICE_NORMAL = 0,
        /// The application expects to access the specified data sequentially from lower offsets to higher offsets.
        ADVICE_SEQUENTIAL = 1,
        /// The application expects to access the specified data in a random order.
        ADVICE_RANDOM = 2,
        /// The application expects to access the specified data in the near future.
        ADVICE_WILLNEED = 3,
        /// The application expects that it will not access the specified data in the near future.
        ADVICE_DONTNEED = 4,
        /// The application expects to access the specified data once and then not reuse it thereafter.
        ADVICE_NOREUSE = 5,
    }

    public enum Filetype : byte
    {
        /// The type of the file descriptor or file is unknown or is different from any of the other types specified.
        FILETYPE_UNKNOWN = 0,
        /// The file descriptor or file refers to a block device inode.
        FILETYPE_BLOCK_DEVICE = 1,
        /// The file descriptor or file refers to a character device inode.
        FILETYPE_CHARACTER_DEVICE = 2,
        /// The file descriptor or file refers to a directory inode.
        FILETYPE_DIRECTORY = 3,
        /// The file descriptor or file refers to a regular file inode.
        FILETYPE_REGULAR_FILE = 4,
        /// The file descriptor or file refers to a datagram socket.
        FILETYPE_SOCKET_DGRAM = 5,
        /// The file descriptor or file refers to a byte-stream socket.
        FILETYPE_SOCKET_STREAM = 6,
        /// The file refers to a symbolic link inode.
        FILETYPE_SYMBOLIC_LINK = 7,
    }
    public enum Fdflags : UInt16
    {
        /// Append mode: Data written to the file is always appended to the file's end.
        FDFLAGS_APPEND = 0x1,
        /// Write according to synchronized I/O data integrity completion. Only the data stored in the file is synchronized.
        FDFLAGS_DSYNC = 0x2,
        /// Non-blocking mode.
        FDFLAGS_NONBLOCK = 0x4,
        /// Synchronized read I/O operations.
        FDFLAGS_RSYNC = 0x8,
        /// Write according to synchronized I/O file integrity completion. In
        /// addition to synchronizing the data stored in the file, the implementation
        /// may also synchronously update the file's metadata.
        FDFLAGS_SYNC = 0x10,
    }
    public struct Fdstat
    {
        /// File type.
        Filetype fs_filetype;
        /// File descriptor flags.
        Fdflags fs_flags;
        /// Rights that apply to this file descriptor.
        Rights fs_rights_base;
        /// Maximum set of rights that may be installed on new file descriptors that
        /// are created through this file descriptor, e.g., through `path_open`.
        Rights fs_rights_inheriting;
    }
    public struct Filestat
    {
        /// Device ID of device containing the file.
        device dev;
        /// File serial number.
        inode ino;
        /// File type.
        Filetype filetype;
        /// Number of hard links to the file.
        linkcount nlink;
        /// For regular files, the file size in bytes. For symbolic links, the length in bytes of the pathname contained in the symbolic link.
        filesize size;
        /// Last data access timestamp.
        timestamp atim;
        /// Last data modification timestamp.
        timestamp mtim;
        /// Last file status change timestamp.
        timestamp ctim;
    }
    [Flags]
    public enum Fstflags : UInt16
    {
        /// Adjust the last data access timestamp to the value stored in `filestat::st_atim`.
        FSTFLAGS_ATIM = 0x1,
        /// Adjust the last data access timestamp to the time of clock `clock::realtime`.
        FSTFLAGS_ATIM_NOW = 0x2,
        /// Adjust the last data modification timestamp to the value stored in `filestat::st_mtim`.
        FSTFLAGS_MTIM = 0x4,
        /// Adjust the last data modification timestamp to the time of clock `clock::realtime`.
        FSTFLAGS_MTIM_NOW = 0x8,
    }
    public struct Iovec
    {
        /// The address of the buffer to be filled.
        byte[] buf;
        /// The length of the buffer to be filled.
        size buf_len;
    }
    public struct Ciovec
    {
        /// The address of the buffer to be written.
        byte[] buf;
        /// The length of the buffer to be written.
        size buf_len;
    }
    public enum Preopentype : byte
    {
        PREOPENTYPE_DIR = 0
    }

    public struct PrestatDir
    {
        /// The length of the directory name for use with `fd_prestat_dir_name`.
        public size pr_name_len;
    }

    public struct PrestatU
    {
        /// When type is `preopentype::dir`:
        public PrestatDir dir;
    }
    public struct Prestat
    {
        /// The type of the pre-opened capability.
        Preopentype pr_type;
        /// The contents of the information.
        PrestatU u;
    }
    [Flags]
    public enum Lookupflags : UInt32
    {
        LOOKUPFLAGS_SYMLINK_FOLLOW = 0x1,
    }
    public enum Oflags : UInt16
    {
        /// Create file if it does not exist.
        OFLAGS_CREAT = 0x1,
        /// Fail if not a directory.
        OFLAGS_DIRECTORY = 0x2,
        /// Fail if file already exists.
        OFLAGS_EXCL = 0x4,
        /// Truncate file to size 0.
        OFLAGS_TRUNC = 0x8,
    }
    public enum Roflags : UInt16
    {
        /// Returned by `sock_recv`: Message data has been truncated.
        ROFLAGS_RECV_DATA_TRUNCATED = 0x1,
    }

    public enum Riflags : UInt16
    {
        /// Returns the message without removing it from the socket's receive queue.
        RIFLAGS_RECV_PEEK = 0x1,
        /// On byte-stream sockets, block until the full amount of data can be returned.
        RIFLAGS_RECV_WAITALL = 0x2,
    }

    public enum Sdflags : byte
    {
        /// Disables further receive operations.
        SDFLAGS_RD = 0x1,
        /// Disables further send operations.
        SDFLAGS_WR = 0x2,
    }
    internal interface wasi_snapshot_preview1
    {
        // args_get(argv: Pointer<Pointer<u8>>, argv_buf: Pointer<u8>) -> errno
        errno args_get(UIntPtr argv, UIntPtr argv_buf);
        // args_sizes_get() -> (errno, size, size)
        Tuple<errno, size, size> args_sizes_get();
        // environ_get(environ: Pointer<Pointer<u8>>, environ_buf: Pointer<u8>) -> errno
        errno environ_get(UIntPtr environ, UIntPtr environ_buf);
        // environ_sizes_get() -> (errno, size, size)
        Tuple<errno, size, size> environ_sizes_get();
        // clock_res_get(id: clockid) -> (errno, timestamp)
        Tuple<errno, timestamp> clock_res_get(clockid id);
        // clock_time_get(id: clockid, precision: timestamp) -> (errno, timestamp)
        Tuple<errno, timestamp> clock_time_get(clockid id, timestamp precision);
        // fd_advise(fd: fd, offset: filesize, len: filesize, advice: advice) -> errno
        errno fd_advise(fd fd, filesize offset, filesize len, Advice advice);
        // fd_allocate(fd: fd, offset: filesize, len: filesize) -> errno
        errno fd_allocate(fd fd, filesize offset, filesize len);
        // fd_close(fd: fd) -> errno
        errno fd_close(fd fd);
        // fd_datasync(fd: fd) -> errno
        errno fd_datasync(fd fd);
        // fd_fdstat_get(fd: fd) -> (errno, fdstat)
        Tuple<errno, Fdstat> fd_fdstat_get(fd fd);
        // fd_fdstat_set_flags(fd: fd, flags: fdflags) -> errno
        errno fd_fdstat_set_flags(fd fd, Fdflags flags);
        // fd_fdstat_set_rights(fd: fd, fs_rights_base: rights, fs_rights_inheriting: rights) -> errno
        errno fd_fdstat_set_rights(fd fd, Rights fs_rights_base, Rights fs_rights_inheriting);
        // fd_filestat_get(fd: fd) -> (errno, filestat);
        Tuple<errno, Filestat> fd_filestat_get(fd fd);
        // fd_filestat_set_size(fd: fd, size: filesize) -> errno
        errno fd_filestat_set_size(fd fd, filesize size);
        // fd_filestat_set_times(fd: fd, atim: timestamp, mtim: timestamp, fst_flags: fstflags) -> errno
        errno fd_filestat_set_times(fd fd, timestamp atim, timestamp mtim, Fstflags fst_flags);
        // fd_pread(fd: fd, iovs: iovec_array, offset: filesize) -> (errno, size)
        Tuple<errno, size> fd_pread(fd fd, Iovec[] iovs, filesize offset);
        // fd_prestat_get(fd: fd) -> (errno, prestat)
        Tuple<errno, Prestat> fd_prestat_get(fd fd);
        // fd_prestat_dir_name(fd: fd, path: Pointer<u8>, path_len: size) -> errno
        errno fd_prestat_dir_name(fd fd, UIntPtr path, size path_len);
        // fd_pwrite(fd: fd, iovs: ciovec_array, offset: filesize) -> (errno, size)
        Tuple<errno, size> fd_pwrite(fd fd, UIntPtr iovs, filesize offset);
        // fd_read(fd: fd, iovs: iovec_array) -> (errno, size)
        Tuple<errno, size> fd_read(fd fd, UIntPtr iovs, filesize offset);
        // fd_readdir(fd: fd, buf: Pointer<u8>, buf_len: size, cookie: dircookie) -> (errno, size)
        Tuple<errno, size> fd_readdir(fd fd, UIntPtr buf, size buf_len, dircookie cookie);
        // fd_renumber(fd: fd, to: fd) -> errno
        errno fd_renumber(fd fd, fd to);
        // fd_seek(fd: fd, offset: filedelta, whence: whence) -> (errno, filesize)
        Tuple<errno, filesize> fd_seed(fd fd, filedelta offset, whence whence);
        // fd_sync(fd: fd) -> errno
        errno fd_sync(fd fd);
        // fd_tell(fd: fd) -> (errno, filesize)
        Tuple<errno, filesize> fd_tell(fd fd);
        // fd_write(fd: fd, iovs: ciovec_array) -> (errno, size)
        Tuple<errno, size> fd_write(fd fd, UIntPtr iovs);
        // path_create_directory(fd: fd, path: string) -> errno
        errno path_create_directory(fd fd, string path);
        // path_filestat_get(fd: fd, flags: lookupflags, path: string) -> (errno, filestat)
        Tuple<errno, Filestat> path_filestat_get(fd fd, Lookupflags flags, string path);
        // path_filestat_set_times(fd: fd, flags: lookupflags, path: string, atim: timestamp, mtim: timestamp, fst_flags: fstflags) -> errno
        errno path_filestat_set_times(fd fd, Lookupflags flags, string path, timestamp atim, timestamp mtim, Fstflags fstflags);
        // path_link(old_fd: fd, old_flags: lookupflags, old_path: string, new_fd: fd, new_path: string) -> errno
        errno path_link(fd old_fs, Lookupflags old_flags, string old_path, fd new_fd, string new_path);
        // path_open(fd: fd, dirflags: lookupflags, path: string, oflags: oflags, fs_rights_base: rights, fs_rights_inherting: rights, fdflags: fdflags) -> (errno, fd)
        Tuple<errno, fd> path_open(fd fd, Lookupflags dirflags, string path, Oflags oflags, Rights fs_rights_base, Rights fs_rights_inherting, Fdflags fdflags);
        // path_readlink(fd: fd, path: string, buf: Pointer<u8>, buf_len: size) -> (errno, size)
        errno path_readlink(fd fd, string path, UIntPtr buf, size buf_len );
        // path_remove_directory(fd: fd, path: string) -> errno
        errno path_remove_directory(fd fd, string path);
        // path_rename(fd: fd, old_path: string, new_fd: fd, new_path: string) -> errno
        errno path_rename(fd fd, string old_path, fd new_fd, string new_path);
        // path_symlink(old_path: string, fd: fd, new_path: string) -> errno
        errno path_symlink(string old_path, fd fd, string new_path);
        // path_unlink_file(fd: fd, path: string) -> errno
        errno path_unlink_file(fd fd, string path);
        // poll_oneoff(in: ConstPointer<subscription>, out: Pointer<event>, nsubscriptions: size) -> (errno, size)
        Tuple<errno, size> poll_oneoff(UIntPtr _in, UIntPtr _out, size nsubscriptions);
        // proc_exit(rval: exitcode)
        void proc_exit(exitcode rval);
        // proc_raise(sig: signal) -> errno
        errno proc_raise(Signal sig);
        // sched_yield() -> errno
        errno sched_yield();
        // random_get(buf: Pointer<u8>, buf_len: size) -> errno
        errno random_get(UIntPtr buf, size buf_len);
        // sock_recv(fd: fd, ri_data: iovec_array, ri_flags: riflags) -> (errno, size, roflags)
        Tuple<errno, size, Roflags> sock_recv(fd fd, UIntPtr si_data, Riflags ri_flags);
        // sock_send(fd: fd, si_data: ciovec_array, si_flags: siflags) -> (errno, size)
        Tuple<errno, size> sock_send(fd fd, UIntPtr si_data, siflags si_flags);
        // sock_shutdown(fd: fd, how: sdflags) -> errno
        errno sock_shutdown(fd fd, Sdflags how);
    }
    internal static class WASIHelpers
    {
        internal static void BindAll(wasi_snapshot_preview1 inter, Wasmtime.Host bindToHost)
        {
            var map = inter.GetType().GetInterfaceMap(typeof(wasi_snapshot_preview1));

            //foreach (var i in map.InterfaceMethods)
            //{
             


                //bindToHost.DefineFunction("wasi_snapshot_preview1", i.Name, i.MakeGenericMethod);
            //}

            //bindToHost.DefineFunction<UIntPtr, size,errno>("wasi_snapshot_preview1", "random_get", inter.random_get);
            //bindToHost.DefineFunction<fd, UIntPtr, Riflags, Tuple<errno, size, Roflags>>("wasi_snapshot_preview1", "sock_recv", inter.sock_recv);
            //bindToHost.DefineFunction<fd, UIntPtr,siflags, Tuple<errno,size>>("wasi_snapshot_preview1", "sock_send", inter.sock_send);
            //bindToHost.DefineFunction<fd, Sdflags, errno>("wasi_snapshot_preview1", "sock_shutdown", inter.sock_shutdown);

        }
    }
}
