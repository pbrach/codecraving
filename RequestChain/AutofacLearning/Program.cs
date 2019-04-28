using System;
using Autofac;
using Components;
using Components.Interface;
using IContainer = Autofac.IContainer;

namespace AutofacLearning
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = InitContainer();
            var user1 = container.Resolve<IUser>();
            
            Console.WriteLine(user1.Name);
        }

        private static IContainer InitContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new SkateModule());
            var container = builder.Build();
            
            return container;
        }
    }
}