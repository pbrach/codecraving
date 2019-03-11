using System;
using System.Threading;
using Unit;
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
            var unitInterface = new UnitInterface(adder);

            var d = (unitInterface.UnitData as AdderData).Number;
            Console.WriteLine($"Number is: {d}");

            unitInterface.UnitCommand = new AddCommand{ToBeAdded = 5};
            Thread.Sleep(800);

            d = (unitInterface.UnitData as AdderData).Number;
            Console.WriteLine($"Number is: {d}");
            
            unitInterface.UnitCommand = new StopUnitCommand();
        }
    }
}