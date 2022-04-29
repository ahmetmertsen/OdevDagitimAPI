using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using uyg0033.Models;
using uyg0033.WiewModel;

namespace uyg0033.Controllers
{
    public class ServisController : ApiController
    {
        DB1Entities db = new DB1Entities();
        SonucModel sonuc = new SonucModel();

        #region Kategori
        [HttpGet]
        [Route("api/kategoriliste")]
        public List<KategoriModel> KategoriListe()
        {
            List<KategoriModel> liste = db.Kategori.Select(x => new KategoriModel()
            {
                katId = x.katId,
                katAdi = x.katAdi
            }).ToList();

            return liste;
        }

        [HttpGet]
        [Route("api/kategoribyid/{katId}")]

        public KategoriModel KategoriById(string katId)
        {
            KategoriModel kayit = db.Kategori.Where(s => s.katId == katId).Select(x => new KategoriModel()
            {
                katId = x.katId,
                katAdi = x.katAdi
            }).SingleOrDefault();

            return kayit;
        }

        [HttpPost]
        [Route("api/kategoriekle")]
        public SonucModel KategoriEkle(KategoriModel Model)
        {
            if (db.Kategori.Count(s => s.katAdi == Model.katAdi) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kategori Adı Kayıtlıdır!";
            }

            Kategori yeni = new Kategori();
            yeni.katAdi = Model.katAdi;
            db.Kategori.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kategori Eklendi";
            return sonuc;
        }

        [HttpPut]
        [Route("api/kategoriduzenle")]
        public SonucModel KategoriDuzenle(KategoriModel Model)
        {
            Kategori kayit = db.Kategori.Where(s => s.katId == Model.katId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kategori Bulunamadı!";
                return sonuc;
            }

            kayit.katAdi = Model.katAdi;
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Kategori Düzenlendi";
            return sonuc;
        }

        [HttpDelete]
        [Route("api/kategorisil/{katId}")]
        public SonucModel KategoriSil(string katId)
        {
            Kategori kayit = db.Kategori.Where(s => s.katId == katId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kategori Bulunamadı!";
                return sonuc;
            }
            db.Kategori.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kategori Silindi!";
            return sonuc;
        }
        #endregion


        #region Ödevler
        [HttpGet]
        [Route("api/odevlerliste")]
        public List<OdevlerModel> OdevlerListe()
        {
            List<OdevlerModel> liste = db.Odevler.Select(x=>new OdevlerModel()
            {
                odevId=x.odevId,
                odevAdi=x.odevAdi
            }).ToList();

            return liste;
        }

        [HttpGet]
        [Route("api/odevlerbyid/{odevId}")]

        public OdevlerModel OdevById(string OdevId)
        {
            OdevlerModel kayit = db.Odevler.Where(s => s.odevId == OdevId).Select(x => new OdevlerModel()
            {
                odevId = x.odevId,
                odevAdi = x.odevAdi
            }).SingleOrDefault();

            return kayit;
        }

        [HttpPost]
        [Route("api/odevekle")]
        public SonucModel OdevEkle(OdevlerModel Model)
        {
            Odevler yeni = new Odevler();
            yeni.odevId = Guid.NewGuid().ToString();
            yeni.odevAdi = Model.odevAdi;
            db.Odevler.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Ödev Eklendi";
            return sonuc;
        }

        [HttpPut]
        [Route("api/odevduzenle")]
        public SonucModel OdevDuzenle(OdevlerModel Model)
        {
            Odevler kayit = db.Odevler.Where(s=>s.odevId==Model.odevId).SingleOrDefault();

            if (kayit==null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Ödev Bulunamadı!";
                return sonuc;
            }

            kayit.odevAdi = Model.odevAdi;
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Ödev Düzenlendi";
            return sonuc;
        }

