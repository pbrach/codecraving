using System.Collections.Generic;
using System.Linq;

namespace MainApp.ShelfDomain
{
    public class Shelf
    {
        private List<Package> Compartments { get; }

        public Shelf(int numberOfCompartments)
        {
            Compartments = Enumerable.Range(0, numberOfCompartments).Select<int, Package>(_ => Package.Null).ToList();
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

        public override string ToString()
        {
            var statusString = $"This shelf has {Compartments.Count} compartments.\n";
            foreach (var (compartment, idx) in Compartments.Select((c, i) => (c, i)))
            {
                var content = compartment == null? "nothing" : compartment.Name;
                statusString += $"compartment {idx} has {content}\n";
            }

            return statusString;
        }
    }
}