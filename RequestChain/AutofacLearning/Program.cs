using System;
using System.Collections.Generic;
using Autofac;
using Components;
using Components.Implementation;
using Components.Interface;
using IContainer = Autofac.IContainer;

namespace AutofacLearning
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = InitContainer();
            var skills = container.Resolve < IEnumerable<ISkillType>>();
            var user1 = container.Resolve<ISimpleExample>(
                new TypedParameter(typeof(string), "sectionName")
//                new NamedParameter("skills", new List<IUserSkill>()),
//                new NamedParameter("friends", new List<IUser>())
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