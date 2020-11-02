using System;

namespace _0Assignment1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine("This is a Celluar Automaton programmed in C#");
            BitCA myCA = new BitCA();
            myCA.RunProcess();
        }
    }
}
