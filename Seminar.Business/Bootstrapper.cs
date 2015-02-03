using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Seminar.Business.Api;
using Seminar.Business.Api.Models;

namespace Seminar.Business
{
	public static class BootStrapper
	{
        public static void Configure(IUnityContainer container)
		{
			Database.SetInitializer<SeminarDbContext>(new SeminarDbContext.SeminarCreateDbInitializer());

            // Repositories
            container.RegisterType<SeminarDbContext>(new HierarchicalLifetimeManager());

            // API's
            container.RegisterType<IKursApi, KursApi>();
		}
    }
}
