using BeemoTv3.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BeemoTv3.Services
{
    public static class MemoryService
    {
        [DllImport("kernel32.dll")]
        private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int dwSize, out int lpNumberOfBytesRead);

        public static Process GetGameProcess()
        {
            foreach (var processName in new[] { "Trickster.bin", "LifeTO", "TO" })
            {
                var processes = Process.GetProcessesByName(processName);
                if (processes.Length > 0)
                    return processes[0];
            }
            throw new Exception("Processo do jogo não encontrado! Verifique se o jogo está rodando.");
        }

        public static int ReadMemoryChainInt(IntPtr processHandle, IntPtr moduleBaseAddress, MemoryPath path)
        {
            // Começa com o endereço base do módulo + offset inicial
            IntPtr pointer = IntPtr.Add(moduleBaseAddress, path.BaseAddress);

            // Lê o primeiro ponteiro
            pointer = ReadPointer(processHandle, pointer);
            if (pointer == IntPtr.Zero) return 0;

            // Percorre a cadeia de offsets
            foreach (var offset in path.Offsets)
            {
                if (pointer == IntPtr.Zero) break;

                // Se não for o último offset, lê o próximo ponteiro
                if (offset != path.Offsets[^1])
                {
                    pointer = IntPtr.Add(pointer, offset);
                    pointer = ReadPointer(processHandle, pointer);
                }
                else
                {
                    // No último offset, apenas soma sem ler
                    pointer = IntPtr.Add(pointer, offset);
                }
            }

            // Lê o valor final
            if (pointer != IntPtr.Zero)
            {
                byte[] buffer = new byte[4];
                if (ReadProcessMemory(processHandle, pointer, buffer, 4, out int bytesRead) && bytesRead == 4)
                {
                    return BitConverter.ToInt32(buffer, 0);
                }
            }

            return 0;
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
