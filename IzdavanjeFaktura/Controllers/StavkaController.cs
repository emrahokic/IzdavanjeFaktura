using IzdavanjeFaktura.Repository;
using IzdavanjeFaktura.Services;
using IzdavanjeFaktura.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IzdavanjeFaktura.Controllers
{
    [Authorize]
    public class StavkaController : Controller
    {
        private readonly IStavkaService _stavkaService;

        public StavkaController(IStavkaService service)
        {
            _stavkaService = service;
        }


        public ActionResult Index()
        {
            return View(_stavkaService.GetAll_VM());
        }
        public ActionResult Details(int id)
        {
            var stavka = _stavkaService.GetById(id);

            StavkaVM details = new StavkaVM()
            {
                Cijena = stavka.Cijena,
                Opis = stavka.Opis,
                Naziv = stavka.Naziv,
                StavkaID = stavka.StavkaID
            };

            return View(details);
        }
        public ActionResult Edit(int id)
        {
            var stavka = _stavkaService.GetById(id);

            StavkaVM edit = new StavkaVM()
            {
                Cijena = stavka.Cijena,
                Opis = stavka.Opis,
                Naziv = stavka.Naziv,
                StavkaID = stavka.StavkaID
            };

            return View(edit);
        }
        [HttpPost]
        public ActionResult Edit(int id,StavkaVM model)
        {
            if (ModelState.IsValid)
            {
                var edited = _stavkaService.Edit(id, model);
                return RedirectToActionPermanent(nameof(Index));

            }
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(StavkaVM model)
        {
            if (ModelState.IsValid)
            {
                var inserted =  _stavkaService.Insert(model);
                return RedirectToActionPermanent(nameof(Index));

            }
            return View();
        }
        public ActionResult Delete(int id)
        {
            _stavkaService.Delete(id);
            return RedirectToActionPermanent(nameof(Index));
        }
    }
}