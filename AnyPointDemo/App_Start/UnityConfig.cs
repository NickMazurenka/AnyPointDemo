using System.Configuration;
using AnyPointDemo.Entities;
using AnyPointDemo.Services;
using Microsoft.Practices.Unity;

namespace AnyPointDemo
{
	public class UnityConfig
	{
		public static void RegisterTypes(IUnityContainer container)
		{
			// Win App Driver
			container.RegisterType<ICityInfoRepository, CityInfoRepository>(
				new ContainerControlledLifetimeManager(),
				new InjectionFactory(_ => new CityInfoRepository(new CityInfoContext())));
		}
	}
}