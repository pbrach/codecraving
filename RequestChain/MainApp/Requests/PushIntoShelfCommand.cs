using System;
using System.Threading;
using System.Threading.Tasks;
using MainApp.ShelfDomain;
using MediatR;

namespace MainApp.Requests
{
    public class PushIntoShelfCommand: IRequest<Shelf>
    {
        public Package Package { get; set; }
        public int Compartment { get; set; }
    }
    
    public class PushIntoShelfHandler: IRequestHandler<PushIntoShelfCommand, Shelf>
    {
        private readonly Shelf _shelf;

        public PushIntoShelfHandler(Shelf shelf)
        {
            _shelf = shelf;
        }
#pragma warning disable 1998
        public async Task<Shelf> Handle(PushIntoShelfCommand request, CancellationToken cancellationToken)
#pragma warning restore 1998
        {
            Console.WriteLine("Handling Request with Package: " + request.Package.Name);
            _shelf.PushInto(request.Package, request.Compartment);
            return _shelf;
        }
    }
}