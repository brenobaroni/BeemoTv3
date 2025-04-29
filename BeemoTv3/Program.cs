using BeemoTv3.Entities;
using BeemoTv3.Services;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace BeemoTv3
{
    class Program
    {
        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        

        async static Task Main()
        {
            var process = MemoryService.GetGameProcess();
            var processHandle = OpenProcess(BasePath.PROCESS_VM_READ, false, process.Id);

            if (processHandle == IntPtr.Zero)
            {
                throw new Exception("Não foi possível abrir o processo. Verifique se você tem permissões suficientes.");
            }
    
            try
            {
                
                while(true)
                {
                    Console.WriteLine("BeemoTv3 - Bot para Trickster Online");
                    BasicAttributesService.GetBasicAttributes(processHandle, process.MainModule.BaseAddress);
                    await Task.Delay(100);
                    Console.Clear();
                }
            }
            catch (Exception ex)
            {
                if (processHandle != IntPtr.Zero)
                {
                    Console.WriteLine("Jogo Fechado, Fechando o processo...");
                }
            }
        }

        

        

        
    }
}
