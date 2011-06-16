﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Press;
using Petal;

namespace Tests
{
    [TestClass]
    public class PressTests
    {
        [TestMethod]
        public void ShouldPushAndPop()
        {
            var source = @"
#start 4

#4
loada 200
loadb postpushfirst
:postpushfirst
jumpto push

#100
:push
storeato 150
jumptob
";

            var memory = new byte[byte.MaxValue];
            Assm.Compile(source, ref memory, 0, 1, 2, 3);
        }

        [TestMethod]
        public void ShouldCompileLoadAndStore()
        {
            var source = @"
// my first piece of code
#start 4
#define apple 56

#4
loada 64
storeato apple
jumpto loadgoodness

#100
:loadgoodness
loadbfrom apple
";

            var memory = new byte[byte.MaxValue];
            Assm.Compile(source, ref memory, 0, 1, 2, 3);
            Assert.AreEqual(4, memory[0]);
            Assert.AreEqual((byte)Instruction.LoadA, memory[4]);
            Assert.AreEqual(64, memory[5]);
            Assert.AreEqual((byte)Instruction.StoreATo, memory[6]);
            Assert.AreEqual(56, memory[7]);
            Assert.AreEqual((byte)Instruction.JumpTo, memory[8]);
            Assert.AreEqual(100, memory[9]);

            Assert.AreEqual((byte)Instruction.LoadBFrom, memory[100]);
            Assert.AreEqual(56, memory[101]);
        }
    }
}