using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Petal
{
    public enum Instruction : byte
    {
        Nop,
        LoadA,
        LoadB,
        LoadC,        
        LoadAFrom,
        LoadBFrom,
        LoadCFrom,
        StoreATo,
        StoreBTo,
        StoreCTo,
        JumpTo,
        JumpToA,
        JumpToB,
        JumpToC,
        IncrementABy,
        IncrementBBy,
        IncrementCBy

    }
}
