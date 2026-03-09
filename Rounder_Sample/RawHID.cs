using System.Runtime.InteropServices;

namespace RounderSample
{
    public static class RawHID
    {

        private static bool dllIsLoaded = false;
        private static IntPtr dllHandle = IntPtr.Zero;


        // dynamically loaded functions
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int rawhid_open_delegate(int max, int vid, int pid, int usage_page, int usage);
        private static rawhid_open_delegate rawhid_open;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int rawhid_recv_delegate(int num, [Out]byte[] buf, int len, int timeout);
        private static rawhid_recv_delegate rawhid_recv;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int rawhid_send_delegate(int num, [In]byte[] buf, int len, int timeout);
        private static rawhid_send_delegate rawhid_send;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void rawhid_close_delegate(int num);
        private static rawhid_close_delegate rawhid_close;


        /// <summary>
        /// rawhid_open - open 1 or more devices
        /// Inputs:
        ///     max = maximum number of devices to open
        ///     vid = Vendor ID, or -1 if any
        ///     pid = Product ID, or -1 if any
        ///     usage_page = top level usage page, or -1 if any
        ///     usage = top level usage number, or -1 if any
        /// Output:
        ///     actual number of devices opened
        /// </summary>
        public static int Open(int max, int vid, int pid, int usage_page, int usage)
        {
            //For IGEMS Straighter & IGEMS Rounder: Product ID 0x16C0, Vendor ID 0x0486. Straighter usage_page
            loadDllFunctions();
            return rawhid_open(max, vid, pid, usage_page, usage);
        }

        /// <summary>
        /// Receive a packet from device "num" (zero based). Buffer "buf" receives the data, and "len" must be the packet size (default is 64 bytes). Wait up to "timeout" milliseconds. Return is the number of bytes received, or zero if no packet received within timeout, or -1 if an error (typically indicating the device was unplugged). 
        /// </summary>
        public static int Recieve(int num, byte[] buffer, int timeout)
        {
            return rawhid_recv(num, buffer, buffer.Length, timeout);
        }

        /// <summary>
        /// Send a packet to device "num" (zero based). Buffer "buf" contains the data to transmit, and "len" must be the packet size (default is 64 bytes). Wait up to "timeout" milliseconds. Return is the number of bytes sent, or zero if unable to send before timeout, or -1 if an error (typically indicating the device was unplugged). 
        /// </summary>
        public static int Send(int num, byte[] buffer, int len, int timeout)
        {
            return rawhid_send(num, buffer, len, timeout);

        }


        private static void loadDllFunctions()
        {
            if (dllIsLoaded == true)
                return;

            string dllName;
            if (IntPtr.Size == 8)
                dllName = "RawHIDDriverX64.dll";
            else
                dllName = "RawHIDDriverX86.dll";


            IntPtr dllhandle = LoadLibrary(dllName);
            if (dllhandle == IntPtr.Zero)
                throw new Exception("Unable to locate dll: " + dllName);

            IntPtr funcptr;

            funcptr = GetProcAddress(dllhandle, "rawhid_open");
            rawhid_open = (rawhid_open_delegate)Marshal.GetDelegateForFunctionPointer(funcptr, typeof(rawhid_open_delegate));

            funcptr = GetProcAddress(dllhandle, "rawhid_recv");
            rawhid_recv = (rawhid_recv_delegate)Marshal.GetDelegateForFunctionPointer(funcptr, typeof(rawhid_recv_delegate));

            funcptr = GetProcAddress(dllhandle, "rawhid_send");
            rawhid_send = (rawhid_send_delegate)Marshal.GetDelegateForFunctionPointer(funcptr, typeof(rawhid_send_delegate));

            funcptr = GetProcAddress(dllhandle, "rawhid_close");
            rawhid_close = (rawhid_close_delegate)Marshal.GetDelegateForFunctionPointer(funcptr, typeof(rawhid_close_delegate));


            dllIsLoaded = true;
        }


        #region PINVOKE

        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Ansi)]
        static extern IntPtr LoadLibrary([MarshalAs(UnmanagedType.LPStr)]string lpFileName);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool FreeLibrary(IntPtr hModule);

        [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        static extern IntPtr GetProcAddress(IntPtr hModule, string procName);



        #endregion PINVOKE
    }




}
