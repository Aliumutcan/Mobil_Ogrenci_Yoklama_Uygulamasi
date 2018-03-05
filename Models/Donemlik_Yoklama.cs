namespace Mobil_Ogrenci_Yoklama_Uygulamasi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("aliumutc_Blog.Donemlik_Yoklama")]
    public partial class Donemlik_Yoklama
    {
        public int id { get; set; }

        public int? ders_id { get; set; }

        public int? ogrenci_id { get; set; }
        
        public DateTime tarih { get; set; }

        public virtual Dersler Dersler { get; set; }

        public virtual Ogrenciler Ogrenciler { get; set; }
    }
}
