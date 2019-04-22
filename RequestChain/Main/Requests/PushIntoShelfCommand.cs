using System.Threading;
using System.Threading.Tasks;
using Main.ShelfDomain;
using MediatR;

namespace Main.Requests
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
        public async Task<Shelf> Handle(PushIntoShelfCommand request, CancellationToken cancellationToken)
        {
            _shelf.PushInto(request.Package, request.Compartment);
            return _shelf;
        }
    }
}