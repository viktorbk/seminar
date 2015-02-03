using System;
using System.Data.Entity;
using NUnit.Framework;
using Seminar.Business.Api.Models;

namespace Seminar.Business.Tests.Helpers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class DatabaseAttribute : Attribute, ITestAction
    {
        public void BeforeTest(TestDetails testDetails)
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Directory.GetCurrentDirectory());

            Database.SetInitializer(new SeminarDbContext.SeminarDropCreateDbInitializer());

            using (var ctx = new SeminarDbContext())
            {
                ctx.Database.Initialize(true);
            }
        }

        public void AfterTest(TestDetails testDetails)
        {
        }

        public ActionTargets Targets
        {
            get { return ActionTargets.Test; }
        }
    }
}