using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace _0Assignment1
{
    class BitCA
    {
        // Variables
        private uint current; // Current step
        private byte rule; // Rule followed by the automaton
        private byte nbSteps; // Number of steps
        private byte typeInit; // Initialized type: 0 -> Random, and 1 -> Single non-zero entry in the middle
        private uint[] ruleArray; 

        NumberToArr decToBin = new NumberToArr();

        // Constructor
        public BitCA()
        {
            current = 0;
            rule = 1; // At least 00000001 not have only zeros
            nbSteps = 1; // At least one step
            typeInit = 0; // Initialize type to Random
        }

        // Setter methods for rule, nbSteps, and typeInit
        private byte SetRule()
        {
            Console.WriteLine("Please enter the rule (integer between 0 and 255):");
            string input = Console.ReadLine();
            byte byte_input;

            try
            {
                byte_input = Convert.ToByte(input);
                if (byte_input > 255 || byte_input < 0)
                {
                    Console.WriteLine("Please enter a integer between 0 and 255");
                    return 0;
                }
                return byte_input;
            }
            catch (System.ArgumentNullException)
            {
                Console.WriteLine("String is null.");
            }
            catch (System.FormatException)
            {
                Console.WriteLine("String does not consist of an " +
                                "byte type. Please enter an INTEGER");
            }
            catch (System.OverflowException)
            {
                Console.WriteLine(
                "Overflow in string to byte conversion.");
            }
            return 0;
        }

        private byte SetNbSteps()
        {
            Console.WriteLine("Please enter the number of steps (integer between 0 and 200):");
            string input = Console.ReadLine();
            byte byte_input;

            try
            {
                byte_input = Convert.ToByte(input);
                if (byte_input > 200 || byte_input < 0)
                {
                    Console.WriteLine("Please enter a integer between 0 and 200");
                    return 0;
                }
                return byte_input;
            }
            catch (System.ArgumentNullException)
            {
                Console.WriteLine("String is null.");                
            }
            catch (System.FormatException)
            {
                Console.WriteLine("String does not consist of an " +
                                "byte type. Please enter an INTEGER"); 
            }
            catch (System.OverflowException)
            {
                Console.WriteLine(
                "Overflow in string to byte conversion.");
            }
            return 0;
        }

        private byte SetType()
        {
            Console.WriteLine("Please enter the type of initialization (0 for Random or 1 for Single non-zero entry in the middle):");
            string input = Console.ReadLine();
            byte byte_input;

            try
            {
                byte_input = Convert.ToByte(input);
                if ((byte_input != 0 && byte_input != 1))
                {
                    Console.WriteLine("Please enter either 0 (Random) or 1 (Single non-zero entry in the middle)");
                    return 0;
                }
                return byte_input;
            }
            catch (System.ArgumentNullException)
            {
                Console.WriteLine("String is null.");
            }
            catch (System.FormatException)
            {
                Console.WriteLine("String does not consist of an " +
                                "byte type. Please enter an INTEGER");
            }
            catch (System.OverflowException)
            {
                Console.WriteLine(
                "Overflow in string to byte conversion.");
            }
            return 0;
        }
        
        // Initialize process with user's entries
        private void InitProcess()
        {
            rule = SetRule();
            ruleArray = decToBin.NumberToUintArr(rule, 8);
            nbSteps = SetNbSteps();
            typeInit = SetType();            
            Console.WriteLine("rule {0}, nbSteps {1}, typeInit {2}", rule, nbSteps, typeInit);            
        }
        
        // Display the chosen rule
        private void DisplayRule(byte rule)
        {
            Console.WriteLine("Chosen rule:");
            char[] valuesArray;
            int i = 0;
            for (i = 0; i < ruleArray.Length; i++)
            {
                valuesArray = decToBin.NumberToCharArr((byte)(i), 3);
                Console.WriteLine("({0}, {1}, {2}) -> {3}", 
                    valuesArray[2], valuesArray[1], valuesArray[0], ruleArray[i]);
            }
        }

        // Initialisation
        /* "1" -> put 1 at place 16 (from right)
         * "0" -> random UINT */
        private void HandleInitEntry()
        {
            Console.WriteLine("The initialization is:");
            uint output;
            if (typeInit == 0)
            {
                Random rand = new Random();
                /* Generate random uint by generating two random ints on 16
                 * then combining them */
                uint half1 = (uint)rand.Next(1 << 16);
                uint half2 = (uint)rand.Next(1 << 16);
                output = (half1 << 16) | half2;
                Console.WriteLine("Decimal: {0}", output);
            }
            else
            {
                output = 1 << 16;
                Console.WriteLine("Decimal: {0}", output);
            }
            uint[] initArray_uint = decToBin.NumberToUintArr(output, 32);
            int i = 0;
            for (i = initArray_uint.Length - 1; i >= 0; i--)
            {
                Console.Write(initArray_uint[i]);
            }
            current = output;
            Console.Write("\n");
        }

        // Cellular automaton system
        // Output Current Step
        private void OutputCurrentStep(uint value)
        {
            current = value;
            int i = 0;
            uint mask = 1, res = 0;
            mask = mask << 31;
            for (i = 0; i < 32; i++)
            {
                res = current & mask;
                if (res == 0)
                {
                    Console.Write('0');
                }
                else
                {
                    Console.Write('1');
                }
                mask = mask >> 1;
            }
            Console.Write("\n");
        }

        // Circular Right Shift
        private uint CircularRightShift(uint value)
        {
            uint mask = 1;
            uint res = 0;
            bool firstBit = false;
            res = mask & value;
            if (res == 0)
            {
                firstBit = false;
            }
            else
            {
                firstBit = true;
            }
            res = value >> 1;
            // Set the bit 31 to 0 or 1
            mask = 1;
            mask = mask << 31;
            if (firstBit == true)
            {
                res = res ^ mask;
            }
            return res;
        }

        // Circular Left Shift
        private uint CircularLeftShift(uint value)
        {
            uint mask = 1;
            uint res = 0;
            bool firstBit = false;
            res = mask & value;
            if (res == 0)
            {
                firstBit = false;
            }
            else
            {
                firstBit = true;
            }
            res = value << 1;
            // Set the bit 31 to 0 or 1
            mask = 1;
            mask = mask << 31;
            if (firstBit == true)
            {
                res = res ^ mask;
            }
            return res;
        }

        // Creates next step and update current step
        private void CreateNextStep()
        {
            uint prev = current;
            uint mask = 7;
            uint res = 0;
            prev = CircularLeftShift(prev);
            for(int i = 0; i < 32; i++)
            {
                res += ruleArray[prev & mask] * (uint) Math.Pow(2, i);
                prev = CircularRightShift(prev);
            }
            current = res;
        }

        // Run programme
        public void RunProcess()
        {
            InitProcess();
            DisplayRule(rule);
            HandleInitEntry();
            for(int i = 0; i < nbSteps; i++)
            {
                CreateNextStep();
                OutputCurrentStep(current);
            }
        }
    }
}
