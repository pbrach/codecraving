﻿using System;
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

            //Lame Test:
            var user1 = container.Resolve<IUser>(
                new TypedParameter(typeof(string), "User1")
            );

            Console.WriteLine(user1.Name);

            // The actual injection
            using (var scope = container.BeginLifetimeScope())
            {
                var innerClass = scope.Resolve<InnerClass>();
                innerClass.Run();
            }
        }

        private static IContainer InitContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new SkateModule());
            builder.RegisterType<InnerClass>().AsSelf();
            var container = builder.Build();

            return container;
        }

        private class InnerClass
        {
            private readonly Func<string, IUser> _userFac;
            private readonly IEnumerable<ISkillType> _skillTypes;
            private readonly IEnumerable<ISkillGroup> _skillGroups;

            public InnerClass(Func<string, IUser> userFac, IEnumerable<ISkillType> skillTypes,
                IEnumerable<ISkillGroup> skillGroups)
            {
                _userFac = userFac;
                _skillTypes = skillTypes;
                _skillGroups = skillGroups;
            }

            public void Run()
            {
                {
                    var user1 = _userFac("user1");
                    var user2 = _userFac("user2");
                    var user3 = _userFac("user3");

                    user1.AddFriend(user2);
                    user1.AddFriend(user3);

                    var friendNames = user1.Friends.Select(x => x.Name);
                    Console.WriteLine($"\n{user1.Name} has friends: {string.Join(", ", friendNames)}");

                    var skillGroupNames = _skillGroups.Select(x => "\n\t- "+x.GetType().Name);
                    Console.WriteLine($"\nthese groups where injected: {string.Join("", skillGroupNames)}");
                    foreach (var skillGroup in _skillGroups)
                    {
                        Console.WriteLine("\nGroup " + skillGroup.GetType().Name + " has skills:");
                        Console.WriteLine(string.Join("\n", skillGroup.Skills.Select(s => "\t\t" + s.Name)));
                    }

                    var skillNames = _skillTypes.Select(x => "\n\t- " + x.Name);
                    Console.WriteLine($"\n\nthese skills where injected: {string.Join("", skillNames)}");
                }
            }
        }
    }
}