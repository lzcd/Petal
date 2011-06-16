using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Petal;

namespace Tests
{
    [TestClass]
    public class PetalTests
    {
        [TestMethod]
        public void ShouldLoadAndStore()
        {
            var memory = new byte[byte.MaxValue];
            memory[0] = 4;
            memory[4] = (byte)Instruction.LoadA;
            memory[5] = 64;
            memory[6] = (byte)Instruction.StoreATo;
            memory[7] = 56;
            memory[8] = (byte)Instruction.LoadBFrom;
            memory[9] = 56;

            Cpu.Execute(ref memory, 0, 1, 2, 3);
            Assert.AreEqual(6, memory[0]);
            Assert.AreEqual(64, memory[1]);

            Cpu.Execute(ref memory, 0, 1, 2, 3);
            Assert.AreEqual(8, memory[0]);
            Assert.AreEqual(64, memory[56]);

            Cpu.Execute(ref memory, 0, 1, 2, 3);
            Assert.AreEqual(10, memory[0]);
            Assert.AreEqual(64, memory[2]);
        }
    }
}
