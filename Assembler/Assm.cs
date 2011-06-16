using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Petal;

namespace Press
{
    public class Assm
    {
        public static void Compile(string source, ref byte[] memory, byte instructionPointerAddress, byte aRegisterAddress, byte bRegisterAddress, byte cRegisterAddress)
        {
            var whitespace = new char[] { ' ', '\t' };
            var variableByName = new Dictionary<string, byte>();
            var referencesByLabel = new Dictionary<string, List<byte>>();
            var addressByLabel = new Dictionary<string, byte>();

            var writeAddress = (byte)0;
            foreach (var line in source.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
            {
                var words = line.Split(whitespace, StringSplitOptions.RemoveEmptyEntries);

                if (words.Length == 0)
                {
                    continue;
                }

                var firstWord = words[0];

                if (firstWord.StartsWith(@"//"))
                {
                    continue;
                }

                if (firstWord.StartsWith("#"))
                {
                    var firstDirective = firstWord.Substring(1);
                    switch (firstDirective.ToLower())
                    {
                        case "start":
                            memory[instructionPointerAddress] = byte.Parse(words[1]);
                            break;
                        case "define":
                            variableByName[words[1]] = byte.Parse(words[2]);
                            break;
                        default:
                            writeAddress = byte.Parse(firstDirective);
                            break;
                    }
                    continue;
                }

                if (firstWord.StartsWith(":"))
                {
                    var label = firstWord.Substring(1);
                    addressByLabel[label] = writeAddress;
                    continue;
                }

                var isFirstWord = true;
                var value = (byte)0;
                foreach (var word in words)
                {
                    if (isFirstWord)
                    {
                        var instruction = (Instruction)Enum.Parse(typeof(Instruction), firstWord, true);
                        value = (byte)instruction;
                        isFirstWord = false;
                    }
                    else
                    {
                        if (!byte.TryParse(word, out value))
                        {
                            if (!variableByName.TryGetValue(word, out value))
                            {
                                List<byte> references;
                                if (!referencesByLabel.TryGetValue(word, out references))
                                {
                                    references = new List<byte>();
                                    referencesByLabel[word] = references;
                                }
                                references.Add(writeAddress);
                            }
                        }
                    }
                    memory[writeAddress] = value;
                    writeAddress += 1;
                }

            }


            foreach (var referencesLabelPair in referencesByLabel)
            {
                var label = referencesLabelPair.Key;
                var references = referencesLabelPair.Value;

                var labelAddress = addressByLabel[label];

                foreach (var reference in references)
                {
                    memory[reference] = labelAddress;
                }
            }

        }
    }
}
