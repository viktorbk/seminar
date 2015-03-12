namespace Seminar.Business.Api.Models
{
	public class Kursdeltaker: Deltaker
	{
		public Kurs Kurs { get; private set; }

		private Kursdeltaker() { }

		public Kursdeltaker(int id, string navn, string epost, Kurs kurs) : base(id, navn, epost)
		{
			Kurs = kurs;
		}
	}
}