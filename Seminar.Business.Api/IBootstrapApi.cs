using Microsoft.Practices.Unity;

namespace Seminar.Business.Api
{
	public interface IBootstrapApi
	{
		void Configure(IUnityContainer container);
	}
}
