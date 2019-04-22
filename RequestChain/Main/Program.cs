using System;
using System.IO;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Main.Requests;
using Main.ShelfDomain;
using MediatR;
using MediatR.Pipeline;

namespace Main
{
    class Program
    {
        public static IContainer Container { get; set; }

        public static async Task Main(string[] args)
        {
            var mediator = BuildMediator();

            var pushCmd = new PushIntoShelfCommand
            {
                Package = new Package("Boots"),
                Compartment = 0
            };

            var shelf = await mediator.Send(pushCmd, CancellationToken.None);   
            
            pushCmd = new PushIntoShelfCommand
            {
                Package = new Package("Carrots"),
                Compartment = 0
            };
            shelf = await mediator.Send(pushCmd, CancellationToken.None);  
        }


        private static IMediator BuildMediator()
        {
            var builder = new ContainerBuilder();
            builder.Register(_ => new Shelf(3)).SingleInstance().As<Shelf>();
            builder.RegisterType<PushIntoShelfHandler>().As<IRequestHandler<PushIntoShelfCommand, Shelf>>();

            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();


            var mediatrOpenTypes = new[]
            {
                typeof(IRequestHandler<,>),
            };

            foreach (var mediatrOpenType in mediatrOpenTypes)
            {
                builder
                    .RegisterAssemblyTypes(typeof(Shelf).GetTypeInfo().Assembly)
                    .AsClosedTypesOf(mediatrOpenType)
                    .AsImplementedInterfaces();
            }

            // It appears Autofac returns the last registered types first
//            builder.RegisterGeneric(typeof(RequestPostProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
//            builder.RegisterGeneric(typeof(RequestPreProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
//            builder.RegisterGeneric(typeof(GenericRequestPreProcessor<>)).As(typeof(IRequestPreProcessor<>));
//            builder.RegisterGeneric(typeof(GenericRequestPostProcessor<,>)).As(typeof(IRequestPostProcessor<,>));
//            builder.RegisterGeneric(typeof(GenericPipelineBehavior<,>)).As(typeof(IPipelineBehavior<,>));
//            builder.RegisterGeneric(typeof(ConstrainedRequestPostProcessor<,>)).As(typeof(IRequestPostProcessor<,>));
//            builder.RegisterGeneric(typeof(ConstrainedPingedHandler<>)).As(typeof(INotificationHandler<>));

            builder.Register<ServiceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });

            var container = builder.Build();

            // The below returns:
            //  - RequestPreProcessorBehavior
            //  - RequestPostProcessorBehavior
            //  - GenericPipelineBehavior

            //var behaviors = container
            //    .Resolve<IEnumerable<IPipelineBehavior<Ping, Pong>>>()
            //    .ToList();

            var mediator = container.Resolve<IMediator>();

            return mediator;
        }
    }
}