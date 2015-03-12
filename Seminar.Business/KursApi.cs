using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Seminar.Business.Api;
using Seminar.Business.Api.Models;
using Seminar.Data;

namespace Seminar.Business
{
    public class KursApi: IKursApi
    {
		private readonly SeminarDbContext _ctx;

		public IEnumerable<Kurs> AllKurs
		{
			get
			{
				return _ctx.Kurs.AsNoTracking().ToList().Select(s => new Kurs(s.Id, s.Name, s.Kursholder, s.Rom, s.From, s.To));
			}
		}

		public KursApi(SeminarDbContext ctx)
		{
			_ctx = ctx;
		}

        public void CreateKurs(Kurs kurs)
        {
			if (kurs == null) throw new ArgumentNullException("kurs");

            var entity = new Data.Entities.Kurs(kurs.Navn, kurs.Kursholder, kurs.Rom, kurs.Fra, kurs.Til);
			_ctx.Kurs.Add(entity);

			_ctx.SaveChanges();
        }

		public IEnumerable<Deltaker> AllDeltakereForKurs(int kursId)
		{
			return _ctx.Kursdeltakere.AsNoTracking().Where(w => w.Kurs.Id == kursId).ToList().Select(s => new Deltaker(s.Id, s.Navn, s.Epost));
		}

		public void CreateKursdeltaker(Kursdeltaker kursdeltaker)
		{
			if (kursdeltaker == null) throw new ArgumentNullException("kursdeltaker");

			var kurs = _ctx.Kurs.FirstOrDefault(w => w.Id == kursdeltaker.Kurs.Id);
			if (kurs == null)
			{
				kurs = new Data.Entities.Kurs(kursdeltaker.Kurs.Navn, kursdeltaker.Kurs.Kursholder, kursdeltaker.Kurs.Rom, kursdeltaker.Kurs.Fra, kursdeltaker.Kurs.Til);
				_ctx.Kurs.Add(kurs);
			}

			var entity = new Data.Entities.Kursdeltaker(kurs, kursdeltaker.Navn, kursdeltaker.Epost);
			kurs.AddKursdeltaker(entity);

			_ctx.SaveChanges();
		}
	}
}
