﻿using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Seminar.Business;
using Seminar.Business.Api;
using Unity.Mvc5;

namespace Seminar
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType<IKursApi, KursApi>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}