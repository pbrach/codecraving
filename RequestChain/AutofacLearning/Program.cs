using System;
using System.Collections.Generic;
using Autofac;
using Components;
using Components.Interface;
using IContainer = Autofac.IContainer;
// ReSharper disable ClassNeverInstantiated.Global

namespace AutofacLearning
{
    internal class Program
    {
        private static void Main()
        {
            var container = InitContainer();
            var skills = container.Resolve < IEnumerable<ISkillType>>();
            var groups = container.Resolve < IEnumerable<ISkillGroup>>();
            var user1 = container.Resolve<IUser>(
                new TypedParameter(typeof(string), "User1")
                );
            
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