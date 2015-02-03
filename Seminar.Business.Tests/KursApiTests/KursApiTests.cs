using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Seminar.Business.Api.Models;
using Seminar.Business.Tests.Helpers;

namespace Seminar.Business.Tests.KursApiTests
{
    [TestFixture]
    public class KursApiTests
    {
        [Test, Database]
        public void KursApiTests_GetAllKurs_ExpectKursListReturned()
        {
            // Arrange
            using (var ctx = new SeminarDbContext())        
            {
                ctx.Kurs.Add(
                    new Data.Entities.Kurs("Kursnavn 1", "Kursholder", "Rom", new DateTime(2015, 2, 1), new DateTime(2015, 2, 2)));
                ctx.Kurs.Add(
                    new Data.Entities.Kurs("Kursnavn 2", "Kursholder", "Rom", new DateTime(2015, 2, 1), new DateTime(2015, 2, 2)));
                ctx.SaveChanges();
            }

            // Act
            var kursApi = new KursApi();
            var kursList = kursApi.GetAllKurs().ToList();

            // Assert
            Assert.IsNotNull(kursList);
            Assert.IsTrue(kursList.Any());
            Assert.AreEqual(kursList.Count(), 2);

            var kurs = kursList.FirstOrDefault();
            Assert.IsNotNull(kurs);
            Assert.AreEqual(kurs.Kursholder, "Kursholder");
        }
 

        [Test, Database]
        public void KursApiTests_CreateKurs_ExpectKursCreated()
        {
            // Arrange
            var kurs = new Kurs("Kursnavn", "Kursholder", "Rom", new DateTime(2015, 2, 1), new DateTime(2015, 2, 2));

            // Act
            var kursApi = new KursApi();
            kursApi.CreateKurs(kurs);

            IEnumerable<Data.Entities.Kurs> dbKursList;
            using (var ctx = new SeminarDbContext())
            {
                dbKursList = ctx.Kurs.ToList();
            }

            // Assert
            Assert.IsNotNull(dbKursList);
            Assert.IsTrue(dbKursList.Any());
            Assert.AreEqual(dbKursList.Count(), 1);

            var dbKurs = dbKursList.FirstOrDefault();
            Assert.IsNotNull(dbKurs);
            Assert.AreEqual(dbKurs.Kursholder, "Kursholder");
        }
    }
}
