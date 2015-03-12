using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Seminar.Business.Api.Models;

namespace Seminar.Business.Api
{
    public interface IKursApi
    {
		IEnumerable<Kurs> AllKurs { get; }
        void CreateKurs(Kurs kurs);
	    IEnumerable<Deltaker> AllDeltakereForKurs(int kursId);
	    void CreateKursdeltaker(Kursdeltaker kursdeltaker);
    }
}
