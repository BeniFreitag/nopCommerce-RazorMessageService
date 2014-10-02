using Autofac;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Services.Messages;

namespace ToSic.Nop.Plugins.RazorMessageService
{
	public class DependencyRegistrar : IDependencyRegistrar
	{
		public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder)
		{
			builder.RegisterType<RazorWorkflowMessageService>().As<IWorkflowMessageService>().InstancePerLifetimeScope();
		}

		public int Order
		{
			get { return 1; }
		}
	}
}
