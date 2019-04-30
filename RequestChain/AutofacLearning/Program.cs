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

            LameResolveParameterExample(container);

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
        
        
        private static void LameResolveParameterExample(IContainer container)
        {
            var user1 = container.Resolve<IUser>(
                new TypedParameter(typeof(string), "User1")
            );

            Console.WriteLine(user1.Name);
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
                    var user1 = CreateUser();
                    PrintFriendNames(user1);

                    PrintSkillGroups(_skillGroups);

                    PrintSkillTypes(_skillTypes);

                    LearnSkills(user1);
                }
            }

            private void LearnSkills(IUser user1)
            {
                var backwardStrideSkill = _skillTypes.First(type => type.Name == "Backward stride");
                user1.LearnSkill(backwardStrideSkill);
                user1.LearnSkill(backwardStrideSkill);
                user1.LearnSkill(backwardStrideSkill);
                user1.LearnSkill(backwardStrideSkill);
                
                // Higher than professional: NOT POSSIBLE!
                user1.LearnSkill(backwardStrideSkill);
            }

            private IUser CreateUser()
            {
                var user1 = _userFac("user1");
                var user2 = _userFac("user2");
                var user3 = _userFac("user3");

                user1.AddFriend(user2);
                user1.AddFriend(user3);

                return user1;
            }

            private static void PrintSkillGroups(IEnumerable<ISkillGroup> skillGroups)
            {
                var skillGroupNames = skillGroups.Select(x => "\n\t- " + x.GetType().Name);
                Console.WriteLine($"\nthese groups where injected: {string.Join("", skillGroupNames)}");
                
                foreach (var skillGroup in skillGroups)
                {
                    Console.WriteLine("\nGroup " + skillGroup.GetType().Name + " has skills:");
                    Console.WriteLine(string.Join("\n", skillGroup.Skills.Select(s => "\t\t- " + s.Name)));
                }
            }
            
            private static void PrintSkillTypes(IEnumerable<ISkillType> skillTypes)
            {
                var skillNames = skillTypes.Select(x => "\n\t- " + x.Name);
                Console.WriteLine($"\n\nthese skills where injected: {string.Join("", skillNames)}");
            }

            private static void PrintFriendNames(IUser user1)
            {
                var friendNames = user1.Friends.Select(x => x.Name);
                Console.WriteLine($"\n{user1.Name} has friends: {string.Join(", ", friendNames)}");
            }            
        }
    }
}