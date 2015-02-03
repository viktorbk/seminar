using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Seminar.Business.Api;
using Seminar.Business.Api.Models;

namespace Seminar.Business
{
    public class KursApi: IKursApi
    {
        public void CreateKurs(Kurs kurs)
        {
            using (var db = new SeminarDbContext()) 
            {
                try
                {
                    var entity = new Data.Entities.Kurs(kurs.Navn, kurs.Kursholder, kurs.Rom, kurs.Fra, kurs.Til);
                    db.Kurs.Add(entity);

                    db.SaveChanges();
                }
                catch (Exception)
                {                 
                    throw;
                } 

            }
        }


        public IEnumerable<Kurs> GetAllKurs()
        {
            using (var db = new SeminarDbContext())
            {
                try
                {
                    return db.Kurs.ToList().Select(s => new Kurs(s.Name, s.Kursholder, s.Rom, s.From, s.To));
                }
                catch (Exception)
                {
                    throw;
                }

            }
        }
    }
}
