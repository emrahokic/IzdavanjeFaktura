using IzdavanjeFaktura.Models;
using IzdavanjeFaktura.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IzdavanjeFaktura.Services
{
    public interface IFakturaService
    {
        List<Faktura> GetAll();
        List<FakturaVM> GetAll_VM();
        Faktura GetById(int id);
        Faktura Insert(FakturaFormaVM faktura,string id);
        double _izracunavanjeUkupneCijene(List<FakturaFormaVM.Row> selektovaneStavke);

    }
}
