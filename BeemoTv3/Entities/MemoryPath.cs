using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeemoTv3.Entities
{
    public class MemoryPath
    {
        public string Name { get; set; }
        public int BaseAddress { get; set; }
        public int[] Offsets { get; set; }
    }
}
