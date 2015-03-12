namespace Seminar.Business.Api.Models
{
	public class Deltaker
	{
		public int? Id { get; set; }
		public string Navn { get; private set; }
		public string Epost { get; private set; }

		protected Deltaker() { }

		public Deltaker(int? id, string navn, string epost)
		{
			Id = id;
			Navn = navn;
			Epost = epost;
		}
	}
}