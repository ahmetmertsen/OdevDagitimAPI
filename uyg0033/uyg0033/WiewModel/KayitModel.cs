using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace uyg0033.WiewModel
{
    public class KayitModel
    {
        public string kayitId { get; set; }
        public string kayitKatId { get; set; }
        public string kayitOgrId { get; set; }
        public string kayitOdevId { get; set; }

        public KategoriModel katBilgi { get; set; }
        public OgrencilerModel ogrBilgi { get; set; }
        public OdevlerModel odevBilgi { get; set; }
    }
}