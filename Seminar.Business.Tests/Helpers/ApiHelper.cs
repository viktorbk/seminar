using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Seminar.Business.Api;

namespace Seminar.Business.Tests.Helpers
{
	public class ApiHelper : IDisposable
	{
		private IUnityContainer _unityContainer;

		public IKursApi KursApi { get { return _unityContainer.Resolve<IKursApi>(); } }

		public ApiHelper()
		{
			_unityContainer = new UnityContainer();
			BusinessApi.Initialize(_unityContainer);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (_unityContainer != null)
				{
					_unityContainer.Dispose();
					_unityContainer = null;
				}
			}
		}
	}
}
