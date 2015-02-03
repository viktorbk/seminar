using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Seminar.Business.Api;
using Seminar.Business.Api.Models;
using Seminar.Views.Kurs;

namespace Seminar.Controllers
{
    public class KursController : Controller
    {
        private readonly IKursApi _kursApi;

        public KursController(IKursApi kursApi)
        {
            _kursApi = kursApi;
        }

        public ActionResult Index()
        {
            var vm = new KurslisteVm {Kursliste = _kursApi.GetAllKurs().Select(s => new KursVm(s.Navn, s.Kursholder, s.Rom, s.Fra, s.Til))};

            return View(vm);
        }

        public ActionResult Opprett()
        {
            var vm = new KursVm();

            return View(vm);
        }

        [HttpPost]
        public ActionResult Opprett(KursVm kursVm)
        { 
            if (ModelState.IsValid) {
                var kurs = new Kurs(kursVm.Navn, kursVm.Kursholder, kursVm.Rom, (DateTime)kursVm.Fra, (DateTime)kursVm.Til);
                _kursApi.CreateKurs(kurs);

                var vm = new KursVm { KursCreated = true };
                return View(vm);
            }

            return View();
        }
    }
}