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
            }

            memory[instructionPointerAddress] = instructionPointer;
        }

    }
}
