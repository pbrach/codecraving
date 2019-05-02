using System;
using System.Threading;
using System.Threading.Tasks;
using MainApp.ShelfDomain;
using MediatR;
#pragma warning disable 1998

namespace MainApp.Requests
{
    public class PushIntoShelfCommand: IRequest<Unit>
    {
        public PushIntoShelfCommand()
        {
            Package = Package.Null;
            Compartment = 0;
        }
        public Package Package { get; set; }
        public int Compartment { get; set; }
    }
    
    public class PushIntoShelfHandler: IRequestHandler<PushIntoShelfCommand, Unit>
    {
        private readonly Shelf _shelf;

        public PushIntoShelfHandler(Shelf shelf)
        {
            _shelf = shelf;
        }
        public async Task<Unit> Handle(PushIntoShelfCommand request, CancellationToken cancellationToken)
        {
            Console.WriteLine("Handling Request with Package: " + request.Package.Name);
            _shelf.PushInto(request.Package, request.Compartment);
            return Unit.Value;
        }
    }
}