namespace MainApp.ShelfDomain
{
    public class Package
    {
        public int Id { get; }

        public string Name { get; }

        private static int _numberOfPackages = 0; // not thead safe!
        
        public Package(string name)
        {
            Name = name;
            Id = ++_numberOfPackages;
        }
    }
}