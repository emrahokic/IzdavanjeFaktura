using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IzdavanjeFaktura.MEF
{
    public interface IPorezKalkulator
    {
        double Izracunaj(double iznos, double porez);
    }
}