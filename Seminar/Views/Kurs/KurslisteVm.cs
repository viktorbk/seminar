using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Seminar.Views.Kurs
{
    public class KurslisteVm: IEnumerable<KursVm>
    {
        public IEnumerable<KursVm> Kursliste { get; set; }

        public IEnumerator<KursVm> GetEnumerator()
        {
            return Kursliste.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Kursliste.GetEnumerator();
        }
    }
}
