using System.Collections.Generic;
using System.Linq;

namespace MainApp.ShelfDomain
{
    public class Shelf
    {
        private List<Package> Compartments { get;}

        public Shelf(int numberOfCompartments)
        {
            Compartments = Enumerable.Range(0, numberOfCompartments).Select<int, Package>(_ => null).ToList();
        }

        public void PushInto(Package package, int compartmentNumber)
        {
            var currentContent = Compartments[compartmentNumber];
            if (currentContent != null)
            {
                // push over
            }

            Compartments[compartmentNumber] = package;
        }
    }
}