using Autofac;
using Nop.Core.Configuration;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Services.Messages;

namespace ToSic.Nop.Plugins.RazorMessageService
{
	public class DependencyRegistrar : IDependencyRegistrar
	{
		public void Register(ContainerBuilder builder, ITypeFinder typeFinder, NopConfig config)
		{
			builder.RegisterType<RazorWorkflowMessageService>().As<IWorkflowMessageService>().InstancePerLifetimeScope();
		}

		public int Order
		{
			get { return 1; }
		}
	}
}
