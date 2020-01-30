using IzdavanjeFaktura.Models.Models;
using IzdavanjeFaktura.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IzdavanjeFaktura.Services
{
    public interface IPorezService
    {
        IEnumerable<Porez>GetAll();
        Porez GetById(int id);
    }
}