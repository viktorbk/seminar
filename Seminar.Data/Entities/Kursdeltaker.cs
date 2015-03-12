using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seminar.Data.Entities
{
	public class Kursdeltaker
	{
		public int Id { get; private set; }
		public Kurs Kurs { get; private set; }
		public int KursId { get; private set; }
		public string Navn { get; private set; }
		public string Epost { get; private set; }

		public Kursdeltaker() { }

		public Kursdeltaker(Kurs kurs, string navn, string epost)
		{
			if (kurs == null) throw new ArgumentNullException("kurs");

			Kurs = kurs;
			KursId = kurs.Id;
			Navn = navn;
			Epost = epost;
		}
	}
}
