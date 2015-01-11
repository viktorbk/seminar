using System;

namespace Seminar.Business.Api.Models
{
	public class Kurs
	{
        public int Id { get; set; }
        public string Navn { get; private set; }
        public string Kursholder { get; private set; }
        public string Rom { get; private set; }
		public DateTime Fra { get; private set; }
        public DateTime Til { get; private set; }

		private Kurs() { }

		public Kurs(string navn, string kursholder, string rom, DateTime fra, DateTime til)
		{
			Navn = navn;
			Kursholder = kursholder;
			Rom = rom;
			Fra = fra;
			Til = til;
		}
	}
}
