using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using MainApp.Requests;
using MainApp.ShelfDomain;
using MediatR;
using MediatR.Pipeline;

// ReSharper disable UnusedMember.Local
// ReSharper disable UnusedParameter.Global

namespace MainApp
{
    internal static class Program
    {
        private static IContainer Container { get; set; }

        public static async Task Main(string[] args)
        {
            var mediator = BuildMediator();

            var pushCmd = new PushIntoShelfCommand
            {
                Package = new Package("Boots"),
                Compartment = 0
            };

            await mediator.Send(pushCmd, CancellationToken.None); // command 
            var shelf = await mediator.Send(new GetShelfStatusCommand(), CancellationToken.None); // query 
            Console.WriteLine(shelf.ToString());
            
            pushCmd = new PushIntoShelfCommand
            {
                Package = new Package("Carrots"),
                Compartment = 0
            };
            await mediator.Send(pushCmd, CancellationToken.None);  // command
            shelf = await mediator.Send(new GetShelfStatusCommand(), CancellationToken.None); // query 
            Console.WriteLine(shelf.ToString());
        }


        private static IMediator BuildMediator()
        {
            var builder = new ContainerBuilder();
            // My/Test global State
            builder.Register(_ => new Shelf(3)).SingleInstance().As<Shelf>();

            // Request Handler
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.IsClosedTypeOf(typeof(IRequestHandler<,>)))
                .AsImplementedInterfaces();
                    
            // Requests
            builder.RegisterAssemblyTypes(Assembly.Load(nameof(MainApp)))
                .Where(type => type.IsInNamespace(nameof(Requests)))
                .AsImplementedInterfaces();
            
            // MediatR
            builder.RegisterAssemblyTypes(Assembly.Load(nameof(MediatR))).AsImplementedInterfaces();
            builder.Register<ServiceFactory>(ctx =>
            {
                // resolve IComponentContext again, because building IComponentContext can't be used after the container is built
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            }); 

            // It appears Autofac returns the last registered types first
            builder.RegisterGeneric(typeof(RequestPostProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(RequestPreProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(GenericRequestPreProcessor<>)).As(typeof(IRequestPreProcessor<>));
            builder.RegisterGeneric(typeof(PostProc1<,>)).As(typeof(IRequestPostProcessor<,>));
            builder.RegisterGeneric(typeof(PostProc2<,>)).As(typeof(IRequestPostProcessor<,>));
            builder.RegisterGeneric(typeof(GenericRequestPostProcessor<,>)).As(typeof(IRequestPostProcessor<,>));
            builder.RegisterGeneric(typeof(GenericRequestPostProcessor<,>)).As(typeof(IRequestPostProcessor<,>));
            builder.RegisterGeneric(typeof(GenericPipelineBehavior<,>)).As(typeof(IPipelineBehavior<,>));

            var writer = new WrappingWriter(Console.Out);
            builder.RegisterInstance(writer).As<TextWriter>();
            
            var container = builder.Build();
            var mediator = container.Resolve<IMediator>();

            return mediator;
        }
    }

    public class PostProc1<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse>
    {
        private readonly TextWriter _writer;

        public PostProc1(TextWriter writer)
        {
            _writer = writer;
        }

        public Task Process(TRequest request, TResponse response)
        {
            return _writer.WriteLineAsync("- PostProc1");
        }
    }
    
    
    public class PostProc2<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse>
    {
        private readonly TextWriter _writer;

        public PostProc2(TextWriter writer)
        {
            _writer = writer;
        }
        public Task Process(TRequest request, TResponse response)
        {
            return _writer.WriteLineAsync("- PostProc2");
        }
    }
    
    public class WrappingWriter : TextWriter
    {
        private readonly TextWriter _innerWriter;
        private readonly StringBuilder _stringWriter = new StringBuilder();

        public WrappingWriter(TextWriter innerWriter)
        {
            _innerWriter = innerWriter;
        }

        public override void Write(char value)
        {
            _stringWriter.Append(value);
            _innerWriter.Write(value);
        }

        public override Task WriteLineAsync(string value)
        {
            _stringWriter.AppendLine(value);
            return _innerWriter.WriteLineAsync(value);
        }

        public override Encoding Encoding => _innerWriter.Encoding;

        public string Contents => _stringWriter.ToString();
    }
    
    public class GenericRequestPreProcessor<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly TextWriter _writer;

        public GenericRequestPreProcessor(TextWriter writer)
        {
            _writer = writer;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            return _writer.WriteLineAsync("- Starting Up");
        }
    }
    
    public class GenericRequestPostProcessor<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse>
    {
        private readonly TextWriter _writer;

        public GenericRequestPostProcessor(TextWriter writer)
        {
            _writer = writer;
        }

        public Task Process(TRequest request, TResponse response, CancellationToken cancellationToken)
        {
            return _writer.WriteLineAsync("- All Done");
        }

        public Task Process(TRequest request, TResponse response)
        {
            return Process(request, response, CancellationToken.None);
        }
    }
    
    
    public class GenericPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly TextWriter _writer;

        public GenericPipelineBehavior(TextWriter writer)
        {
            _writer = writer;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            await _writer.WriteLineAsync("-- Handling Request");
            var response = await next();
            await _writer.WriteLineAsync("-- Finished Request");
            return response;
        }
    }
}
