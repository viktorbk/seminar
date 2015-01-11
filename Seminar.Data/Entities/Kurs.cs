using System;

namespace Seminar.Data.Entities
{
	public class Kurs
	{
		public int Id { get; private set; }
		public string Name { get; set; }
		public string Kursholder { get; set; }
		public string Rom { get; set; }
		public DateTime From { get; set; }
		public DateTime To { get; set; }

		private Kurs() { }

		public Kurs(string name, string kursholder, string rom, DateTime from, DateTime to)
		{
			Name = name;
			Kursholder = kursholder;
			Rom = rom;
			From = @from;
			To = to;
		}
	}
}
