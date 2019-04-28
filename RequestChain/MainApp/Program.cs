using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using MainApp.Requests;
using MainApp.ShelfDomain;
using MediatR;

namespace MainApp
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

            builder.RegisterAssemblyTypes(Assembly.Load(nameof(MainApp))).Where(type => type.IsInNamespace(nameof(Requests))).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(Assembly.Load(nameof(MediatR))).AsImplementedInterfaces();
            

            // It appears Autofac returns the last registered types first
//            builder.RegisterGeneric(typeof(RequestPostProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
//            builder.RegisterGeneric(typeof(RequestPreProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
//            builder.RegisterGeneric(typeof(GenericRequestPreProcessor<>)).As(typeof(IRequestPreProcessor<>));
//            builder.RegisterGeneric(typeof(GenericRequestPostProcessor<,>)).As(typeof(IRequestPostProcessor<,>));
//            builder.RegisterGeneric(typeof(GenericPipelineBehavior<,>)).As(typeof(IPipelineBehavior<,>));
//            builder.RegisterGeneric(typeof(ConstrainedRequestPostProcessor<,>)).As(typeof(IRequestPostProcessor<,>));
//            builder.RegisterGeneric(typeof(ConstrainedPingedHandler<>)).As(typeof(INotificationHandler<>));

            // resolve IComponentContext again, because building IComponentContext can't be used after the container is built
            builder.Register<ServiceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            } ); 

            var container = builder.Build();


            var mediator = container.Resolve<IMediator>();

            return mediator;
        }
    }
}