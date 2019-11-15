using IzdavanjeFaktura.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IzdavanjeFaktura.Services
{
    public interface IPorezService
    {
        List<Porez>GetAll();
        Porez GetById(int id);
    }
}