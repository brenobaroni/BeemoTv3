using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BeemoTv3.Services
{
    public static class BasicAttributesService
    {
        [DllImport("kernel32.dll")]
        private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int dwSize, out int lpNumberOfBytesRead);


        public static void GetBasicAttributes(IntPtr processHandle, IntPtr baseAddress)
        {
            byte[] buffer = new byte[4];
            int bytesRead;

            var HP = MemoryService.ReadMemoryChainInt(processHandle, baseAddress, BasePath._HP);
            var MANA = MemoryService.ReadMemoryChainInt(processHandle, baseAddress, BasePath._MANA);
            var LVL = MemoryService.ReadMemoryChainInt(processHandle, baseAddress, BasePath._LVL);
            var XP = "0.1%"; //MemoryService.ReadMemoryChainInt(processHandle, baseAddress, BasePath._XP);

            Console.SetCursorPosition(0, 0); // Volta ao topo

            // Limpa as duas linhas do HUD
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, 0);
            // Linha do nível e XP
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("[LVL]: ");
            Console.Write($"{LVL} ");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("| [XP]: ");
            Console.WriteLine($"{XP}");
            Console.ResetColor();

            // Linha do HP e MP
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[HP]: ");
            Console.Write($"{HP} ");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("| [MP]: ");
            Console.WriteLine($"{MANA}");
            Console.ResetColor();

        }
    }
}
