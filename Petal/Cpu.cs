using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Petal
{
    public static class Cpu
    {
        public static void Execute(ref byte[] memory, byte instructionPointerAddress, byte aRegisterAddress, byte bRegisterAddress, byte cRegisterAddress)
        {
            var instructionPointer = memory[instructionPointerAddress];
            var instruction = (Instruction)memory[instructionPointer];

            switch (instruction)
            {
                case Instruction.LoadA:
                    memory[aRegisterAddress] = memory[instructionPointer + 1];
                    instructionPointer += 2;
                    break;
                case Instruction.LoadB:
                    memory[bRegisterAddress] = memory[instructionPointer + 1];
                    instructionPointer += 2;
                    break;
                case Instruction.LoadC:
                    memory[cRegisterAddress] = memory[instructionPointer + 1];
                    instructionPointer += 2;
                    break;
                case Instruction.LoadAFrom:
                    var loadASourceAddress = memory[instructionPointer + 1];
                    memory[aRegisterAddress] = memory[loadASourceAddress];
                    instructionPointer += 2;
                    break;
                case Instruction.LoadBFrom:
                    var loadBSourceAddress = memory[instructionPointer + 1];
                    memory[bRegisterAddress] = memory[loadBSourceAddress];
                    instructionPointer += 2;
                    break;
                case Instruction.LoadCFrom:
                    var loadCSourceAddress = memory[instructionPointer + 1];
                    memory[cRegisterAddress] = memory[loadCSourceAddress];
                    instructionPointer += 2;
                    break;
                case Instruction.DereferenceA:
                    var aReference = memory[aRegisterAddress];
                    memory[aRegisterAddress] = memory[aReference];
                    instructionPointer += 1;
                    break;
                case Instruction.DereferenceB:
                    var bReference = memory[bRegisterAddress];
                    memory[bRegisterAddress] = memory[bReference];
                    instructionPointer += 1;
                    break;
                case Instruction.DereferenceC:
                    var cReference = memory[cRegisterAddress];
                    memory[cRegisterAddress] = memory[cReference];
                    instructionPointer += 1;
                    break;
                case Instruction.StoreATo:
                    var storeATargetAddress = memory[instructionPointer + 1];
                    memory[storeATargetAddress] = memory[aRegisterAddress];
                    instructionPointer += 2;
                    break;
                case Instruction.StoreBTo:
                    var storeBTargetAddress = memory[instructionPointer + 1];
                    memory[storeBTargetAddress] = memory[bRegisterAddress];
                    instructionPointer += 2;
                    break;
                case Instruction.StoreCTo:
                    var storeCTargetAddress = memory[instructionPointer + 1];
                    memory[storeCTargetAddress] = memory[cRegisterAddress];
                    instructionPointer += 2;
                    break;
                case Instruction.StoreAToBAddress:
                    var storeAToBTargetAddress = memory[bRegisterAddress];
                    memory[storeAToBTargetAddress] = memory[aRegisterAddress];
                    instructionPointer += 1;
                    break;
                case Instruction.StoreAToCAddress:
                    var storeAToCTargetAddress = memory[cRegisterAddress];
                    memory[storeAToCTargetAddress] = memory[aRegisterAddress];
                    instructionPointer += 1;
                    break;
                case Instruction.StoreBToAAddress:
                    var storeBToATargetAddress = memory[aRegisterAddress];
                    memory[storeBToATargetAddress] = memory[bRegisterAddress];
                    instructionPointer += 1;
                    break;
                case Instruction.StoreBToCAddress:
                    var storeBToCTargetAddress = memory[cRegisterAddress];
                    memory[storeBToCTargetAddress] = memory[bRegisterAddress];
                    instructionPointer += 1;
                    break;
                case Instruction.StoreCToAAddress:
                    var storeCToATargetAddress = memory[aRegisterAddress];
                    memory[storeCToATargetAddress] = memory[cRegisterAddress];
                    instructionPointer += 1;
                    break;
                case Instruction.StoreCToBAddress:
                    var storeCToBTargetAddress = memory[bRegisterAddress];
                    memory[storeCToBTargetAddress] = memory[cRegisterAddress];
                    instructionPointer += 1;
                    break;
                case Instruction.JumpTo:
                    var jumpToAddress = memory[instructionPointer + 1];
                    instructionPointer = jumpToAddress;
                    break;
                case Instruction.JumpToA:
                    instructionPointer = memory[aRegisterAddress];
                    break;
                case Instruction.JumpToB:
                    instructionPointer = memory[bRegisterAddress];
                    break;
                case Instruction.JumpToC:
                    instructionPointer = memory[cRegisterAddress];
                    break;
                case Instruction.IncrementABy:
                    var aIncrement = memory[instructionPointer +1];
                    var incrementedA = ((int)memory[aRegisterAddress] + aIncrement) % 256;
                    memory[aRegisterAddress] = (byte)incrementedA;
                    instructionPointer += 2;
                    break;
                case Instruction.IncrementBBy:
                    var bIncrement = memory[instructionPointer + 1];
                    var incrementedB = ((int)memory[bRegisterAddress] + bIncrement) % 256;
                    memory[bRegisterAddress] = (byte)incrementedB;
                    instructionPointer += 2;
                    break;
                case Instruction.IncrementCBy:
                    var cIncrement = memory[instructionPointer + 1];
                    var incrementedC = ((int)memory[cRegisterAddress] + cIncrement) % 256;
                    memory[cRegisterAddress] = (byte)incrementedC;
                    instructionPointer += 2;
                    break;
                case Instruction.DecrementABy:
                    var aDecrement = memory[instructionPointer + 1];
                    var decrementedA = ((int)memory[aRegisterAddress] + aDecrement) % 256;
                    memory[aRegisterAddress] = (byte)decrementedA;
                    instructionPointer += 2;
                    break;
                case Instruction.DecrementBBy:
                    var bDecrement = memory[instructionPointer + 1];
                    var decrementedB = ((int)memory[bRegisterAddress] + bDecrement) % 256;
                    memory[bRegisterAddress] = (byte)decrementedB;
                    instructionPointer += 2;
                    break;
                case Instruction.DecrementCBy:
                    var cDecrement = memory[instructionPointer + 1];
                    var decrementedC = ((int)memory[cRegisterAddress] + cDecrement) % 256;
                    memory[cRegisterAddress] = (byte)decrementedC;
                    instructionPointer += 2;
                    break;
            }

            memory[instructionPointerAddress] = instructionPointer;
        }

    }
}
