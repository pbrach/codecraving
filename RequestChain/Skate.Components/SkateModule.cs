using Autofac;
using Components.Implementation;
using Components.Interface;

namespace Components
{
    public class SkateModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<User>().As<IUser>();
        }
    }
}