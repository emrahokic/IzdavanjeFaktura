using IzdavanjeFaktura.Models;
using IzdavanjeFaktura.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IzdavanjeFaktura.Services
{
    public interface IStavkaService
    {
        List<Stavka> GetAll();
        List<StavkaVM> GetAll_VM();
        Stavka GetById(int id);
        Stavka Insert(StavkaVM stavka);
        Stavka Edit(int id,StavkaVM stavka);
        void Delete(int id);
    }
}
