using BeemoTv3.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeemoTv3
{
    public static class BasePath
    {
        public const int PROCESS_VM_READ = 0x0010;


        public static MemoryPath _HP = new MemoryPath
        {
            Name = "HP",
            BaseAddress = 0x009cdf18,
            Offsets = new[] { 0x10, 0x1C4 }
        };

        public static MemoryPath _MANA = new MemoryPath
        {
            Name = "Mana",
            BaseAddress = 0x007E734C,
            Offsets = new[] { 0xD4, 0x1C8 }
        };

        public static MemoryPath _LVL = new MemoryPath
        {
            Name = "Level",
            BaseAddress = 0x009c3938,
            Offsets = new[] { 0x28 }
        };
    }
}
