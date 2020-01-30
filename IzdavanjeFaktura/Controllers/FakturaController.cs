using IzdavanjeFaktura.Repository;
using IzdavanjeFaktura.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using System.Data.Entity;
using IzdavanjeFaktura.Services;
using IzdavanjeFaktura.Models.Models;

namespace IzdavanjeFaktura.Controllers
{
    [Authorize]
    public class FakturaController : Controller
    {
        #region Servisi
        private readonly IFakturaService _fakturaService;
        private readonly IPorezService _porezService;
        private readonly IStavkaService _stavkaService;
        #endregion
        public FakturaController(IFakturaService fakturaService,IPorezService porezService,IStavkaService stavkaService)
        {
            _fakturaService = fakturaService;
            _porezService = porezService;
            _stavkaService = stavkaService;
        }
        public ActionResult Index()
        {
            return View(_fakturaService.GetAll_VM());
        }
        public ActionResult Create()
        {
            FakturaFormaVM createModel = new FakturaFormaVM();
            //Uzimanje iz baze stavke i dodavanje u DropDown
            createModel.Stavke = _stavkaService.GetAll().Select(x => new SelectListItem()
            {
                Text = x.Naziv + " - " + x.Cijena + " €",
                Selected = false,
                Value = x.StavkaID.ToString()
            }).ToList();
            
            createModel.Stavke.Insert(0, new SelectListItem()
            {
                Text = "Odaberite Stavku",
                Value = "-1",
                Selected = true,
                Disabled = true

            });


            createModel.PDV = _porezService.GetAll().Select(x => new SelectListItem()
            {
                Value = x.PorezID.ToString(),
                Text = x.Naziv
            }).ToList();
           
            createModel.PDV.Insert(0,new SelectListItem()
            {
                Text = "Odaberite PDV",
                Value = "-1",
                Selected = true,
                Disabled = true
            });
            /////////
            createModel.DatumFakture = DateTime.Now;
            createModel.BrojFakture = "BR: "+DateTime.Now.Ticks + "/" + DateTime.Now.Year;
            return View(createModel);
        }

        [HttpPost]
        public ActionResult Create(FakturaFormaVM model)
        {
            if (ModelState.IsValid)
            {
                if (model.SelektovaneStavke.Count>0 && model.PDVID > 0)
                {
                    string _stvarateljRacunaID = User.Identity.GetUserId();
                    var faktura = _fakturaService.Insert(model, _stvarateljRacunaID);
                    return RedirectToActionPermanent(nameof(Details), new { id = faktura.FakturaID });
                }
                else
                {
                    return RedirectToActionPermanent(nameof(Create));
                }
              

            }
            return View(nameof(Create), model);

        }

        public ActionResult Details(int id)
        {
            Faktura f = _fakturaService.GetById(id);
            FakturaVM details = new FakturaVM()
            {
                BrojFakture = f.BrojFakture,
                DatumFakture = f.DatumFakture,
                DatumPlacanjaFakture = f.DatumPlacanjaFakture,
                NazivPrimatelja = f.NazivPrimatelja,
                Placeno = f.Placeno,
                Porez = f.Porez.Naziv,
                StvarateljRacuna = f.StvarateljRacuna.Email,
                UkupnaCijenaBezPoreza = f.UkupnaCijenaBezPoreza,
                UkupnaCijenaSaPorezom = f.UkupnaCijenaSaPorezom
            };
            foreach (var item in f.Stavke)
            {
                Stavka stavka = _stavkaService.GetById(item.StavkaID);
                details.Stavke.Add(new StavkaVM()
                {
                    Cijena = item.UkupnaCijenaBezPoreza,
                    Kolicina = item.Kolicina,
                    JedinicnaCijena = stavka.Cijena,
                    Opis = stavka.Opis,
                    Naziv = stavka.Naziv
                }); 
            }
            return View(details);
        }

        //Preuzimanje Stavke preko ajax poziva nakon odabira iz dropdowna id stavke  
        public ActionResult GetStavka(int id)
        {
            if (id > 0)
            {
                Stavka stavka = _stavkaService.GetById(id);

                StavkaVM model = new StavkaVM()
                {
                    Cijena = stavka.Cijena,
                    Naziv = stavka.Naziv,
                    Kolicina = 1,
                    Opis = stavka.Opis,
                    StavkaID = stavka.StavkaID
                };
                return PartialView("StavkaInsertPV", model);

            }

            return PartialView("StavkaInsertPV", null);
        }


        //Postavljanje stavke na formu Faktura preko ajax poziva
        public ActionResult SetStavka(StavkaVM _model, int i)
        {
            FakturaFormaVM model = new FakturaFormaVM();

            model.SelektovaneStavke = new List<FakturaFormaVM.Row>();
            for (int a = 0; a < i; a++)
            {
                model.SelektovaneStavke.Add(new FakturaFormaVM.Row());
            }
            Stavka stavka = _stavkaService.GetById(_model.StavkaID);

            model.SelektovaneStavke.Insert(i, new FakturaFormaVM.Row()
            {
                StavkaID = stavka.StavkaID,
                Cijena = stavka.Cijena,
                Kolicina = _model.Kolicina,
                Naziv = stavka.Naziv
            });
            model.i = i;
            return PartialView("StavkaFakturaPV", model);
        }
    }
}