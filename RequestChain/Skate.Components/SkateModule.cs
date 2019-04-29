using System.Reflection;
using Autofac;
using Components.Implementation;
using Components.Implementation.SkillTypes;
using Components.Interface;
using Module = Autofac.Module;

namespace Components
{
    public class SkateModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserSkill>().As<IUserSkill>();
            builder.RegisterType<User>().UsingConstructor(typeof(string)).As<IUser>();
            builder.RegisterType<SimpleExample>().As<ISimpleExample>();


//            builder.RegisterType<BackwardStrideSkill>().As<ISkillType>();
//            builder.RegisterType<HeelBrakeSkill>().As<ISkillType>();
//            builder.RegisterType<LeftTurnForwardCrossoverSkill>().As<ISkillType>();
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(x => x.IsInNamespace(typeof(DragStopSkill).Namespace))
                .AsImplementedInterfaces();
        }
        
    }
}