using System;
using System.ComponentModel.DataAnnotations;

namespace Seminar.Views.Kurs
{
	public class KursVm
	{
        [Required]
		public string Navn { get; set; }
		public string Kursholder { get; set; }
		public string Rom { get; set; }
        [Required, Display(Name = "Fra dato")]
		public DateTime? Fra { get; set; }
        [Required, Display(Name = "Til dato")]
        public DateTime? Til { get; set; }

        public bool KursCreated { get; set; }

		public KursVm() { }

        public KursVm(string navn, string kursholder, string rom, DateTime fra, DateTime til)
		{
			Navn = navn;
			Kursholder = kursholder;
			Rom = rom;
			Fra = fra;
			Til = til;
		}
	}
}
