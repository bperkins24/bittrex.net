using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using ApocoCrypto.CoinGuard.Wpf.Models;
using Caliburn.Micro;
using ApocoCrypto.CoinGuard.Wpf.ViewModels;
using ApocoCrypto.MarketData.Bittrex.Api;
using ApocoCrypto.MarketData.Bittrex.Wire;
using AutoMapper;
using AutoMapper.EquivalencyExpression;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace ApocoCrypto.CoinGuard.Wpf.Bootstrap
{
    public class AppBootstrapper : BootstrapperBase
	{
		WindsorContainer _container;

	    public AppBootstrapper()
	    {
	        Initialize();
	    }

        protected override void OnStartup(object sender, StartupEventArgs e)
	    {
	        base.OnStartup(sender, e);

            DisplayRootViewFor<IShellViewModel>();
	    }

	    protected override void Configure()
		{
            _container = new WindsorContainer();

		    _container.AddFacility<EventRegistrationFacility>();

		    _container.Register(
		        Component.For<IWindowManager>().ImplementedBy<WindowManager>().LifestyleSingleton(),
		        Component.For<IEventAggregator>().ImplementedBy<EventAggregator>().LifestyleSingleton(),
                Classes.FromThisAssembly().InSameNamespaceAs<IShellViewModel>().WithServiceDefaultInterfaces().LifestyleTransient(),
                Component.For<BittrexClient>().LifestyleSingleton(),
                Component.For<BittrexClientConfig>().Instance(BittrexClientConfig.Default)
		        );

		    Mapper.Initialize(cfg =>
		    {
		        cfg.AddCollectionMappers();
		        cfg.CreateMap<MarketSummary, MarketSummaryModel>().EqualityComparison((mm, m) => mm.MarketName == m.MarketName);
            });
        }

		protected override object GetInstance(Type serviceType, string key)
		{
            if (string.IsNullOrEmpty(key))
            {
                return _container.Resolve(serviceType);
            }
		    return _container.Resolve(key, serviceType);
		}

		protected override IEnumerable<object> GetAllInstances(Type serviceType)
		{
			return _container.ResolveAll(serviceType).Cast<object>();
		}
	}
}