using Memory;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GARVIS.Servises
{
   public class FreeFireHack
    {
        private Mem memory = new Mem();
        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(
         int dwDesiredAccess,
         bool bInheritHandle,
         int dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr VirtualAllocEx(
          IntPtr hProcess,
          IntPtr lpAddress,
          uint dwSize,
          uint flAllocationType,
          uint flProtect);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool WriteProcessMemory(
          IntPtr hProcess,
          IntPtr lpBaseAddress,

          byte[] lpBuffer,
          uint nSize,
          out int lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        public static extern IntPtr CreateRemoteThread(
          IntPtr hProcess,
          IntPtr lpThreadAttributes,
          uint dwStackSize,
          IntPtr lpStartAddress,
          IntPtr lpParameter,
          uint dwCreationFlags,
          IntPtr lpThreadId);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern uint WaitForSingleObject(IntPtr hHandle, uint dwMilliseconds);

        [DllImport("kernel32.dll")]
        public static extern void CloseHandle(IntPtr hObject);
        public void ActiveChams()
        {
            Cham3D();
        }
        public async Task ActiveAimHead()
        {
           await Aim();
        } 
        public async void ActiveFastChangeAwm()
        {
            await FastChange();
        } 
        public async Task ActiveAimAwm()
        {
           await AwmAim();
        }
        private void Cham3D()
        {
            Process[] processesByName = Process.GetProcessesByName("HD-Player");
            if (processesByName.Length != 0)
            {
                IntPtr num1 = OpenProcess(2035711, false, processesByName[0].Id);
                if (num1 != IntPtr.Zero)
                {
                    byte[] bytes = Encoding.ASCII.GetBytes("C:\\Windows\\hk4x.dll");
                    IntPtr num2 = VirtualAllocEx(num1, IntPtr.Zero, (uint)bytes.Length, 4096U, 64U);
                    WriteProcessMemory(num1, num2, bytes, (uint)bytes.Length, out int _);
                    IntPtr procAddress = GetProcAddress(GetModuleHandle("kernel32.dll"), "LoadLibraryA");
                    int num3 = (int)WaitForSingleObject(CreateRemoteThread(num1, IntPtr.Zero, 0U, procAddress, num2, 0U, IntPtr.Zero), uint.MaxValue);
                    CloseHandle(num1);
                    
                    Console.Beep(600, 300);
                }
                
            }
            else
            {
               
                Console.Beep(200, 300);
            }
        }
        private async Task Aim()
        {
            if (Process.GetProcessesByName("HD-Player").Length == 0)
            {
            
                Console.Beep(200, 300);
            }
            else
            {
                
                int proc = Process.GetProcessesByName("HD-Player")[0].Id;
                this.memory.OpenProcess(proc);
                IEnumerable<long> result = await this.memory.AoBScan("FF FF FF FF 00 00 00 00 00 00 00 00 00 00 00 00 ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 A5 43 00 00 00 00", true, true, "");
                if (result.Any<long>())
                {
                    foreach (long Current in result)
                    {
                        long leitura = Current + 96L;
                        long escri = Current + 92L;
                        int Read = this.memory.ReadMemory<int>(leitura.ToString("X"), "");
                        this.memory.WriteMemory(escri.ToString("X"), "int", Read.ToString(), "", (Encoding)null, true);
                    }
                   
                    Console.Beep(600, 300);
                }
              
                result = (IEnumerable<long>)null;
            }
        }
       
        private async Task FastChange()
        {
           
                string search = "?? ?? ?? 3F 00 00 80 3E 00 00 00 00 ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? 00 00 00 00 00 00 00 00 00 00 80 3F";
                string replace = "EC 51 B8 3D 8F C2 F5 3C";
                bool k = false;
                if (Process.GetProcessesByName("HD-Player").Length == 0)
                {
                    
                    Console.Beep(200, 300);
                }
                else
                {
                    this.memory.OpenProcess("HD-Player");
                    int i2 = 22000000;
                    IEnumerable<long> wl = await this.memory.AoBScan(search, true, true, "");
                    long num = wl.FirstOrDefault<long>();
                    string u = "0x" + num.ToString("X");
                    if (wl.Count<long>() != 0)
                    {
                        for (int i = 0; i < wl.Count<long>(); ++i)
                        {
                            ++i2;
                            Mem memory = this.memory;
                            num = wl.ElementAt<long>(i);
                            string str1 = num.ToString("X");
                            string str2 = replace;
                            memory.WriteMemory(str1, "bytes", str2, "", (Encoding)null, true);
                        }
                        k = true;
                    }
                    if (k)
                    {
                       
                        Console.Beep(600, 300);
                    }
                    else
                       
                    wl = (IEnumerable<long>)null;
                    u = (string)null;
                }
                search = (string)null;
                replace = (string)null;
            
         
        }
        private async Task AwmAim()
        {
            string search = "01 00 00 00 00 00 00 00 00 00 00 00 ?? ?? ?? ?? ?? ?? ?? ?? ?? 00 00 00 00 00 00 00 00 00 00 00 ?? ?? ?? ?? ?? ?? ?? ?? ?? 00 00 00 01 00 00 00 ?? ?? ?? 3F 01 00 00 00 ?? ?? ?? 3F 00 00 00 00";
            string replace = "00 00";
            bool k = false;
            if (Process.GetProcessesByName("HD-Player").Length == 0)
            {
                
                Console.Beep(200, 300);
                search = (string)null;
                replace = (string)null;
            }
            else
            {
                this.memory.OpenProcess("HD-Player");
                int i2 = 22000000;
                IEnumerable<long> wl = await this.memory.AoBScan(search, true, true, "");
                string u = "0x" + wl.FirstOrDefault<long>().ToString("X");
                if (wl.Count<long>() != 0)
                {
                    for (int i = 0; i < wl.Count<long>(); ++i)
                    {
                        ++i2;
                        this.memory.WriteMemory(wl.ElementAt<long>(i).ToString("X"), "bytes", replace, "", (Encoding)null, true);
                    }
                    k = true;
                }
                if (k)
                {
                 
                    Console.Beep(600, 300);
                }
           
                  
                wl = (IEnumerable<long>)null;
                u = (string)null;
                search = (string)null;
                replace = (string)null;
            }
        }

    }
}
