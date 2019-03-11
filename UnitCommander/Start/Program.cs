using System;
using System.Threading;
using Unit.UnitCommands;
using Unit.Units;

namespace Start
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var adder = new NumberAdderUnit();

            var d = (double)9;
            Console.WriteLine($"Number is: {d}");

            adder.UnitCommand = new AddCommand{ToBeAdded = 5};
            Thread.Sleep(800);

            d = (adder.UnitData as AdderData).Number;
            Console.WriteLine($"Number is: {d}");
            
            adder.UnitCommand = new StopUnitCommand();
        }
    }
}