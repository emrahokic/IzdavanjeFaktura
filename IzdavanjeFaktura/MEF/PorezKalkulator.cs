using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;

namespace IzdavanjeFaktura.MEF
{
    [Export(typeof(IPorezKalkulator))]
    public class PorezKalkulator : IPorezKalkulator
    {
        public double Izracunaj(double iznos, double porez)
        {
            return iznos + (iznos * (porez / 100));
        }
    }
}