using System;
using System.Collections.Generic;

namespace Seminar.Data.Entities
{
	public class Kurs
	{
		public int Id { get; private set; }
		public string Name { get; private set; }
		public string Kursholder { get; private set; }
		public string Rom { get; private set; }
		public DateTime From { get; private set; }
		public DateTime To { get; private set; }
		public ICollection<Kursdeltaker> Kursdeltakere { get; private set; }

		private Kurs()
		{
			Kursdeltakere = new HashSet<Kursdeltaker>();			
		}

		public Kurs(string name, string kursholder, string rom, DateTime @from, DateTime to) : this()
		{
			Name = name;
			Kursholder = kursholder;
			Rom = rom;
			From = @from;
			To = to;
		}

		public void AddKursdeltaker(Kursdeltaker kursdeltaker)
		{
			if (kursdeltaker == null) throw new ArgumentNullException("kursdeltaker");

			Kursdeltakere.Add(kursdeltaker);
		}
	}
}
