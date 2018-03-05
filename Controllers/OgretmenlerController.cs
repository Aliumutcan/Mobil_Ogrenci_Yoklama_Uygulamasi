using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Mobil_Ogrenci_Yoklama_Uygulamasi.Models;
using Newtonsoft.Json.Linq;

namespace Mobil_Ogrenci_Yoklama_Uygulamasi.Controllers
{
    public class OgretmenlerController : ApiController
    {
        Mobil_Yoklama_Veritabani db = new Mobil_Yoklama_Veritabani();
        
        public IHttpActionResult POST_Giris(Ogretmenler ogretmen)
        {
            if (ogretmen.kullanici_adi.Trim().Length>0 && ogretmen.sifre.Length>0)
            {
                var ogretmen2 = db.Ogretmenler.Where(x => x.kullanici_adi == ogretmen.kullanici_adi && x.sifre == ogretmen.sifre).Select(y=>new { y.id,y.adi_soyadi,y.bolum,y.fakulte,acilan_ders_id=y.Acilan_Ders.Where(m=>m.durum==true).Select(l=>new {l.id,l.Dersler.ders_adi }).Take(1)}).Take(1).FirstOrDefault();
                
                return Ok(ogretmen2);
            }
            JObject jo = new JObject();
            jo.Add("durmu", "false");
            return Ok(jo);
        }
        
        public IHttpActionResult GET_Derler(int id)
        {
            if (id>0)
            {
                return Ok(db.Dersler.Where(x=>db.Ogretmenler.Where(y=>y.id==id).Count()>0).Select(k=>new {k.ders_adi,k.id }));
            }
            JObject jo = new JObject();
            jo.Add("durmu", "false");
            return Ok(jo);
        }

        public IHttpActionResult POST_Ders_Ac(Acilan_Ders dersac)
        {
            if (dersac.ogretmen_id>0 &&  dersac.ders_id>0)
            {
                dersac.durum = true;
                dersac.acilma_tarihi = DateTime.Now;
                db.Acilan_Ders.Add(dersac);
                db.SaveChanges();
                return Ok(dersac);
            }

            JObject jo = new JObject();
            jo.Add("durmu", "false");
            return Ok(jo);
        }

        public IHttpActionResult GET_Ders_Kapa(int id)
        {
            JObject jo = new JObject();
            if (id>0 && db.Acilan_Ders.Where(x => x.durum == true).Count() == 1)
            {
                List<Ogrenciler> kacaklar=null;
                var mac_adresleri = db.Yoklamadakiler.Where(k=>k.acilan_ders_id==id).GroupBy(x=>x.mac_adres).Select(g => new {adres=g.Key,adet=g.Count() });
                if (mac_adresleri.Count() > 1)
                {
                    if (mac_adresleri.Where(x=>x.adet<=1).Count()>0)
                    {
                        List<Disardan_Baglanan> disaridan_baglananlar = new List<Disardan_Baglanan>();
                        //burada azınlıkdaki mac adresleri bulunan ve yklamalar tablosunda bulunanaları getiriyor 
                        //bu tabloyu hocanın uygulamasınan gönder
                        //azınlıkta olmayanları yoklamasını onayla
                        var kacaklar2= db.Yoklamadakiler
                            .Where(x => mac_adresleri.Where(l => l.adet <= 1)
                            .Any(y => y.adres==x.mac_adres))
                            .Select(y=>new {y.acilan_ders_id,y.ogrenci_id,y.Ogrenciler })
                            .ToList();
                        kacaklar = kacaklar2.Select(x => x.Ogrenciler).ToList();
                        foreach (var item in kacaklar2)
                        {
                            disaridan_baglananlar.Add(new Disardan_Baglanan { acilan_ders_id = item.acilan_ders_id, ogrenci_id = item.ogrenci_id });
                        }
                        foreach (var item in disaridan_baglananlar)
                        {
                            db.Disardan_Baglanan.Add(item);
                        }
                        db.SaveChanges();
                    }
                }
                List<string> sadecemacadres = mac_adresleri.Where(y=>y.adet>1).Select(x => x.adres).ToList();
                var dogru_yoklama = db.Yoklamadakiler.Where(x=>sadecemacadres.Any(y=>y==x.mac_adres)).Select(y => new { y.ogrenci_id, y.Acilan_Ders.ders_id }).ToList();
                List<Donemlik_Yoklama> yoklama_verisini_yaz = new List<Donemlik_Yoklama>();
                for (int i = 0; i < dogru_yoklama.Count; i++)
                {
                    Donemlik_Yoklama yoklama = new Donemlik_Yoklama();
                    yoklama.ders_id= dogru_yoklama[i].ders_id;
                    yoklama.ogrenci_id = dogru_yoklama[i].ogrenci_id;
                    yoklama.tarih = DateTime.Now;
                    yoklama_verisini_yaz.Add(yoklama);
                    db.Donemlik_Yoklama.Add(yoklama_verisini_yaz[i]);
                }
                
                Acilan_Ders kapa = db.Acilan_Ders.Where(x => x.id == id).Take(1).FirstOrDefault();
                if (kapa != null)
                {
                    kapa.durum = false;
                    kapa.kapanma_tarihi = DateTime.Now;
                }

                List<Yoklamadakiler> sil = db.Yoklamadakiler.Where(x => x.acilan_ders_id == id).ToList();
                foreach (var item in sil)
                {
                    db.Yoklamadakiler.Remove(item);
                }

                db.SaveChanges();
                
                jo.Add("durum", "true");
                if (kacaklar != null)
                {
                    return Ok(kacaklar.Select(x=>new {x.id,x.kullanici_adi,x.adi_soyadi,x.bolum,x.fakulte,x.no,x.sinif }));
                }else
                    return Ok(jo);
            }
            jo.Remove("durum");
            jo.Add("durum", "false");
            return Ok(jo);
        }

    }
}
