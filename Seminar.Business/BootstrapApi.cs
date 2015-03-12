using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Seminar.Business.Api;
using Seminar.Business.Api.Models;
using Seminar.Data;

namespace Seminar.Business
{
	public class BootstrapApi : IBootstrapApi
	{
        public void Configure(IUnityContainer container)
		{
			Database.SetInitializer<SeminarDbContext>(new SeminarDbContext.SeminarCreateDbInitializer());

            // Repositories
            container.RegisterType<SeminarDbContext>(new HierarchicalLifetimeManager());

            // Apis
            container.RegisterType<IKursApi, KursApi>();
		}
    }
}
