using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Seminar.Business.Api;

namespace Seminar.Business.Tests
{
    [TestFixture]
    public class KursApiTests
    {
        [Test]
        public void KursApi_GetAllKurs_RetrieveAllKurs_Success()
        {  
            // Arrange
            IKursApi kursApi = new KursApi();

            // Act

            // Assert
        }
    }


}