        [HttpDelete]
        [Route("api/odevsil/{odevId}")]
        public SonucModel OdevSil(string odevId)
        {
            Odevler kayit = db.Odevler.Where(s => s.odevId == odevId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Ödev Bulunamadı!";
                return sonuc;
            }

            if (db.Kayit.Count(s=>s.kayitOdevId==odevId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Ödevi Alan Öğrenci Olduğu İçin Ödev Silinemez!";
                return sonuc;
            }
            db.Odevler.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Ödev Silindi!";
            return sonuc;
        }
        #endregion

    #region Ogrenciler

        [HttpGet]
        [Route("api/ogrencilerliste")]
        
        public List<OgrencilerModel> OgrencilerListe()
        {
            List<OgrencilerModel> liste = db.Ogrenciler.Select(x=>new OgrencilerModel() {
                OgrId=x.OgrId,
                ogrNo = x.ogrNo,
                ogrAdsoyad =x.ogrAdsoyad
            }).ToList();
            return liste;
        }

        [HttpGet]
        [Route("api/ogrencilerbyId")]

        public OgrencilerModel OgrencilerById(string OgrId)
        {
            OgrencilerModel kayit = db.Ogrenciler.Where(s=>s.OgrId==OgrId).Select(x => new OgrencilerModel()
            {
                OgrId = x.OgrId,
                ogrNo = x.ogrNo,
                ogrAdsoyad = x.ogrAdsoyad
            }).SingleOrDefault();
            return kayit;
        }

        [HttpPost]
        [Route("api/ogrenciekle")]

        public SonucModel OgrenciEkle(OgrencilerModel Model)
        {
            if (db.Ogrenciler.Count(s=>s.ogrNo==Model.ogrNo) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Öğrenci Numarası Kayıtlıdır!";
            }

            Ogrenciler yeni = new Ogrenciler();
            yeni.OgrId = Guid.NewGuid().ToString();
            yeni.ogrNo = Model.ogrNo;
            yeni.ogrAdsoyad = Model.ogrAdsoyad;
            db.Ogrenciler.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Öğrenci Eklendi";
            return sonuc;
        }

        [HttpPut]
        [Route("api/ogrenciduzenle")]

        public SonucModel OgrenciDuzenle(OgrencilerModel Model)
        {
            Ogrenciler kayit = db.Ogrenciler.Where(s => s.OgrId == Model.OgrId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Ögrenci Bulunamadi!";
                return sonuc;
            }

            kayit.ogrNo = Model.ogrNo;
            kayit.ogrAdsoyad = Model.ogrAdsoyad;
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Öğrenci Düzenlendi";
            return sonuc;
        }

        [HttpDelete]
        [Route("api/ogrencisil/{OgrId}")]
        public SonucModel OgrenciSil(string OgrId)
        {
            Ogrenciler kayit = db.Ogrenciler.Where(s => s.OgrId == OgrId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Öğrenci Bulunamadı!";
                return sonuc;
            }

            if (db.Kayit.Count(s => s.kayitOgrId == OgrId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Öğrenci Ödev Aldığı İçin Öğrenci Silinemez!";
                return sonuc;
            }
            db.Ogrenciler.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Öğrenci Silindi!";
            return sonuc;
        }
        #endregion

        #region Kayit

        [HttpGet]
        [Route("api/ogrenciodevliste/{OgrId}")]
        public List<KayitModel> OgrenciOdevListe(string OgrId)
        {
            List<KayitModel> liste = db.Kayit.Where(s => s.kayitOgrId == OgrId).Select(x => new KayitModel()
            {
                kayitId=x.kayitId,
                kayitOgrId=x.kayitOgrId,
                kayitOdevId=x.kayitOdevId,
                kayitKatId=x.kayitKatId
            }).ToList();

            foreach (var kayit in liste)
            {
                kayit.katBilgi = KategoriById(kayit.kayitKatId);
                kayit.ogrBilgi = OgrencilerById(kayit.kayitOgrId);
                kayit.odevBilgi = OdevById(kayit.kayitOdevId);
            }

            return liste;
        }

        [HttpGet]
        [Route("api/odevogrenciliste/{odevId}")]
        public List<KayitModel> OdevOgrenciListe(string odevId)
        {
            List<KayitModel> liste = db.Kayit.Where(s => s.kayitOdevId == odevId).Select(x => new KayitModel()
            {
                kayitId = x.kayitId,
                kayitOgrId = x.kayitOgrId,
                kayitOdevId = x.kayitOdevId,
                kayitKatId = x.kayitKatId
            }).ToList();

            foreach (var kayit in liste)
            {
                kayit.katBilgi = KategoriById(kayit.kayitKatId);
                kayit.ogrBilgi = OgrencilerById(kayit.kayitOgrId);
                kayit.odevBilgi = OdevById(kayit.kayitOdevId);
            }
            return liste;
        }


        [HttpPost]
        [Route("api/kayitekle")]
        public SonucModel KayitEkle(KayitModel Model)
        {
            if (db.Kayit.Count(s=> s.kayitOdevId==Model.kayitOdevId && s.kayitOgrId==Model.kayitOgrId 
            && s.kayitKatId==Model.kayitKatId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Öğrenciye Ödev Verilmiştir!";
                return sonuc;
            }

            Kayit yeni = new Kayit();
            yeni.kayitId = Guid.NewGuid().ToString();
            yeni.kayitOgrId = Model.kayitOgrId;
            yeni.kayitOdevId = Model.kayitOdevId;
            yeni.kayitKatId = Model.kayitKatId;
            db.Kayit.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Ödev Kaydı Eklendi";
            return sonuc;
        }

        [HttpDelete]
        [Route("api/kayitsil/{kayitId}")]
        public SonucModel KayitSil(string kayitId)
        {
            Kayit kayit = db.Kayit.Where(s=>s.kayitId==kayitId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            }

            db.Kayit.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Ödev Kaydı Silindi";
            return sonuc;
        }


        #endregion


    }
}
