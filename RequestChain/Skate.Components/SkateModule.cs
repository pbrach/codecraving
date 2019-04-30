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
            builder.RegisterType<User>().As<IUser>();
            builder.RegisterType<Notifier>().SingleInstance().AsImplementedInterfaces();


            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(x =>
                    x.IsAssignableTo<ISkillType>()     ||
                    x.IsAssignableTo<ISkillGroup>())
                .SingleInstance()
                .AsImplementedInterfaces();
        }
    }
}