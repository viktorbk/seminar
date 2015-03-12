using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Seminar.Business.Api.Models;
using Seminar.Business.Tests.Helpers;
using Seminar.Data;
using Seminar.Data.Entities;

namespace Seminar.Business.Tests.KursApiTests
{
    [TestFixture]
    public class KursApiTests
    {
        [Test, Database]
        public void KursApiTests_AllKurs_ExpectKursListReturned()
        {
			using (var api = new ApiHelper())
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
				var kursList = api.KursApi.AllKurs.ToList();

				// Assert
				Assert.IsNotNull(kursList);
				Assert.IsTrue(kursList.Any());
				Assert.AreEqual(2, kursList.Count());

				var kurs = kursList.FirstOrDefault();
				Assert.IsNotNull(kurs);
				Assert.AreEqual("Kursholder", kurs.Kursholder);
			}
        }
 
        [Test, Database]
        public void KursApiTests_CreateKurs_ExpectKursCreated()
        {
            // Arrange
            var kurs = new Api.Models.Kurs(1, "Kursnavn", "Kursholder", "Rom", new DateTime(2015, 2, 1), new DateTime(2015, 2, 2));

            // Act
			using (var api = new ApiHelper())
			{
				api.KursApi.CreateKurs(kurs);
			}

			IEnumerable<Data.Entities.Kurs> dbKursList;
            using (var ctx = new SeminarDbContext())
            {
				dbKursList = ctx.Kurs.AsNoTracking().ToList();
            }

            // Assert
            Assert.IsNotNull(dbKursList);
            Assert.IsTrue(dbKursList.Any());
            Assert.AreEqual(1, dbKursList.Count());

            var dbKurs = dbKursList.FirstOrDefault();
            Assert.IsNotNull(dbKurs);
            Assert.AreEqual("Kursholder", dbKurs.Kursholder);
        }

		[Test, Database]
		public void KursApiTests_GetAllDeltakereForKurs_ExpectDeltakereListReturned()
		{
			using (var api = new ApiHelper())
			{
				// Arrange
				using (var ctx = new SeminarDbContext())
				{
					var dbKurs = new Data.Entities.Kurs("Kursnavn", "Kursholder", "Rom", new DateTime(2015, 2, 1), new DateTime(2015, 2, 2));
					ctx.Kurs.Add(dbKurs);

					for (var i=0; i<3; i++)				
					{ 
						var dbKursdeltaker = new Data.Entities.Kursdeltaker(dbKurs, "Kursdeltaker " + i, "mail@dot.com");
						dbKurs.AddKursdeltaker(dbKursdeltaker);
						//ctx.Kursdeltakere.Add(dbKursdeltaker); // Not necessary because of "AddKursdeltaker"
					}

					ctx.SaveChanges();
				}

				var kurs = api.KursApi.AllKurs.FirstOrDefault();

				// Act
				var deltakerList = api.KursApi.AllDeltakereForKurs((int)kurs.Id).ToList();

				// Assert
				Assert.IsNotNull(deltakerList);
				Assert.IsTrue(deltakerList.Any());
				Assert.AreEqual(3, deltakerList.Count());

				var deltaker = deltakerList.FirstOrDefault();
				Assert.IsNotNull(deltaker);
				Assert.AreEqual("mail@dot.com", deltaker.Epost);
			}
		}

		[Test, Database]
		public void KursApiTests_CreateKursAndDetaltaker_ExpectKursAndDeltakerCreated()
		{
			// Arrange
			var kurs = new Api.Models.Kurs(null, "Kursnavn", "Kursholder", "Rom", new DateTime(2015, 2, 1), new DateTime(2015, 2, 2));
			var kursDeltaker = new Api.Models.Kursdeltaker(1, "Kursdeltaker", "mail@dot.com", kurs);

			// Act
			using (var api = new ApiHelper())
			{
				api.KursApi.CreateKursdeltaker(kursDeltaker);
			}

			IEnumerable<Data.Entities.Kursdeltaker> dbKursdeltakerList;
			using (var ctx = new SeminarDbContext())
			{
				dbKursdeltakerList = ctx.Kursdeltakere.Include("Kurs").AsNoTracking().ToList();
			}

			// Assert
			Assert.IsNotNull(dbKursdeltakerList);
			Assert.IsTrue(dbKursdeltakerList.Any());
			Assert.AreEqual(1, dbKursdeltakerList.Count());

			var dbKursdeltaker = dbKursdeltakerList.FirstOrDefault();
			Assert.IsNotNull(dbKursdeltaker);
			Assert.AreEqual("Kursdeltaker", dbKursdeltaker.Navn);

			var dbKurs = dbKursdeltaker.Kurs;
			Assert.IsNotNull(dbKurs);
			Assert.AreEqual("Kursholder", dbKurs.Kursholder);
		}

		[Test, Database]
		public void KursApiTests_CreateDetaltakerExistingKurs_ExpectDeltakerNoNewKursCreated()
		{
			// Arrange
			using (var ctx = new SeminarDbContext())
			{
				ctx.Kurs.Add(
					new Data.Entities.Kurs("Kursnavn", "Kursholder", "Rom", new DateTime(2015, 2, 1), new DateTime(2015, 2, 2)));

				ctx.SaveChanges();
			}

			// Act
			using (var api = new ApiHelper())
			{
				var kurs = api.KursApi.AllKurs.FirstOrDefault();
				var kursDeltaker = new Api.Models.Kursdeltaker(1, "Kursdeltaker", "mail@dot.com", kurs);
				api.KursApi.CreateKursdeltaker(kursDeltaker);
			}

			IEnumerable<Data.Entities.Kurs> dbKursList;
			IEnumerable<Data.Entities.Kursdeltaker> dbKursdeltakerList;
			using (var ctx = new SeminarDbContext())
			{
				dbKursList = ctx.Kurs.AsNoTracking().ToList();
				dbKursdeltakerList = ctx.Kursdeltakere.Include("Kurs").AsNoTracking().ToList();
			}

			// Assert
			Assert.IsNotNull(dbKursdeltakerList);
			Assert.IsTrue(dbKursdeltakerList.Any());
			Assert.AreEqual(1, dbKursdeltakerList.Count());
			Assert.AreEqual(1, dbKursList.Count()); // Check that duplicate not created

			var dbKursdeltaker = dbKursdeltakerList.FirstOrDefault();
			Assert.IsNotNull(dbKursdeltaker);
			Assert.AreEqual("Kursdeltaker", dbKursdeltaker.Navn);

			var dbKurs = dbKursdeltaker.Kurs;
			Assert.IsNotNull(dbKurs);
			Assert.AreEqual("Kursholder", dbKurs.Kursholder);
		}
    }
}
