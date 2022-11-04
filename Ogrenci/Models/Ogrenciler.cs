using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ogrenci.Models
{
    public class Ogrenciler
    {
        public int Numara { get; set; }

        public string Ad { get; set; }

        public string Soyad { get; set; }

        public int Sinif { get; set; }

        // Constructor 
        public Ogrenciler (int numara,string ad,string soyad, int sinif)
        {
            this.Numara = numara;
            this.Ad = ad;
            this.Soyad = soyad;
            this.Sinif = sinif;

        }

    }
}
