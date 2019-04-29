using System;
using System.Collections.Generic;
using System.Linq;
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

            Run(container.Resolve<Func<string, IUser>>());
        }

        private static IContainer InitContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new SkateModule());
            var container = builder.Build();
            
            return container;
        }

        private static void Run(Func<string, IUser> userFac)
        {
            var user1 = userFac("user1");
            var user2 = userFac("user2");
            var user3 = userFac("user3");
            
            user1.AddFriend(user2);
            user1.AddFriend(user3);

            var friendNames = user1.Friends.Select(x => x.Name);
            Console.WriteLine($"{user1.Name} has friends: {string.Join(", ", friendNames)}");
        }
    }
}