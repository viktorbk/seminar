using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace Seminar.Business.Api
{
	public class BusinessApi
	{
		const string BootstrapperTypeName = "Seminar.Business.BootstrapApi,Seminar.Business";

		public static void Initialize(IUnityContainer container)
		{
			var bootstrapper = CreateBootstrapper();
			container.RegisterInstance(bootstrapper);

			// Finally, configure
			bootstrapper.Configure(container);
		}

		private static IBootstrapApi CreateBootstrapper()
		{
			// Get implentation assembly
			var bootstrapperType = Type.GetType(BootstrapperTypeName, true);
			if (bootstrapperType == null)
			{
				throw new ArgumentNullException(string.Format(CultureInfo.CurrentCulture, "Initialize: Unable to find type '{0}'.", BootstrapperTypeName));
			}

			// Get instance of bootstrapper
			var bootstrapper = Activator.CreateInstance(bootstrapperType) as IBootstrapApi;
			if (bootstrapper == null)
			{
				throw new ArgumentNullException(string.Format(CultureInfo.CurrentCulture, "Initialize: Unable to create instance of '{0}'.", BootstrapperTypeName));
			}
			return bootstrapper;
		}
	}
}
