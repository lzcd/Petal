using System;
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
// initalise stack with its own address
loada stack
storeato stack

// push first value
loada 200
loadb postpushfirst
jumpto push
:postpushfirst

// push second value
loada 201
loadb postpushsecond
jumpto push
:postpushsecond

// push third value
loada 202
loadb postpushthird
jumpto push
:postpushthird

// pop third value
loadb postpopthirdvalue
jumpto pop
:postpopthirdvalue
storeato 100

// pop second value
loadb postpopsecondvalue
jumpto pop
:postpopsecondvalue
storeato 101

// pop first value
loadb postpopfirstvalue
jumpto pop
:postpopfirstvalue
storeato 102

:end
jumpto end


:push
loadcfrom stack
incrementcby 1
storecto stack
storeatocaddress
jumptob

:pop
loadafrom stack
dereferencea
loadcfrom stack
decrementcby 1
storecto stack
jumptob


:stack
// load its own address in by default
#data stack
";

            var memory = new byte[byte.MaxValue];
            Assm.Compile(source, ref memory, 0, 1, 2, 3);
            for (var i = 0; i < 100; i++)
            {
                Cpu.Execute(ref memory, 0, 1, 2, 3);
            }
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
