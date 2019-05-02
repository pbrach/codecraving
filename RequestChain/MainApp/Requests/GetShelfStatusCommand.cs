using System.Threading;
using System.Threading.Tasks;
using MainApp.ShelfDomain;
using MediatR;
#pragma warning disable 1998

namespace MainApp.Requests
{
    public class GetShelfStatusCommand: IRequest<Shelf>
    {
        
    }
    
    public class GetShelfStatusHandler: IRequestHandler<GetShelfStatusCommand, Shelf>
    {
        private readonly Shelf _shelf;

        public GetShelfStatusHandler(Shelf shelf)
        {
            _shelf = shelf;
        }
        public async Task<Shelf> Handle(GetShelfStatusCommand request, CancellationToken cancellationToken)
        {
            return _shelf;
        }
    }
}