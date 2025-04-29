using System;
using System.Diagnostics;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace BeemoTv3
{
    class Program
    {
        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int dwSize, out int lpNumberOfBytesRead);

        const int PROCESS_VM_READ = 0x0010;

        async static Task Main()
        {
            Console.WriteLine("BeemoTv3 - Bot para Trickster Online");

            var processes = Process.GetProcessesByName("Trickster.bin");
            if (processes.Length == 0)
            {
                processes = Process.GetProcessesByName("LifeTO");
                if (processes.Length == 0)
                {
                    processes = Process.GetProcessesByName("TO");
                }
            }

            if (processes.Length == 0)
            {
                throw new Exception("Processo do jogo não encontrado! Verifique se o jogo está rodando.");
            }

            IntPtr processHandle = OpenProcess(PROCESS_VM_READ, false, processes[0].Id);
            if (processHandle == IntPtr.Zero)
            {
                throw new Exception("Não foi possível abrir o processo. Verifique se você tem permissões suficientes.");
            }

            try
            {
                while (true)
                {
                    IntPtr baseAddress = processes[0].MainModule.BaseAddress;

                    // Usando o endereço base 007E734C
                    IntPtr pointer = IntPtr.Add(baseAddress, 0x007E734C);

                    // Primeira leitura para obter o valor do ponteiro
                    pointer = ReadPointer(processHandle, pointer);

                    // Adiciona offset D4
                    pointer = IntPtr.Add(pointer, 0xD4);

                    // Segunda leitura do ponteiro
                    pointer = ReadPointer(processHandle, pointer);

                    // Adiciona offset 1C8
                    pointer = IntPtr.Add(pointer, 0x1C8);

                    // Lê valor final
                    byte[] buffer = new byte[4];
                    if (ReadProcessMemory(processHandle, pointer, buffer, 4, out int bytesRead) && bytesRead == 4)
                    {
                        int value = BitConverter.ToInt32(buffer, 0);
                        Console.WriteLine($"Valor final: {value}");
                    }
                    else
                    {
                        Console.WriteLine("Falha ao ler memória");
                    }

                    await Task.Delay(2000);
                }
            }
            finally
            {
                // Fecha o handle do processo
                if (processHandle != IntPtr.Zero)
                {
                    // Aqui você precisaria adicionar o CloseHandle se necessário
                }
            }
        }

        static IntPtr ReadPointer(IntPtr processHandle, IntPtr address)
        {
            byte[] buffer = new byte[4];
            if (ReadProcessMemory(processHandle, address, buffer, 4, out int bytesRead) && bytesRead == 4)
            {
                return (IntPtr)BitConverter.ToInt32(buffer, 0);
            }
            return IntPtr.Zero;
        }
    }
}
