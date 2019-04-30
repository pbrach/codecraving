using System.Reflection;
using Autofac;
using Components.Implementation;
using Components.Interface;
using Module = Autofac.Module;

namespace Components
{
    public class SkateModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserSkill>().As<IUserSkill>();
            builder.RegisterType<User>().UsingConstructor(typeof(string)).As<IUser>();


            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(x =>
                    x.IsAssignableTo<ISkillType>()     ||
                    x.IsAssignableTo<ISkillGroup>())
                .SingleInstance()
                .AsImplementedInterfaces();
        }
    }
}