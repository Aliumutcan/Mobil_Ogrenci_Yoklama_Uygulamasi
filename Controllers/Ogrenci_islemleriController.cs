using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Mobil_Ogrenci_Yoklama_Uygulamasi.Models;
using System.Web.Http.Cors;
using Newtonsoft.Json.Linq;
using System.Web.Http.Results;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace Mobil_Ogrenci_Yoklama_Uygulamasi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    //[Authorize]
    public class Ogrenci_islemleriController : ApiController
    {
        Mobil_Yoklama_Veritabani db = new Mobil_Yoklama_Veritabani();
        public IHttpActionResult POST_Giris(Ogrenciler ogrenci)
        {
            if (ogrenci.kullanici_adi.Trim().Count() > 0 && ogrenci.sifre.Count() > 0)
            {
                int y_id=0;
                var ogrenci2 = db.Ogrenciler
                    .Where(x => x.sifre == ogrenci.sifre && x.kullanici_adi == ogrenci.kullanici_adi)
                    .Select(y =>new { y.id, y.adi_soyadi,y.bolum,y.fakulte,y.sinif })
                    .Take(1).FirstOrDefault();
                var yoklama = db.Yoklamadakiler.Where(x => x.ogrenci_id == ogrenci2.id).Select(y => new { y.id, y.Acilan_Ders.Dersler.ders_adi }).Take(1).FirstOrDefault();
            
                JObject json = new JObject();
                json.Add("id",ogrenci2.id);
                json.Add("adi_soyadi", ogrenci2.adi_soyadi);
                json.Add("sinif", ogrenci2.sinif);
                json.Add("bolum",ogrenci2.bolum);
                json.Add("fakulte",ogrenci2.fakulte);
                if (yoklama!=null && yoklama.id>0)
                {
                    json.Add("y_id", yoklama.id);
                    json.Add("ders_adi", yoklama.ders_adi);
                }
                else
                {
                    json.Add("y_id",0);
                    json.Add("ders_adi","ders yok");
                }
                return Json(json);

            }
            return Json("false");
        }

        public IHttpActionResult GET_Acilandersler(int id)
        {
            JObject jo = new JObject();
            var yoklamda = db.Yoklamadakiler.Where(x => x.ogrenci_id == id).Select(k => new { k.Acilan_Ders.Dersler.ders_adi,k.id });
            if (yoklamda.Count() == 1)
            {
                jo.Add("durum", "false");
                return Json(jo);
            }
            var kontrol = db.Ogrenciler.Where(x => x.id == id).Select(y=>y.Dersler.Select(k=>k.id));
            var ogrencinin_acilan_dersi_varmi = 
                db.Acilan_Ders
                .Where(
                    x=>x.Dersler.Ogrenciler.Where(j => j.id == id).Select(y => y.Dersler.Select(f=>f.id)).Count()>0
                    && x.durum==true).Select(t=>new {t.Dersler.ders_adi,t.id });
            
            if (ogrencinin_acilan_dersi_varmi.Count() <= 0)
            {
                jo.Add("durum", "yok");
                return Ok(jo);
            }
            else
                return Json(ogrencinin_acilan_dersi_varmi);
            
        }

        public IHttpActionResult POST_Yoklamaya_Gir(Yoklamadakiler yoklama_kat)
        {
            JObject jo = new JObject();
            if (yoklama_kat.ogrenci_id > 0 && yoklama_kat.mac_adres.Trim().Length > 0 && yoklama_kat.acilan_ders_id > 0)
            {
                try
                {
                    if (db.Yoklamadakiler.Where(x => x.ogrenci_id == yoklama_kat.ogrenci_id).Count() > 0)
                    {
                        jo.Add("durum", "false");
                        return Ok(jo);
                    }
                        
                    db.Yoklamadakiler.Add(yoklama_kat);
                    db.SaveChanges();
                    return Ok(yoklama_kat.id);
                }
                catch (Exception)
                {
                    jo.Add("durum", "false");
                    return Ok();
                }
            }
            jo.Add("durum","false");
            return Ok();
        }   

        public IHttpActionResult GET_Yoklamdan_cik(int id)
        {
            JObject json = new JObject();
            
            if (id > 0)
            {
                try
                {
                    db.Yoklamadakiler.Remove(db.Yoklamadakiler.Where(x=>x.id== id).Take(1).FirstOrDefault());
                    db.SaveChanges();
                    json.Add("durum", "true");
                    return Ok(json);
                }
                catch (Exception)
                {
                    json.Add("durum", "false");
                    return Ok(json);
                }
            }
            json.Add("durum", "false");
            return Ok(json);
        }

        public IHttpActionResult GET_Tum_Dersler(int id)
        {
            return Json(db.Ogrenciler.Where(t=>t.id==id).Select(x =>x.Dersler.Select(y=>new {y.id,y.ders_adi })).ToList());
        }

        public IHttpActionResult GET_Ogrenci_Yoklama_Bilgisi(int ders_id,int ogrenci_id)
        {
            JObject jo = new JObject();
            if (ders_id>0 && ogrenci_id>0)
            {
                var yoklama_bilgisi = db.Donemlik_Yoklama.Where(x => x.ders_id == ders_id && x.ogrenci_id == ogrenci_id).Select(y => y.tarih).ToList();
               
                return Ok(yoklama_bilgisi);
            }
            jo.Add("durum","false");
            return Ok(jo);
        }
    }
}
